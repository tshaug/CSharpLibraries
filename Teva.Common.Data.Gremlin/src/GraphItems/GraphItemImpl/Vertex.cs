using Newtonsoft.Json;
using System.Runtime.Serialization;
using Teva.Common.Data.Gremlin.GraphItems.GraphItemId;

namespace Teva.Common.Data.Gremlin.GraphItems.GraphItemImpl
{

    /// <summary>
    /// Titan-Implementation of IVertex, derives from TitanGraphItem
    /// </summary>
    [DataContract] 
    public class Vertex : IVertex
    {

        public Vertex() { }
        public Vertex(string label, int localId, IVertexProperties properties)
        {
            Label = label;
            ID = localId;
            Properties = properties;
        }

        [JsonProperty("id")]
        public ItemId ID { get; set; }
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
        public IVertexProperties Properties { get; set; }

        public override string ToString()
        {
            return "Vertex - ID: " + ID + ", Label: " + Label + ", Type: " + Type; 

        }

    }
}

