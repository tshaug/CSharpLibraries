﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Teva.Common.Data.Gremlin.Impl
{
    public class GremlinClient : IGremlinClient
    {
        public GremlinClient(string Host, string postfix= "/gremlin", int Port = 8182, string Username = null, string Password = null)
        {
            this.Host = Host;
            this.Port = Port;
            this.Client = new GremlinServerClient(Host, postfix, Port, Username: Username, Password: Password);
        }

        #region VertexExists
        public bool VertexExistsByIndex(string IndexName, object ID)
        {
            return GetBoolean(new GremlinScript().Append_VertexExistsByIndex(IndexName, ID));
        }
        public Task<bool> VertexExistsByIndexAsync(string IndexName, object ID)
        {
            return GetBooleanAsync(new GremlinScript().Append_VertexExistsByIndex(IndexName, ID));
        }

        public bool VertexExistsByIndexAndLabel(string Label, string IndexName, object ID)
        {
            return GetBoolean(new GremlinScript().Append_VertexExistsByIndexAndLabel(Label, IndexName, ID));
        }
        public Task<bool> VertexExistsByIndexAndLabelAsync(string Label, string IndexName, object ID)
        {
            return GetBooleanAsync(new GremlinScript().Append_VertexExistsByIndexAndLabel(Label, IndexName, ID));
        }
        #endregion

        #region GetVertex
        public GraphItems.IVertex GetVertex(GremlinScript Script)
        {
            return Client.ExecuteScalar<GraphItems.IVertex>(Script.GetScript(), Script.GetBindings());
        }
        public Task<GraphItems.IVertex> GetVertexAsync(GremlinScript Script)
        {
            return Client.ExecuteScalarAsync<GraphItems.IVertex>(Script.GetScript(), Script.GetBindings());
        }

        public GraphItems.IVertex GetVertex(string ID)
        {
            return GetVertex(new GremlinScript().Append_GetVertex(ID));
        }
        public Task<GraphItems.IVertex> GetVertexAsync(string ID)
        {
            return GetVertexAsync(new GremlinScript().Append_GetVertex(ID));
        }

        public GraphItems.IVertex GetVertexByIndex(string IndexName, object ID)
        {
            return GetVertex(new GremlinScript().Append_GetVerticesByIndex(IndexName, ID));
        }
        public Task<GraphItems.IVertex> GetVertexByIndexAsync(string IndexName, object ID)
        {
            return GetVertexAsync(new GremlinScript().Append_GetVerticesByIndex(IndexName, ID));
        }

        public GraphItems.IVertex GetVertexByIndexAndLabel(string Label, string IndexName, object ID)
        {
            return GetVertex(new GremlinScript().Append_GetVerticesByIndexAndLabel(Label, IndexName, ID));
        }
        public Task<GraphItems.IVertex> GetVertexByIndexAndLabelAsync(string Label, string IndexName, object ID)
        {
            return GetVertexAsync(new GremlinScript().Append_GetVerticesByIndexAndLabel(Label, IndexName, ID));
        }
        #endregion

        #region GetVertices
        public List<GraphItems.IVertex> GetVertices(GremlinScript Script)
        {
            return Client.Execute<GraphItems.IVertex>(Script.GetScript(), Script.GetBindings());
        }
        public Task<List<GraphItems.IVertex>> GetVerticesAsync(GremlinScript Script)
        {
            return Client.ExecuteAsync<GraphItems.IVertex>(Script.GetScript(), Script.GetBindings());
        }

        public List<GraphItems.IVertex> GetVerticesByIndex(string IndexName, object ID)
        {
            return GetVertices(new GremlinScript().Append_GetVerticesByIndex(IndexName, ID));
        }
        public Task<List<GraphItems.IVertex>> GetVerticesByIndexAsync(string IndexName, object ID)
        {
            return GetVerticesAsync(new GremlinScript().Append_GetVerticesByIndex(IndexName, ID));
        }

        public List<GraphItems.IVertex> GetVerticesByIndex(string IndexName, IEnumerable<object> IDs)
        {
            return GetVertices(new GremlinScript().Append_GetVerticesByIndex(IndexName, IDs));
        }
        public Task<List<GraphItems.IVertex>> GetVerticeByIndexAsync(string IndexName, IEnumerable<object> IDs)
        {
            return GetVerticesAsync(new GremlinScript().Append_GetVerticesByIndex(IndexName, IDs));
        }

        public List<GraphItems.IVertex> GetVerticesByIndexAndLabel(string Label, string IndexName, object ID)
        {
            return GetVertices(new GremlinScript().Append_GetVerticesByIndexAndLabel(Label, IndexName, ID));
        }
        public Task<List<GraphItems.IVertex>> GetVerticesByIndexAndLabelAsync(string Label, string IndexName, object ID)
        {
            return GetVerticesAsync(new GremlinScript().Append_GetVerticesByIndexAndLabel(Label, IndexName, ID));
        }

        public List<GraphItems.IVertex> GetVerticesByIndexAndLabel(string Label, string IndexName, IEnumerable<object> IDs)
        {
            return GetVertices(new GremlinScript().Append_GetVerticesByIndexAndLabel(Label, IndexName, IDs));
        }
        public Task<List<GraphItems.IVertex>> GetVerticesByIndexAndLabelAsync(string Label, string IndexName, IEnumerable<object> IDs)
        {
            return GetVerticesAsync(new GremlinScript().Append_GetVerticesByIndexAndLabel(Label, IndexName, IDs));
        }
        #endregion

        #region GetVertexID
        public string GetVertexIDByIndex(string IndexName, object ID)
        {
            return GetString(new GremlinScript().Append_GetVertexIDByIndex(IndexName, ID));
        }
        public Task<string> GetVertexIDByIndexAsync(string IndexName, object ID)
        {
            return GetStringAsync(new GremlinScript().Append_GetVertexIDByIndex(IndexName, ID));
        }

        public string GetVertexIDByIndexAndLabel(string Label, string IndexName, object ID)
        {
            return GetString(new GremlinScript().Append_GetVertexIDByIndexAndLabel(Label, IndexName, ID));
        }
        public Task<string> GetVertexIDByIndexAndLabelAsync(string Label, string IndexName, object ID)
        {
            return GetStringAsync(new GremlinScript().Append_GetVertexIDByIndexAndLabel(Label, IndexName, ID));
        }
        #endregion

        #region CreateVertex
        public GraphItems.IVertex CreateVertex(Dictionary<string, List<GraphItems.IVertexValue>> Properties)
        {
            return GetVertex(new GremlinScript().Append_CreateVertex(Properties));
        }
        public Task<GraphItems.IVertex> CreateVertexAsync(Dictionary<string, List<GraphItems.IVertexValue>> Properties)
        {
            return GetVertexAsync(new GremlinScript().Append_CreateVertex(Properties));
        }

        public GraphItems.IVertex CreateVertexAndLabel(string Label, Dictionary<string, List<GraphItems.IVertexValue>> Properties)
        {
            return GetVertex(new GremlinScript().Append_CreateVertexWithLabel(Label, Properties));
        }
        public Task<GraphItems.IVertex> CreateVertexAndLabelAsync(string Label, Dictionary<string, List<GraphItems.IVertexValue>> Properties)
        {
            return GetVertexAsync(new GremlinScript().Append_CreateVertexWithLabel(Label, Properties));
        }
        #endregion

        #region DeleteVertex
        public void DeleteVertex(string ID)
        {
            Execute(new GremlinScript().Append_DeleteVertex(ID));
        }
        public Task DeleteVertexAsync(string ID)
        {
            return ExecuteAsync(new GremlinScript().Append_DeleteVertex(ID));
        }

        public void DeleteVertexByIndex(string IndexName, object ID)
        {
            Execute(new GremlinScript().Append_DeleteVertexByIndex(IndexName, ID));
        }
        public Task DeleteVertexByIndexAsync(string IndexName, object ID)
        {
            return ExecuteAsync(new GremlinScript().Append_DeleteVertexByIndex(IndexName, ID));
        }

        public void DeleteVertexByIndexAndLabel(string Label, string IndexName, object ID)
        {
            Execute(new GremlinScript().Append_DeleteVertexByIndexAndLabel(Label, IndexName, ID));
        }
        public Task DeleteVertexByIndexAndLabelAsync(string Label, string IndexName, object ID)
        {
            return ExecuteAsync(new GremlinScript().Append_DeleteVertexByIndexAndLabel(Label, IndexName, ID));
        }
        #endregion

        #region UpdateVertex
        public void UpdateVertex(string ID, Dictionary<string, List<GraphItems.IVertexValue>> Properties, bool RemoveOtherProperties)
        {
            Execute(new GremlinScript().Append_UpdateVertex(ID, Properties, RemoveOtherProperties).Append("null;"));
        }
        public Task UpdateVertexAsync(string ID, Dictionary<string, List<GraphItems.IVertexValue>> Properties, bool RemoveOtherProperties)
        {
            return ExecuteAsync(new GremlinScript().Append_UpdateVertex(ID, Properties, RemoveOtherProperties).Append("null;"));
        }
        #endregion

        #region EdgeExists
        public bool EdgeExistsBoth(string StartVertexID, string Name)
        {
            return GetBoolean(new GremlinScript().Append_EdgeExistsBoth(StartVertexID, Name));
        }
        public Task<bool> EdgeExistsBothAsync(string StartVertexID, string Name)
        {
            return GetBooleanAsync(new GremlinScript().Append_EdgeExistsBoth(StartVertexID, Name));
        }

        public bool EdgeExistsOut(string StartVertexID, string Name)
        {
            return GetBoolean(new GremlinScript().Append_EdgeExistsOut(StartVertexID, Name));
        }
        public Task<bool> EdgeExistsOutAsync(string StartVertexID, string Name)
        {
            return GetBooleanAsync(new GremlinScript().Append_EdgeExistsOut(StartVertexID, Name));
        }

        public bool EdgeExistsIn(string StartVertexID, string Name)
        {
            return GetBoolean(new GremlinScript().Append_EdgeExistsIn(StartVertexID, Name));
        }
        public Task<bool> EdgeExistsInAsync(string StartVertexID, string Name)
        {
            return GetBooleanAsync(new GremlinScript().Append_EdgeExistsIn(StartVertexID, Name));
        }

        public bool EdgeExistsBetweenBoth(string StartVertexID, string EndVertexID, string Name)
        {
            return GetBoolean(new GremlinScript().Append_EdgeExistsBetweenBoth(StartVertexID, EndVertexID, Name));
        }
        public bool EdgeExistsBetweenOut(string StartVertexID, string EndVertexID, string Name)
        {
            return GetBoolean(new GremlinScript().Append_EdgeExistsBetweenOut(StartVertexID, EndVertexID, Name));
        }
        public bool EdgeExistsBetweenIn(string StartVertexID, string EndVertexID, string Name)
        {
            return GetBoolean(new GremlinScript().Append_EdgeExistsBetweenIn(StartVertexID, EndVertexID, Name));
        }

        #endregion

        #region CreateEdge
        public GraphItems.IEdge CreateEdge(string StartVertexID, string EndVertexID, string Name, Dictionary<string, object> Properties = null)
        {
            return GetEdge(new GremlinScript().Append_CreateEdge(StartVertexID, EndVertexID, Name, Properties));
        }
        public Task<GraphItems.IEdge> CreateEdgeAsync(string StartVertexID, string EndVertexID, string Name, Dictionary<string, object> Properties = null)
        {
            return GetEdgeAsync(new GremlinScript().Append_CreateEdge(StartVertexID, EndVertexID, Name, Properties));
        }
        #endregion

        #region UpdateEdge
        public void UpdateEdgeOut(string StartVertexID, string EndVertexID, string Name, Dictionary<string, object> Properties, bool RemoveOtherProperties)
        {
            Execute(new GremlinScript().Append_UpdateEdgeBetween_Out(StartVertexID, EndVertexID, Name, Properties, RemoveOtherProperties));
        }
        public Task UpdateEdgeOutAsync(string StartVertexID, string EndVertexID, string Name, Dictionary<string, object> Properties, bool RemoveOtherProperties)
        {
            return ExecuteAsync(new GremlinScript().Append_UpdateEdgeBetween_Out(StartVertexID, EndVertexID, Name, Properties, RemoveOtherProperties));
        }
        #endregion

        #region DeleteEdge
        public void DeleteEdge(string ID)
        {
            Execute(new GremlinScript().Append_DeleteEdge(ID));
        }
        public Task DeleteEdgeAsync(string ID)
        {
            return ExecuteAsync(new GremlinScript().Append_DeleteEdge(ID));
        }

        public void DeleteEdgeBoth(string StartVertexID, string Name)
        {
            Execute(new GremlinScript().Append_DeleteEdge_Both(StartVertexID, Name));
        }
        public Task DeleteEdgeBothAsync(string StartVertexID, string Name)
        {
            return ExecuteAsync(new GremlinScript().Append_DeleteEdge_Both(StartVertexID, Name));
        }

        public void DeleteEdgeOut(string StartVertexID, string Name)
        {
            Execute(new GremlinScript().Append_DeleteEdge_Out(StartVertexID, Name));
        }
        public Task DeleteEdgeOutAsync(string StartVertexID, string Name)
        {
            return ExecuteAsync(new GremlinScript().Append_DeleteEdge_Out(StartVertexID, Name));
        }

        public void DeleteEdgeIn(string StartVertexID, string Name)
        {
            Execute(new GremlinScript().Append_DeleteEdge_In(StartVertexID, Name));
        }
        public Task DeleteEdgeInAsync(string StartVertexID, string Name)
        {
            return ExecuteAsync(new GremlinScript().Append_DeleteEdge_In(StartVertexID, Name));
        }
        #endregion

        #region GetEdge
        public GraphItems.IEdge GetEdge(GremlinScript Script)
        {
            return Client.ExecuteScalar<GraphItems.IEdge>(Script.GetScript(), Script.GetBindings());
        }
        public Task<GraphItems.IEdge> GetEdgeAsync(GremlinScript Script)
        {
            return Client.ExecuteScalarAsync<GraphItems.IEdge>(Script.GetScript(), Script.GetBindings());
        }

        public GraphItems.IEdge GetEdge(string ID)
        {
            return GetEdge(new GremlinScript().Append_GetEdge(ID));
        }
        public Task<GraphItems.IEdge> GetEdgeAsync(string ID)
        {
            return GetEdgeAsync(new GremlinScript().Append_GetEdge(ID));
        }
        #endregion

        #region GetEdges
        public List<GraphItems.IEdge> GetEdges(GremlinScript Script)
        {
            return Client.Execute<GraphItems.IEdge>(Script.GetScript(), Script.GetBindings());
        }
        public Task<List<GraphItems.IEdge>> GetEdgesAsync(GremlinScript Script)
        {
            return Client.ExecuteAsync<GraphItems.IEdge>(Script.GetScript(), Script.GetBindings());
        }
        #endregion

        #region GetBoolean
        public bool GetBoolean(GremlinScript Script)
        {
            return Client.ExecuteScalar<bool>(Script.GetScript(), Script.GetBindings());
        }
        public Task<bool> GetBooleanAsync(GremlinScript Script)
        {
            return Client.ExecuteScalarAsync<bool>(Script.GetScript(), Script.GetBindings());
        }
        #endregion

        #region GetString
        public string GetString(GremlinScript Script)
        {
            return Client.ExecuteScalar<string>(Script.GetScript(), Script.GetBindings());
        }
        public Task<string> GetStringAsync(GremlinScript Script)
        {
            return Client.ExecuteScalarAsync<string>(Script.GetScript(), Script.GetBindings());
        }
        #endregion

        #region GetArray
        public List<object> GetArray(GremlinScript Script)
        {
            return Client.Execute<object>(Script.GetScript(), Script.GetBindings());
        }
        public Task<List<object>> GetArrayAsync(GremlinScript Script)
        {
            return Client.ExecuteAsync<object>(Script.GetScript(), Script.GetBindings());
        }

        public List<ValueType> GetArray<ValueType>(GremlinScript Script)
        {
            return Client.Execute<ValueType>(Script.GetScript(), Script.GetBindings());
        }
        public Task<List<ValueType>> GetArrayAsync<ValueType>(GremlinScript Script)
        {
            return Client.ExecuteAsync<ValueType>(Script.GetScript(), Script.GetBindings());
        }
        #endregion

        #region Scalar
        public object GetScalar(GremlinScript Script)
        {
            return Client.ExecuteScalar<object>(Script.GetScript(), Script.GetBindings());
        }
        public Task<object> GetScalarAsync(GremlinScript Script)
        {
            return Client.ExecuteScalarAsync<object>(Script.GetScript(), Script.GetBindings());
        }

        public ValueType GetScalar<ValueType>(GremlinScript Script)
        {
            return Client.ExecuteScalar<ValueType>(Script.GetScript(), Script.GetBindings());
        }
        public Task<ValueType> GetScalarAsync<ValueType>(GremlinScript Script)
        {
            return Client.ExecuteScalarAsync<ValueType>(Script.GetScript(), Script.GetBindings());
        }
        #endregion

        #region GetDictionaryArray
        public List<Dictionary<string, object>> GetDictionaryArray(GremlinScript Script)
        {
            return Client.Execute<Dictionary<string, object>>(Script.GetScript(), Script.GetBindings());
        }
        public Task<List<Dictionary<string, object>>> GetDictionaryArrayAsync(GremlinScript Script)
        {
            return Client.ExecuteAsync<Dictionary<string, object>>(Script.GetScript(), Script.GetBindings());
        }
        #endregion

        #region Execute
        public void Execute(GremlinScript Script)
        {
            Client.Execute<object>(Script.GetScript(), Script.GetBindings());
        }
        public Task ExecuteAsync(GremlinScript Script)
        {
            return Client.ExecuteAsync<object>(Script.GetScript(), Script.GetBindings());
        }
        #endregion

        private GremlinServerClient Client { get; set; }
        public string Host { get; private set; }
        public int Port { get; private set; }

        public List<GraphItems.IVertex> GetVerticesFromJArray(object Object)
        {
            if (Object == null)
                return null;
            var Array = (JArray)Object;
            return Array.ToObject<List<GraphItems.IVertex>>();
        }
        public Dictionary<string, object> GetDictionaryFromJArray(object Object)
        {
            if (Object == null)
                return null;
            var Array = (JArray)Object;
            if (Array.Count == 0)
                return null;
            if (Array.Count != 1)
                throw new Exception("Invalid JArray length");
            return Array[0].ToObject<Dictionary<string, object>>();
        }
        public List<Dictionary<string, object>> GetDictionariesFromJArray(object Object)
        {
            if (Object == null)
                return null;
            var Array = (JArray)Object;
            if (Array.Count == 0)
                return null;
            var ToReturn = new List<Dictionary<string, object>>();
            foreach (var Item in Array)
            {
                if (Item.Type == JTokenType.Array && Item.Count() <= 1)
                {
                    if (Item.First == null)
                        continue;
                    if (Item.First.Type == JTokenType.Array && Item.First.Count() <= 1)
                    {
                        if (Item.First.First == null)
                            continue;
                        ToReturn.Add(Item.First.First.ToObject<Dictionary<string, object>>());
                    }
                    else
                        ToReturn.Add(Item.First.ToObject<Dictionary<string, object>>());
                }
                else
                    ToReturn.Add(Item.ToObject<Dictionary<string, object>>());
            }
            return ToReturn;
        }
        public GraphItems.IVertex GetVertexFromJObject(object Object)
        {
            return GetValueFromJObject<GraphItems.IVertex>(Object);
        }
        public Dictionary<string, object> GetDictionaryFromJObject(object Object)
        {
            return GetValueFromJObject<Dictionary<string, object>>(Object);
        }
        public T GetValueFromJObject<T>(object Object)
        {
            if (Object == null)
                return default(T);
            return ((JObject)Object).ToObject<T>();
        }
    }
}