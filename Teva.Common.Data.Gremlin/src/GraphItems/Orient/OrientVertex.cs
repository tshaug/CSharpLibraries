using Newtonsoft.Json;

namespace Teva.Common.Data.Gremlin.GraphItems.Orient
{
    public class OrientVertex : OrientGraphItem, IVertex
    {
        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("properties")]
        public OrientVertexProperties Properties { get; set; }
        IVertexProperties IVertex.Properties { get { return Properties; } set { Properties = (OrientVertexProperties)value; } }
    }
}
