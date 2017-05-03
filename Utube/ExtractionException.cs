using System;

namespace Utube
{
    /// <summary>
    ///     Exception thrown when there is an error when extracting data.
    /// </summary>
    public class ExtractionException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ExtractionException"/> class.
        /// </summary>
        public ExtractionException() : base()
        {
            // Space
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExtractionException"/> class with
        ///     the specified error message.
        /// </summary>
        /// <param name="message">
        ///     The message that describes the error.
        /// </param>
        public ExtractionException(string message) : base(message)
        {
            // Space
        }
    }
}
