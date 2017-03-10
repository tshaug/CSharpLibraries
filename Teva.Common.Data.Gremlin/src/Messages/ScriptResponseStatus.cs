using Newtonsoft.Json;

namespace Teva.Common.Data.Gremlin.Messages
{
    /// <summary>
    /// Class for Response Status of the Response
    /// </summary>
    public class ScriptResponseStatus
    {
        /// <summary>
        /// HTTP status code
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; set; }

        /// <summary>
        /// Protocol-level information
        /// </summary>
        [JsonProperty("attributes")]
        public object Attributes { get; set; }

        /// <summary>
        /// A message, usually a error message
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
