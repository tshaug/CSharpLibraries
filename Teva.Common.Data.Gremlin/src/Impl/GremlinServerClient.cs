using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using log4net;

namespace Teva.Common.Data.Gremlin.Impl
{
    /// <summary>
    /// Class to communicate with Gremlin Server
    /// </summary>
    public class GremlinServerClient : IGremlinServerClient
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Intializes a new instance of GremlinServerClient, communicates directly with Gremlin Server
        /// </summary>
        /// <param name="Host">Host to connect</param>
        /// <param name="postfix">"/gremlin" required since TinkerPop 3.2</param>
        /// <param name="Port">Port, which is provided from Host (mostly 8182)</param>
        /// <param name="UseBinary">Metric for TinkerPop-Connection</param>
        /// <param name="ReadBufferSize">Metric for TinkerPop-Connection</param>
        /// <param name="Username">Username if required</param>
        /// <param name="Password">Password if required</param>
        public GremlinServerClient(string Host = "localhost", string postfix = "/gremlin", int Port = 8182, bool UseBinary = true, int ReadBufferSize = 1024, string Username = null, string Password = null)
        {
            this.Host = Host;
            this.Port = Port;
            this.UseBinary = UseBinary;
            this.ReadBufferSize = ReadBufferSize;
            this.Uri = new Uri("ws://" + Host + ":" + Port + postfix);
            this.Username = Username;
            this.Password = Password;
        }
        /// <summary>
        /// Executes a statement and returns a list of a generic type
        /// </summary>
        /// <typeparam name="ResultDataType">Datatype to parse in</typeparam>
        /// <param name="Script">Script to send</param>
        /// <param name="Bindings">Bindings of the script</param>
        /// <param name="Session">Session if available</param>
        /// <returns>List of generic type</returns>
        public List<ResultDataType> Execute<ResultDataType>(string Script, Dictionary<string, object> Bindings = null, Guid? Session = null)
        {
            return RunSync(delegate ()
            {
                return ExecuteAsync<ResultDataType>(Script, Bindings, Session);
            });
        }
        /// <summary>
        /// Executes a statement with a provided WebSocket and returns a list of generic type
        /// </summary>
        /// <typeparam name="ResultDataType">Datatype to parse in</typeparam>
        /// <param name="Socket">Socket that is in use</param>
        /// <param name="Script">Script to send</param>
        /// <param name="Bindings">Bindings of the script</param>
        /// <param name="Session">Session if available</param>
        /// <returns>List of generic type</returns>
        public List<ResultDataType> Execute<ResultDataType>(ClientWebSocket Socket, string Script, Dictionary<string, object> Bindings = null, Guid? Session = null)
        {
            return RunSync(delegate ()
            {
                return ExecuteAsync<ResultDataType>(Socket, Script, Bindings, Session);
            });
        }
        /// <summary>
        /// Utilized from Execute, which awaits the task with new ClientWebSocket
        /// </summary>
        /// <typeparam name="ResultDataType">Datatype to parse in</typeparam>
        /// <param name="Script">Script to send</param>
        /// <param name="Bindings">Bindings of the script</param>
        /// <param name="Session">Session if available</param>
        /// <returns>Task that returns list of generic type</returns>
        public Task<List<ResultDataType>> ExecuteAsync<ResultDataType>(string Script, Dictionary<string, object> Bindings = null, Guid? Session = null)
        {
            var Message = new Messages.ScriptRequestMessage
            {
                Arguments = new Messages.ScriptRequestArguments(Script, Bindings, Session)
            };
            return ExecuteAsync<ResultDataType>(Message);
        }
        /// <summary>
        /// Utilized from Execute, which awaits the task with provided Websocket. Deserialisation of JSON
        /// </summary>
        /// <typeparam name="ResultDataType">Datatype to parse in</typeparam>
        /// <param name="Socket">Socket that is in use</param>
        /// <param name="Script">Script to send</param>
        /// <param name="Bindings">Bindings of the script</param>
        /// <param name="Session">Session if available</param>
        /// <returns>Task that returns list of generic type</returns>
        public Task<List<ResultDataType>> ExecuteAsync<ResultDataType>(ClientWebSocket Socket, string Script, Dictionary<string, object> Bindings = null, Guid? Session = null)
        {
            var Message = new Messages.ScriptRequestMessage
            {
                Arguments = new Messages.ScriptRequestArguments(Script, Bindings, Session)
            };
            return ExecuteAsync<ResultDataType>(Socket, Message);
        }

