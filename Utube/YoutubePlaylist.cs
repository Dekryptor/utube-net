using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Utube.Extractors;

namespace Utube
{
    /// <summary>
    /// Represents a Youtube playlist.
    /// </summary>
    public class YoutubePlaylist : IReadOnlyCollection<YoutubeVideo>
    {
        #region Constants
        /// <summary>
        ///     Base URL of Youtube playlists.
        /// </summary>
        public static readonly Uri BaseUrl = new Uri("https://www.youtube.com/playlist?list=");
        #endregion

        #region Constructors
        /// <summary>
        ///     Initializes a new instance of the <see cref="YoutubePlaylist"/> class.
        /// </summary>
        public YoutubePlaylist()
        {
            // Space
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="YoutubePlaylist"/> class with the specified
        ///     Youtube playlist ID.
        /// </summary>
        /// 
        /// <param name="playlistId">
        ///     Youtube playlist ID.
        /// </param>
        public YoutubePlaylist(string playlistId) : this(playlistId, PlaylistRefreshFlags.All)
        {
            // Space
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="YoutubePlaylist"/> class with the specified
        ///     Youtube playlist ID and the specified <see cref="PlaylistRefreshFlags"/> which will be passed to
        ///     the <see cref="Refresh(PlaylistRefreshFlags, VideoRefreshFlags)"/> with <see cref="VideoRefreshFlags.None"/>
        ///     method when the <see cref="YoutubePlaylist"/> object is done initializing.
        /// </summary>
        /// 
        /// <param name="playlistId">
        ///     Youtube playlist ID.
        /// </param>
        /// <param name="flags">
        ///     The <see cref="PlaylistRefreshFlags"/> which will be passed to the <see cref="Refresh(PlaylistRefreshFlags)"/> method
        ///     when the <see cref="YoutubePlaylist"/> is done initializing.
        /// </param>
        public YoutubePlaylist(string playlistId, PlaylistRefreshFlags flags) : this(playlistId, flags, VideoRefreshFlags.None)
        {
            // Space
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="YoutubePlaylist"/> class with the specified
        ///     Youtube playlist ID and the specified <see cref="PlaylistRefreshFlags"/> and <see cref="VideoRefreshFlags"/> 
        ///     which will be passed to the <see cref="Refresh(PlaylistRefreshFlags, VideoRefreshFlags)"/> method when the <see cref="YoutubePlaylist"/> 
        ///     object is done initializing.
        /// </summary>
        /// 
        /// <param name="playlistId">
        ///     Youtube playlist ID.
        /// </param>
        /// <param name="flags">
        ///     The <see cref="PlaylistRefreshFlags"/> which will be passed to the <see cref="Refresh(PlaylistRefreshFlags, VideoRefreshFlags)"/> method
        ///     when the <see cref="YoutubePlaylist"/> is done initializing.
        /// </param>
        /// <param name="vidFlags">
        ///     The <see cref="VideoRefreshFlags"/> which will be passed to the <see cref="Refresh(PlaylistRefreshFlags, VideoRefreshFlags)"/> method
        ///     when the <see cref="YoutubePlaylist"/> is done initializing.
        /// </param>
        public YoutubePlaylist(string playlistId, PlaylistRefreshFlags flags, VideoRefreshFlags vidFlags)
        {
            if (string.IsNullOrWhiteSpace(playlistId))
                throw new ArgumentNullException(nameof(playlistId));

            _playlist = new List<YoutubeVideo>();
            Url = new Uri(BaseUrl.OriginalString + playlistId);

            if (flags != PlaylistRefreshFlags.None)
                Refresh(flags, vidFlags);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="YoutubePlaylist"/> class with the specified
        ///     URL pointing to the video on Youtube.
        /// </summary>
        /// 
        /// <param name="url">
        ///     URL pointing to the video on Youtube.
        /// </param>
        public YoutubePlaylist(Uri url) : this(url, PlaylistRefreshFlags.All, VideoRefreshFlags.None)
        {
            // Space
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="YoutubePlaylist"/> class with the specified
        ///     URL pointing to the video on Youtube and the specified <see cref="PlaylistRefreshFlags"/> which will be passed to
        ///     the <see cref="Refresh(PlaylistRefreshFlags, VideoRefreshFlags)"/> with <see cref="VideoRefreshFlags.None"/>
        ///     method when the <see cref="YoutubePlaylist"/> object is done initializing.
        /// </summary>
        /// 
        /// <param name="url">
        ///     URL pointing to the video on Youtube.
        /// </param>
        /// <param name="flags">
        ///     The <see cref="PlaylistRefreshFlags"/> which will be passed to the <see cref="Refresh(PlaylistRefreshFlags)"/> method
        ///     when the <see cref="YoutubePlaylist"/> is done initializing.
        /// </param>
        public YoutubePlaylist(Uri url, PlaylistRefreshFlags flags) : this(url, flags, VideoRefreshFlags.None)
        {
            // Space
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="YoutubePlaylist"/> class with the specified
        ///     URL pointing to the video on Youtube and the specified <see cref="PlaylistRefreshFlags"/> and <see cref="VideoRefreshFlags"/> 
        ///     which will be passed to the <see cref="Refresh(PlaylistRefreshFlags, VideoRefreshFlags)"/> method when the <see cref="YoutubePlaylist"/> 
        ///     object is done initializing.
        /// </summary>
        /// 
        /// <param name="url">
        ///     URL pointing to the video on Youtube.
        /// </param>
        /// <param name="flags">
        ///     The <see cref="PlaylistRefreshFlags"/> which will be passed to the <see cref="Refresh(PlaylistRefreshFlags, VideoRefreshFlags)"/> method
        ///     when the <see cref="YoutubePlaylist"/> is done initializing.
        /// </param>
        /// <param name="vidFlags">
        ///     The <see cref="VideoRefreshFlags"/> which will be passed to the <see cref="Refresh(PlaylistRefreshFlags, VideoRefreshFlags)"/> method
        ///     when the <see cref="YoutubePlaylist"/> is done initializing.
        /// </param>
        public YoutubePlaylist(Uri url, PlaylistRefreshFlags flags, VideoRefreshFlags vidFlags)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));

            Url = url;

            if (flags != PlaylistRefreshFlags.None)
                Refresh(flags, vidFlags);
        }
        #endregion

        #region Properties & Fields
        // List of YoutubeVideo which are in the playlist.
        private List<YoutubeVideo> _playlist;
        // Full URL of the YoutubePlaylist.
        private Uri _url;
        // ID of the playlist.
        private string _playlistId;
        // Title of the playlist.
        private string _title;
        // Number of views of the playlist.
        private long _views;

        /// <summary>
        ///     Gets the <see cref="YoutubeVideo"/> at the specified index.
        /// </summary>
        /// 
        /// <param name="index">
        ///     The zero-based index of the <see cref="YoutubeVideo"/> to get.
        /// </param>
        /// <returns>
        ///     The <see cref="YoutubeVideo"/> at the specified index.
        /// </returns>
        public YoutubeVideo this[int index]
        {
            get
            {
                if (index < 0 || index > Count - 1)
                    throw new ArgumentOutOfRangeException("index", "index was out of range.");

                return _playlist[index];
            }
        }

        /// <summary>
        ///     Gets or sets the URL pointing to the <see cref="YoutubePlaylist"/> on Youtube.
        /// </summary>
        public Uri Url
        {
            get
            {
                return _url;
            }
            set
            {
                if (!Utils.ValidPlaylistUrl(value))
                    throw new ArgumentException("value must be a valid Youtube playlist URL.", "value");

                var indexId = value.Query.IndexOf("list=");
                if (indexId == -1)
                    throw new ArgumentException("Could not find playlist ID in '" + value + "'.", "value");

                var playlistId = value.Query.Substring(indexId + 5);
                if (!Utils.ValidYoutubeID(playlistId))
                    throw new ArgumentException("'" + playlistId + "' is not a valid youtube ID.", "value");

                _playlistId = playlistId;
                _url = value;
            }
        }

        /// <summary>
        ///     Gets the playlist ID of the <see cref="YoutubePlaylist"/>.
        /// </summary>
        public string Id => _playlistId;

        /// <summary>
        ///     Gets the title of the <see cref="YoutubePlaylist"/>.
        /// </summary>
        public string Title => _title;

        /// <summary>
        ///     Gets the number of views on the <see cref="YoutubePlaylist"/>.
        /// </summary>
        public long Views => _views;

        /// <summary>
        ///     Gets the number of videos in the <see cref="YoutubePlaylist"/>.
        /// </summary>
        public int Count => _playlist.Count;
        #endregion

        #region Methods
        /// <summary>
        ///     Refreshes the <see cref="YoutubePlaylist"/> object with the flags <see cref="PlaylistRefreshFlags.All"/> and updates it.
        /// </summary>
        public void Refresh()
        {
            Refresh(PlaylistRefreshFlags.All);
        }

        /// <summary>
        ///     Refreshes the <see cref="YoutubePlaylist"/> object with the specified <see cref="PlaylistRefreshFlags"/> and updates it.
        /// </summary>
        /// 
        /// <param name="flags">
        ///     <see cref="PlaylistRefreshFlags"/> with which to update the <see cref="YoutubePlaylist"/> object.
        /// </param>
        public void Refresh(PlaylistRefreshFlags flags)
        {
            Refresh(flags, VideoRefreshFlags.None);
        }

        /// <summary>
        ///     Refreshes the <see cref="YoutubePlaylist"/> object with the specified <see cref="PlaylistRefreshFlags"/> and 
        ///     the specified <see cref="VideoRefreshFlags"/> with which to update the <see cref="YoutubeVideo"/> objects in
        ///     the <see cref="YoutubePlaylist"/>.
        /// </summary>
        /// 
        /// <param name="flags">
        ///     <see cref="PlaylistRefreshFlags"/> with which to update the <see cref="YoutubePlaylist"/> object.
        /// </param>
        /// <param name="vidFlags">
        ///     <see cref="VideoRefreshFlags"/> with which to update the <see cref="YoutubeVideo"/> objects in the
        ///     <see cref="YoutubePlaylist"/>.
        /// </param>
        public void Refresh(PlaylistRefreshFlags flags, VideoRefreshFlags vidFlags)
        {
            if (_url == null)
                throw new InvalidOperationException("Cannot Refresh YoutubePlaylist object when Url is null.");

            if (flags == PlaylistRefreshFlags.None)
                return;

            var webpage = (string)null;
            using (var client = new WebClient())
                webpage = client.DownloadString(_url);

            var extractor = new YoutubePlaylistExtractor(webpage);
            if (!extractor.ExtractAvailability())
                throw new YoutubePlaylistNotAvailableException(this, "Youtube playlist is unavailable.");

            if (flags.HasFlag(PlaylistRefreshFlags.Title))
                _title = extractor.ExtractTitle();

            if (flags.HasFlag(PlaylistRefreshFlags.ViewCount))
                _views = extractor.ExtractViews();

            if (flags.HasFlag(PlaylistRefreshFlags.Playlist))
                _playlist = extractor.ExtractPlaylist(vidFlags);
        }

        /// <summary>
        ///     Returns an enumerator that iterates through the <see cref="YoutubePlaylist"/>.
        /// </summary>
        /// <returns>
        ///     Enumerator that iterates through the <see cref="YoutubePlaylist"/>.
        /// </returns>
        public IEnumerator<YoutubeVideo> GetEnumerator()
        {
            return _playlist.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
