using NUnit.Framework;
using System;

namespace Utube.Tests
{
    [TestFixture]
    public partial class UtilsTests
    {
        [Test]
        public void ValidPlaylistUrl_InvalidUrl_ReturnFalse()
        {
            Assert.False(Utils.ValidPlaylistUrl(new Uri("https://www.youtu.be/")));
            Assert.False(Utils.ValidPlaylistUrl(new Uri("https://www.youtube.com/")));
            Assert.False(Utils.ValidPlaylistUrl(new Uri("https://www.youtube.com/asdf")));
            Assert.False(Utils.ValidPlaylistUrl(new Uri("https://www.youtube.com/watch")));

            Assert.False(Utils.ValidPlaylistUrl(new Uri("https://www.google.com/")));
            Assert.False(Utils.ValidPlaylistUrl(new Uri("https://www.google.com/asdf")));
            Assert.False(Utils.ValidPlaylistUrl(new Uri("https://www.google.com/watch")));
            Assert.False(Utils.ValidPlaylistUrl(new Uri("https://www.google.com/playlist")));
        }

        [Test]
        public void ValidPlaylistUrl_ValidUrl_ReturnTrue()
        {
            Assert.True(Utils.ValidPlaylistUrl(new Uri("https://www.youtube.com/playlist")));
            Assert.True(Utils.ValidPlaylistUrl(new Uri("https://www.youtube.com/watch?v=ewBulJdtGlM&list=PLDF056FBA2E172F8")));
        }
    }
}