        private async Task<List<ResultDataType>> ExecuteAsync<ResultDataType>(Messages.RequestMessage Message)
        {
            using (var Socket = new ClientWebSocket())
            {
                await Socket.ConnectAsync(Uri, System.Threading.CancellationToken.None);
                try
                {
                    return await ExecuteAsync<ResultDataType>(Socket, Message);
                }
                finally
                {
                    await Socket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, null, System.Threading.CancellationToken.None);
                }
            }
        }
        private async Task<List<ResultDataType>> ExecuteAsync<ResultDataType>(ClientWebSocket Socket, Messages.RequestMessage Message)
        {
            byte[] MessageBytes = GetMessageBytes(Message);
            await Socket.SendAsync(new ArraySegment<byte>(MessageBytes), UseBinary ? WebSocketMessageType.Binary : WebSocketMessageType.Text, true, System.Threading.CancellationToken.None);

            List<ResultDataType> ToReturn = null;

            while (true)
            {
                var MS = new System.IO.MemoryStream();
                var Buffer = new ArraySegment<byte>(new byte[ReadBufferSize]);
                while (true)
                {
                    var Data = await Socket.ReceiveAsync(Buffer, System.Threading.CancellationToken.None);
                    MS.Write(Buffer.Array, 0, Data.Count);
                    if (Data.EndOfMessage)
                        break;
                }
                var ResponseString = System.Text.Encoding.UTF8.GetString(MS.ToArray());
                var Response = JsonConvert.DeserializeObject<Messages.ScriptResponse<ResultDataType>>(ResponseString);

                if (Response.Result.Data != null)
                {
                    if (ToReturn == null)
                        ToReturn = Response.Result.Data;
                    else
                        ToReturn.AddRange(Response.Result.Data);
                }

                switch (Response.Status.Code)
                {
                    case 200: // SUCCESS
                    case 204: // NO CONTENT
                        logger.Info("HTTP 204 NO CONTENT");
                        if (ToReturn == null)
                            ToReturn = new List<ResultDataType>();
                        return ToReturn;
                    case 206: // PARTIAL CONTENT
                        continue;
                    case 401:
                        logger.Error("HTTP 401 UNAUTHORIZED");
                        throw new Exceptions.UnauthorizedException(Response.Status.Message);
                    case 407: // AUTHENTICATE
                        logger.Error("HTTP 407 Proxy Authentication Required");
                        if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                            throw new Exceptions.UnauthorizedException(Response.Status.Message);

                        MessageBytes = GetMessageBytes(new Messages.AuthenticationRequestMessage
                        {
                            Arguments = new Messages.AuthenticationRequestArguments(Username, Password)
                        });
                        await Socket.SendAsync(new ArraySegment<byte>(MessageBytes), UseBinary ? WebSocketMessageType.Binary : WebSocketMessageType.Text, true, System.Threading.CancellationToken.None);

                        break;
                    case 498:
                        logger.Error("Malformed Request: " + Response.Status.Message);
                        throw new Exceptions.MalformedRequestException(Response.Status.Message);
                    case 499:
                        logger.Error("Invalid Request Arguments: " + Response.Status.Message);
                        throw new Exceptions.InvalidRequestArgumentsException(Response.Status.Message);
                    case 500:
                        logger.Error("HTTP 500 Server Error");
                        throw new Exceptions.ServerErrorException(Response.Status.Message);
                    case 597:
                        logger.Error("Script Evaluation Error. Maybe false Gremlin Statement?");
                        throw new Exceptions.ScriptEvaluationErrorException(Response.Status.Message);
                    case 598:
                        logger.Error("Server Timeout Exception");
                        throw new Exceptions.ServerTimeoutException(Response.Status.Message);
                    case 599:
                        logger.Error("Server Serialization Error");
                        throw new Exceptions.ServerSerializationError(Response.Status.Message);
                    default:
                        throw new Exception("Unsupported StatusCode (" + Response.Status.Code + "): " + Response.Status.Message);
                }
            }
        }
        /// <summary>
        /// Executes a statement and returns a scalar
        /// </summary>
        /// <typeparam name="ResultDataType">Datatype to parse in</typeparam>
        /// <param name="Script">Script to send</param>
        /// <param name="Bindings">Bindings of the script</param>
        /// <param name="Session">Session if available</param>
        /// <returns>Scalar that comes from TinkerPop</returns>
        public ResultDataType ExecuteScalar<ResultDataType>(string Script, Dictionary<string, object> Bindings = null, Guid? Session = null)
        {
            return RunSync(delegate ()
            {
                return ExecuteScalarAsync<ResultDataType>(Script, Bindings, Session);
            });
        }
        /// <summary>
        /// Executes a statement with provided WebSocket and returns a scalar. 
        /// </summary>
        /// <typeparam name="ResultDataType">Datatype to parse in</typeparam>
        /// <param name="Socket">Socket that is in use</param>
        /// <param name="Script">Script to send</param>
        /// <param name="Bindings">Bindings of the script</param>
        /// <param name="Session">Session if available</param>
        /// <returns>Scalar that comes from TinkerPop</returns>
        public ResultDataType ExecuteScalar<ResultDataType>(ClientWebSocket Socket, string Script, Dictionary<string, object> Bindings = null, Guid? Session = null)
        {
            return RunSync(delegate ()
            {
                return ExecuteScalarAsync<ResultDataType>(Socket, Script, Bindings, Session);
            });
        }
        /// <summary>
        /// Utilized from Execute, which awaits the task with new ClientWebSocket
        /// </summary>
        /// <typeparam name="ResultDataType">Datatype to parse in</typeparam>
        /// <param name="Script">Script to send</param>
        /// <param name="Bindings">Bindings of the script</param>
        /// <param name="Session">Session if available</param>
        /// <returns>Task that returns scalar that comes from TinkerPop</returns>
        public Task<ResultDataType> ExecuteScalarAsync<ResultDataType>(string Script, Dictionary<string, object> Bindings = null, Guid? Session = null)
        {
            var Message = new Messages.ScriptRequestMessage
            {
                Arguments = new Messages.ScriptRequestArguments(Script, Bindings, Session)
            };
            return ExecuteScalarAsync<ResultDataType>(Message);
        }
        /// <summary>
        /// Utilized from Execute, which awaits the task with provided WebSocket
        /// </summary>
        /// <typeparam name="ResultDataType">Datatype to parse in</typeparam>
        /// <param name="Socket">Socket that is in use</param>
        /// <param name="Script">Script to send</param>
        /// <param name="Bindings">Bindings of the script</param>
        /// <param name="Session">Session if available</param>
        /// <returns>Task that returns scalar that comes from TinkerPop</returns>
        public Task<ResultDataType> ExecuteScalarAsync<ResultDataType>(ClientWebSocket Socket, string Script, Dictionary<string, object> Bindings = null, Guid? Session = null)
        {
            var Message = new Messages.ScriptRequestMessage
            {
                Arguments = new Messages.ScriptRequestArguments(Script, Bindings, Session)
            };
            return ExecuteScalarAsync<ResultDataType>(Socket, Message);
        }

        private async Task<ResultDataType> ExecuteScalarAsync<ResultDataType>(Messages.RequestMessage Message)
        {
            using (var Socket = new ClientWebSocket())
            {
                await Socket.ConnectAsync(Uri, System.Threading.CancellationToken.None);
                try
                {
                    return await ExecuteScalarAsync<ResultDataType>(Socket, Message);
                }
                finally
                {
                    await Socket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, null, System.Threading.CancellationToken.None);
                }
            }
        }
        private async Task<ResultDataType> ExecuteScalarAsync<ResultDataType>(ClientWebSocket Socket, Messages.RequestMessage Message)
        {
            byte[] MessageBytes = GetMessageBytes(Message);
            await Socket.SendAsync(new ArraySegment<byte>(MessageBytes), UseBinary ? WebSocketMessageType.Binary : WebSocketMessageType.Text, true, System.Threading.CancellationToken.None);

            while (true)
            {
                var MS = new System.IO.MemoryStream();
                var Buffer = new ArraySegment<byte>(new byte[ReadBufferSize]);
                while (true)
                {
                    var Data = await Socket.ReceiveAsync(Buffer, System.Threading.CancellationToken.None);
                    MS.Write(Buffer.Array, 0, Data.Count);
                    if (Data.EndOfMessage)
                        break;
                }
                var ResponseString = System.Text.Encoding.UTF8.GetString(MS.ToArray());
                var Response = JsonConvert.DeserializeObject<Messages.ScriptResponse<ResultDataType>>(ResponseString);

                if (Response.Result.Data != null && Response.Result.Data.Count > 0)
                    return Response.Result.Data[0];

                switch (Response.Status.Code)
                {
                    case 200: // SUCCESS
                        
                    case 204: // NO CONTENT
                        logger.Info("HTTP 204 NO CONTENT");
                        return default(ResultDataType);
                    case 401:
                        logger.Error("HTTP 401 UNAUTHORIZED");
                        throw new Exceptions.UnauthorizedException(Response.Status.Message);
                    case 407: // AUTHENTICATE
                        logger.Error("HTTP 407 Proxy Authentication Required");
                        if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                            throw new Exceptions.UnauthorizedException(Response.Status.Message);

                        MessageBytes = GetMessageBytes(new Messages.AuthenticationRequestMessage
                        {
                            Arguments = new Messages.AuthenticationRequestArguments(Username, Password)
                        });
                        await Socket.SendAsync(new ArraySegment<byte>(MessageBytes), UseBinary ? WebSocketMessageType.Binary : WebSocketMessageType.Text, true, System.Threading.CancellationToken.None);

                        break;
                    case 498:
                        logger.Error("Malformed Request: " + Response.Status.Message);
                        throw new Exceptions.MalformedRequestException(Response.Status.Message);
                    case 499:
                        logger.Error("Invalid Request Arguments: " + Response.Status.Message);
                        throw new Exceptions.InvalidRequestArgumentsException(Response.Status.Message);
                    case 500:
                        logger.Error("HTTP 500 Server Error");
                        throw new Exceptions.ServerErrorException(Response.Status.Message);
                    case 597:
                        logger.Error("Script Evaluation Error. Maybe false Gremlin Statement? " + Response.Status.Message);
                        throw new Exceptions.ScriptEvaluationErrorException(Response.Status.Message);
                    case 598:
                        logger.Error("Server Timeout Exception: " + Response.Status.Message);
                        throw new Exceptions.ServerTimeoutException(Response.Status.Message);
                    case 599:
                        logger.Error("Server Serialization Error: " + Response.Status.Message);
                        throw new Exceptions.ServerSerializationError(Response.Status.Message);
                    default:
                        throw new Exception("Unsupported StatusCode (" + Response.Status.Code + "): " + Response.Status.Message);
                }
            }
        }

        private byte[] GetMessageBytes(Messages.RequestMessage Message)
        {
            var Json = JsonConvert.SerializeObject(Message);
            if (UseBinary)
            {
                var SB = new StringBuilder();
                SB.Append((char)("application/json".Length));
                SB.Append("application/json");
                SB.Append(Json);
                return System.Text.Encoding.UTF8.GetBytes(SB.ToString());
            }
            else
                return System.Text.Encoding.UTF8.GetBytes(Json);
        }

        private static readonly TaskFactory TaskFactory = new TaskFactory(System.Threading.CancellationToken.None, TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);
        private static TResult RunSync<TResult>(Func<Task<TResult>> Function)
        {
            return TaskFactory.StartNew(Function).Unwrap().GetAwaiter().GetResult();
        }
        /// <summary>
        /// Host to connect
        /// </summary>
        public string Host { get; private set; }
        /// <summary>
        /// Port, which is provided from Host (mostly 8182)
        /// </summary>
        public int Port { get; private set; }
        /// <summary>
        /// Metric for TinkerPop-Connection
        /// </summary>
        public bool UseBinary { get; private set; }
        /// <summary>
        /// Uri is consisting of Host, Port and postfix
        /// </summary>
        public Uri Uri { get; private set; }
        /// <summary>
        /// Metric for TinkerPop-Connection
        /// </summary>
        public int ReadBufferSize { get; private set; }
        /// <summary>
        /// Username if required
        /// </summary>
        public string Username { get; private set; }
        /// <summary>
        /// Username if provided
        /// </summary>
        public string Password { get; private set; }
    }
}
