using System;

namespace Teva.Common.Data.Gremlin.Exceptions
{
    class NotSupportedDatabaseException : Exception
    {
        public NotSupportedDatabaseException(string message)
            : base(message)
        {

        }
    }
}
