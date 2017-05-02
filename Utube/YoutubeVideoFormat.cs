using System;
using System.Collections.Generic;

namespace Utube
{
    /// <summary>
    /// Represents a Youtube video format.
    /// </summary>
    public class YoutubeVideoFormat
    {
        static YoutubeVideoFormat()
        {
            _itagDictionary = new Dictionary<int, YoutubeVideoFormat>();
            _itagDictionary.Add(5, new YoutubeVideoFormat(".flv", new VideoSize(400, 240)));
            _itagDictionary.Add(6, new YoutubeVideoFormat(".flv", new VideoSize(450, 270)));

            _itagDictionary.Add(17, new YoutubeVideoFormat(".3gpp", new VideoSize(176, 144)));
            _itagDictionary.Add(18, new YoutubeVideoFormat(".mp4", new VideoSize(640, 360)));
            _itagDictionary.Add(22, new YoutubeVideoFormat(".mp4", new VideoSize(1280, 720)));
            _itagDictionary.Add(36, new YoutubeVideoFormat(".3gpp", new VideoSize(320, 240)));
            _itagDictionary.Add(37, new YoutubeVideoFormat(".mp4", new VideoSize(1920, 1080)));
            _itagDictionary.Add(43, new YoutubeVideoFormat(".3gpp", new VideoSize(640, 360)));

            // DASH .mp4 video
            _itagDictionary.Add(136, new YoutubeVideoFormat(".mp4", new VideoSize(1280, 720)));
            _itagDictionary.Add(137, new YoutubeVideoFormat(".mp4", new VideoSize(1920, 1080)));
            _itagDictionary.Add(248, new YoutubeVideoFormat(".webm", new VideoSize(1920, 1080)));
        }

        private static readonly Dictionary<int, YoutubeVideoFormat> _itagDictionary;

        internal YoutubeVideoFormat(string ext, VideoSize size, bool is3d = false)
        {
            Extension = ext;
            Size = size;
        }

        internal YoutubeVideoFormat(Uri url, VideoSize size, int formatCode)
        {
            VideoUrl = url;
            FormatCode = formatCode;
            Size = size;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YoutubeVideoFormat"/> with the specified
        /// format code and tries to fill the <see cref="YoutubeVideoFormat"/> with known data according to
        /// the format code provided.
        /// </summary>
        /// <param name="formatCode">Format code of the <see cref="YoutubeVideo"/>.</param>
        public YoutubeVideoFormat(int formatCode) : this(formatCode, true)
        {
            // Space
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YoutubeVideoFormat"/> with the specified
        /// format code and tries to fill the <see cref="YoutubeVideoFormat"/> with known data according to
        /// the format code if specified.
        /// </summary>
        /// <param name="formatCode">Format code of the <see cref="YoutubeVideo"/>.</param>
        /// <param name="fill">If set to <c>true</c> it will try to fill the <see cref="YoutubeVideoFormat"/> with known data; otherwise not.</param>
        public YoutubeVideoFormat(int formatCode, bool fill)
        {
            FormatCode = formatCode;
            if (fill && _itagDictionary.ContainsKey(formatCode))
            {
                var format = _itagDictionary[formatCode];
                Size = format.Size;
                Extension = format.Extension;
            }
            else
            {
                Size = VideoSize.UnknownSize;
            }
        }

        /// <summary>
        /// Gets the format code for the <see cref="YoutubeVideoFormat"/>.
        /// </summary>
        public int FormatCode { get; internal set; }

        /// <summary>
        /// Gets the URL pointing to the <see cref="YoutubeVideo"/> file.
        /// </summary>
        public Uri VideoUrl { get; internal set; }

        /// <summary>
        /// Gets the size/dimensions of the <see cref="YoutubeVideo"/>. Returns <see cref="VideoSize.UnknownSize"/> if
        /// the size/dimensions of the <see cref="YoutubeVideo"/> is unknown.
        /// </summary>
        public VideoSize Size { get; internal set; }

        /// <summary>
        /// Gets the extension of the <see cref="YoutubeVideoFormat"/> file.
        /// </summary>
        public string Extension { get; internal set; }
    }
}
