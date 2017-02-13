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
        GraphType type { get; set; }

        List<IVertex> Vertices { get; set; }

        List<IEdge> Edges { get; set; }
        #endregion

        #region Edges/Vertex-Methods
        /// <summary>
        /// Erstellt eine gerichtete Kante und speichert sie in die DB
        /// </summary>
        /// <param name="InVertex">eingehende Kante</param>
        /// <param name="OutVertex">ausgehende Kante</param>
        IEdge AddDirectedEdge(string label, IVertex OutVertex, IVertex InVertex, IEdgeProperties Properties = null);

        /// <summary>
        /// Erstellt eine bidirektionale Kante zwischen zwei Knoten
        /// </summary>
        /// <param name="label"></param>
        /// <param name="vertex1"></param>
        /// <param name="vertex2"></param>
        /// <returns></returns>
        List<IEdge> AddBiDirectedEdge(string label, IVertex vertex1, IVertex vertex2, IEdgeProperties Properties = null);

        /// <summary>
        /// Fügt einen Knoten hinzu und speichert in DB
        /// </summary>
        /// <param name="label">Label als string</param>
        /// <param name="keyValuePairs">Dictonary als Property</param>
        /// <returns></returns>
        IVertex AddVertex(string label, IVertexProperties properties = null);

        object GetValueFromPropertie(string key, IVertexProperties properties);
        #endregion
    }
    public enum GraphType { Orient, Titan };
}