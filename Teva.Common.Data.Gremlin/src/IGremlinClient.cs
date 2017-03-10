using System.Collections.Generic;
using System.Threading.Tasks;

namespace Teva.Common.Data.Gremlin
{
    /// <summary>
    /// Interface for IGremlinClient, Layer on GremlinServerClient
    /// </summary>
    public interface IGremlinClient
    {
        #region VertexExists
        /// <summary>
        /// Vertex Exists by IndexName and Value, e.g. g.V().has('Name','John').hasNext()
        /// </summary>
        /// <param name="IndexName">Key of property</param>
        /// <param name="ID">Value of Property</param>
        /// <returns>Bool whether Vertex exists or not</returns>
        bool VertexExistsByIndex(string IndexName, object ID);
        /// <summary>
        /// Async for VertexExistsByIndex
        /// </summary>
        /// <param name="IndexName">Key of property</param>
        /// <param name="ID">Value of property</param>
        /// <returns>Task whether Vertex exists or not</returns>
        Task<bool> VertexExistsByIndexAsync(string IndexName, object ID);
        /// <summary>
        /// Vertex Exists by Label, IndexName and IndexValue, e.g. g.V().has(label,'Person','Name','John').hasNext()
        /// </summary>
        /// <param name="Label">Label of wanted vertex</param>
        /// <param name="IndexName">Key of wanted property</param>
        /// <param name="ID">Value of wanted property</param>
        /// <returns>Bool wether Vertex exist or not</returns>
        bool VertexExistsByIndexAndLabel(string Label, string IndexName, object ID);
        /// <summary>
        /// Async for VertexExistsByIndexAndLabel
        /// </summary>
        /// <param name="Label">Label of wanted vertex</param>
        /// <param name="IndexName">Key of wanted property</param>
        /// <param name="ID">Value of wanted property</param>
        /// <returns>Task whether vertex exists or not</returns>
        Task<bool> VertexExistsByIndexAndLabelAsync(string Label, string IndexName, object ID);
        #endregion

        #region GetVertex
        /// <summary>
        /// Gets IVertex from a specific Query
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>The wanted IVertex</returns>
        GraphItems.IVertex GetVertex(GremlinScript Script);
        /// <summary>
        /// Async for GetVertex(GremlinScript Script)
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>Task that returns wanted IVertex</returns>
        Task<GraphItems.IVertex> GetVertexAsync(GremlinScript Script);
        /// <summary>
        /// Gets IVertex with specific ID
        /// </summary>
        /// <param name="ID">ID of wanted vertex</param>
        /// <returns>The wanted IVertex</returns>
        GraphItems.IVertex GetVertex(string ID);
        /// <summary>
        /// Async for GetVertex(string ID)
        /// </summary>
        /// <param name="ID">ID of wanted vertex</param>
        /// <returns>Task that returns wanted IVertex</returns>
        Task<GraphItems.IVertex> GetVertexAsync(string ID);
        /// <summary>
        /// Gets IVertex by IndexName and IndexValue
        /// </summary>
        /// <param name="IndexName">Key of wanted property of wanted vertex</param>
        /// <param name="ID">Value of wanted property</param>
        /// <returns>The wanted IVertex</returns>
        GraphItems.IVertex GetVertexByIndex(string IndexName, object ID);
        /// <summary>
        /// Async for GetVertexByIndex
        /// </summary>
        /// <param name="IndexName">Key of wanted property of wanted vertex</param>
        /// <param name="ID">Value of wanted property</param>
        /// <returns>Task that returns wanted vertex</returns>
        Task<GraphItems.IVertex> GetVertexByIndexAsync(string IndexName, object ID);
        /// <summary>
        /// Gets IVertex by Label, IndexName and IndexValue
        /// </summary>
        /// <param name="Label">Label of wanted vertex</param>
        /// <param name="IndexName">Key of wanted property of wanted vertex</param>
        /// <param name="ID">Value of wanted property</param>
        /// <returns>The wanted IVertex</returns>
        GraphItems.IVertex GetVertexByIndexAndLabel(string Label, string IndexName, object ID);
        /// <summary>
        /// Async for GetVertexByIndexAndLabel
        /// </summary>
        /// <param name="Label">Label of wanted vertex</param>
        /// <param name="IndexName">Key of wanted property of wanted vertex</param>
        /// <param name="ID">Value of wanted property</param>
        /// <returns>Task that returns wanted IVertex</returns>
        Task<GraphItems.IVertex> GetVertexByIndexAndLabelAsync(string Label, string IndexName, object ID);
        #endregion

