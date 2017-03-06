using Newtonsoft.Json;

namespace Teva.Common.Data.Gremlin.GraphItems.Titan
{
    /// <summary>
    /// Titan-Implementation of IEdge, derives from TitanGraphItem
    /// </summary>
    public class TitanEdge : TitanGraphItem, IEdge
    {
        /// <summary>
        /// Initializes a new instance of TitanEdge
        /// </summary>
        public TitanEdge()
        {
        }
        /// <summary>
        /// ID of the ingoing vertex
        /// </summary>
        [JsonProperty("inV")]
        public string InVertex { get; set; }
        /// <summary>
        /// Label of the ingoing vertex
        /// </summary>
        [JsonProperty("inVLabel")]
        public string InVertexLabel { get; set; }
        /// <summary>
        /// ID of the outgoing vertex
        /// </summary>
        [JsonProperty("outV")]
        public string OutVertex { get; set; }
        /// <summary>
        /// Label of the outgoing vertex
        /// </summary>
        [JsonProperty("outVLabel")]
        public string OutVertexLabel { get; set; }
        /// <summary>
        /// Label of the edge
        /// </summary>
        [JsonProperty("label")]
        public string Label { get; set; }
        /// <summary>
        /// Type of the edge (database sets mostly "edge"
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
        /// <summary>
        /// Properties of the edge
        /// </summary>
        [JsonProperty("properties")]
        public TitanEdgeProperties Properties { get; set; }
        IEdgeProperties IEdge.Properties { get; set; }
    }
}