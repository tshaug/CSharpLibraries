using System.Collections.Generic;

namespace Teva.Common.Data.Gremlin.GraphItems
{
    /// <summary>
    /// Interface for a local Graph implementation
    /// </summary>
    public interface IGraph
    {
        #region Fields/Properties
        /// <summary>
        /// Send Data via client
        /// </summary>
        IGremlinClient GremlinClient { get; set; }
       
        /// <summary>
        /// Type of Graph 
        /// </summary>
        GraphType Type { get; }
        /// <summary>
        /// Amount of vertices
        /// </summary>
        List<IVertex> Vertices { get; set; }
        /// <summary>
        /// Amount of edges
        /// </summary>
        List<IEdge> Edges { get; set; }


        #endregion

        #region Edges/Vertex-Methods
        /// <summary>
        /// Creates a directed Edge and saves to DB
        /// </summary>
        /// <param name="label">Label of created edge</param>
        /// <param name="InVertex">Ingoing vertex to connect</param>
        /// <param name="OutVertex">Outgoing vertex to connect</param>
        /// <param name="Properties">Properties of created edge</param>
        /// <returns>Created IEdge</returns>
        IEdge AddDirectedEdge(string label, IVertex OutVertex, IVertex InVertex, IEdgeProperties Properties = null);

        /// <summary>
        /// Creates a bidirected Edge and saves to DB
        /// </summary>
        /// <param name="label">Label of created edge</param>
        /// <param name="vertex1">A vertex to connect</param>
        /// <param name="vertex2">Another vertex to connect</param>
        /// <param name="Properties">Properties of created edge</param>
        /// <returns>Created bidirected edge as List of IEdge</returns>
        List<IEdge> AddBiDirectedEdge(string label, IVertex vertex1, IVertex vertex2, IEdgeProperties Properties = null);

        /// <summary>
        /// Creates a vertex with given label and properties 
        /// </summary>
        /// <param name="label">Label of created vertex</param>
        /// <param name="properties">Properties of created vertex</param>
        /// <returns>Created IVertex</returns>
        IVertex AddVertex(string label, IVertexProperties properties = null);

        /// <summary>
        /// Gets a value from propertie with a key
        /// </summary>
        /// <param name="key">key of the value</param>
        /// <param name="properties">properties, that contains value</param>
        /// <returns>wanted value</returns>
        object GetValueFromProperty(string key, IVertexProperties properties);

        /// <summary>
        /// Commits all changes of current transaction
        /// </summary>
        void CommitChanges();

        /// <summary>
        /// Creates an Index on a Propertykey
        /// </summary>
        /// <param name="propertykey">Propertykey to Index</param>
        void CreateIndexOnProperty(string propertykey, string label=null);

        /// <summary>
        /// Deletes all Vertices and Edges of a Graph
        /// </summary>
        void DeleteExistingGraph();
        #endregion

        #region GremlienClient

        IGraph AddClient(IGremlinClient client);

        #endregion
    }


}