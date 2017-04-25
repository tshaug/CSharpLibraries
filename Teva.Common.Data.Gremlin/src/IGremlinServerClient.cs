using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Teva.Common.Data.Gremlin
{
    /// <summary>
    /// Inferface for GremlinServerClient, communicates directly with Gremlin Server
    /// </summary>
    public interface IGremlinServerClient
    {
        /// <summary>
        /// Executes a statement and returns a list of a generic type
        /// </summary>
        /// <typeparam name="
        /// ">Datatype to parse in</typeparam>
        /// <param name="Script">Script to send</param>
        /// <param name="Bindings">Bindings of the script</param>
        /// <param name="Session">Session if available</param>
        /// <returns>List of generic type</returns>
        List<ResultDataType> Execute<ResultDataType>(string Script, Dictionary<string, object> Bindings = null, Guid? Session = null);
        /// <summary>
        /// Executes a statement with a provided WebSocket and returns a list of generic type
        /// </summary>
        /// <typeparam name="ResultDataType">Datatype to parse in</typeparam>
        /// <param name="Socket">Socket that is in use</param>
        /// <param name="Script">Script to send</param>
        /// <param name="Bindings">Bindings of the script</param>
        /// <param name="Session">Session if available</param>
        /// <returns>List of generic type</returns>
        List<ResultDataType> Execute<ResultDataType>(ClientWebSocket Socket, string Script, Dictionary<string, object> Bindings = null, Guid? Session = null);
        /// <summary>
        /// Utilized from Execute, which awaits the task with new ClientWebSocket
        /// </summary>
        /// <typeparam name="ResultDataType">Datatype to parse in</typeparam>
        /// <param name="Script">Script to send</param>
        /// <param name="Bindings">Bindings of the script</param>
        /// <param name="Session">Session if available</param>
        /// <returns>Task that returns list of generic type</returns>
        Task<List<ResultDataType>> ExecuteAsync<ResultDataType>(string Script, Dictionary<string, object> Bindings = null, Guid? Session = null);
        /// <summary>
        /// Utilized from Execute, which awaits the task with provided Websocket. Deserialisation of JSON
        /// </summary>
        /// <typeparam name="ResultDataType">Datatype to parse in</typeparam>
        /// <param name="Socket">Socket that is in use</param>
        /// <param name="Script">Script to send</param>
        /// <param name="Bindings">Bindings of the script</param>
        /// <param name="Session">Session if available</param>
        /// <returns>Task that returns list of generic type</returns>
        Task<List<ResultDataType>> ExecuteAsync<ResultDataType>(ClientWebSocket Socket, string Script, Dictionary<string, object> Bindings = null, Guid? Session = null);
        /// <summary>
        /// Executes a statement and returns a scalar
        /// </summary>
        /// <typeparam name="ResultDataType">Datatype to parse in</typeparam>
        /// <param name="Script">Script to send</param>
        /// <param name="Bindings">Bindings of the script</param>
        /// <param name="Session">Session if available</param>
        /// <returns>Scalar that comes from TinkerPop</returns>
        ResultDataType ExecuteScalar<ResultDataType>(string Script, Dictionary<string, object> Bindings = null, Guid? Session = null);
        /// <summary>
        /// Executes a statement with provided WebSocket and returns a scalar. 
        /// </summary>
        /// <typeparam name="ResultDataType">Datatype to parse in</typeparam>
        /// <param name="Socket">Socket that is in use</param>
        /// <param name="Script">Script to send</param>
        /// <param name="Bindings">Bindings of the script</param>
        /// <param name="Session">Session if available</param>
        /// <returns>Scalar that comes from TinkerPop</returns>
        ResultDataType ExecuteScalar<ResultDataType>(ClientWebSocket Socket, string Script, Dictionary<string, object> Bindings = null, Guid? Session = null);
        /// <summary>
        /// Utilized from Execute, which awaits the task with new ClientWebSocket
        /// </summary>
        /// <typeparam name="ResultDataType">Datatype to parse in</typeparam>
        /// <param name="Script">Script to send</param>
        /// <param name="Bindings">Bindings of the script</param>
        /// <param name="Session">Session if available</param>
        /// <returns>Task that returns scalar that comes from TinkerPop</returns>
        Task<ResultDataType> ExecuteScalarAsync<ResultDataType>(string Script, Dictionary<string, object> Bindings = null, Guid? Session = null);
        /// <summary>
        /// Utilized from Execute, which awaits the task with provided WebSocket
        /// </summary>
        /// <typeparam name="ResultDataType">Datatype to parse in</typeparam>
        /// <param name="Socket">Socket that is in use</param>
        /// <param name="Script">Script to send</param>
        /// <param name="Bindings">Bindings of the script</param>
        /// <param name="Session">Session if available</param>
        /// <returns>Task that returns scalar that comes from TinkerPop</returns>
        Task<ResultDataType> ExecuteScalarAsync<ResultDataType>(ClientWebSocket Socket, string Script, Dictionary<string, object> Bindings = null, Guid? Session = null);
        /// <summary>
        /// Host to connect
        /// </summary>
        string Host { get; }
        /// <summary>
        /// Port, which is provided from Host (mostly 8182)
        /// </summary>
        int Port { get; }
        /// <summary>
        /// Metric for TinkerPop-Connection
        /// </summary>
        bool UseBinary { get; }
        /// <summary>
        /// Uri is consisting of Host, Port and postfix
        /// </summary>
        Uri Uri { get; }
        /// <summary>
        /// Metric for TinkerPop-Connection
        /// </summary>
        int ReadBufferSize { get; }
        /// <summary>
        /// Username if required
        /// </summary>
        string Username { get; }
        /// <summary>
        /// Username if provided
        /// </summary>
        string Password { get; }
    }
}