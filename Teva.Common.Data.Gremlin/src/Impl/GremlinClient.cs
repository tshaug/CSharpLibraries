using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Teva.Common.Data.Gremlin.Impl
{
    /// <summary>
    /// Implementation of IGremlinClient
    /// </summary>
    public class GremlinClient : IGremlinClient
    {
        /// <summary>
        /// Initializes a new instance of the GremlinClient with needed parameteres
        /// </summary>
        /// <param name="Host">Host-Adress of TinkerPop</param>
        /// <param name="postfix">"/gremlin" needed since TinkerPop 3.2</param>
        /// <param name="Port">Port of Host</param>
        /// <param name="Username">Username if required</param>
        /// <param name="Password">Password if required</param>
        public GremlinClient(string Host, string postfix= "/gremlin", int Port = 8182, string Username = null, string Password = null)
        {
            this.Host = Host;
            this.Port = Port;
            this.Client = new GremlinServerClient(Host, postfix, Port, Username: Username, Password: Password);
        }

        #region VertexExists
        /// <summary>
        /// Vertex Exists by IndexName and Value, e.g. g.V().has('Name','John').hasNext()
        /// </summary>
        /// <param name="IndexName">Key of property</param>
        /// <param name="ID">Value of Property</param>
        /// <returns>Bool whether Vertex exists or not</returns>
        public bool VertexExistsByIndex(string IndexName, object ID)
        {
            return GetBoolean(new GremlinScript().Append_VertexExistsByIndex(IndexName, ID));
        }
        /// <summary>
        /// Async for VertexExistsByIndex
        /// </summary>
        /// <param name="IndexName">Key of property</param>
        /// <param name="ID">Value of property</param>
        /// <returns>Task whether Vertex exists or not</returns>
        public Task<bool> VertexExistsByIndexAsync(string IndexName, object ID)
        {
            return GetBooleanAsync(new GremlinScript().Append_VertexExistsByIndex(IndexName, ID));
        }
        /// <summary>
        /// Vertex Exists by Label, IndexName and IndexValue, e.g. g.V().has(label,'Person','Name','John').hasNext()
        /// </summary>
        /// <param name="Label">Label of wanted vertex</param>
        /// <param name="IndexName">Key of wanted property</param>
        /// <param name="ID">Value of wanted property</param>
        /// <returns>Bool wether Vertex exist or not</returns>
        public bool VertexExistsByIndexAndLabel(string Label, string IndexName, object ID)
        {
            return GetBoolean(new GremlinScript().Append_VertexExistsByIndexAndLabel(Label, IndexName, ID));
        }
        /// <summary>
        /// Async for VertexExistsByIndexAndLabel
        /// </summary>
        /// <param name="Label">Label of wanted vertex</param>
        /// <param name="IndexName">Key of wanted property</param>
        /// <param name="ID">Value of wanted property</param>
        /// <returns>Task whether vertex exists or not</returns>
        public Task<bool> VertexExistsByIndexAndLabelAsync(string Label, string IndexName, object ID)
        {
            return GetBooleanAsync(new GremlinScript().Append_VertexExistsByIndexAndLabel(Label, IndexName, ID));
        }
        #endregion

        #region GetVertex
        /// <summary>
        /// Gets IVertex from a specific Query
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>The wanted IVertex</returns>
        public GraphItems.IVertex GetVertex(GremlinScript Script)
        {
            return Client.ExecuteScalar<GraphItems.IVertex>(Script.GetScript(), Script.GetBindings());
        }
        /// <summary>
        /// Async for GetVertex(GremlinScript Script)
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>Task that returns wanted IVertex</returns>
        public Task<GraphItems.IVertex> GetVertexAsync(GremlinScript Script)
        {
            return Client.ExecuteScalarAsync<GraphItems.IVertex>(Script.GetScript(), Script.GetBindings());
        }
        /// <summary>
        /// Gets IVertex with specific ID
        /// </summary>
        /// <param name="ID">ID of wanted vertex</param>
        /// <returns>The wanted IVertex</returns>
        public GraphItems.IVertex GetVertex(string ID)
        {
            return GetVertex(new GremlinScript().Append_GetVertex(ID));
        }
        /// <summary>
        /// Async for GetVertex(string ID)
        /// </summary>
        /// <param name="ID">ID of wanted vertex</param>
        /// <returns>Task that returns wanted IVertex</returns>
        public Task<GraphItems.IVertex> GetVertexAsync(string ID)
        {
            return GetVertexAsync(new GremlinScript().Append_GetVertex(ID));
        }
        /// <summary>
        /// Gets IVertex by IndexName and IndexValue
        /// </summary>
        /// <param name="IndexName">Key of wanted property of wanted vertex</param>
        /// <param name="ID">Value of wanted property</param>
        /// <returns>The wanted IVertex</returns>
        public GraphItems.IVertex GetVertexByIndex(string IndexName, object ID)
        {
            return GetVertex(new GremlinScript().Append_GetVerticesByIndex(IndexName, ID));
        }
        /// <summary>
        /// Async for GetVertexByIndex
        /// </summary>
        /// <param name="IndexName">Key of wanted property of wanted vertex</param>
        /// <param name="ID">Value of wanted property</param>
        /// <returns>Task that returns wanted vertex</returns>
        public Task<GraphItems.IVertex> GetVertexByIndexAsync(string IndexName, object ID)
        {
            return GetVertexAsync(new GremlinScript().Append_GetVerticesByIndex(IndexName, ID));
        }
        /// <summary>
        /// Gets IVertex by Label, IndexName and IndexValue
        /// </summary>
        /// <param name="Label">Label of wanted vertex</param>
        /// <param name="IndexName">Key of wanted property of wanted vertex</param>
        /// <param name="ID">Value of wanted property</param>
        /// <returns>The wanted IVertex</returns>
        public GraphItems.IVertex GetVertexByIndexAndLabel(string Label, string IndexName, object ID)
        {
            return GetVertex(new GremlinScript().Append_GetVerticesByIndexAndLabel(Label, IndexName, ID));
        }
        /// <summary>
        /// Async for GetVertexByIndexAndLabel
        /// </summary>
        /// <param name="Label">Label of wanted vertex</param>
        /// <param name="IndexName">Key of wanted property of wanted vertex</param>
        /// <param name="ID">Value of wanted property</param>
        /// <returns>Task that returns wanted IVertex</returns>
        public Task<GraphItems.IVertex> GetVertexByIndexAndLabelAsync(string Label, string IndexName, object ID)
        {
            return GetVertexAsync(new GremlinScript().Append_GetVerticesByIndexAndLabel(Label, IndexName, ID));
        }
        #endregion

        #region GetVertices
        /// <summary>
        /// Gets Vertices from a specific Query
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>List of IVertex</returns>
        public List<GraphItems.IVertex> GetVertices(GremlinScript Script)
        {
            return Client.Execute<GraphItems.IVertex>(Script.GetScript(), Script.GetBindings());
        }
        /// <summary>
        /// Async for GetVertices
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>Task that returns list of IVertex</returns>
        public Task<List<GraphItems.IVertex>> GetVerticesAsync(GremlinScript Script)
        {
            return Client.ExecuteAsync<GraphItems.IVertex>(Script.GetScript(), Script.GetBindings());
        }
        /// <summary>
        /// Gets Vertices by IndexName and IndexValue
        /// </summary>
        /// <param name="IndexName">Key of wanted property of wanted vertices</param>
        /// <param name="ID">Value of wanted property</param>
        /// <returns>List of IVertex</returns>
        public List<GraphItems.IVertex> GetVerticesByIndex(string IndexName, object ID)
        {
            return GetVertices(new GremlinScript().Append_GetVerticesByIndex(IndexName, ID));
        }
        /// <summary>
        /// Async for GetsVerticesByIndex(string IndexName, object ID)
        /// </summary>
        /// <param name="IndexName">Key of wanted property of wanted vertices</param>
        /// <param name="ID">Value of wanted property</param>
        /// <returns>Task that return list of IVertex</returns>
        public Task<List<GraphItems.IVertex>> GetVerticesByIndexAsync(string IndexName, object ID)
        {
            return GetVerticesAsync(new GremlinScript().Append_GetVerticesByIndex(IndexName, ID));
        }
        /// <summary>
        /// Gets Vertices by IndexName and a List of IndexValues
        /// </summary>
        /// <param name="IndexName">Key of wanted property of wanted vertices</param>
        /// <param name="IDs">List of IndexValues</param>
        /// <returns>List of IVertex</returns>
        public List<GraphItems.IVertex> GetVerticesByIndex(string IndexName, IEnumerable<object> IDs)
        {
            return GetVertices(new GremlinScript().Append_GetVerticesByIndex(IndexName, IDs));
        }
        /// <summary>
        /// Async for GetVerticesByIndex(string IndexName, IEnumerable IDs)
        /// </summary>
        /// <param name="IndexName">Key of wanted property of wanted vertices</param>
        /// <param name="IDs">List of IndexValues</param>
        /// <returns>Task that returns list of IVertex</returns>
        public Task<List<GraphItems.IVertex>> GetVerticeByIndexAsync(string IndexName, IEnumerable<object> IDs)
        {
            return GetVerticesAsync(new GremlinScript().Append_GetVerticesByIndex(IndexName, IDs));
        }
        /// <summary>
        /// Gets Vertices by Label, IndexName and IndexValue
        /// </summary>
        /// <param name="Label">Label of wanted vertices</param>
        /// <param name="IndexName">Key of wanted property of wanted vertices</param>
        /// <param name="ID">Value of wanted property</param>
        /// <returns>List of IVertex</returns>
        public List<GraphItems.IVertex> GetVerticesByIndexAndLabel(string Label, string IndexName, object ID)
        {
            return GetVertices(new GremlinScript().Append_GetVerticesByIndexAndLabel(Label, IndexName, ID));
        }
        /// <summary>
        /// Async for GetVerticesByIndexAndLabel(string Label, string IndexName, object ID)
        /// </summary>
        /// <param name="Label">Label of wanted vertices</param>
        /// <param name="IndexName">Key of wanted property of wanted vertices</param>
        /// <param name="ID">Value of wanted property</param>
        /// <returns>Task that returns list of IVertex</returns>
        public Task<List<GraphItems.IVertex>> GetVerticesByIndexAndLabelAsync(string Label, string IndexName, object ID)
        {
            return GetVerticesAsync(new GremlinScript().Append_GetVerticesByIndexAndLabel(Label, IndexName, ID));
        }
        /// <summary>
        /// Gets Vertices by Label, IndexName and a List of IndexValues
        /// </summary>
        /// <param name="Label">Label of wanted vertices</param>
        /// <param name="IndexName">Key of wanted property of wanted vertices</param>
        /// <param name="IDs">List of values of wanted property</param>
        /// <returns>List of IVertex</returns>
        public List<GraphItems.IVertex> GetVerticesByIndexAndLabel(string Label, string IndexName, IEnumerable<object> IDs)
        {
            return GetVertices(new GremlinScript().Append_GetVerticesByIndexAndLabel(Label, IndexName, IDs));
        }
        /// <summary>
        /// Async for GetVerticesByIndexAndLabel(string Label, string IndexName, IEnumerable IDs)
        /// </summary>
        /// <param name="Label">Label of wanted vertices</param>
        /// <param name="IndexName">Key of wanted property of wanted vertices</param>
        /// <param name="IDs">List of values of wanted property</param>
        /// <returns>Task that returns list of IVertex</returns>
        public Task<List<GraphItems.IVertex>> GetVerticesByIndexAndLabelAsync(string Label, string IndexName, IEnumerable<object> IDs)
        {
            return GetVerticesAsync(new GremlinScript().Append_GetVerticesByIndexAndLabel(Label, IndexName, IDs));
        }
        #endregion

        #region GetVertexID
        /// <summary>
        /// Gets ID of Vertex by IndexName and IndexValue
        /// </summary>
        /// <param name="IndexName">Key of wanted property of wanted Vertex</param>
        /// <param name="ID">Value of property</param>
        /// <returns>ID as string</returns>
        public string GetVertexIDByIndex(string IndexName, object ID)
        {
            return GetString(new GremlinScript().Append_GetVertexIDByIndex(IndexName, ID));
        }
        /// <summary>
        /// Async for GetVertexIDByIndex(string IndexName, object ID)
        /// </summary>
        /// <param name="IndexName">Key of wanted property of wanted Vertex</param>
        /// <param name="ID">Value of property</param>
        /// <returns>Task that returns ID as string</returns>
        public Task<string> GetVertexIDByIndexAsync(string IndexName, object ID)
        {
            return GetStringAsync(new GremlinScript().Append_GetVertexIDByIndex(IndexName, ID));
        }
        /// <summary>
        /// Gets ID of Vertex by Label, IndexName and IndexValue
        /// </summary>
        /// <param name="Label">Label of wanted vertex</param>
        /// <param name="IndexName">Key of wanted property of wanted Vertex</param>
        /// <param name="ID">Value of property</param>
        /// <returns>ID as string</returns>
        public string GetVertexIDByIndexAndLabel(string Label, string IndexName, object ID)
        {
            return GetString(new GremlinScript().Append_GetVertexIDByIndexAndLabel(Label, IndexName, ID));
        }
        /// <summary>
        /// Async for GetVertexIDByIndexAndLabel(string Label, string IndexName, object ID)
        /// </summary>
        /// <param name="Label">Label of wanted vertex</param>
        /// <param name="IndexName">Key of wanted property of wanted Vertex</param>
        /// <param name="ID">Value of property</param>
        /// <returns>Task that returns ID as string</returns>
        public Task<string> GetVertexIDByIndexAndLabelAsync(string Label, string IndexName, object ID)
        {
            return GetStringAsync(new GremlinScript().Append_GetVertexIDByIndexAndLabel(Label, IndexName, ID));
        }
        #endregion

        #region CreateVertex
        /// <summary>
        /// Creates a vertex in database with given properties but without a label
        /// </summary>
        /// <param name="Properties">Properties for created vertex</param>
        /// <returns>created IVertex</returns>
        public GraphItems.IVertex CreateVertex(Dictionary<string, List<GraphItems.IVertexValue>> Properties)
        {
            return GetVertex(new GremlinScript().Append_CreateVertex(Properties));
        }
        /// <summary>
        /// Async for CreateVertex
        /// </summary>
        /// <param name="Properties">Properties for created vertex</param>
        /// <returns>Task that returns created IVertex</returns>
        public Task<GraphItems.IVertex> CreateVertexAsync(Dictionary<string, List<GraphItems.IVertexValue>> Properties)
        {
            return GetVertexAsync(new GremlinScript().Append_CreateVertex(Properties));
        }
        /// <summary>
        /// Creates Vertex with provided Label and properties
        /// </summary>
        /// <param name="Label">Label for created vertex</param>
        /// <param name="Properties">Properties for created vertex</param>
        /// <returns>created IVertex</returns>
        public GraphItems.IVertex CreateVertexAndLabel(string Label, Dictionary<string, List<GraphItems.IVertexValue>> Properties)
        {
            return GetVertex(new GremlinScript().Append_CreateVertexWithLabel(Label, Properties));
        }
        /// <summary>
        /// Async for CreateVertexAndLabel
        /// </summary>
        /// <param name="Label">Label for created vertex</param>
        /// <param name="Properties">Properties for created vertex</param>
        /// <returns>Task that returns created IVertex</returns>
        public Task<GraphItems.IVertex> CreateVertexAndLabelAsync(string Label, Dictionary<string, List<GraphItems.IVertexValue>> Properties)
        {
            return GetVertexAsync(new GremlinScript().Append_CreateVertexWithLabel(Label, Properties));
        }
        #endregion

        #region DeleteVertex
        /// <summary>
        /// Deletes a vertex with provided ID
        /// </summary>
        /// <param name="ID">ID of vertex to delete</param>
        public void DeleteVertex(string ID)
        {
            Execute(new GremlinScript().Append_DeleteVertex(ID));
        }
        /// <summary>
        /// Async for DeleteVertex
        /// </summary>
        /// <param name="ID">ID of vertex to delete</param>
        /// <returns>Task that sends query</returns>
        public Task DeleteVertexAsync(string ID)
        {
            return ExecuteAsync(new GremlinScript().Append_DeleteVertex(ID));
        }
        /// <summary>
        /// Deletes vertex that has the provided IndexName and IndexValue
        /// </summary>
        /// <param name="IndexName">Key of wanted property of wanted vertex to delete</param>
        /// <param name="ID">Value of wanted property</param>
        public void DeleteVertexByIndex(string IndexName, object ID)
        {
            Execute(new GremlinScript().Append_DeleteVertexByIndex(IndexName, ID));
        }
        /// <summary>
        /// Async for DeleteVertexByIndex
        /// </summary>
        /// <param name="IndexName">Key of wanted property of wanted vertex to delete</param>
        /// <param name="ID">Value of wanted property</param>
        /// <returns>Task that sends query</returns>
        public Task DeleteVertexByIndexAsync(string IndexName, object ID)
        {
            return ExecuteAsync(new GremlinScript().Append_DeleteVertexByIndex(IndexName, ID));
        }
        /// <summary>
        /// Deletes Vertex with the provided Label, IndexName and IndexValue
        /// </summary>
        /// <param name="Label">Label of wanted vertex to delete</param>
        /// <param name="IndexName">Key of wanted property of wanted vertex to delete</param>
        /// <param name="ID">Value of wanted property</param>
        public void DeleteVertexByIndexAndLabel(string Label, string IndexName, object ID)
        {
            Execute(new GremlinScript().Append_DeleteVertexByIndexAndLabel(Label, IndexName, ID));
        }
        /// <summary>
        /// Async for DeleteVertexByIndexAndLabel
        /// </summary>
        /// <param name="Label">Label of wanted vertex to delete</param>
        /// <param name="IndexName">Key of wanted property of wanted vertex to delete</param>
        /// <param name="ID">Value of wanted property</param>
        /// <returns>Task that sends query</returns>
        public Task DeleteVertexByIndexAndLabelAsync(string Label, string IndexName, object ID)
        {
            return ExecuteAsync(new GremlinScript().Append_DeleteVertexByIndexAndLabel(Label, IndexName, ID));
        }
        #endregion

        #region UpdateVertex
        /// <summary>
        /// Updates properties of vertex, other properties can be removed
        /// </summary>
        /// <param name="ID">ID of Vertex to update</param>
        /// <param name="Properties">properties that should be added or altered</param>
        /// <param name="RemoveOtherProperties">If other properties should be removed</param>
        public void UpdateVertex(string ID, Dictionary<string, List<GraphItems.IVertexValue>> Properties, bool RemoveOtherProperties)
        {
            Execute(new GremlinScript().Append_UpdateVertex(ID, Properties, RemoveOtherProperties).Append("null;"));
        }
        /// <summary>
        /// Asnyc for UpdateVertex
        /// </summary>
        /// <param name="ID">ID of Vertex to update</param>
        /// <param name="Properties">properties that should be added or altered</param>
        /// <param name="RemoveOtherProperties">If other properties should be removed</param>
        /// <returns>TAsk that sends the query</returns>
        public Task UpdateVertexAsync(string ID, Dictionary<string, List<GraphItems.IVertexValue>> Properties, bool RemoveOtherProperties)
        {
            return ExecuteAsync(new GremlinScript().Append_UpdateVertex(ID, Properties, RemoveOtherProperties).Append("null;"));
        }
        #endregion

        #region EdgeExists
        /// <summary>
        /// Checks if ingoing or outgoing edge with edge-label from a specific vertex exists
        /// </summary>
        /// <param name="StartVertexID">ID of outgoing vertex</param>
        /// <param name="Name">edge-label</param>
        /// <returns>Whether edge exists or not</returns>
        public bool EdgeExistsBoth(string StartVertexID, string Name)
        {
            return GetBoolean(new GremlinScript().Append_EdgeExistsBoth(StartVertexID, Name));
        }
        /// <summary>
        /// async for EdgeExistsBoth
        /// </summary>
        /// <param name="StartVertexID">ID of outgoing vertex</param>
        /// <param name="Name">edge-label</param>
        /// <returns>Task that returns whether edge exists or not</returns>
        public Task<bool> EdgeExistsBothAsync(string StartVertexID, string Name)
        {
            return GetBooleanAsync(new GremlinScript().Append_EdgeExistsBoth(StartVertexID, Name));
        }
        /// <summary>
        /// checks if outgoing edge with edge-label from a specific vertex exists
        /// </summary>
        /// <param name="StartVertexID">ID of outgoing vertex</param>
        /// <param name="Name">Label of edge</param>
        /// <returns>Whether edge exists or not</returns>
        public bool EdgeExistsOut(string StartVertexID, string Name)
        {
            return GetBoolean(new GremlinScript().Append_EdgeExistsOut(StartVertexID, Name));
        }
        /// <summary>
        /// async for EdgeExistsOut
        /// </summary>
        /// <param name="StartVertexID">ID of outgoing vertex</param>
        /// <param name="Name">Label of edge</param>
        /// <returns>Task that returns whether edge exists or not</returns>
        public Task<bool> EdgeExistsOutAsync(string StartVertexID, string Name)
        {
            return GetBooleanAsync(new GremlinScript().Append_EdgeExistsOut(StartVertexID, Name));
        }
        /// <summary>
        /// checks if ingoing edge with edge-label from a specific vertex exists
        /// </summary>
        /// <param name="StartVertexID">ID of ingoing vertex</param>
        /// <param name="Name">edge-label</param>
        /// <returns>Whether edge exists or not</returns>
        public bool EdgeExistsIn(string StartVertexID, string Name)
        {
            return GetBoolean(new GremlinScript().Append_EdgeExistsIn(StartVertexID, Name));
        }
        /// <summary>
        /// async for EdgeExistsIn
        /// </summary>
        /// <param name="StartVertexID">ID of ingoing vertex</param>
        /// <param name="Name">edge-label</param>
        /// <returns>Task that returns whether edge exists or not</returns>
        public Task<bool> EdgeExistsInAsync(string StartVertexID, string Name)
        {
            return GetBooleanAsync(new GremlinScript().Append_EdgeExistsIn(StartVertexID, Name));
        }
        /// <summary>
        /// Checks if a bidirected edge from a vertex to another vertex exists 
        /// </summary>
        /// <param name="StartVertexID">ID of a vertex</param>
        /// <param name="EndVertexID">ID of an another vertex</param>
        /// <param name="Name">edge-label</param>
        /// <returns>Whether bidirected edge exists or not</returns>
        public bool EdgeExistsBetweenBoth(string StartVertexID, string EndVertexID, string Name)
        {
            return GetBoolean(new GremlinScript().Append_EdgeExistsBetweenBoth(StartVertexID, EndVertexID, Name));
        }
        /// <summary>
        /// checks if an outgoing, directed edge from a vertex to another vertex exists
        /// </summary>
        /// <param name="StartVertexID">ID of outgoing vertex</param>
        /// <param name="EndVertexID">ID of ingoing vertex</param>
        /// <param name="Name">edge-label</param>
        /// <returns>Whether edge exists or not</returns>
        public bool EdgeExistsBetweenOut(string StartVertexID, string EndVertexID, string Name)
        {
            return GetBoolean(new GremlinScript().Append_EdgeExistsBetweenOut(StartVertexID, EndVertexID, Name));
        }
        /// <summary>
        /// checks if an ingoing, directed edge from a vertex to another vertex exists 
        /// </summary>
        /// <param name="StartVertexID">ID of ingoing vertex</param>
        /// <param name="EndVertexID">ID of outgoing vertex</param>
        /// <param name="Name">edge-label</param>
        /// <returns>Whether edge exists or not</returns>
        public bool EdgeExistsBetweenIn(string StartVertexID, string EndVertexID, string Name)
        {
            return GetBoolean(new GremlinScript().Append_EdgeExistsBetweenIn(StartVertexID, EndVertexID, Name));
        }

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
        public GraphItems.IEdge CreateEdge(string StartVertexID, string EndVertexID, string Name, Dictionary<string, object> Properties = null)
        {
            return GetEdge(new GremlinScript().Append_CreateEdge(StartVertexID, EndVertexID, Name, Properties));
        }
        /// <summary>
        /// async for CreateEdge
        /// </summary>
        /// <param name="StartVertexID">ID of outgoing vertex</param>
        /// <param name="EndVertexID">ID of ingoing vertex</param>
        /// <param name="Name">edge-label</param>
        /// <param name="Properties">Properties of edge</param>
        /// <returns>Task that returns created edge</returns>
        public Task<GraphItems.IEdge> CreateEdgeAsync(string StartVertexID, string EndVertexID, string Name, Dictionary<string, object> Properties = null)
        {
            return GetEdgeAsync(new GremlinScript().Append_CreateEdge(StartVertexID, EndVertexID, Name, Properties));
        }
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
        public void UpdateEdgeOut(string StartVertexID, string EndVertexID, string Name, Dictionary<string, object> Properties, bool RemoveOtherProperties)
        {
            Execute(new GremlinScript().Append_UpdateEdgeBetween_Out(StartVertexID, EndVertexID, Name, Properties, RemoveOtherProperties));
        }
        /// <summary>
        /// async for UpdateEdgeOut
        /// </summary>
        /// <param name="StartVertexID">ID of outgoing vertex</param>
        /// <param name="EndVertexID">ID of ingoing vertex</param>
        /// <param name="Name">edge-label</param>
        /// <param name="Properties">Properties to add to an edge</param>
        /// <param name="RemoveOtherProperties">If other properties should be removed</param>
        /// <returns>Task that sends query</returns>
        public Task UpdateEdgeOutAsync(string StartVertexID, string EndVertexID, string Name, Dictionary<string, object> Properties, bool RemoveOtherProperties)
        {
            return ExecuteAsync(new GremlinScript().Append_UpdateEdgeBetween_Out(StartVertexID, EndVertexID, Name, Properties, RemoveOtherProperties));
        }
        #endregion

        #region DeleteEdge
        /// <summary>
        /// Deletes edge with specific ID
        /// </summary>
        /// <param name="ID">ID of edge</param>
        public void DeleteEdge(string ID)
        {
            Execute(new GremlinScript().Append_DeleteEdge(ID));
        }
        /// <summary>
        /// asnyc for DeleteEdge
        /// </summary>
        /// <param name="ID">ID of edge</param>
        /// <returns>Task that sends the query</returns>
        public Task DeleteEdgeAsync(string ID)
        {
            return ExecuteAsync(new GremlinScript().Append_DeleteEdge(ID));
        }
        /// <summary>
        /// Deletes a bidirectional edge from a vertex
        /// </summary>
        /// <param name="StartVertexID">ID of target vertex</param>
        /// <param name="Name">edge-label</param>
        public void DeleteEdgeBoth(string StartVertexID, string Name)
        {
            Execute(new GremlinScript().Append_DeleteEdge_Both(StartVertexID, Name));
        }
        /// <summary>
        /// async for DeleteEdgeBoth
        /// </summary>
        /// <param name="StartVertexID">ID of target vertex</param>
        /// <param name="Name">edge-label</param>
        /// <returns>Task that sends the query</returns>
        public Task DeleteEdgeBothAsync(string StartVertexID, string Name)
        {
            return ExecuteAsync(new GremlinScript().Append_DeleteEdge_Both(StartVertexID, Name));
        }
        /// <summary>
        /// Deletes an outgoing edge from a vertex
        /// </summary>
        /// <param name="StartVertexID">ID of outgoing vertex</param>
        /// <param name="Name">edge-label</param>
        public void DeleteEdgeOut(string StartVertexID, string Name)
        {
            Execute(new GremlinScript().Append_DeleteEdge_Out(StartVertexID, Name));
        }
        /// <summary>
        /// async for DeleteEdgeOut
        /// </summary>
        /// <param name="StartVertexID">ID of outgoing vertex</param>
        /// <param name="Name">edge-label</param>
        /// <returns>Task that sends the query</returns>
        public Task DeleteEdgeOutAsync(string StartVertexID, string Name)
        {
            return ExecuteAsync(new GremlinScript().Append_DeleteEdge_Out(StartVertexID, Name));
        }
        /// <summary>
        /// deletes an ingoing edge from a vertex
        /// </summary>
        /// <param name="StartVertexID">ID of ingoing vertex</param>
        /// <param name="Name">edge-label</param>
        public void DeleteEdgeIn(string StartVertexID, string Name)
        {
            Execute(new GremlinScript().Append_DeleteEdge_In(StartVertexID, Name));
        }
        /// <summary>
        /// async for DeleteEdgeIn
        /// </summary>
        /// <param name="StartVertexID">ID of ingoing vertex</param>
        /// <param name="Name">edge-label</param>
        /// <returns>Task that sends the query</returns>
        public Task DeleteEdgeInAsync(string StartVertexID, string Name)
        {
            return ExecuteAsync(new GremlinScript().Append_DeleteEdge_In(StartVertexID, Name));
        }
        #endregion

        #region GetEdge
        /// <summary>
        /// gets an edge with provided query
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>wanted IEdge</returns>
        public GraphItems.IEdge GetEdge(GremlinScript Script)
        {
            return Client.ExecuteScalar<GraphItems.IEdge>(Script.GetScript(), Script.GetBindings());
        }
        /// <summary>
        /// async for GetEdge(GremlinScript Script)
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>Task that returns wanted IEdge</returns>
        public Task<GraphItems.IEdge> GetEdgeAsync(GremlinScript Script)
        {
            return Client.ExecuteScalarAsync<GraphItems.IEdge>(Script.GetScript(), Script.GetBindings());
        }
        /// <summary>
        /// gets an edge with the ID
        /// </summary>
        /// <param name="ID">ID of edge</param>
        /// <returns>wanted IEdge</returns>
        public GraphItems.IEdge GetEdge(string ID)
        {
            return GetEdge(new GremlinScript().Append_GetEdge(ID));
        }
        /// <summary>
        /// async for GetEdge(string ID)
        /// </summary>
        /// <param name="ID">ID of edge</param>
        /// <returns>Task that returns wanted IEdge</returns>
        public Task<GraphItems.IEdge> GetEdgeAsync(string ID)
        {
            return GetEdgeAsync(new GremlinScript().Append_GetEdge(ID));
        }
        #endregion

        #region GetEdges
        /// <summary>
        /// gets edges with provided query
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>List of IEdge</returns>
        public List<GraphItems.IEdge> GetEdges(GremlinScript Script)
        {
            return Client.Execute<GraphItems.IEdge>(Script.GetScript(), Script.GetBindings());
        }
        /// <summary>
        /// async for GetEdges(GremlinScript Script)
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>Task that returns list of IEdge</returns>
        public Task<List<GraphItems.IEdge>> GetEdgesAsync(GremlinScript Script)
        {
            return Client.ExecuteAsync<GraphItems.IEdge>(Script.GetScript(), Script.GetBindings());
        }
        #endregion

        #region GetBoolean
        /// <summary>
        /// gets a Boolean of a query
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>Whether statement is true or false</returns>
        public bool GetBoolean(GremlinScript Script)
        {
            return Client.ExecuteScalar<bool>(Script.GetScript(), Script.GetBindings());
        }
        /// <summary>
        /// async for GetBoolean
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>Task that returns whether statement is true or false</returns>
        public Task<bool> GetBooleanAsync(GremlinScript Script)
        {
            return Client.ExecuteScalarAsync<bool>(Script.GetScript(), Script.GetBindings());
        }
        #endregion

        #region GetString
        /// <summary>
        /// gets a string of a query, e.g. a value from a index
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>Wanted string from query</returns>
        public string GetString(GremlinScript Script)
        {
            return Client.ExecuteScalar<string>(Script.GetScript(), Script.GetBindings());
        }
        /// <summary>
        /// async for GetString
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>Task that returns wanted string from query</returns>
        public Task<string> GetStringAsync(GremlinScript Script)
        {
            return Client.ExecuteScalarAsync<string>(Script.GetScript(), Script.GetBindings());
        }
        #endregion

        #region GetArray
        /// <summary>
        /// gets an array of objects from database, e.g. list of objects from a index
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>An array/list of objects</returns>
        public List<object> GetArray(GremlinScript Script)
        {
            return Client.Execute<object>(Script.GetScript(), Script.GetBindings());
        }
        /// <summary>
        /// async for GetArray
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>Task that returns an array/list of objects</returns>
        public Task<List<object>> GetArrayAsync(GremlinScript Script)
        {
            return Client.ExecuteAsync<object>(Script.GetScript(), Script.GetBindings());
        }
        /// <summary>
        /// gets an array of generic values from database, e.g. list of strings from a index
        /// </summary>
        /// <typeparam name="ValueType">ValueType in array or list</typeparam>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>An array/list of specific ValueTyp</returns>
        public List<ValueType> GetArray<ValueType>(GremlinScript Script)
        {
            return Client.Execute<ValueType>(Script.GetScript(), Script.GetBindings());
        }
        /// <summary>
        /// async for GetArray
        /// </summary>
        /// <typeparam name="ValueType">ValueType in array or list</typeparam>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>Task that returns an array/list of specific ValueTyp</returns>
        public Task<List<ValueType>> GetArrayAsync<ValueType>(GremlinScript Script)
        {
            return Client.ExecuteAsync<ValueType>(Script.GetScript(), Script.GetBindings());
        }
        #endregion

        #region Scalar
        /// <summary>
        /// gets an scalar from database
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>Wanted scalar-object</returns>
        public object GetScalar(GremlinScript Script)
        {
            return Client.ExecuteScalar<object>(Script.GetScript(), Script.GetBindings());
        }
        /// <summary>
        /// async for GetScalar
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>Task that returns wanted scalar-object</returns>
        public Task<object> GetScalarAsync(GremlinScript Script)
        {
            return Client.ExecuteScalarAsync<object>(Script.GetScript(), Script.GetBindings());
        }
        /// <summary>
        /// gets a generic scalar from database
        /// </summary>
        /// <typeparam name="ValueType">ValueType to return</typeparam>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>Wanted scalar</returns>
        public ValueType GetScalar<ValueType>(GremlinScript Script)
        {
            return Client.ExecuteScalar<ValueType>(Script.GetScript(), Script.GetBindings());
        }
        /// <summary>
        /// async for GetScalar
        /// </summary>
        /// <typeparam name="ValueType">ValueType to return</typeparam>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>Task that returns wanted scalar</returns>
        public Task<ValueType> GetScalarAsync<ValueType>(GremlinScript Script)
        {
            return Client.ExecuteScalarAsync<ValueType>(Script.GetScript(), Script.GetBindings());
        }
        #endregion

        #region GetDictionaryArray
        /// <summary>
        /// gets a List of a Dictionary from database, e.g. a valueMap of several vertices
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>List of dictionaries</returns>
        public List<Dictionary<string, object>> GetDictionaryArray(GremlinScript Script)
        {
            return Client.Execute<Dictionary<string, object>>(Script.GetScript(), Script.GetBindings());
        }
        /// <summary>
        /// async for GetDictionaryArray
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>Task that returns list of dictionaries</returns>
        public Task<List<Dictionary<string, object>>> GetDictionaryArrayAsync(GremlinScript Script)
        {
            return Client.ExecuteAsync<Dictionary<string, object>>(Script.GetScript(), Script.GetBindings());
        }
        #endregion

        #region Execute
        /// <summary>
        /// executes a statement, e.g. deleting of a vertex
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        public void Execute(GremlinScript Script)
        {
            Client.Execute<object>(Script.GetScript(), Script.GetBindings());
        }
        /// <summary>
        /// async for Execute
        /// </summary>
        /// <param name="Script">Query via GremlinScript</param>
        /// <returns>Task that sends the query</returns>
        public Task ExecuteAsync(GremlinScript Script)
        {
            return Client.ExecuteAsync<object>(Script.GetScript(), Script.GetBindings());
        }
        #endregion

        private GremlinServerClient Client { get; set; }
        /// <summary>
        /// Host-Name
        /// </summary>
        public string Host { get; private set; }
        /// <summary>
        /// Port of the application
        /// </summary>
        public int Port { get; private set; }
        /// <summary>
        /// deserialize a Json Array to Vertices
        /// </summary>
        /// <param name="Object">Json-object to parse</param>
        /// <returns>List of IVertex</returns>
        public List<GraphItems.IVertex> GetVerticesFromJArray(object Object)
        {
            if (Object == null)
                return null;
            var Array = (JArray)Object;
            return Array.ToObject<List<GraphItems.IVertex>>();
        }
        /// <summary>
        /// deserialize a Json Array to a dictionary
        /// </summary>
        /// <param name="Object">Json-object to deserialize</param>
        /// <returns>Parsed dictionary</returns>
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
        /// <summary>
        /// deserialize a Json Array to a list of dictionary
        /// </summary>
        /// <param name="Object">Json-object to parse</param>
        /// <returns>Parsed list of dictionaries</returns>
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
        /// <summary>
        /// deserializes a Json Object to a Vertex
        /// </summary>
        /// <param name="Object">Json-object to parse</param>
        /// <returns>Parsed vertex</returns>
        public GraphItems.IVertex GetVertexFromJObject(object Object)
        {
            return GetValueFromJObject<GraphItems.IVertex>(Object);
        }
        /// <summary>
        /// deserializes a Json Object to a dictionary
        /// </summary>
        /// <param name="Object">Json-object to parse</param>
        /// <returns>Parsed dictionary</returns>
        public Dictionary<string, object> GetDictionaryFromJObject(object Object)
        {
            return GetValueFromJObject<Dictionary<string, object>>(Object);
        }
        /// <summary>
        /// desrializes a Json Object to a generic value
        /// </summary>
        /// <typeparam name="T">Type in that should be parsed</typeparam>
        /// <param name="Object">Json-object to parse</param>
        /// <returns>Parsed T</returns>
        public T GetValueFromJObject<T>(object Object)
        {
            if (Object == null)
                return default(T);
            return ((JObject)Object).ToObject<T>();
        }
    }
}