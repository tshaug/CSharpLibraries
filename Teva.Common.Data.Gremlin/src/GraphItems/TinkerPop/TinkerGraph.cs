
namespace Teva.Common.Data.Gremlin.GraphItems
{
    /// <summary>
    /// TinkerPop-Implementation of IGraph
    /// </summary>
    public class TinkerGraph : TitanGraph
    {

        /// <summary>
        /// Initializes a new instance of TinkerGraph
        /// </summary>
        /// <param name="client">Client that is in use</param>
        public TinkerGraph() : base()
        {
            Type = GraphType.TinkerPop;
        }
        
        /// <summary>
        /// TODO: Must be implemented
        /// </summary>
        /// <param name="propertykey">Propertykey to index</param>
        public new void CreateIndexOnProperty(string propertykey)
        {
            // do nothing
        }
    }
}
