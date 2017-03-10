using System;

namespace Teva.Common.Data.Gremlin.Exceptions
{
    /// <summary>
    /// Exception-Class for Unauthorized Requests
    /// </summary>
    public class UnauthorizedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of UnauthorizedException with given message
        /// </summary>
        /// <param name="Message">Message to throw</param>
        public UnauthorizedException(string Message)
            : base(Message)
        {
        }
    }
}
