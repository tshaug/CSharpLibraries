using System.Collections.Generic;
using System.Threading.Tasks;

namespace Teva.Common.Data.Gremlin
{
    public interface IGremlinClient
    {
        bool VertexExistsByIndex(string IndexName, object ID);
        Task<bool> VertexExistsByIndexAsync(string IndexName, object ID);
        bool VertexExistsByIndexAndLabel(string Label, string IndexName, object ID);
        Task<bool> VertexExistsByIndexAndLabelAsync(string Label, string IndexName, object ID);
        GraphItems.IVertex GetVertex(GremlinScript Script);
        Task<GraphItems.IVertex> GetVertexAsync(GremlinScript Script);
        GraphItems.IVertex GetVertex(string ID);
        Task<GraphItems.IVertex> GetVertexAsync(string ID);
        GraphItems.IVertex GetVertexByIndex(string IndexName, object ID);
        Task<GraphItems.IVertex> GetVertexByIndexAsync(string IndexName, object ID);
        GraphItems.IVertex GetVertexByIndexAndLabel(string Label, string IndexName, object ID);
        Task<GraphItems.IVertex> GetVertexByIndexAndLabelAsync(string Label, string IndexName, object ID);
        List<GraphItems.IVertex> GetVertices(GremlinScript Script);
        Task<List<GraphItems.IVertex>> GetVerticesAsync(GremlinScript Script);
        List<GraphItems.IVertex> GetVerticesByIndex(string IndexName, object ID);
        Task<List<GraphItems.IVertex>> GetVerticesByIndexAsync(string IndexName, object ID);
        List<GraphItems.IVertex> GetVerticesByIndex(string IndexName, IEnumerable<object> IDs);
        Task<List<GraphItems.IVertex>> GetVerticeByIndexAsync(string IndexName, IEnumerable<object> IDs);
        List<GraphItems.IVertex> GetVerticesByIndexAndLabel(string Label, string IndexName, object ID);
        Task<List<GraphItems.IVertex>> GetVerticesByIndexAndLabelAsync(string Label, string IndexName, object ID);
        List<GraphItems.IVertex> GetVerticesByIndexAndLabel(string Label, string IndexName, IEnumerable<object> IDs);
        Task<List<GraphItems.IVertex>> GetVerticesByIndexAndLabelAsync(string Label, string IndexName, IEnumerable<object> IDs);
        string GetVertexIDByIndex(string IndexName, object ID);
        Task<string> GetVertexIDByIndexAsync(string IndexName, object ID);
        string GetVertexIDByIndexAndLabel(string Label, string IndexName, object ID);
        Task<string> GetVertexIDByIndexAndLabelAsync(string Label, string IndexName, object ID);
        GraphItems.IVertex CreateVertex(Dictionary<string, List<GraphItems.IVertexValue>> Properties);
        Task<GraphItems.IVertex> CreateVertexAsync(Dictionary<string, List<GraphItems.IVertexValue>> Properties);
        GraphItems.IVertex CreateVertexAndLabel(string Label, Dictionary<string, List<GraphItems.IVertexValue>> Properties);
        Task<GraphItems.IVertex> CreateVertexAndLabelAsync(string Label, Dictionary<string, List<GraphItems.IVertexValue>> Properties);
        void DeleteVertex(string ID);
        Task DeleteVertexAsync(string ID);
        void DeleteVertexByIndex(string IndexName, object ID);
        Task DeleteVertexByIndexAsync(string IndexName, object ID);
        void DeleteVertexByIndexAndLabel(string Label, string IndexName, object ID);
        Task DeleteVertexByIndexAndLabelAsync(string Label, string IndexName, object ID);
        void UpdateVertex(string ID, Dictionary<string, List<GraphItems.IVertexValue>> Properties, bool RemoveOtherProperties);
        Task UpdateVertexAsync(string ID, Dictionary<string, List<GraphItems.IVertexValue>> Properties, bool RemoveOtherProperties);
        bool EdgeExistsBoth(string StartVertexID, string Name);
        Task<bool> EdgeExistsBothAsync(string StartVertexID, string Name);
        bool EdgeExistsOut(string StartVertexID, string Name);
        Task<bool> EdgeExistsOutAsync(string StartVertexID, string Name);
        bool EdgeExistsIn(string StartVertexID, string Name);
        Task<bool> EdgeExistsInAsync(string StartVertexID, string Name);
        bool EdgeExistsBetweenBoth(string StartVertexID, string EndVertexID, string Name);
        bool EdgeExistsBetweenOut(string StartVertexID, string EndVertexID, string Name);
        bool EdgeExistsBetweenIn(string StartVertexID, string EndVertexID, string Name);
        GraphItems.IEdge CreateEdge(string StartVertexID, string EndVertexID, string Name, Dictionary<string, object> Properties = null);
        Task<GraphItems.IEdge> CreateEdgeAsync(string StartVertexID, string EndVertexID, string Name, Dictionary<string, object> Properties = null);
        void UpdateEdgeOut(string StartVertexID, string EndVertexID, string Name, Dictionary<string, object> Properties, bool RemoveOtherProperties);
        Task UpdateEdgeOutAsync(string StartVertexID, string EndVertexID, string Name, Dictionary<string, object> Properties, bool RemoveOtherProperties);
        void DeleteEdge(string ID);
        Task DeleteEdgeAsync(string ID);
        void DeleteEdgeBoth(string StartVertexID, string Name);
        Task DeleteEdgeBothAsync(string StartVertexID, string Name);
        void DeleteEdgeOut(string StartVertexID, string Name);
        Task DeleteEdgeOutAsync(string StartVertexID, string Name);
        void DeleteEdgeIn(string StartVertexID, string Name);
        Task DeleteEdgeInAsync(string StartVertexID, string Name);
        GraphItems.IEdge GetEdge(GremlinScript Script);
        Task<GraphItems.IEdge> GetEdgeAsync(GremlinScript Script);
        GraphItems.IEdge GetEdge(string ID);
        Task<GraphItems.IEdge> GetEdgeAsync(string ID);
        List<GraphItems.IEdge> GetEdges(GremlinScript Script);
        Task<List<GraphItems.IEdge>> GetEdgesAsync(GremlinScript Script);
        bool GetBoolean(GremlinScript Script);
        Task<bool> GetBooleanAsync(GremlinScript Script);
        string GetString(GremlinScript Script);
        Task<string> GetStringAsync(GremlinScript Script);
        List<object> GetArray(GremlinScript Script);
        Task<List<object>> GetArrayAsync(GremlinScript Script);
        List<ValueType> GetArray<ValueType>(GremlinScript Script);
        Task<List<ValueType>> GetArrayAsync<ValueType>(GremlinScript Script);
        object GetScalar(GremlinScript Script);
        Task<object> GetScalarAsync(GremlinScript Script);
        ValueType GetScalar<ValueType>(GremlinScript Script);
        Task<ValueType> GetScalarAsync<ValueType>(GremlinScript Script);
        List<Dictionary<string, object>> GetDictionaryArray(GremlinScript Script);
        Task<List<Dictionary<string, object>>> GetDictionaryArrayAsync(GremlinScript Script);
        void Execute(GremlinScript Script);
        Task ExecuteAsync(GremlinScript Script);
        string Host { get; }
        int Port { get; }
        List<GraphItems.IVertex> GetVerticesFromJArray(object Object);
        Dictionary<string, object> GetDictionaryFromJArray(object Object);
        List<Dictionary<string, object>> GetDictionariesFromJArray(object Object);
        GraphItems.IVertex GetVertexFromJObject(object Object);
        Dictionary<string, object> GetDictionaryFromJObject(object Object);
        T GetValueFromJObject<T>(object Object);
    }
}