using Newtonsoft.Json;

namespace Teva.Common.Data.Gremlin.GraphItems.Orient
{
    public class OrientVertexValue : OrientGraphItem, IVertexValue
    {
        public OrientVertexValue()
        {
        }

        public OrientVertexValue(object Contents)
        {
            this.Contents = Contents;
        }

        [JsonProperty("value")]
        public object Contents { get; set; }
    }
}
