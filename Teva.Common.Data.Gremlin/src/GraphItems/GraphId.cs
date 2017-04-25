using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teva.Common.Data.Gremlin.GraphItems
{
    
    public class GraphId 
    {

        GraphId(String id)
        {
            ID = id;
        }

        public string ID { get; set; }

        public int clusterId { get; set; }
        public int clusterPosition { get; set; }

        //public static implicit operator string(GraphId id)
        //{
        //    return id.ToString();
        //}

        public static implicit operator GraphId(string id)
        {
            return new GraphId(id);
        }

        public override string ToString()
        {
            if(ID != null)
            {
                return ID;
            }
            else
            {
                return (clusterId + ":" + clusterPosition);
            }
        }

    }
}
