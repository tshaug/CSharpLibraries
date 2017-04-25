using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teva.Common.Data.Gremlin.GraphItems.GraphItemId
{
    public class ItemId
    {
         
        private string ID;
        private bool isClustered = true;

        [JsonProperty("clusterId")]
        public string clusterId;
        [JsonProperty("clusterPosition")]
        public string clusterPosition;

        public ItemId() { }

        private ItemId(string id)
        {
            ID = id;
            isClustered = false;            
        }

        public static implicit operator ItemId(string id)
        {
            return new ItemId(id);
        }
        public static implicit operator ItemId(int id)
        {
            return new ItemId(id.ToString());
        }
        public static implicit operator ItemId(long id)
        {
            return new ItemId(id.ToString());
        }


        public static implicit operator string(ItemId id)
        {
            return id.ToString();
        }



        public override string ToString()
        {
            if (isClustered)
            {
                return clusterId + ":" + clusterPosition;
            }
            else
            {
                return ID;
            }
        }

        //public void saveId(string rid)
        //{
        //    string[] ids = rid.Split(':');
        //    if (ids.Length < 2)
        //    {
        //        clusterId = ids[0];
        //        clusterPosition = "0";
        //    }
        //    else
        //    {
        //        clusterId = ids[0];
        //        clusterPosition = ids[1];
        //    }
        //}
    }
}

