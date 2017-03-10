using System;

namespace Teva.Common.Data.Gremlin.Exceptions
{
    /// <summary>
    /// Exception-Class for Invalid Request Arguments
    /// </summary>
    public class InvalidRequestArgumentsException : Exception
    {
        /// <summary>
        /// Intializes a new instance of InvalidRequestArgumentsException with given message
        /// </summary>
        /// <param name="Message">Message to throw</param>
        public InvalidRequestArgumentsException(string Message)
            : base(Message)
        {
        }
    }
}
