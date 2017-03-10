using Newtonsoft.Json;
using System.Collections.Generic;

namespace Teva.Common.Data.Gremlin.Messages
{
    /// <summary>
    /// Class for the Result in the Response
    /// </summary>
    /// <typeparam name="DataType">Datatype to parse Data</typeparam>
    public class ScriptResponseResult<DataType>
    {
        /// <summary>
        /// Data which actually returned from the server
        /// </summary>
        [JsonProperty("data")]
        public List<DataType> Data { get; set; }

        /// <summary>
        /// Meta-data related to the response
        /// </summary>
        [JsonProperty("meta")]
        public object Meta { get; set; }
    }
}
