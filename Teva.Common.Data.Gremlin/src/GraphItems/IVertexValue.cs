using Newtonsoft.Json;
using Teva.Common.Data.Gremlin.Impl;

namespace Teva.Common.Data.Gremlin.GraphItems
{
    /// <summary>
    /// Interface for VertexValues, implementation must contain Contents, is derivation of IGraphItem
    /// </summary>
    [JsonConverter(typeof(JsonGraphItemConverter))]
    public interface IVertexValue : IGraphItem
    {
        /// <summary>
        /// Contents of Vertex Value
        /// </summary>
        object Contents { get; set; }
    }
}
