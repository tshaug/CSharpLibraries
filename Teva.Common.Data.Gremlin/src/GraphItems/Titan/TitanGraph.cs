using System;
using System.Collections.Generic;
using Teva.Common.Data.Gremlin.GraphItems;
using Newtonsoft.Json;
using System.Net.WebSockets;
using log4net;

namespace Teva.Common.Data.Gremlin.GraphItems
{
    /// <summary>
    /// Titan-Implementation of IGraph
    /// </summary>
    public class TitanGraph : GraphClass
    {
        #region Konstruktoren
        /// <summary>
        /// Initializes a new instance of TitanGraph with given client
        /// </summary>
        /// <param name="client">Client to communicate with</param>
        public TitanGraph()
        {
            base.Type = GraphType.Titan;
            base.localId = 1;

        }

        #endregion

        #region Edges/Vertex-Methods

        /// <summary>
        /// Sets a composite index (see http://s3.thinkaurelius.com/docs/titan/1.0.0/indexes.html#_composite_index ) 
        /// </summary>
        /// <param name="propertykey">Propertykey to index</param>
        public override void CreateIndexOnProperty(string propertykey, string label = null)
        {
            object indexPropertyKey = GremlinClient.GetScalar(new GremlinScript("mgmt = graph.openManagement(); mgmt.getPropertyKey('" + propertykey + "')"));
            if (indexPropertyKey == null)
            {
                GremlinClient.Execute(
                new GremlinScript("graph.tx().rollback(); mgmt = graph.openManagement(); name = mgmt.makePropertyKey('" + propertykey + "').dataType(String.class).make(); mgmt.buildIndex('" + propertykey + "', Vertex.class).addKey(name).buildCompositeIndex(); mgmt.commit();")
                );
            }
        }

        /// <summary>
        /// Deletes all vertices and edges of a Graph
        /// </summary>
        public override void DeleteExistingGraph()
        {
            GremlinClient.Execute(new GremlinScript("g.V().drop()"));
        }

        public override void CommitChanges()
        {
            this.GremlinClient.Execute(new GremlinScript("graph.tx().commit()"));
        }
        #endregion
    }
}
