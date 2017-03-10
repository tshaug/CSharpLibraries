using Newtonsoft.Json;

namespace Teva.Common.Data.Gremlin.GraphItems.Titan
{
    /// <summary>
    /// Titan-Implementation of IGraphItem
    /// </summary>
    public abstract class TitanGraphItem : IGraphItem
    {
        /// <summary>
        /// Every GraphItem has an ID
        /// </summary>
        [JsonProperty("id")]
        public string ID { get; set; }
    }
}
