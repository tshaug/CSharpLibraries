using Newtonsoft.Json;

namespace Teva.Common.Data.Gremlin.GraphItems.Titan
{
    /// <summary>
    /// Titan-Implementation of IVertex, derives from TitanGraphItem
    /// </summary>
    public class TitanVertex : TitanGraphItem, IVertex
    {
        /// <summary>
        /// Label of Vertex
        /// </summary>
        [JsonProperty("label")]
        public string Label { get; set; }
        /// <summary>
        /// Type of Vertex (database sets mostly "vertex")
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
        /// <summary>
        /// Properties of Vertex
        /// </summary>
        [JsonProperty("properties")]
        public TitanVertexProperties Properties { get; set; }
        IVertexProperties IVertex.Properties { get { return Properties; } set { Properties = (TitanVertexProperties)value; } }
    }
}
