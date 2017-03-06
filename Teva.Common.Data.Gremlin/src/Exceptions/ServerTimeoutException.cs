using System;

namespace Teva.Common.Data.Gremlin.Exceptions
{
    /// <summary>
    /// Exception-Class for Server Timeouts
    /// </summary>
    public class ServerTimeoutException : Exception
    {
        /// <summary>
        /// Initializes a new instance of ServerTimeoutException with given message
        /// </summary>
        /// <param name="Message">Message to throw</param>
        public ServerTimeoutException(string Message)
            : base(Message)
        {
        }
    }
}
