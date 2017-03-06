using Newtonsoft.Json;

namespace Teva.Common.Data.Gremlin.GraphItems.Titan
{
    /// <summary>
    /// Titan-Implementation of IVertexValue, derives from TitanGraphItem
    /// </summary>
    public class TitanVertexValue : TitanGraphItem, IVertexValue
    {
        /// <summary>
        /// Initializes a new instance of TitanVertexValue
        /// </summary>
        public TitanVertexValue()
        {
        }
        /// <summary>
        /// Initializes a new instance of TitanVertexValue and sets given Contents
        /// </summary>
        /// <param name="Contents"></param>
        public TitanVertexValue(object Contents)
        {
            this.Contents = Contents;
        }
        /// <summary>
        /// Contents of Vertex Value
        /// </summary>
        [JsonProperty("value")]
        public object Contents { get; set; }
    }
}
