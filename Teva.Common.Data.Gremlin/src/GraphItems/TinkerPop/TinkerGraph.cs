
namespace Teva.Common.Data.Gremlin.GraphItems.TinkerPop
{
    /// <summary>
    /// TinkerPop-Implementation of IGraph
    /// </summary>
    public class TinkerGraph : TitanGraph
    {
        /// <summary>
        /// Type of Graph
        /// </summary>
        public new GraphType type { get; private set; }

        /// <summary>
        /// Initializes a new instance of TinkerGraph
        /// </summary>
        /// <param name="client">Client that is in use</param>
        public TinkerGraph(IGremlinClient client) : base(client)
        {
            type = GraphType.TinkerPop;
        }

        /// <summary>
        /// TODO: Must be implemented
        /// </summary>
        /// <param name="propertykey">Propertykey to index</param>
        public override void CreateIndexOnProperty(string propertykey)
        {
            // do nothing
        }
    }
}
