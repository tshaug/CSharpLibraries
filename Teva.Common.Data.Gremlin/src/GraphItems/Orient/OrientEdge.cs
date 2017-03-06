using Newtonsoft.Json;

namespace Teva.Common.Data.Gremlin.GraphItems.Orient
{
    /// <summary>
    /// Orient-Implementation of IEdge, derives from OrienGraphItem
    /// </summary>
    public class OrientEdge : OrientGraphItem, IEdge
    {
        /// <summary>
        /// Intitializes a new instance of OrientEdge
        /// </summary>
        public OrientEdge()
        {
        }
        [JsonProperty("inV")]
        private OrientGraphItemId inVertex { get; set; }
        /// <summary>
        /// ID of the ingoing vertex
        /// </summary>
        public string InVertex
        {
            get { return inVertex.ToString(); }
            set
            {
                inVertex = new OrientGraphItemId();
                inVertex.saveId(value);
            }
        }
        /// <summary>
        /// Label of the ingoing vertex
        /// </summary>
        [JsonProperty("inVLabel")]
        public string InVertexLabel { get; set; }

        [JsonProperty("outV")]
        private OrientGraphItemId outVertex { get; set; }
        /// <summary>
        /// ID of the outgoing vertex
        /// </summary>
        public string OutVertex
        {
            get { return outVertex.ToString(); }
            set
            {
                outVertex = new OrientGraphItemId();
                outVertex.saveId(value);
            }
        }
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
        public OrientEdgeProperties Properties { get; set; }
        IEdgeProperties IEdge.Properties { get; set; }
    }
}