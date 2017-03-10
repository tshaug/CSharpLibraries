using Newtonsoft.Json;

namespace Teva.Common.Data.Gremlin.GraphItems.Orient
{
    /// <summary>
    /// Orient-Implementation of IVertexValue, derives from OrientGraphItem
    /// </summary>
    public class OrientVertexValue : OrientGraphItem, IVertexValue
    {
        /// <summary>
        /// Initializes a new instance of OrientVertexValue
        /// </summary>
        public OrientVertexValue()
        {
        }
        /// <summary>
        /// Initializes a new instance of OrientVertexValue and sets given Contents
        /// </summary>
        /// <param name="Contents">Contents to set</param>
        public OrientVertexValue(object Contents)
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
