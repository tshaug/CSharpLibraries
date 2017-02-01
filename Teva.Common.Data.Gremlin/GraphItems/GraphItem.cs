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
        private string Id;
        [JsonProperty("id")]
        public GraphItemId id { get; set; }

        public string ID
        {
            get { return id.ToString(); }
            set { Id = value; }
        }
    }
}
