using System;
using System.Collections.Generic;
using Teva.Common.Data.Gremlin.GraphItems.Titan;
using Newtonsoft.Json;
using System.Net.WebSockets;
using log4net;

namespace Teva.Common.Data.Gremlin.GraphItems
{
    public class TitanGraph : IGraph
    {
        #region Fields/Properties
        public IGremlinClient Gremlin { get; set; }
        public int localId { get; set; }
        public GraphType type { get; private set; }

        public List<IVertex> Vertices { get; private set; }

        public List<IEdge> Edges { get; private set; }
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Konstruktoren
        /// <summary>
        /// Erzeugt leeren graph
        /// </summary>
        public TitanGraph(IGremlinClient client)
        {
            Vertices = new List<IVertex>();
            Edges = new List<IEdge>();
            Gremlin = client;
            type = GraphType.Titan;
            localId = 1;
        }
        #endregion

        #region Edges/Vertex-Methods
        /// <summary>
        /// Erstellt eine gerichtete Kante und speichert sie in die DB
        /// </summary>
        /// <param name="InVertex">eingehende Kante</param>
        /// <param name="OutVertex">ausgehende Kante</param>
        public IEdge AddDirectedEdge(string label, IVertex OutVertex, IVertex InVertex, IEdgeProperties Properties = null)
        {
            IEdge tmpEdge = new TitanEdge();
            IVertex titanOutVertex = OutVertex;
            IVertex titanInVertex = InVertex;
            IEdgeProperties titanProperties = Properties;
            tmpEdge.Label = label;
            tmpEdge.ID = localId.ToString();
            localId++;
            tmpEdge.InVertexLabel = InVertex.Label;
            tmpEdge.OutVertexLabel = OutVertex.Label;
            tmpEdge.Properties = titanProperties;
            tmpEdge.InVertex = titanInVertex.ID;
            tmpEdge.OutVertex = titanOutVertex.ID;

            try
            {
                IEdge createdEdge = Gremlin.CreateEdge(tmpEdge.OutVertex, tmpEdge.InVertex, label, (Dictionary<string, object>)Properties);
                logger.Info("Edge " + createdEdge.ID + " has been created.");
                tmpEdge = createdEdge;
            }
            catch (WebSocketException we)
            {
                logger.Error("Can not connect to Server. " + we);
                throw;
            }
            catch(JsonReaderException jre)
            {
                logger.Error("Can not read JSON. " + jre.Data + jre.StackTrace);
                throw;
            }
            catch (Exception e)
            {
                logger.Error("Unhandled Exception occured. " + e);
                throw;
            }
            Edges.Add(tmpEdge);
            return tmpEdge;
        }

        /// <summary>
        /// Erstellt eine bidirektionale Kante zwischen zwei Knoten
        /// </summary>
        /// <param name="label"></param>
        /// <param name="vertex1"></param>
        /// <param name="vertex2"></param>
        /// <returns></returns>
        public List<IEdge> AddBiDirectedEdge(string label, IVertex vertex1, IVertex vertex2, IEdgeProperties Properties = null)
        {
            List<IEdge> biDirectedEdges = new List<IEdge>();
            biDirectedEdges.Add(AddDirectedEdge(label, vertex1, vertex2, Properties));
            biDirectedEdges.Add(AddDirectedEdge(label, vertex2, vertex1, Properties));
            return biDirectedEdges;
        }


        /// <summary>
        /// Fügt einen Knoten hinzu und speichert in DB
        /// </summary>
        /// <param name="label">Label als string</param>
        /// <param name="keyValuePairs">Dictonary als Property</param>
        /// <returns></returns>
        public IVertex AddVertex(string label, IVertexProperties properties = null)
        {
            IVertex tmpVertex = new TitanVertex();
            tmpVertex.Label = label;
            tmpVertex.ID = localId.ToString();
            localId++;
            tmpVertex.Properties = properties;

            try
            {
                IVertex createdVert = Gremlin.CreateVertexAndLabel(label, (Dictionary<string, List<IVertexValue>>)properties);
                logger.Info("Vertex " + createdVert.ID + " has been created.");
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

            Vertices.Add(tmpVertex);
            return tmpVertex;
        }

        public object GetValueFromProperty(string key, IVertexProperties properties)
        {
            return properties.GetProperty(key);
        }
        #endregion        
    }
}
