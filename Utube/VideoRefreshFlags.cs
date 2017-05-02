using System;

namespace Utube
{
    /// <summary>
    /// Flags which instructs the <see cref="YoutubeVideo.Refresh(VideoRefreshFlags)"/> method and 
    /// the <see cref="YoutubePlaylist.Refresh(PlaylistRefreshFlags, VideoRefreshFlags)"/> method how
    /// to refresh a <see cref="YoutubeVideo"/> object.
    /// </summary>
    [Flags]
    public enum VideoRefreshFlags
    {
        /// <summary>
        /// Does not refresh anything.
        /// </summary>
        None = 0,

        /// <summary>
        /// Refreshes the title of the <see cref="YoutubeVideo"/>.
        /// That is it updates the <see cref="YoutubeVideo.Title"/> property.
        /// </summary>
        Title = 2,

        /// <summary>
        /// Refreshes the description of the <see cref="YoutubeVideo"/>.
        /// That is it updates the <see cref="YoutubeVideo.Description"/> property.
        /// </summary>
        Description = 4,

        /// <summary>
        /// Refreshes the view count of the <see cref="YoutubeVideo"/>.
        /// That is it updates the <see cref="YoutubeVideo.Views"/> property.
        /// </summary>
        ViewCount = 8,

        /// <summary>
        /// Refreshes the length/duration of the <see cref="YoutubeVideo"/>.
        /// That is it updates the <see cref="YoutubeVideo.Length"/> property.
        /// </summary>
        Length = 16,

        /// <summary>
        /// Refreshes the qualities/formats available of the <see cref="YoutubeVideo"/>.
        /// That is it updates the <see cref="YoutubeVideo.FormatsAvailable"/> property.
        /// </summary>
        Formats = 32,

        /// <summary>
        /// Refreshes the playlist which the <see cref="YoutubeVideo"/> is in, if
        /// <see cref="YoutubeVideo.Playlist"/> is not null.
        /// That is it updates the <see cref="YoutubeVideo.Playlist"/> property.
        /// </summary>
        Playlist = 64,

        /// <summary>
        /// Refreshes the whole <see cref="YoutubeVideo"/> object.
        /// </summary>
        All = Title | Description | ViewCount | Length | Formats | Playlist
    };
}