        #region GetVertices
        /// <summary>
        /// Gets Vertices from a specific Query
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>List of IVertex</returns>
        List<GraphItems.IVertex> GetVertices(GremlinScript Script);
        /// <summary>
        /// Async for GetVertices
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>Task that returns list of IVertex</returns>
        Task<List<GraphItems.IVertex>> GetVerticesAsync(GremlinScript Script);
        /// <summary>
        /// Gets Vertices by IndexName and IndexValue
        /// </summary>
        /// <param name="IndexName">Key of wanted property of wanted vertices</param>
        /// <param name="ID">Value of wanted property</param>
        /// <returns>List of IVertex</returns>
        List<GraphItems.IVertex> GetVerticesByIndex(string IndexName, object ID);
        /// <summary>
        /// Async for GetsVerticesByIndex(string IndexName, object ID)
        /// </summary>
        /// <param name="IndexName">Key of wanted property of wanted vertices</param>
        /// <param name="ID">Value of wanted property</param>
        /// <returns>Task that return list of IVertex</returns>
        Task<List<GraphItems.IVertex>> GetVerticesByIndexAsync(string IndexName, object ID);
        /// <summary>
        /// Gets Vertices by IndexName and a List of IndexValues
        /// </summary>
        /// <param name="IndexName">Key of wanted property of wanted vertices</param>
        /// <param name="IDs">List of IndexValues</param>
        /// <returns>List of IVertex</returns>
        List<GraphItems.IVertex> GetVerticesByIndex(string IndexName, IEnumerable<object> IDs);
        /// <summary>
        /// Async for GetVerticesByIndex(string IndexName, IEnumerable IDs)
        /// </summary>
        /// <param name="IndexName">Key of wanted property of wanted vertices</param>
        /// <param name="IDs">List of IndexValues</param>
        /// <returns>Task that returns list of IVertex</returns>
        Task<List<GraphItems.IVertex>> GetVerticeByIndexAsync(string IndexName, IEnumerable<object> IDs);
        /// <summary>
        /// Gets Vertices by Label, IndexName and IndexValue
        /// </summary>
        /// <param name="Label">Label of wanted vertices</param>
        /// <param name="IndexName">Key of wanted property of wanted vertices</param>
        /// <param name="ID">Value of wanted property</param>
        /// <returns>List of IVertex</returns>
        List<GraphItems.IVertex> GetVerticesByIndexAndLabel(string Label, string IndexName, object ID);
        /// <summary>
        /// Async for GetVerticesByIndexAndLabel(string Label, string IndexName, object ID)
        /// </summary>
        /// <param name="Label">Label of wanted vertices</param>
        /// <param name="IndexName">Key of wanted property of wanted vertices</param>
        /// <param name="ID">Value of wanted property</param>
        /// <returns>Task that returns list of IVertex</returns>
        Task<List<GraphItems.IVertex>> GetVerticesByIndexAndLabelAsync(string Label, string IndexName, object ID);
        /// <summary>
        /// Gets Vertices by Label, IndexName and a List of IndexValues
        /// </summary>
        /// <param name="Label">Label of wanted vertices</param>
        /// <param name="IndexName">Key of wanted property of wanted vertices</param>
        /// <param name="IDs">List of values of wanted property</param>
        /// <returns>List of IVertex</returns>
        List<GraphItems.IVertex> GetVerticesByIndexAndLabel(string Label, string IndexName, IEnumerable<object> IDs);
        /// <summary>
        /// Async for GetVerticesByIndexAndLabel(string Label, string IndexName, IEnumerable IDs)
        /// </summary>
        /// <param name="Label">Label of wanted vertices</param>
        /// <param name="IndexName">Key of wanted property of wanted vertices</param>
        /// <param name="IDs">List of values of wanted property</param>
        /// <returns>Task that returns list of IVertex</returns>
        Task<List<GraphItems.IVertex>> GetVerticesByIndexAndLabelAsync(string Label, string IndexName, IEnumerable<object> IDs);
        #endregion

