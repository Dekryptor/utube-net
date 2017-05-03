using System;

namespace Utube
{
    /// <summary>
    ///     Represents the size of a <see cref="YoutubeVideo"/>.
    /// </summary>
    public struct VideoSize
    {
        /// <summary>
        ///     Represents an unknown <see cref="VideoSize"/>.
        /// </summary>
        public static readonly VideoSize UnknownSize = new VideoSize()
        {
            _width = -1,
            _height = -1
        };

        /// <summary>
        ///     Initializes a new instance of the <see cref="VideoSize"/> class with the specified
        ///     height and width;
        /// </summary>
        /// 
        /// <param name="width">
        ///     Width of the <see cref="YoutubeVideo"/>.
        /// </param>
        /// <param name="height">
        ///     Height of the <see cref="YoutubeVideo"/>.
        /// </param>
        public VideoSize(int width, int height)
        {
            if (width < 0)
                throw new ArgumentOutOfRangeException(nameof(width), "width must be non-negative.");
            if (height < 0)
                throw new ArgumentOutOfRangeException(nameof(height), "height must be non-negative.");

            _width = width;
            _height = height;
        }

        private int _width;
        private int _height;

        /// <summary>
        ///     Gets or sets the width of the <see cref="YoutubeVideo"/>.
        /// </summary>
        public int Width
        {
            get
            {
                return _width;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "value must be non-negative.");

                _width = value;
            }
        }

        /// <summary>
        ///     Gets or sets the height of the <see cref="YoutubeVideo"/>.
        /// </summary>
        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "value must be non-negative.");

                _height = value;
            }
        }

        /// <summary>
        ///     Determines if the specified <see cref="VideoSize"/>, <paramref name="a"/> is equal to the
        ///     specified <see cref="VideoSize"/>, <paramref name="b"/>.
        /// </summary>
        /// 
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>
        ///     <c>true</c> if they are equal; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator ==(VideoSize a, VideoSize b)
        {
            return a.Equals(b);
        }

        /// <summary>
        ///     Determines if the specified <see cref="VideoSize"/>, <paramref name="a"/> is not equal to the
        ///     specified <see cref="VideoSize"/>, <paramref name="b"/>.
        /// </summary>
        /// 
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>
        ///     <c>true</c> if they are not equal; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator !=(VideoSize a, VideoSize b)
        {
            return !(a == b);
        }

        /// <summary>
        ///     Determines whether the specified <see cref="VideoSize"/> object is equal to the current <see cref="VideoSize"/> object.
        /// </summary>
        /// 
        /// <param name="obj">
        ///     The <see cref="VideoSize"/> object to compare with the current <see cref="VideoSize"/> object.
        /// </param>
        /// 
        /// <returns>
        ///     <c>true</c> if the specified <see cref="VideoSize"/> object is equal with the current <see cref="VideoSize"/> object; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(VideoSize obj)
        {
            return obj.Width == Width && obj.Height == Height;
        }

        /// <summary>
        ///     Determines whether the specified object is equal to the current <see cref="VideoSize"/> object.
        /// </summary>
        /// 
        /// <param name="obj">
        ///     The object to compare with the current <see cref="VideoSize"/> object.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the specified object is equal with the current <see cref="VideoSize"/> object; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is VideoSize))
                return false;

            var size = (VideoSize)obj;

            return size.Width == Width && size.Height == Height;
        }

        /// <summary>
        ///     Gets the hash code for the current instance of the <see cref="VideoSize"/> object.
        /// </summary>
        /// 
        /// <returns>
        ///     A hash code for the current instance of the <see cref="VideoSize"/> object.
        /// </returns>
        public override int GetHashCode()
        {
            return (_width << 16) ^ _height;
        }

        /// <summary>
        ///     Returns a string that represents the current instance of this <see cref="VideoSize"/> object.
        /// </summary>
        /// 
        /// <returns>
        ///     A string that represents the current instance of this <see cref="VideoSize"/> object.
        /// </returns>
        public override string ToString()
        {
            return _width + "x" + _height;
        }

        /// <summary>
        ///     Converts the string representation of a <see cref="VideoSize"/> into its <see cref="VideoSize"/> object equivalent.
        /// </summary>
        /// 
        /// <param name="value">
        ///     A string representing a <see cref="VideoSize"/>.
        /// </param>
        /// <returns>
        ///     <see cref="VideoSize"/> object equivalent of the specified string.
        /// </returns>
        public static VideoSize Parse(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("size");
            if (value.CountOccurences("x") != 1)
                throw new FormatException("size must contain a digit, an 'x' character and a digit again.");

            var split = value.Split('x');
            var width = 0;
            var height = 0;

            if (string.IsNullOrWhiteSpace(split[0]) || string.IsNullOrWhiteSpace(split[1]))
                throw new FormatException("size must contain a width and a height.");

            if (!int.TryParse(split[0], out width))
                throw new FormatException("size does not contain a valid width '" + split[0] + "'.");
            if (!int.TryParse(split[1], out height))
                throw new FormatException("size does not contain a valid height '" + split[1] + "'.");

            if (width < 0 || height < 0)
                throw new FormatException("width and height must be non-negative.");

            return new VideoSize(width, height);
        }

        /// <summary>
        ///     Converts the string representation of a <see cref="VideoSize"/> into its <see cref="VideoSize"/> object equivalent and
        ///     returns a boolean indicating if the conversion succeeded.
        /// </summary>
        /// 
        /// <param name="s">
        ///     A string representing a <see cref="VideoSize"/>.
        /// </param>
        /// <param name="result">
        /// </param>
        /// <returns>
        ///     <c>true</c> if the conversion succeeded; otherwise, <c>false</c>.
        /// </returns>
        public static bool TryParse(string s, out VideoSize result)
        {
            result = default(VideoSize);
            if (string.IsNullOrWhiteSpace(s))
                return false;
            if (s.CountOccurences("x") != 1)
                return false;

            var split = s.Split('x');
            var width = 0;
            var height = 0;

            if (string.IsNullOrWhiteSpace(split[0]) || string.IsNullOrWhiteSpace(split[1]))
                return false;
            if (!int.TryParse(split[0], out width) || !int.TryParse(split[1], out height))
                return false;
            if (width < 0 || height < 0)
                return false;

            result = new VideoSize(width, height);
            return true;
        }
    }
}
