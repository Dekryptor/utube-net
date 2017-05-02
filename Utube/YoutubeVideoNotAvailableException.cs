using System;

namespace Utube
{
    /// <summary>
    /// Exception thrown when a <see cref="YoutubeVideo"/> is unavailable.
    /// </summary>
    public class YoutubeVideoNotAvailableException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="YoutubeVideoNotAvailableException"/> class.
        /// </summary>
        public YoutubeVideoNotAvailableException() : base()
        {
            // Space
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YoutubeVideoNotAvailableException"/> class
        /// with the <see cref="YoutubeVideo"/> that was unavailable.
        /// </summary>
        /// <param name="video"><see cref="YoutubeVideo"/> that was unavailable.</param>
        public YoutubeVideoNotAvailableException(YoutubeVideo video) : base()
        {
            _video = video;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YoutubeVideoNotAvailableException"/> class
        /// with specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public YoutubeVideoNotAvailableException(string message) : base(message)
        {
            // Space
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YoutubeVideoNotAvailableException"/> class
        /// with specified error message.
        /// </summary>
        /// <param name="video"><see cref="YoutubeVideo"/> that was unavailable.</param>
        /// <param name="message">The message that describes the error.</param>
        public YoutubeVideoNotAvailableException(YoutubeVideo video, string message) : base(message)
        {
            _video = video;
        }

        private readonly YoutubeVideo _video;

        /// <summary>
        /// Gets the <see cref="YoutubeVideo"/> that is unavailable.
        /// </summary>
        public YoutubeVideo Video => _video;
    }
}
