using Newtonsoft.Json;

namespace Teva.Common.Data.Gremlin.GraphItems.Orient
{
    /// <summary>
    /// Orient-Implementation of IVertex, derives from OrientGraphItem
    /// </summary>
    public class OrientVertex : OrientGraphItem, IVertex
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
        public OrientVertexProperties Properties { get; set; }
        IVertexProperties IVertex.Properties { get { return Properties; } set { Properties = (OrientVertexProperties)value; } }
    }
}