        #region GetVertexID
        /// <summary>
        /// Gets ID of Vertex by IndexName and IndexValue
        /// </summary>
        /// <param name="IndexName">Key of wanted property of wanted Vertex</param>
        /// <param name="ID">Value of property</param>
        /// <returns>ID as string</returns>
        string GetVertexIDByIndex(string IndexName, object ID);
        /// <summary>
        /// Async for GetVertexIDByIndex(string IndexName, object ID)
        /// </summary>
        /// <param name="IndexName">Key of wanted property of wanted Vertex</param>
        /// <param name="ID">Value of property</param>
        /// <returns>Task that returns ID as string</returns>
        Task<string> GetVertexIDByIndexAsync(string IndexName, object ID);
        /// <summary>
        /// Gets ID of Vertex by Label, IndexName and IndexValue
        /// </summary>
        /// <param name="Label">Label of wanted vertex</param>
        /// <param name="IndexName">Key of wanted property of wanted Vertex</param>
        /// <param name="ID">Value of property</param>
        /// <returns>ID as string</returns>
        string GetVertexIDByIndexAndLabel(string Label, string IndexName, object ID);
        /// <summary>
        /// Async for GetVertexIDByIndexAndLabel(string Label, string IndexName, object ID)
        /// </summary>
        /// <param name="Label">Label of wanted vertex</param>
        /// <param name="IndexName">Key of wanted property of wanted Vertex</param>
        /// <param name="ID">Value of property</param>
        /// <returns>Task that returns ID as string</returns>
        Task<string> GetVertexIDByIndexAndLabelAsync(string Label, string IndexName, object ID);
        #endregion

        #region CreateVertex
        /// <summary>
        /// Creates a vertex in database with provided properties but without a label
        /// </summary>
        /// <param name="Properties">Properties for created vertex</param>
        /// <returns>created IVertex</returns>>
        GraphItems.IVertex CreateVertex(Dictionary<string, List<GraphItems.IVertexValue>> Properties);
        /// <summary>
        /// Async for CreateVertex
        /// </summary>
        /// <param name="Properties">Properties for created vertex</param>
        /// <returns>Task that returns created IVertex</returns>
        Task<GraphItems.IVertex> CreateVertexAsync(Dictionary<string, List<GraphItems.IVertexValue>> Properties);
        /// <summary>
        /// Creates Vertex with provided Label and properties
        /// </summary>
        /// <param name="Label">Label for created vertex</param>
        /// <param name="Properties">Properties for created vertex</param>
        /// <returns>created IVertex</returns>
        GraphItems.IVertex CreateVertexAndLabel(string Label, Dictionary<string, List<GraphItems.IVertexValue>> Properties);
        /// <summary>
        /// Async for CreateVertexAndLabel
        /// </summary>
        /// <param name="Label">Label for created vertex</param>
        /// <param name="Properties">Properties for created vertex</param>
        /// <returns>Task that returns created IVertex</returns>
        Task<GraphItems.IVertex> CreateVertexAndLabelAsync(string Label, Dictionary<string, List<GraphItems.IVertexValue>> Properties);
        #endregion

