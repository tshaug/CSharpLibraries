using System;

namespace Teva.Common.Data.Gremlin.Exceptions
{
    /// <summary>
    /// Exception-Class for Malformed Requests
    /// </summary>
    public class MalformedRequestException : Exception
    {
        /// <summary>
        /// Initializes a new instance of MalformedRequestException with given message
        /// </summary>
        /// <param name="Message">Message to throw</param>
        public MalformedRequestException(string Message)
            : base(Message)
        {
        }
    }
}
