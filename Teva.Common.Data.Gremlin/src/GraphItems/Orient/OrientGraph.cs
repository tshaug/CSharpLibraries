using System;
using System.Collections.Generic;
using Teva.Common.Data.Gremlin.GraphItems.Orient;
using Newtonsoft.Json;
using System.Net.WebSockets;
using log4net;

namespace Teva.Common.Data.Gremlin.GraphItems
{
    /// <summary>
    /// Orient-Implementation of IGraph
    /// </summary>
    public class OrientGraph : IGraph
    {
        #region Fields/Properties
        /// <summary>
        /// Send Data via client
        /// </summary>
        public IGremlinClient Gremlin { get; set; }
        private static int localId;
        /// <summary>
        /// Type of Graph 
        /// </summary>
        public GraphType type { get; private set; }
        /// <summary>
        /// Amount of vertices
        /// </summary>
        public List<IVertex> Vertices { get; set; }
        /// <summary>
        /// Amount of edges
        /// </summary>
        public List<IEdge> Edges { get; set; }
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Konstruktoren
        /// <summary>
        /// Initializes a new instance of OrientGraph with given client
        /// </summary>
        /// <param name="client">Client that is in use</param>
        public OrientGraph(IGremlinClient client)
        {
            Gremlin = client;
            type = GraphType.Orient;
            localId = 1;
        }
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
        public IEdge AddDirectedEdge(string label, IVertex OutVertex, IVertex InVertex, IEdgeProperties Properties = null)
        {
            IEdge tmpEdge = new OrientEdge();
            IVertex orientOutVertex = OutVertex;
            IVertex orientInVertex = InVertex;
            IEdgeProperties orientProperties = Properties;
            tmpEdge.Label = label;
            tmpEdge.ID = localId.ToString();
            localId++;
            tmpEdge.InVertexLabel = InVertex.Label;
            tmpEdge.OutVertexLabel = OutVertex.Label;
            tmpEdge.Properties = orientProperties;
            tmpEdge.InVertex = orientInVertex.ID;
            tmpEdge.OutVertex = orientOutVertex.ID;

            try
            {
                IEdge createdEdge = Gremlin.CreateEdge(tmpEdge.OutVertex.ToString(), tmpEdge.InVertex.ToString(), label);
                logger.Info("Edge " + createdEdge.ID + "has been created.");
                tmpEdge = createdEdge;
            }
            catch (WebSocketException we)
            {
                logger.Error("Can not connect to Server. " + we);
                throw;
            }
            catch (JsonReaderException jre)
            {
                logger.Error("Can not read JSON. " + jre.Data + jre.StackTrace);
                throw;
            }
            catch (Exception e)
            {
                logger.Error("Unhandled Exception occured: " + e);
                throw;
            }
            return tmpEdge;
        }

        /// <summary>
        /// Creates a bidirected Edge and saves to DB
        /// </summary>
        /// <param name="label">Label of created edge</param>
        /// <param name="vertex1">A vertex to connect</param>
        /// <param name="vertex2">Another vertex to connect</param>
        /// <param name="Properties">Properties of created edge</param>
        /// <returns>Created bidirected edge as List of IEdge</returns>
        public List<IEdge> AddBiDirectedEdge(string label, IVertex vertex1, IVertex vertex2, IEdgeProperties Properties = null)
        {
            List<IEdge> biDirectedEdges = new List<IEdge>();
            biDirectedEdges.Add(AddDirectedEdge(label, vertex1, vertex2, Properties));
            biDirectedEdges.Add(AddDirectedEdge(label, vertex2, vertex1, Properties));
            return biDirectedEdges;
        }


        /// <summary>
        /// Creates a Vertex with given label and properties
        /// </summary>
        /// <param name="label">Label of created vertex</param>
        /// <param name="properties">Properties of created vertex</param>
        /// <returns>Created IVertex</returns>
        public IVertex AddVertex(string label, IVertexProperties properties = null)
        {
            IVertex tmpVertex = new OrientVertex();
            IVertexProperties orientProperties = properties;
            tmpVertex.Label = label;
            tmpVertex.ID = localId.ToString();
            localId++;
            tmpVertex.Properties = orientProperties;

            try
            {
                IVertex createdVert = Gremlin.CreateVertexAndLabel(label, (Dictionary<string, List<IVertexValue>>)properties);
                logger.Info("Vertex " + createdVert.ID + "has been created.");
                tmpVertex = createdVert;
            }
            catch (WebSocketException we)
            {
                logger.Error("Can not connect to Server. " + we);
                throw;
            }
            catch (JsonReaderException jre)
            {
                logger.Error("Can not read JSON. " + jre.Data + jre.StackTrace);
                throw;
            }
            catch (Exception e)
            {
                logger.Error(e);
                throw;
            }
            return tmpVertex;
        }
        /// <summary>
        /// Gets a value from propertie with a key
        /// </summary>
        /// <param name="key">key of the value</param>
        /// <param name="properties">properties, that contains value</param>
        /// <returns>wanted value</returns>
        public object GetValueFromProperty(string key, IVertexProperties properties)
        {
            return properties.GetProperty(key);
        }
        #endregion
    }
}
