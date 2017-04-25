using System.Collections.Generic;

namespace Teva.Common.Data.Gremlin.GraphItems
{
    /// <summary>
    /// Orient-Implementation of IGraph
    /// </summary>
    public class OrientGraph : GraphClass
    {
       
        #region Konstruktoren
        /// <summary>
        /// Initializes a new instance of OrientGraph with given client
        /// </summary>
        public OrientGraph()
        {
            base.Type = GraphType.OrientDB;
            base.localId = 1;
           
        }
        #endregion

        #region Edges/Vertex-Methods
        public override void CreateIndexOnProperty(string propertykey, string label)
        {
            logger.Info(label);
            label = "V";
            List<string> set = GremlinClient.GetArray<string>(new GremlinScript("graph.getVertexIndexedKeys(\"" + label + "\")"));
            
            if (set == null)
            {
                GremlinClient.Execute(new GremlinScript("def config = new BaseConfiguration(); graph.prepareIndexConfiguration(config); graph.createVertexIndex(\"" + propertykey + "\", \"" + label + "\", config)"));
                logger.Info("Create index on Property: prop: " + propertykey + " label: " + label);
            }
            if (set.Contains(propertykey))
            {
                logger.Info("Tryed to create index on Property: prop: " + propertykey + " label: " + label);
            }
            else
            {
                GremlinClient.Execute(new GremlinScript("def config = new BaseConfiguration(); graph.prepareIndexConfiguration(config); graph.createVertexIndex(\"" + propertykey + "\", \"" + label + "\", config)"));
                logger.Info("Create index on Property: prop: " + propertykey + " label: " + label);
            }
        }


        /// <summary>
        /// Deletes all vertices and edges of a Graph
        /// </summary>
        public override void DeleteExistingGraph()
        {
            GremlinClient.Execute(new GremlinScript("graph.executeSql(\"delete vertex V\")"));
        }
        #endregion
    }
}
