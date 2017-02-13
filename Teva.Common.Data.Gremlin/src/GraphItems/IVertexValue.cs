using Newtonsoft.Json;
using Teva.Common.Data.Gremlin.Impl;

namespace Teva.Common.Data.Gremlin.GraphItems
{
    [JsonConverter(typeof(JsonGraphItemConverter))]
    public interface IVertexValue : IGraphItem
    {
        object Contents { get; set; }
    }
}
