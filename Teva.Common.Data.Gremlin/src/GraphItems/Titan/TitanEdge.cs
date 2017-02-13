using Newtonsoft.Json;

namespace Teva.Common.Data.Gremlin.GraphItems.Titan
{
    public class TitanEdge : TitanGraphItem, IEdge
    {
        public TitanEdge()
        {
        }
        [JsonProperty("inV")]
        public string InVertex { get; set; }

        [JsonProperty("inVLabel")]
        public string InVertexLabel { get; set; }

        [JsonProperty("outV")]
        public string OutVertex { get; set; }

        [JsonProperty("outVLabel")]
        public string OutVertexLabel { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("properties")]
        public TitanEdgeProperties Properties { get; set; }
        IEdgeProperties IEdge.Properties { get; set; }
    }
}