        #region DeleteVertex
        /// <summary>
        /// Deletes a vertex with provided ID
        /// </summary>
        /// <param name="ID">ID of vertex to delete</param>
        void DeleteVertex(string ID);
        /// <summary>
        /// Async for DeleteVertex
        /// </summary>
        /// <param name="ID">ID of vertex to delete</param>
        /// <returns>Task that sends query</returns>
        Task DeleteVertexAsync(string ID);
        /// <summary>
        /// Deletes vertex that has the provided IndexName and IndexValue
        /// </summary>
        /// <param name="IndexName">Key of wanted property of wanted vertex to delete</param>
        /// <param name="ID">Value of wanted property</param>
        void DeleteVertexByIndex(string IndexName, object ID);
        /// <summary>
        /// Async for DeleteVertexByIndex
        /// </summary>
        /// <param name="IndexName">Key of wanted property of wanted vertex to delete</param>
        /// <param name="ID">Value of wanted property</param>
        /// <returns>Task that sends query</returns>
        Task DeleteVertexByIndexAsync(string IndexName, object ID);
        /// <summary>
        /// Deletes Vertex with the provided Label, IndexName and IndexValue
        /// </summary>
        /// <param name="Label">Label of wanted vertex to delete</param>
        /// <param name="IndexName">Key of wanted property of wanted vertex to delete</param>
        /// <param name="ID">Value of wanted property</param>
        void DeleteVertexByIndexAndLabel(string Label, string IndexName, object ID);
        /// <summary>
        /// Async for DeleteVertexByIndexAndLabel
        /// </summary>
        /// <param name="Label">Label of wanted vertex to delete</param>
        /// <param name="IndexName">Key of wanted property of wanted vertex to delete</param>
        /// <param name="ID">Value of wanted property</param>
        /// <returns>Task that sends query</returns>
        Task DeleteVertexByIndexAndLabelAsync(string Label, string IndexName, object ID);
        #endregion

        #region UpdateVertex
        /// <summary>
        /// Updates properties of vertex, other properties can be removed
        /// </summary>
        /// <param name="ID">ID of Vertex to update</param>
        /// <param name="Properties">properties that should be added or altered</param>
        /// <param name="RemoveOtherProperties">If other properties should be removed</param>
        void UpdateVertex(string ID, Dictionary<string, List<GraphItems.IVertexValue>> Properties, bool RemoveOtherProperties);
        /// <summary>
        /// Asnyc for UpdateVertex
        /// </summary>
        /// <param name="ID">ID of Vertex to update</param>
        /// <param name="Properties">properties that should be added or altered</param>
        /// <param name="RemoveOtherProperties">If other properties should be removed</param>
        /// <returns>TAsk that sends the query</returns>
        Task UpdateVertexAsync(string ID, Dictionary<string, List<GraphItems.IVertexValue>> Properties, bool RemoveOtherProperties);
        #endregion

        #region EdgeExists
        /// <summary>
        /// Checks if ingoing or outgoing edge with edge-label from a specific vertex exists
        /// </summary>
        /// <param name="StartVertexID">ID of outgoing vertex</param>
        /// <param name="Name">edge-label</param>
        /// <returns>Whether edge exists or not</returns>
        bool EdgeExistsBoth(string StartVertexID, string Name);
        /// <summary>
        /// async for EdgeExistsBoth
        /// </summary>
        /// <param name="StartVertexID">ID of outgoing vertex</param>
        /// <param name="Name">edge-label</param>
        /// <returns>Task that returns whether edge exists or not</returns>
        Task<bool> EdgeExistsBothAsync(string StartVertexID, string Name);
        /// <summary>
        /// checks if outgoing edge with edge-label from a specific vertex exists
        /// </summary>
        /// <param name="StartVertexID">ID of outgoing vertex</param>
        /// <param name="Name">Label of edge</param>
        /// <returns>Whether edge exists or not</returns>
        bool EdgeExistsOut(string StartVertexID, string Name);
        /// <summary>
        /// async for EdgeExistsOut
        /// </summary>
        /// <param name="StartVertexID">ID of outgoing vertex</param>
        /// <param name="Name">Label of edge</param>
        /// <returns>Task that returns whether edge exists or not</returns>
        Task<bool> EdgeExistsOutAsync(string StartVertexID, string Name);
        /// <summary>
        /// checks if ingoing edge with edge-label from a specific vertex exists
        /// </summary>
        /// <param name="StartVertexID">ID of ingoing vertex</param>
        /// <param name="Name">edge-label</param>
        /// <returns>Whether edge exists or not</returns>
        bool EdgeExistsIn(string StartVertexID, string Name);
        /// <summary>
        /// async for EdgeExistsIn
        /// </summary>
        /// <param name="StartVertexID">ID of ingoing vertex</param>
        /// <param name="Name">edge-label</param>
        /// <returns>Task that returns whether edge exists or not</returns>
        Task<bool> EdgeExistsInAsync(string StartVertexID, string Name);
        /// <summary>
        /// Checks if a bidirectional edge from a vertex to another vertex exists 
        /// </summary>
        /// <param name="StartVertexID">ID of a vertex</param>
        /// <param name="EndVertexID">ID of an another vertex</param>
        /// <param name="Name">edge-label</param>
        /// <returns>Whether bidirected edge exists or not</returns>
        bool EdgeExistsBetweenBoth(string StartVertexID, string EndVertexID, string Name);
        /// <summary>
        /// checks if an outgoing, directed edge from a vertex to another vertex exists
        /// </summary>
        /// <param name="StartVertexID">ID of outgoing vertex</param>
        /// <param name="EndVertexID">ID of ingoing vertex</param>
        /// <param name="Name">edge-label</param>
        /// <returns>Whether edge exists or not</returns>
        bool EdgeExistsBetweenOut(string StartVertexID, string EndVertexID, string Name);
        /// <summary>
        /// checks if an ingoing, directed edge from a vertex to another vertex exists 
        /// </summary>
        /// <param name="StartVertexID">ID of ingoing vertex</param>
        /// <param name="EndVertexID">ID of outgoing vertex</param>
        /// <param name="Name">edge-label</param>
        /// <returns>Whether edge exists or not</returns>
        bool EdgeExistsBetweenIn(string StartVertexID, string EndVertexID, string Name);
        #endregion

