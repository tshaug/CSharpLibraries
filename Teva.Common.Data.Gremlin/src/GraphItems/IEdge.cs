using Newtonsoft.Json;
using Teva.Common.Data.Gremlin.GraphItems.GraphItemId;
using Teva.Common.Data.Gremlin.Impl;

namespace Teva.Common.Data.Gremlin.GraphItems
{
    /// <summary>
    /// Interface for Edges, is a derivation of IGraphItem
    /// </summary>
    [JsonConverter(typeof(JsonGraphItemConverter))]
    public interface IEdge : IGraphItem
    {
        /// <summary>
        /// ID of the ingoing vertex
        /// </summary>
        ItemId InVertex { get; set; }
        /// <summary>
        /// Label of the ingoing vertex
        /// </summary>
        string InVertexLabel { get; set; }
        /// <summary>
        /// ID of the outgoing vertex
        /// </summary>
        ItemId OutVertex { get; set; }
        /// <summary>
        /// Label of the outgoing vertex
        /// </summary>
        string OutVertexLabel { get; set; }
        /// <summary>
        /// Label of the edge
        /// </summary>
        string Label { get; set; }
        /// <summary>
        /// Type of the edge (database sets mostly "edge"
        /// </summary>
        string Type { get; set; }
        /// <summary>
        /// Properties of the edge
        /// </summary>
        IEdgeProperties Properties { get; set; }
    }
}