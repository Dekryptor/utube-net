using NUnit.Framework;
using System.Collections.Generic;

namespace Utube.Tests
{
    [TestFixture]
    public partial class YoutubeVideoTests
    {
        private List<string> _ytIds;

        public YoutubeVideoTests()
        {
            _ytIds = new List<string>();
            _ytIds.Add("UW_VWFVTMFQ");
            _ytIds.Add("UW_VWFVTMFQ");
        }

        [Test]
        public void Refresh_ValidVideos_Equality()
        {
            for (int i = 0; i < _ytIds.Count; i++)
            {
                var video = new YoutubeVideo(_ytIds[i]);
            }
        }

        public class TestData
        {
            public TestData(string ytId, string title, string description)
            {

            }
        }
    }
}
