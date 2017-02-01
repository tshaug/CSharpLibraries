using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Teva.Common.Data.Gremlin.GraphItems
{
    public class GraphItemId
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
            if (ids.Length == 2)
            {
                clusterId = ids[0];
                clusterPosition = ids[1];
            }
            else
            {
                clusterId = ids[0];
                clusterPosition = "0";
            }
        }
    }
}
