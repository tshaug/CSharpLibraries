using Newtonsoft.Json;

namespace Teva.Common.Data.Gremlin.GraphItems.Titan
{
    public abstract class TitanGraphItem : IGraphItem
    {
        [JsonProperty("id")]
        public string ID { get; set; }
    }
}
