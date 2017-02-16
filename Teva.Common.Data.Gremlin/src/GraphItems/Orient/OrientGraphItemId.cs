using Newtonsoft.Json;

namespace Teva.Common.Data.Gremlin.GraphItems.Orient
{
    public class OrientGraphItemId
    {
        [JsonProperty("clusterId")]
        public string clusterId { get; set; }

        [JsonProperty("clusterPosition")]
        public string clusterPosition { get; set; }

        public override string ToString()
        {
            return clusterId + ":" + clusterPosition;
        }

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