        #region CreateEdge
        /// <summary>
        /// creates directed edge from a vertex to another vertex
        /// </summary>
        /// <param name="StartVertexID">ID of outgoing vertex</param>
        /// <param name="EndVertexID">ID of ingoing vertex</param>
        /// <param name="Name">edge-label</param>
        /// <param name="Properties">Properties of edge</param>
        /// <returns>created edge</returns>
        GraphItems.IEdge CreateEdge(string StartVertexID, string EndVertexID, string Name, Dictionary<string, object> Properties = null);
        /// <summary>
        /// async for CreateEdge
        /// </summary>
        /// <param name="StartVertexID">ID of outgoing vertex</param>
        /// <param name="EndVertexID">ID of ingoing vertex</param>
        /// <param name="Name">edge-label</param>
        /// <param name="Properties">Properties of edge</param>
        /// <returns>Task that returns created edge</returns>
        Task<GraphItems.IEdge> CreateEdgeAsync(string StartVertexID, string EndVertexID, string Name, Dictionary<string, object> Properties = null);
        #endregion

        #region UpdateEdge
        /// <summary>
        /// updates an outgoing edge from vertex to another vertex, properties could be removed
        /// </summary>
        /// <param name="StartVertexID">ID of outgoing vertex</param>
        /// <param name="EndVertexID">ID of ingoing vertex</param>
        /// <param name="Name">edge-label</param>
        /// <param name="Properties">Properties to add to an edge</param>
        /// <param name="RemoveOtherProperties">If other properties should be removed</param>
        void UpdateEdgeOut(string StartVertexID, string EndVertexID, string Name, Dictionary<string, object> Properties, bool RemoveOtherProperties);
        /// <summary>
        /// async for UpdateEdgeOut
        /// </summary>
        /// <param name="StartVertexID">ID of outgoing vertex</param>
        /// <param name="EndVertexID">ID of ingoing vertex</param>
        /// <param name="Name">edge-label</param>
        /// <param name="Properties">Properties to add to an edge</param>
        /// <param name="RemoveOtherProperties">If other properties should be removed</param>
        /// <returns>Task that sends query</returns>
        Task UpdateEdgeOutAsync(string StartVertexID, string EndVertexID, string Name, Dictionary<string, object> Properties, bool RemoveOtherProperties);
        #endregion

