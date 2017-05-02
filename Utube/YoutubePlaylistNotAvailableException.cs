using System;

namespace Utube
{
    /// <summary>
    /// Exception thrown when a <see cref="YoutubePlaylist"/> is unavailable.
    /// </summary>
    public class YoutubePlaylistNotAvailableException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="YoutubePlaylistNotAvailableException"/> class.
        /// </summary>
        public YoutubePlaylistNotAvailableException()
            : base()
        {
            // Space
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YoutubePlaylistNotAvailableException"/> class
        /// with the <see cref="YoutubePlaylist"/> that was unavailable.
        /// </summary>
        /// <param name="playlist"><see cref="YoutubePlaylist"/> that was unavailable.</param>
        public YoutubePlaylistNotAvailableException(YoutubePlaylist playlist)
            : base()
        {
            Playlist = playlist;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YoutubePlaylistNotAvailableException"/> class
        /// with specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public YoutubePlaylistNotAvailableException(string message)
            : base(message)
        {
            // Space
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YoutubePlaylistNotAvailableException"/> class
        /// with the <see cref="YoutubePlaylist"/> that was unavailable and the specified error message.
        /// </summary>
        /// <param name="playlist"><see cref="YoutubePlaylist"/> that was unavailable.</param>
        /// <param name="message">The message that describes the error.</param>
        public YoutubePlaylistNotAvailableException(YoutubePlaylist playlist, string message)
            : base(message)
        {
            Playlist = playlist;
        }

        /// <summary>
        /// Gets the <see cref="YoutubePlaylist"/> that is unavailable.
        /// That is the <see cref="YoutubePlaylist"/> that caused the exception.
        /// </summary>
        public YoutubePlaylist Playlist { get; private set; }
    }
}
