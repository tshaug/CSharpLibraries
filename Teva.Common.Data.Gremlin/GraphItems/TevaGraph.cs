using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.WebSockets;
using log4net;

namespace Teva.Common.Data.Gremlin.GraphItems
{
    public class TevaGraph
    {
        #region Fields/Properties
        public IGremlinClient Gremlin { get; set; }
        private static int localId;
        public List<Vertex> Vertices { get; set; }

        public List<Edge> Edges { get; set; }
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Konstruktoren
        /// <summary>
        /// Erzeugt leeren graph
        /// </summary>
        public TevaGraph(IGremlinClient client)
        {
            Vertices = new List<Vertex>();
            Edges = new List<Edge>();
            Gremlin = client;
            localId = 1;
        }
        #endregion

        #region Edges/Vertex-Methods
        /// <summary>
        /// Erstellt eine gerichtete Kante und speichert sie in die DB
        /// </summary>
        /// <param name="InVertex">eingehende Kante</param>
        /// <param name="OutVertex">ausgehende Kante</param>
        public Edge AddDirectedEdge(string label, Vertex OutVertex, Vertex InVertex, EdgeProperties Properties = null)
        {
            Edge tmpEdge = new Edge();
            if (label == "Vertex" || label == "Edge")
            {
                label = label + "Object";
            }
            tmpEdge.Label = label;
            tmpEdge.id = new GraphItemId();
            tmpEdge.id.saveId(localId.ToString());
            localId++;
            tmpEdge.InVertexLabel = InVertex.Label;
            tmpEdge.OutVertexLabel = OutVertex.Label;
            tmpEdge.Properties = Properties;
            tmpEdge.InVertex = InVertex.ID;
            tmpEdge.OutVertex = OutVertex.ID;

            try
            {
                Edge createdEdge = Gremlin.CreateEdge(tmpEdge.OutVertex.ToString(), tmpEdge.InVertex.ToString(), label);
                logger.Info("Edge " + createdEdge.ID + "has been created.");
                tmpEdge = createdEdge;
            }
            //log4net
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
                Console.WriteLine("Unhandled Exception occured.\n{0}\n{1}", e.ToString(), e.StackTrace);
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
        public List<Edge> AddBiDirectedEdge(string label, Vertex vertex1, Vertex vertex2, EdgeProperties Properties = null)
        {
            List<Edge> biDirectedEdges = new List<Edge>();
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
        public Vertex AddVertex(string label, VertexProperties properties = null)
        {
            Vertex tmpVertex = new Vertex();
            if (label == "Vertex" || label == "Edge")
            {
                label = label + "Object";
            }
            tmpVertex.Label = label;
            tmpVertex.id = new GraphItemId();
            tmpVertex.id.saveId(localId.ToString());
            localId++;
            tmpVertex.Properties = properties;

            try
            {
                Vertex createdVert = Gremlin.CreateVertexAndLabel(label, properties);
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

            Vertices.Add(tmpVertex);
            return tmpVertex;
        }

        public object GetValueFromPropertie(string key, VertexProperties properties)
        {
            return properties.GetProperty(key);
        }
        #endregion
    }
}
