using NUnit.Framework;
using System;

namespace Utube.Tests
{
    [TestFixture]
    public partial class YoutubeVideoTests
    {
        [Test]
        public void Constructors_NullArgs_Exceptions()
        {
            Assert.Throws<ArgumentNullException>(() => new YoutubeVideo(videoId: null));
            Assert.Throws<ArgumentNullException>(() => new YoutubeVideo(url: null));
        }

        [Test]
        public void Constructors_InvalidID_Exceptions()
        {
            // Contains an extra character making it 12 characters long.
            Assert.Throws<ArgumentException>(() => new YoutubeVideo("ewBulJdtGlMm"));
            Assert.Throws<ArgumentException>(() => new YoutubeVideo("%$^@#"));
            Assert.Throws<ArgumentException>(() => new YoutubeVideo(new Uri("https://www.youtube.com/watch?v=#@$%^")));
        }

        [Test]
        public void Constructors_InvalidUrl_Exceptions()
        {
            Assert.Throws<ArgumentException>(() => new YoutubeVideo(new Uri("https://www.youtube.com/watch?v=")));

            // URL Youtube video ID is 12 character long instead of 11.
            Assert.Throws<ArgumentException>(() => new YoutubeVideo(new Uri("https://www.youtube.com/watch?v=ewBulJdtGlMm")));

            // URL does not contain a Youtube video ID.
            Assert.Throws<ArgumentException>(() => new YoutubeVideo(new Uri("https://www.youtube.com/watch?v=&list=PLDF056FBA2E172F8")));

            Assert.Throws<ArgumentException>(() => new YoutubeVideo(new Uri("https://www.google.com/")));

            // URL is not a valid Youtube video URL.
            Assert.Throws<ArgumentException>(() => new YoutubeVideo(new Uri("https://www.youtube.com/somestuff?v=")));
        }

        [Test]
        public void Url_ContainsVideo_ParseUrl()
        {
            var video = new YoutubeVideo(new Uri("https://www.youtube.com/watch?v=ewBulJdtGlM"), VideoRefreshFlags.None);
            Assert.AreEqual("ewBulJdtGlM", video.Id);
            Assert.Null(video.Playlist);
        }

        [Test]
        public void Url_ContainsVideoAndPlaylist_ParseUrl()
        {
            var video = new YoutubeVideo(new Uri("https://www.youtube.com/watch?v=ewBulJdtGlM&list=PLDF056FBA2E172F87"), VideoRefreshFlags.None);
            Assert.AreEqual("ewBulJdtGlM", video.Id);
            Assert.NotNull(video.Playlist);
            Assert.AreEqual("PLDF056FBA2E172F87", video.Playlist.Id);
        }

        [Test]
        public void Url_ContainsVideoAndInvalidPlaylist_ParseUrl()
        {
            var video = new YoutubeVideo(new Uri("https://www.youtube.com/watch?v=ewBulJdtGlM&list=####@"), VideoRefreshFlags.None);
            Assert.AreEqual("ewBulJdtGlM", video.Id);
            Assert.Null(video.Playlist);
        }
    }
}
