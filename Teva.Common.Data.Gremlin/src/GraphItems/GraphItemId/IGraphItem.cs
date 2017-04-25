using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teva.Common.Data.Gremlin.Impl;

namespace Teva.Common.Data.Gremlin.GraphItems.GraphItemId
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
        ItemId ID { get; set; }
    }
}
