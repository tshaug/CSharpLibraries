using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teva.Common.Data.Gremlin.Messages
{
    public class ScriptRequestMessage : RequestMessage
    {
        public ScriptRequestMessage()
        {
            Operation = "eval";
        }

        [JsonProperty("args", Order = 3)]
        public ScriptRequestArguments Arguments { get; set; }
    }
}
