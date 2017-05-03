using System;
using System.Net;
using System.Text;
using Utube.Extractors;

namespace Utube
{
    /// <summary>
    /// Represents a Youtube video.
    /// </summary>
    public class YoutubeVideo
    {
        //TODO: Document Exception thrown as well.

        #region Constants
        internal const int VideoIDLength = 11;

        /// <summary>
        ///     Base URL of Youtube videos.
        /// </summary>
        public static readonly Uri BaseUrl = new Uri("https://www.youtube.com/watch?v=");
        #endregion

        #region Constructors
        /// <summary>
        ///     Initializes a new instance of the <see cref="YoutubeVideo"/> class.
        /// </summary>
        public YoutubeVideo()
        {
            // Space
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="YoutubeVideo"/> class with
        ///     the specified Youtube video ID.
        /// </summary>
        /// 
        /// <param name="videoId">
        ///     Youtube video ID.
        /// </param>
        public YoutubeVideo(string videoId) : this(videoId, VideoRefreshFlags.All)
        {
            // Space
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="YoutubeVideo"/> class with
        ///     the specified Youtube video ID and the specified <see cref="VideoRefreshFlags"/> which will be passed to
        ///     the <see cref="Refresh(VideoRefreshFlags)"/> method when the <see cref="YoutubeVideo"/> object is done initializing.
        /// </summary>
        /// 
        /// <param name="videoId">
        ///     Youtube video ID.
        /// </param>
        /// <param name="flags">
        ///     The <see cref="VideoRefreshFlags"/> which will be passed to the <see cref="Refresh(VideoRefreshFlags)"/> method
        ///     when the <see cref="YoutubeVideo"/> is done initializing.
        /// </param>
        public YoutubeVideo(string videoId, VideoRefreshFlags flags)
        {
            if (string.IsNullOrWhiteSpace(videoId))
                throw new ArgumentNullException(nameof(videoId));
            if (videoId.Length != VideoIDLength)
                throw new ArgumentException("Video ID must be 11 characters long.", nameof(videoId));
            if (!Utils.ValidYoutubeID(videoId))
                throw new ArgumentException("Video ID contains invalid Youtube ID characters.", nameof(videoId));

            Url = new Uri(BaseUrl.OriginalString + videoId);

            if (flags != VideoRefreshFlags.None)
                Refresh(flags);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="YoutubeVideo"/> class with
        ///     the specified URL pointing to the video on Youtube.
        /// </summary>
        /// 
        /// <param name="url">
        ///     URL pointing to the video on Youtube.
        /// </param>
        public YoutubeVideo(Uri url) : this(url, VideoRefreshFlags.All)
        {
            // Space
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="YoutubeVideo"/> class with
        ///     the specified URL pointing to the video on Youtube and the specified <see cref="VideoRefreshFlags"/> which will be passed to
        ///     the <see cref="Refresh(VideoRefreshFlags)"/> method when the <see cref="YoutubeVideo"/> object is done initializing.
        /// </summary>
        /// 
        /// <param name="url">
        ///     URL pointing to the video on Youtube.
        /// </param>
        /// <param name="flags">
        ///     The <see cref="VideoRefreshFlags"/> which will be passed to the <see cref="Refresh(VideoRefreshFlags)"/> method
        ///     when the <see cref="YoutubeVideo"/> is done initializing.
        /// </param>
        public YoutubeVideo(Uri url, VideoRefreshFlags flags)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));

            Url = url;

            if (flags != VideoRefreshFlags.None)
                Refresh();
        }
        #endregion

        #region Properties & Fields
        // URL of video.
        private Uri _url;
        // Title of Youtube video.
        private string _title;
        // Description of the video.
        private string _description;
        // Youtube ID of video.
        private string _videoId;
        // Number of views on the video.
        private long _views;
        // Duration of the video.
        private TimeSpan _length;
        // Playlist in which the video is.
        private YoutubePlaylist _playlist;
        // Available video qualities.
        private YoutubeVideoFormat[] _formats;

        /// <summary>
        ///     Gets or sets the URL pointing to the <see cref="YoutubeVideo"/> on Youtube.
        /// </summary>
        /// <returns>
        ///     Returns <c>null</c> when the <see cref="YoutubeVideo"/> object has not been refreshed.
        /// </returns>
        public Uri Url
        {
            get
            {
                return _url;
            }
            set
            {
                // Reset the YoutubeVideo object if the URL is null.
                if (value == null)
                {
                    Reset();
                    return;
                }
                
                // Check if the host of the 
                if (!Utils.ValidVideoHost(value))
                    throw new ArgumentException("Value host must be a Youtube host.", nameof(value));

                // Find the videoId in the Uri value given.
                var videoId = (string)null;
                switch (value.Host)
                {
                    case "www.youtube.com":
                    case "youtube.com":
                        var queryParams = Utils.ParseQuery(value.Query);
                        if (!queryParams.Contains("v"))
                            throw new ArgumentException("Value does not contain a video query.", nameof(value));

                        videoId = (string)queryParams["v"];

                        // If the queryParams contains a list tries to add it to
                        // the YoutubeVideo object.
                        if (queryParams.Contains("list"))
                        {
                            var playlistId = (string)queryParams["list"];
                            if (!string.IsNullOrEmpty(playlistId) && Utils.ValidYoutubeID(playlistId))
                                _playlist = new YoutubePlaylist(playlistId, PlaylistRefreshFlags.None);
                        }
                        break;

                    case "youtu.be":
                        var path = value.LocalPath;
                        if (path != null)
                            videoId = path.Substring(1);
                        break;
                }

                if (string.IsNullOrEmpty(videoId))
                    throw new ArgumentException("Value does not contain a Youtube video ID.", nameof(value));
                if (videoId.Length != VideoIDLength)
                    throw new ArgumentException("value's Youtube video ID must be 11 characters long.", nameof(value));
                if (!Utils.ValidYoutubeID(videoId))
                    throw new ArgumentException("value contains invalid Youtube ID characters.", nameof(value));

                // Reset the object's field if the video Id differs from the current one.
                if (_videoId != null && _videoId != videoId)
                    Reset();

                _videoId = videoId;
                _url = value;
            }
        }

        /// <summary>
        ///     Gets the title of the <see cref="YoutubeVideo"/>.
        /// </summary>
        /// <returns>
        ///     Returns <c>null</c> when the <see cref="YoutubeVideo"/> object has not been refreshed.
        /// </returns>
        public string Title => _title;

        /// <summary>
        ///     Gets the description of the <see cref="YoutubeVideo"/>.
        /// </summary>
        /// <returns>
        ///     Returns <c>null</c> when the <see cref="YoutubeVideo"/> object has not been refreshed.
        /// </returns>
        public string Description => _description;

        /// <summary>
        ///     Gets the Youtube ID of the <see cref="YoutubeVideo"/>.
        /// </summary>
        /// <returns>
        ///     Returns <c>null</c> when the <see cref="YoutubeVideo"/> object has not been refreshed.
        /// </returns>
        public string Id => _videoId;

        /// <summary>
        ///     Gets the number of views on the <see cref="YoutubeVideo"/>.
        /// </summary>
        /// <returns>
        ///     Returns <c>null</c> when the <see cref="YoutubeVideo"/> object has not been refreshed.
        /// </returns>
        public long Views => _views;

        /// <summary>
        ///     Gets the duration or length of the <see cref="YoutubeVideo"/>.
        /// </summary>
        /// <returns>
        ///     Returns <c>null</c> when the <see cref="YoutubeVideo"/> object has not been refreshed.
        /// </returns>
        public TimeSpan Length => _length;

        /// <summary>
        ///     Gets the video qualities/formats available for the <see cref="YoutubeVideo"/>.
        /// </summary>
        /// <returns>
        ///     Returns <c>null</c> when the <see cref="YoutubeVideo"/> object has not been refreshed.
        /// </returns>
        public YoutubeVideoFormat[] Formats => _formats;

        /// <summary>
        ///     Gets the Youtube playlist the <see cref="YoutubeVideo"/> is in.
        /// </summary>
        /// <returns>
        ///     Returns <c>null</c> when the <see cref="YoutubeVideo"/> object has not been refreshed or
        ///     when the <see cref="YoutubeVideo"/> is not in a <see cref="YoutubePlaylist"/>.
        /// </returns>
        public YoutubePlaylist Playlist => _playlist;
        #endregion

        #region Methods
        /// <summary>
        ///     Refreshes the <see cref="YoutubeVideo"/> object with the flags <see cref="VideoRefreshFlags.All"/> and updates it.
        /// </summary>
        public void Refresh()
        {
            Refresh(VideoRefreshFlags.All);
        }

        /// <summary>
        ///     Refreshes the <see cref="YoutubeVideo"/> object with the specified <see cref="VideoRefreshFlags"/> and updates it.
        /// </summary>
        /// 
        /// <param name="flags">
        ///     <see cref="VideoRefreshFlags"/> with which to update the <see cref="YoutubeVideo"/> object.
        /// </param>
        public void Refresh(VideoRefreshFlags flags)
        {
            //TODO: Use get_video_info where possible.

            // Downloads the HTML code of the Youtube video web page.
            if (_url == null)
                throw new InvalidOperationException("Cannot Refresh YoutubeVideo object when Url is null.");

            if (flags == VideoRefreshFlags.None)
                return;

            var webpage = (string)null;
            using (var client = new WebClient() { Encoding = Encoding.UTF8 })
                webpage = client.DownloadString(_url);

            var extractor = new YoutubeVideoExtractor(webpage);
            if (!extractor.ExtractAvailability())
                throw new YoutubeVideoNotAvailableException(this, "Youtube video is not available or is private.");

            //if (flags.HasFlag(VideoRefreshFlags.Playlist))
            //    _playlist.Refresh(PlaylistRefreshFlags.None);

            if (flags.HasFlag(VideoRefreshFlags.Title))
                _title = extractor.ExtractTitle();

            if (flags.HasFlag(VideoRefreshFlags.Description))
                _description = extractor.ExtractDescription();

            if (flags.HasFlag(VideoRefreshFlags.ViewCount))
                _views = extractor.ExtractViewCount();

            if (flags.HasFlag(VideoRefreshFlags.Length))
                _length = extractor.ExtractLength();

            if (flags.HasFlag(VideoRefreshFlags.Formats))
                _formats = extractor.ExtractFormats();
        }

        private void Reset()
        {
            _url = null;
            _title = null;
            _description = null;
            _videoId = null;
            _views = 0;
            _length = TimeSpan.MinValue;
            _playlist = null;
            _formats = null;
        }
        #endregion
    }
}
