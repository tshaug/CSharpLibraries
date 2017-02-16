using Newtonsoft.Json;

namespace Teva.Common.Data.Gremlin.GraphItems.Orient
{
    public class OrientEdge : OrientGraphItem, IEdge
    {
        public OrientEdge()
        {
        }
        [JsonProperty("inV")]
        private OrientGraphItemId inVertex { get; set; }
        public string InVertex
        {
            get { return inVertex.ToString(); }
            set
            {
                inVertex = new OrientGraphItemId();
                inVertex.saveId(value);
            }
        }

        [JsonProperty("inVLabel")]
        public string InVertexLabel { get; set; }

        [JsonProperty("outV")]
        public OrientGraphItemId outVertex { get; set; }
        public string OutVertex
        {
            get { return outVertex.ToString(); }
            set
            {
                outVertex = new OrientGraphItemId();
                outVertex.saveId(value);
            }
        }

        [JsonProperty("outVLabel")]
        public string OutVertexLabel { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("properties")]
        public OrientEdgeProperties Properties { get; set; }
        IEdgeProperties IEdge.Properties { get; set; }
    }
}