using Newtonsoft.Json;

namespace Teva.Common.Data.Gremlin.GraphItems.Titan
{
    public class TitanVertexValue : TitanGraphItem, IVertexValue
    {
        public TitanVertexValue()
        {
        }

        public TitanVertexValue(object Contents)
        {
            this.Contents = Contents;
        }

        [JsonProperty("value")]
        public object Contents { get; set; }
    }
}
