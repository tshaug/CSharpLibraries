using System;

namespace Teva.Common.Data.Gremlin.Exceptions
{
    /// <summary>
    /// Exception-Class for Server Serialization Errors
    /// </summary>
    public class ServerSerializationError : Exception
    {
        /// <summary>
        /// Initializes a new instance of ServerSerializationError with given message
        /// </summary>
        /// <param name="Message">Message to throw</param>
        public ServerSerializationError(string Message)
            : base(Message)
        {
        }
    }
}
