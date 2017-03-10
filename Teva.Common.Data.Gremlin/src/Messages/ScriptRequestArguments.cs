using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Teva.Common.Data.Gremlin.Messages
{
    /// <summary>
    /// Arguments of a eval operation
    /// </summary>
    public class ScriptRequestArguments
    {
        /// <summary>
        /// Initializes a new instance of ScriptRequestArguments
        /// </summary>
        public ScriptRequestArguments()
        {
        }

        /// <summary>
        /// Initializes a new instance of ScriptRequestArguments with given gremlin-query, Bindings and Session (if Session is open)
        /// </summary>
        /// <param name="Gremlin">Gremlin-Query</param>
        /// <param name="Bindings">Bindings of Query</param>
        /// <param name="Session">Potentially open Session</param>
        public ScriptRequestArguments(string Gremlin, Dictionary<string, object> Bindings, Guid? Session)
        {
            this.Gremlin = Gremlin;
            this.Bindings = Bindings;
            this.Session = Session;
        }

        /// <summary>
        /// Gremlin-Query to evaluate
        /// </summary>
        [JsonProperty("gremlin")]
        public string Gremlin { get; set; }

        /// <summary>
        /// Current Session
        /// </summary>
        [JsonProperty("session", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? Session { get; set; }

        /// <summary>
        /// Map of key/value pairs to apply as variables in the context of the Gremlin-Query
        /// </summary>
        [JsonProperty("bindings")]
        public Dictionary<string, object> Bindings { get; set; }

        /// <summary>
        /// Language used, e.g. grmelin-groovy
        /// </summary>
        [JsonProperty("language")]
        public string Language { get; set; } = "gremlin-groovy";
    }
}
