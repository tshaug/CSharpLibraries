using System;

namespace Teva.Common.Data.Gremlin.Exceptions
{
    /// <summary>
    /// Exception-Class for Server Errors
    /// </summary>
    public class ServerErrorException : Exception
    {
        /// <summary>
        /// Initializes a new instance of ServerErrorException with given message
        /// </summary>
        /// <param name="Message">Message to throw</param>
        public ServerErrorException(string Message)
            : base(Message)
        {
        }
    }
}
