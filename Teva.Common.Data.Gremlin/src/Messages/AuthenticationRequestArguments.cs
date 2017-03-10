using Newtonsoft.Json;

namespace Teva.Common.Data.Gremlin.Messages
{
    /// <summary>
    /// Class for the Arguments of an Authentification Request
    /// </summary>
    public class AuthenticationRequestArguments
    {
        /// <summary>
        /// Initializes a new instance of AuthenticationRequestArguments
        /// </summary>
        public AuthenticationRequestArguments()
        {
        }

        /// <summary>
        /// Initializes a new instance of AuthenticationRequestArguments with given Username and Password
        /// </summary>
        /// <param name="Username">Username for Authentification</param>
        /// <param name="Password">Password for Authentification</param>
        public AuthenticationRequestArguments(string Username, string Password)
            : this()
        {
            this.SASL = "\0" + Username + "\0" + Password;
        }

        /// <summary>
        /// Response to the server authentification challenge
        /// </summary>
        [JsonProperty("sasl")]
        public string SASL { get; set; }
    }
}
