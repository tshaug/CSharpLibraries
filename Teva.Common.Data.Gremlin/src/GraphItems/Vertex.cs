using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teva.Common.Data.Gremlin.GraphItems.Orient;

namespace Teva.Common.Data.Gremlin.GraphItems
{
    class Vertex : IVertex
    {

        [JsonProperty("id")]
        public OrientGraphItemId id
        {
            get;
            set;
        }

        public string ID
        {
            get { return id.ToString(); }
            set
            {
                id = new OrientGraphItemId();
                id.saveId(value);
            }
        }


        public string Label { get; set; }

        public string Type { get; set; }



        public OrientVertexProperties Properties { get; set; }

        IVertexProperties IVertex.Properties
        {
            get
            {
                return Properties;
            }
            set
            {
                Properties = (OrientVertexProperties)value;
            }
        }

    }
}