        #region DeleteEdge
        /// <summary>
        /// Deletes edge with specific ID
        /// </summary>
        /// <param name="ID">ID of edge</param>
        void DeleteEdge(string ID);
        /// <summary>
        /// asnyc for DeleteEdge
        /// </summary>
        /// <param name="ID">ID of edge</param>
        /// <returns>Task that sends the query</returns>
        Task DeleteEdgeAsync(string ID);
        /// <summary>
        /// Deletes a bidirectional edge from a vertex
        /// </summary>
        /// <param name="StartVertexID">ID of target vertex</param>
        /// <param name="Name">edge-label</param>
        void DeleteEdgeBoth(string StartVertexID, string Name);
        /// <summary>
        /// async for DeleteEdgeBoth
        /// </summary>
        /// <param name="StartVertexID">ID of target vertex</param>
        /// <param name="Name">edge-label</param>
        /// <returns>Task that sends the query</returns>
        Task DeleteEdgeBothAsync(string StartVertexID, string Name);
        /// <summary>
        /// Deletes an outgoing edge from a vertex
        /// </summary>
        /// <param name="StartVertexID">ID of outgoing vertex</param>
        /// <param name="Name">edge-label</param>
        void DeleteEdgeOut(string StartVertexID, string Name);
        /// <summary>
        /// async for DeleteEdgeOut
        /// </summary>
        /// <param name="StartVertexID">ID of outgoing vertex</param>
        /// <param name="Name">edge-label</param>
        /// <returns>Task that sends the query</returns>
        Task DeleteEdgeOutAsync(string StartVertexID, string Name);
        /// <summary>
        /// deletes an ingoing edge from a vertex
        /// </summary>
        /// <param name="StartVertexID">ID of ingoing vertex</param>
        /// <param name="Name">edge-label</param>
        void DeleteEdgeIn(string StartVertexID, string Name);
        /// <summary>
        /// async for DeleteEdgeIn
        /// </summary>
        /// <param name="StartVertexID">ID of ingoing vertex</param>
        /// <param name="Name">edge-label</param>
        /// <returns>Task that sends the query</returns>
        Task DeleteEdgeInAsync(string StartVertexID, string Name);
        #endregion

        #region GetEdge
        /// <summary>
        /// gets an edge with provided query
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>wanted IEdge</returns>
        GraphItems.IEdge GetEdge(GremlinScript Script);
        /// <summary>
        /// async for GetEdge(GremlinScript Script)
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>Task that returns wanted IEdge</returns>
        Task<GraphItems.IEdge> GetEdgeAsync(GremlinScript Script);
        /// <summary>
        /// gets an edge with the ID
        /// </summary>
        /// <param name="ID">ID of edge</param>
        /// <returns>wanted IEdge</returns>
        GraphItems.IEdge GetEdge(string ID);
        /// <summary>
        /// async for GetEdge(string ID)
        /// </summary>
        /// <param name="ID">ID of edge</param>
        /// <returns>Task that returns wanted IEdge</returns>
        Task<GraphItems.IEdge> GetEdgeAsync(string ID);
        #endregion

        #region GetEdges
        /// <summary>
        /// gets edges with provided query
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>List of IEdge</returns>
        List<GraphItems.IEdge> GetEdges(GremlinScript Script);
        /// <summary>
        /// async for GetEdges(GremlinScript Script)
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>Task that returns list of IEdge</returns>
        Task<List<GraphItems.IEdge>> GetEdgesAsync(GremlinScript Script);
        #endregion

        #region GetBoolean
        /// <summary>
        /// gets a Boolean of a query
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>Whether statement is true or false</returns>
        bool GetBoolean(GremlinScript Script);
        /// <summary>
        /// async for GetBoolean
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>Task that returns whether statement is true or false</returns>
        Task<bool> GetBooleanAsync(GremlinScript Script);
        #endregion

        #region GetString
        /// <summary>
        /// gets a string of a query, e.g. a value from a index
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>Wanted string from query</returns>
        string GetString(GremlinScript Script);
        /// <summary>
        /// async for GetString
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>Task that returns wanted string from query</returns>
        Task<string> GetStringAsync(GremlinScript Script);
        #endregion

