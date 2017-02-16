using Newtonsoft.Json;
using Teva.Common.Data.Gremlin.Impl;

namespace Teva.Common.Data.Gremlin.GraphItems
{
    [JsonConverter(typeof(JsonGraphItemConverter))]
    public interface IVertex : IGraphItem
    {
        string Label { get; set; }
        string Type { get; set; }
        IVertexProperties Properties { get; set; }
    }
}
