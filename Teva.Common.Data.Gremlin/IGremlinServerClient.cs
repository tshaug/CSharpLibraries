using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Teva.Common.Data.Gremlin
{
    public interface IGremlinServerClient
    {
        List<ResultDataType> Execute<ResultDataType>(string Script, Dictionary<string, object> Bindings = null, Guid? Session = null);
        List<ResultDataType> Execute<ResultDataType>(ClientWebSocket Socket, string Script, Dictionary<string, object> Bindings = null, Guid? Session = null);
        Task<List<ResultDataType>> ExecuteAsync<ResultDataType>(string Script, Dictionary<string, object> Bindings = null, Guid? Session = null);
        Task<List<ResultDataType>> ExecuteAsync<ResultDataType>(ClientWebSocket Socket, string Script, Dictionary<string, object> Bindings = null, Guid? Session = null);
        ResultDataType ExecuteScalar<ResultDataType>(string Script, Dictionary<string, object> Bindings = null, Guid? Session = null);
        ResultDataType ExecuteScalar<ResultDataType>(ClientWebSocket Socket, string Script, Dictionary<string, object> Bindings = null, Guid? Session = null);
        Task<ResultDataType> ExecuteScalarAsync<ResultDataType>(string Script, Dictionary<string, object> Bindings = null, Guid? Session = null);
        Task<ResultDataType> ExecuteScalarAsync<ResultDataType>(ClientWebSocket Socket, string Script, Dictionary<string, object> Bindings = null, Guid? Session = null);
        string Host { get; }
        int Port { get; }
        bool UseBinary { get; }
        Uri Uri { get; }
        int ReadBufferSize { get; }
        string Username { get; }
        string Password { get; }
    }
}