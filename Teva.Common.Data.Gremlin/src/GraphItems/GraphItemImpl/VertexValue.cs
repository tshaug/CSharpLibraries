using Newtonsoft.Json;
using Teva.Common.Data.Gremlin.GraphItems.GraphItemId;

namespace Teva.Common.Data.Gremlin.GraphItems.GraphItemImpl
{

    /// <summary>
    /// Orient-Implementation of IVertexValue, derives from OrientGraphItem
    /// </summary>
    public class VertexValue : IVertexValue
    {

        public ItemId ID { get; set; }
        /// <summary>
        /// Initializes a new instance of OrientVertexValue
        /// </summary>
        public VertexValue()
        {
        }
        /// <summary>
        /// Initializes a new instance of OrientVertexValue and sets given Contents
        /// </summary>
        /// <param name="Contents">Contents to set</param>
        public VertexValue(object Contents)
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
