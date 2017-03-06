using System;

namespace Teva.Common.Data.Gremlin.Exceptions
{
    /// <summary>
    /// Exception-Class for not supported Databases
    /// </summary>
    public class NotSupportedDatabaseException : Exception
    {
        /// <summary>
        /// Initializes a new instance of NotSupportedDatabaseException with given message
        /// </summary>
        /// <param name="message">Message to throw</param>
        public NotSupportedDatabaseException(string message)
            : base(message)
        {

        }
    }
}
