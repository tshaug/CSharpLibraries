using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teva.Common.Data.Gremlin.GraphItems
{
    public class Edge : GraphItem
    {
        public Edge()
        {
            InVertexObject = new GraphItemId();
            OutVertexObject = new GraphItemId();
        }
        [JsonProperty("inV")]
        public GraphItemId InVertexObject { get; set; }
        public string InVertex
        {
            get { return InVertexObject.ToString(); }
            set
            {
                InVertexObject.saveId(value);
            }
        }

        [JsonProperty("inVLabel")]
        public string InVertexLabel { get; set; }

        [JsonProperty("outV")]
        public GraphItemId OutVertexObject { get; set; }
        public string OutVertex
        {
            get { return OutVertexObject.ToString(); }
            set
            {
                OutVertexObject.saveId(value);
            }
        }

        [JsonProperty("outVLabel")]
        public string OutVertexLabel { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("properties")]
        public EdgeProperties Properties { get; set; }
    }
}