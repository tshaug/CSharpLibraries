using Newtonsoft.Json;
using Teva.Common.Data.Gremlin.Impl;

namespace Teva.Common.Data.Gremlin.GraphItems
{
    [JsonConverter(typeof(JsonGraphItemConverter))]
    public interface IEdge : IGraphItem
    {
        string InVertex { get; set; }

        string InVertexLabel { get; set; }

        string OutVertex { get; set; }

        string OutVertexLabel { get; set; }

        string Label { get; set; }

        string Type { get; set; }

        IEdgeProperties Properties { get; set; }
    }
}