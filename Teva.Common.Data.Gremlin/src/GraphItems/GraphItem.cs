using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teva.Common.Data.Gremlin.GraphItems
{
    public abstract class GraphItem
    {
        [JsonProperty("id")]
        public GraphItemId id { get; set; }

        public string ID
        {
            get { return id.ToString(); }
            set
            {
                id = new GraphItemId();
                id.saveId(value);
            }
        }
    }
}
