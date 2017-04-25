using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teva.Common.Data.Gremlin.GraphItems
{
    public class JanusGraph : TitanGraph
    {

        public JanusGraph() : base()
        {
            base.Type = GraphType.JanusGraph;
            base.localId = 1;
        }
    
    }
}
