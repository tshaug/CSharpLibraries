using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Teva.Common.Data.Gremlin.GraphItems.GraphItemImpl;

namespace Teva.Common.Data.Gremlin.GraphItems
{
    public abstract class GraphClass : IGraph
    {

        protected static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region Fields/Properties
        protected int localId;
        public List<IEdge> Edges { get; set; }     

        public GraphType Type { get; protected set; }


        public List<IVertex> Vertices { get; set; }

        private IGremlinClient client;
        public IGremlinClient GremlinClient
        {
            get {
                return client;
            }
            set
            {
                if (value != null)
                    client = value;
            }
        }
        #endregion

        #region implemented methods

        /// <summary>
        /// Creates a Vertex with given label and properties
        /// </summary>
        /// <param name="label">Label of created vertex</param>
        /// <param name="properties">Properties of created vertex</param>
        /// <returns>Created IVertex</returns>
        public IVertex AddVertex(string label, IVertexProperties properties)
        {
            ++localId;
            try
            {
                IVertex vertex = GremlinClient.CreateVertexAndLabel(label, (Dictionary<string, List<IVertexValue>>)properties);
                logger.Debug("Vertex " + vertex.Label + " has been created.");
                return vertex;
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
        }

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
            ++localId;
            try
            {
                IEdge edge = GremlinClient.CreateEdge(OutVertex.ID, InVertex.ID, label, (Dictionary<string, object>)Properties);
                logger.Debug("Edge " + edge.Label + " has been created.");
                return edge;
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
                logger.Error("Unhandled Exception occured. " + e);
                throw;
            }           
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
        /// Add GremlinClient to Graph
        /// </summary>
         public IGraph AddClient(IGremlinClient client)
        {
            this.GremlinClient = client;
            return this;    
        }

        /// <summary>
        /// Commits all changes
        /// </summary>
        public virtual void CommitChanges() {}

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
        #region abstract methods
        
        /// <summary>
        /// OrientDB implementation for creating an Index on a Propertykey
        /// </summary>
        /// <param name="propertykey">Key to index</param>
        /// <param name="label">Label of Vertex</param>
        public abstract void CreateIndexOnProperty(string propertykey, string label);

        /// <summary>
        /// Deletes all vertices and edges of current graph
        /// </summary>
        public abstract void DeleteExistingGraph();
        #endregion

    }
}
