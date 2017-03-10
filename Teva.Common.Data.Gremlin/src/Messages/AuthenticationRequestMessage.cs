using Newtonsoft.Json;

namespace Teva.Common.Data.Gremlin.Messages
{
    /// <summary>
    /// Class for the AuthentificationRequest Message 
    /// </summary>
    public class AuthenticationRequestMessage : RequestMessage
    {
        /// <summary>
        /// Initializes a new instance of AuthenticationRequestMessage
        /// </summary>
        public AuthenticationRequestMessage()
        {
            Operation = "authentication";
        }

        /// <summary>
        /// Arguments for Authentification
        /// </summary>
        [JsonProperty("args", Order = 3)]
        public AuthenticationRequestArguments Arguments { get; set; }
    }
}