        #region GetArray
        /// <summary>
        /// gets an array of objects from database, e.g. list of objects from a index
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>An array/list of objects</returns>
        List<object> GetArray(GremlinScript Script);
        /// <summary>
        /// async for GetArray
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>Task that returns an array/list of objects</returns>
        Task<List<object>> GetArrayAsync(GremlinScript Script);
        /// <summary>
        /// gets an array of generic values from database, e.g. list of strings from a index
        /// </summary>
        /// <typeparam name="ValueType">ValueType in array or list</typeparam>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>An array/list of specific ValueTyp</returns>
        List<ValueType> GetArray<ValueType>(GremlinScript Script);
        /// <summary>
        /// async for GetArray
        /// </summary>
        /// <typeparam name="ValueType">ValueType in array or list</typeparam>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>Task that returns an array/list of specific ValueTyp</returns>
        Task<List<ValueType>> GetArrayAsync<ValueType>(GremlinScript Script);
        #endregion

        #region Scalar
        /// <summary>
        /// gets an scalar from database
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>Wanted scalar-object</returns>
        object GetScalar(GremlinScript Script);
        /// <summary>
        /// async for GetScalar
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>Task that returns wanted scalar-object</returns>
        Task<object> GetScalarAsync(GremlinScript Script);
        /// <summary>
        /// gets a generic scalar from database
        /// </summary>
        /// <typeparam name="ValueType">ValueType to return</typeparam>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>Wanted scalar</returns>
        ValueType GetScalar<ValueType>(GremlinScript Script);
        /// <summary>
        /// async for GetScalar
        /// </summary>
        /// <typeparam name="ValueType">ValueType to return</typeparam>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>Task that returns wanted scalar</returns>
        Task<ValueType> GetScalarAsync<ValueType>(GremlinScript Script);
        #endregion

        #region GetDictionaryArray
        /// <summary>
        /// gets a List of a Dictionary from database, e.g. a valueMap of several vertices
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>List of dictionaries</returns>
        List<Dictionary<string, object>> GetDictionaryArray(GremlinScript Script);
        /// <summary>
        /// async for GetDictionaryArray
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>Task that returns list of dictionaries</returns>
        Task<List<Dictionary<string, object>>> GetDictionaryArrayAsync(GremlinScript Script);
        #endregion

        #region Execute
        /// <summary>
        /// executes a statement, e.g. deleting of a vertex
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        void Execute(GremlinScript Script);
        /// <summary>
        /// async for Execute
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>Task that sends the query</returns>
        Task ExecuteAsync(GremlinScript Script);
        #endregion

        /// <summary>
        /// Host-Name
        /// </summary>
        string Host { get; }
        /// <summary>
        /// Port of the Applikation
        /// </summary>
        int Port { get; }
        /// <summary>
        /// deserialize a Json Array to Vertices
        /// </summary>
        /// <param name="Object">Json-object to parse</param>
        /// <returns>List of IVertex</returns>
        List<GraphItems.IVertex> GetVerticesFromJArray(object Object);
        /// <summary>
        /// deserialize a Json Array to a dictionary
        /// </summary>
        /// <param name="Object">Json-object to deserialize</param>
        /// <returns>Parsed dictionary</returns>
        Dictionary<string, object> GetDictionaryFromJArray(object Object);
        /// <summary>
        /// deserialize a Json Array to a list of dictionary
        /// </summary>
        /// <param name="Object">Json-object to parse</param>
        /// <returns>Parsed list of dictionaries</returns>
        List<Dictionary<string, object>> GetDictionariesFromJArray(object Object);
        /// <summary>
        /// deserializes a Json Object to a Vertex
        /// </summary>
        /// <param name="Object">Json-object to parse</param>
        /// <returns>Parsed vertex</returns>
        GraphItems.IVertex GetVertexFromJObject(object Object);
        /// <summary>
        /// deserializes a Json Object to a dictionary
        /// </summary>
        /// <param name="Object">Json-object to parse</param>
        /// <returns>Parsed dictionary</returns>
        Dictionary<string, object> GetDictionaryFromJObject(object Object);
        /// <summary>
        /// desrializes a Json Object to a generic value
        /// </summary>
        /// <typeparam name="T">Type in that should be parsed</typeparam>
        /// <param name="Object">Json-object to parse</param>
        /// <returns>Parsed T</returns>
        T GetValueFromJObject<T>(object Object);
    }
}