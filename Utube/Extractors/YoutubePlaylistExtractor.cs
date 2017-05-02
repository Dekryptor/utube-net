using System.Collections.Generic;

namespace Utube.Extractors
{
    internal class YoutubePlaylistExtractor : BaseExtractor
    {
        public YoutubePlaylistExtractor(string webpage) 
            : base(webpage)
        {
            // Space
        }

        public bool ExtractAvailability()
        {
            var exists = GetBetween("<title>", "</title>");
            if (exists.Contains("went wrong"))
                return false;
            return true;
        }

        public string ExtractTitle()
        {
            var title = GetBetween(" <meta name=\"title\" content=\"", "\"");
            return title;
        }

        public int ExtractViews()
        {
            var viewsStr = GetBetween("videos</li><li>", " views</li>");
            if (viewsStr == null)
                return 0;

            viewsStr = viewsStr.Replace(",", "");
            var view = int.Parse(viewsStr);
            return view;
        }

        public List<YoutubeVideo> ExtractPlaylist(VideoRefreshFlags flags)
        {
            var playlist = new List<YoutubeVideo>();
            var baseIndex = Data.IndexOf("<tr class=\"pl-video yt-uix-tile \"");
            while (baseIndex != -1)
            {
                // Extracts the HTML element <tr> with attribute "class" = "pl-video yt-uix-tile "
                // from the web page.
                var trElement = GetBetween("<", ">", baseIndex);

                // Extracts the HTML attribute "data-video-id" from the element
                // which contains the video ID of the playlist entry.
                var videoId = GetBetween(trElement, "data-video-id=\"", "\"");
                var video = new YoutubeVideo(videoId, flags);
                playlist.Add(video);

                baseIndex = Data.IndexOf("<tr class=\"pl-video yt-uix-tile \"", baseIndex + trElement.Length);
            }

            return playlist;
        }
    }
}
