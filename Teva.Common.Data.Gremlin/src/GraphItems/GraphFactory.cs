using System;

namespace Teva.Common.Data.Gremlin.GraphItems
{
    /// <summary>
    /// Graph Type of IGraph
    /// </summary>
    public enum GraphType
    {
        /// <summary>
        /// Database is OrientDB
        /// </summary>
        OrientDB,
        /// <summary>
        /// Database is Titan
        /// </summary>
        Titan,
        /// <summary>
        /// Database is TinkerPop
        /// </summary>
        TinkerPop,
        /// <summary>
        /// Database is JanusGraph
        /// </summary>
        JanusGraph
    };
    public class GraphFactory
    {

        public static IGraph CreateGraph(GraphType type)
        {
            IGraph graph;
            switch (type)
            {
                case GraphType.OrientDB:
                    graph = new OrientGraph(); break;
                case GraphType.Titan:
                    graph = new TitanGraph(); break;
                case GraphType.JanusGraph:
                    graph = new JanusGraph(); break;
                case GraphType.TinkerPop:
                    graph = new TinkerGraph(); break;
                default:
                    throw new Exceptions.NotSupportedDatabaseException("Not supported GraphDatabase.");
            }
            return graph;
        }            
       
    }
}
