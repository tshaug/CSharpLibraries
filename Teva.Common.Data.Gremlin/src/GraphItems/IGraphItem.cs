using Newtonsoft.Json;
using Teva.Common.Data.Gremlin.Impl;

namespace Teva.Common.Data.Gremlin.GraphItems
{
    /// <summary>
    /// "Abstract" interface for GraphItems
    /// </summary>
    [JsonConverter(typeof(JsonGraphItemConverter))]
    public interface IGraphItem
    {
        /// <summary>
        /// Every GraphItem has an ID
        /// </summary>
        string ID { get; set; }
    }
}
