using Newtonsoft.Json;
using Teva.Common.Data.Gremlin.Impl;

namespace Teva.Common.Data.Gremlin.GraphItems
{
    /// <summary>
    /// Interface for Vertex, is a derivation of IGraphItem
    /// </summary>
    [JsonConverter(typeof(JsonGraphItemConverter))]
    public interface IVertex : IGraphItem
    {
        /// <summary>
        /// Label of Vertex
        /// </summary>
        string Label { get; set; }
        /// <summary>
        /// Type of Vertex (database sets mostly "vertex")
        /// </summary>
        string Type { get; set; }
        /// <summary>
        /// Properties of Vertex
        /// </summary>
        IVertexProperties Properties { get; set; }
    }
}
