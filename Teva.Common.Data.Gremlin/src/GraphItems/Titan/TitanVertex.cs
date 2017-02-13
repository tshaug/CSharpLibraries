using Newtonsoft.Json;

namespace Teva.Common.Data.Gremlin.GraphItems.Titan
{
    public class TitanVertex : TitanGraphItem, IVertex
    {
        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("properties")]
        public TitanVertexProperties Properties { get; set; }
        IVertexProperties IVertex.Properties { get { return Properties; } set { Properties = (TitanVertexProperties)value; } }
    }
}
