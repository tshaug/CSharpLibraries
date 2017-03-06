using Newtonsoft.Json;

namespace Teva.Common.Data.Gremlin.Messages
{
    /// <summary>
    /// Class to structure the Request Message 
    /// </summary>
    public class ScriptRequestMessage : RequestMessage
    {
        /// <summary>
        /// Initializes a new instance of ScriptRequestMessage
        /// </summary>
        public ScriptRequestMessage()
        {
            Operation = "eval";
        }

        /// <summary>
        /// Arguments of the Request Messages
        /// </summary>
        [JsonProperty("args", Order = 3)]
        public ScriptRequestArguments Arguments { get; set; }
    }
}
