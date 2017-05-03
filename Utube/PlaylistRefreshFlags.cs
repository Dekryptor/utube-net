using System;

namespace Utube
{
    /// <summary>
    ///     Flags which instructs the <see cref="YoutubePlaylist.Refresh(PlaylistRefreshFlags)"/> method how
    ///     to refresh the <see cref="YoutubePlaylist"/> object.
    /// </summary>
    [Flags]
    public enum PlaylistRefreshFlags
    {
        /// <summary>
        ///     Does not refresh anything.
        /// </summary>
        None = 0,

        /// <summary>
        ///     Refreshes the title of the <see cref="YoutubePlaylist"/>.
        ///     That is it updates the <see cref="YoutubePlaylist.Title"/> property.
        /// </summary>
        Title = 2,

        /// <summary>
        ///     Refreshes the view count of the <see cref="YoutubePlaylist"/>.
        ///     That is it updates the <see cref="YoutubePlaylist.Views"/> property.
        /// </summary>
        ViewCount = 4,

        /// <summary>
        ///     Refreshes the video playlist of the <see cref="YoutubePlaylist"/>.
        ///     That is it updates the list of videos in the <see cref="YoutubePlaylist"/>.
        /// </summary>
        Playlist = 8,

        /// <summary>
        ///     Refreshes the whole <see cref="YoutubePlaylist"/> object.
        /// </summary>
        All = Title | ViewCount | Playlist
    };
}
