using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.WebSockets;
using log4net;

namespace Teva.Common.Data.Gremlin.GraphItems
{
    public interface IGraph
    {
        #region Fields/Properties
        IGremlinClient Gremlin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        GraphType type { get; }
        /// <summary>
        /// Amount of Nodes in the graph
        /// </summary>
        List<IVertex> Vertices { get; }
        /// <summary>
        /// Amount of Edges in the graph
        /// </summary>
        List<IEdge> Edges { get; }
        #endregion

        #region Edges/Vertex-Methods
        /// <summary>
        /// Creates a directed Edge and saves to DB and local graph
        /// </summary>
        /// <param name="label"></param>
        /// <param name="InVertex">Incoming Edge</param>
        /// <param name="OutVertex">Outgoing Edge</param>
        /// <param name="Properties"></param>
        IEdge AddDirectedEdge(string label, IVertex OutVertex, IVertex InVertex, IEdgeProperties Properties = null);

        /// <summary>
        /// Creates a bidirected Edge and saves to DB and local graph
        /// </summary>
        /// <param name="label"></param>
        /// <param name="vertex1"></param>
        /// <param name="vertex2"></param>
        /// <param name="Properties"></param>
        /// <returns></returns>
        List<IEdge> AddBiDirectedEdge(string label, IVertex vertex1, IVertex vertex2, IEdgeProperties Properties = null);

        /// <summary>
        /// Creates a Node 
        /// </summary>
        /// <param name="label">label as string</param>
        /// <param name="properties">Properties</param>
        /// <returns></returns>
        IVertex AddVertex(string label, IVertexProperties properties = null);

        /// <summary>
        /// Gets a value from propertie with a key
        /// </summary>
        /// <param name="key">key of the value</param>
        /// <param name="properties">properties, that contains value</param>
        /// <returns>wanted value</returns>
        object GetValueFromProperty(string key, IVertexProperties properties);
        #endregion
    }
    /// <summary>
    /// Graph Type of IGraph
    /// </summary>
    public enum GraphType
    {
        /// <summary>
        /// Database is OrientDB
        /// </summary>
        Orient,
        /// <summary>
        /// Database is Titan
        /// </summary>
        Titan
    };
}