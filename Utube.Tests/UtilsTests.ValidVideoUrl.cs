using NUnit.Framework;
using System;

namespace Utube.Tests
{
    [TestFixture]
    public partial class UtilsTests
    {
        [Test]
        public void ValidVideoUrl_InvalidUrl_ReturnFalse()
        {
            Assert.False(Utils.ValidVideoUrl(new Uri("https://www.youtube.com/")));
            Assert.False(Utils.ValidVideoUrl(new Uri("https://www.youtube.com/asdf")));
            Assert.False(Utils.ValidVideoUrl(new Uri("https://www.youtube.com/playlist")));
            // "www.youtu.be" does not exists but "youtu.be" does.
            Assert.True(Utils.ValidVideoUrl(new Uri("https://www.youtu.be/watch")));

            Assert.False(Utils.ValidVideoUrl(new Uri("https://www.google.com/")));
            Assert.False(Utils.ValidVideoUrl(new Uri("https://www.google.com/asdf")));
            Assert.False(Utils.ValidVideoUrl(new Uri("https://www.google.com/watch")));
            Assert.False(Utils.ValidVideoUrl(new Uri("https://www.google.com/playlist")));
        }

        [Test]
        public void ValidVideoUrl_ValidUrl_ReturnTrue()
        {
            Assert.True(Utils.ValidVideoUrl(new Uri("https://www.youtube.com/watch")));
            Assert.True(Utils.ValidPlaylistUrl(new Uri("https://www.youtube.com/watch?v=ewBulJdtGlM&list=PLDF056FBA2E172F8")));
        }
    }
}
