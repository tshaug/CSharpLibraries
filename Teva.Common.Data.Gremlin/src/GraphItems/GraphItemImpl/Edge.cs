using Newtonsoft.Json;
using Teva.Common.Data.Gremlin.GraphItems.GraphItemId;

namespace Teva.Common.Data.Gremlin.GraphItems.GraphItemImpl
{
    /// <summary>
    /// Titan-Implementation of IEdge, derives from TitanGraphItem
    /// </summary>
    public class Edge : IEdge
    {
        /// <summary>
        /// Initializes a new instance of TitanEdge
        /// </summary>
        public Edge()
        {
        }

        public Edge(string label, int localId, IVertex inVertex, IVertex outVertex, IEdgeProperties properties)
        {
            Label = label;
            ID = localId;
            InVertex = inVertex.ID;
            InVertexLabel = inVertex.Label;
            OutVertex = outVertex.ID;
            OutVertexLabel = outVertex.Label;
            Properties = properties;
        }

        [JsonProperty("id")]
        public ItemId ID { get; set; }

        /// <summary>
        /// ID of the ingoing vertex
        /// </summary>
        [JsonProperty("inV")]
        public ItemId InVertex { get; set; }
        /// <summary>
        /// Label of the ingoing vertex
        /// </summary>
        [JsonProperty("inVLabel")]
        public string InVertexLabel { get; set; }
        /// <summary>
        /// ID of the outgoing vertex
        /// </summary>
        [JsonProperty("outV")]
        public ItemId OutVertex { get; set; }
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
        public IEdgeProperties Properties { get; set; }

        public override string ToString()
        {
            return "Edge - ID: " + ID + ", InVertex: " + InVertex + ", OutVertex: " + OutVertex + ", label: " + Label;
        }
    }
}

