using Newtonsoft.Json;

namespace Teva.Common.Data.Gremlin.GraphItems.Orient
{
    /// <summary>
    /// ID of OrientGraphItem
    /// </summary>
    public class OrientGraphItemId
    {
        /// <summary>
        /// clusterId of ID
        /// </summary>
        [JsonProperty("clusterId")]
        public string clusterId { get; set; }
        /// <summary>
        /// clusterPosition of ID
        /// </summary>
        [JsonProperty("clusterPosition")]
        public string clusterPosition { get; set; }
        /// <summary>
        /// Get the right representation of a GraphItemID
        /// </summary>
        /// <returns>Representation of a GraphItemID as string</returns>
        public override string ToString()
        {
            return clusterId + ":" + clusterPosition;
        }
        /// <summary>
        /// Helper-method to save an id
        /// </summary>
        /// <param name="rid">Id to save</param>
        public void saveId(string rid)
        {
            string[] ids = rid.Split(':');
            if (ids.Length < 2)
            {
                clusterId = ids[0];
                clusterPosition = "0";                
            }
            else
            {
                clusterId = ids[0];
                clusterPosition = ids[1];
            }
        }
    }
}
