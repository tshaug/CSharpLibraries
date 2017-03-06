using System;

namespace Teva.Common.Data.Gremlin.Exceptions
{
    /// <summary>
    /// Exception-Class for Script Evaluation Errors
    /// </summary>
    public class ScriptEvaluationErrorException : Exception
    {
        /// <summary>
        /// Initializes a new instance of ScriptEvaluationErrorException with given message
        /// </summary>
        /// <param name="Message">Message to throw</param>
        public ScriptEvaluationErrorException(string Message)
            : base(Message)
        {
        }
    }
}
