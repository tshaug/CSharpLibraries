using Newtonsoft.Json;
using System;

namespace Teva.Common.Data.Gremlin.Messages
{
    /// <summary>
    /// Class for processing of outgoing Json-Message Requests
    /// </summary>
    public class RequestMessage
    {
        /// <summary>
        /// Initializes a new instance of RequestMessage
        /// </summary>
        public RequestMessage()
        {
            RequestID = Guid.NewGuid();
            Processor = "";
        }

        /// <summary>
        /// Unique identification for the request
        /// </summary>
        [JsonProperty("requestId", Order = 0)]
        public Guid RequestID { get; set; }

        /// <summary>
        /// Name of the operation to execute in Gremlin Server (could be "eval" or "authentication")
        /// </summary>
        [JsonProperty("op", Order = 1)]
        public string Operation { get; set; }

        /// <summary>
        /// Name of the processor to utilize (mostly empty string)
        /// </summary>
        [JsonProperty("processor", Order = 2)]
        public string Processor { get; set; }
    }
}
