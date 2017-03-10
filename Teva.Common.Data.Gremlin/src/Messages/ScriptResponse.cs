using Newtonsoft.Json;
using System;

namespace Teva.Common.Data.Gremlin.Messages
{
    /// <summary>
    /// Class for Script Response
    /// </summary>
    /// <typeparam name="DataType">Data Type for parsing purposes</typeparam>
    public class ScriptResponse<DataType>
    {
        /// <summary>
        /// Result-Data of the Request
        /// </summary>
        [JsonProperty("result")]
        public ScriptResponseResult<DataType> Result { get; set; }

        /// <summary>
        /// Identifier of RequestMessage
        /// </summary>
        [JsonProperty("requestId")]
        public Guid? RequestID { get; set; }

        /// <summary>
        /// Status-Map of Response
        /// </summary>
        [JsonProperty("status")]
        public ScriptResponseStatus Status { get; set; }
    }
}
