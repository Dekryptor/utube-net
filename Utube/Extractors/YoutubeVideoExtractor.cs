using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Utube.Extractors
{
    internal class YoutubeVideoExtractor : BaseExtractor
    {
        public YoutubeVideoExtractor(string webpage) : base(webpage)
        {
            // ytplayer.config can be null if the video is unavailable or private.
            if (ExtractAvailability())
                _args = ExtractArgs();
        }

        // Args of ytplayer.config.
        private string _args;

        // Determines if the video exists or is available.
        public bool ExtractAvailability()
        {
            var exists = GetBetween("<div id=\"unavailable-submessage\" class=\"submessage\">", "</div>");
            if (string.IsNullOrWhiteSpace(exists))
                return true;
            if (exists.Contains("Sorry about that") || exists.Contains("If the owner of this video"))
                return false;
            return true;
        }

        // Extracts Youtube video formats available.
        public YoutubeVideoFormat[] ExtractFormats()
        {
            var streamMap = ExtractStreamMap();
            var adpStreamMap = ExtractAdaptiveStreamMap();

            // Combine StreamMap and AdaptiveStreamMap into a single string then
            // split it into queries.
            var finalMap = streamMap + "," + adpStreamMap;
            var queries = finalMap.Split(',');

            var vidFmts = new YoutubeVideoFormat[queries.Length];

            for (int i = 0; i < queries.Length; i++)
            {
                var queryParams = Utils.ParseQuery(queries[i]);

                if (!queryParams.Contains("url"))
                    continue; //throw new ExtractionException("Query does not contain parameter 'url'.");
                var url = Uri.UnescapeDataString((string)queryParams["url"]);

                var itag = -1;
                if (queryParams.Contains("itag"))
                    itag = int.Parse((string)queryParams["itag"]);

                var format = new YoutubeVideoFormat(itag) { VideoUrl = new Uri(url) };
                vidFmts[i] = format;
            }

            return vidFmts;
        }

        // Extracts the video title.
        public string ExtractTitle()
        {
            var title = GetBetween(_args, "title\":\"", "\"");
            return title != null ? Regex.Unescape(title) : null;
        }

        // Extracts the video description.
        public string ExtractDescription()
        {
            var description = GetBetween("<p id=\"eow-description\" class=\"\" >", "</p>");
            if (description == null)
                return null;

            description = description.Replace("<br />", Environment.NewLine);

            // Filters HTML links into plain text links.
            var index = description.IndexOf("<a");
            if (index != -1)
            {
                // Locates all the <a></a> tags in the description and gets its href value.
                // Then adds them to a list of Tuple<string1, string2> where string1 is the
                // HTML link and string2 the href from the HTML string.
                var replaceList = new List<Tuple<string, string>>();
                while (index != -1)
                {
                    var indexEnd = description.IndexOf("</a>", index) + 4;
                    var linkHtml = description.Substring(index, indexEnd - index);
                    var href = GetBetween(linkHtml, "href=\"", "\"");

                    replaceList.Add(new Tuple<string, string>(linkHtml, href));
                    index = description.IndexOf("<a", indexEnd);
                }

                // Replace the HTML links into plain text links.
                for (int i = 0; i < replaceList.Count; i++)
                {
                    var replaceData = replaceList[i];
                    description = description.Replace(replaceData.Item1, replaceData.Item2);
                }

                //TODO: Remove extra space in description.
            }

            return description;
        }

        // Extract the video length.
        public TimeSpan ExtractLength()
        {
            var lengthStr = GetBetween(_args, "length_seconds\":\"", "\"");
            if (lengthStr == null)
                return default(TimeSpan);

            var length = int.Parse(lengthStr);
            return TimeSpan.FromSeconds(length);
        }

        // Extracts the Video view count.
        public int ExtractViewCount()
        {
            var count = GetBetween(_args, "view_count\":\"", "\"");
            if (count == null)
                return default(int);

            return int.Parse(count);
        }

        // Extracts the 'args' field from the ytplayer.config json.
        private string ExtractArgs()
        {
            var config = GetBetween("ytplayer.config = {", "};");
            if (config == null)
                throw new ExtractionException("YoutubeVideoExtrator was unable to extract ytplayer.config.");

            var args = GetBetween(config, "\"args\":{", "\"}");
            if (args == null)
                args = GetBetween(config, "\"args\":{", "true}");
            if (args == null)
                args = GetBetween(config, "\"args\":{", "false}");

            if (args == null)
                throw new ExtractionException("YoutubeVideoExtractor was unable to extract 'args' in ytplayer.config.");

            return args;
        }

        private string ExtractStreamMap()
        {
            var strmMap = GetBetween(_args, "url_encoded_fmt_stream_map\":\"", "\"");
            if (strmMap == null)
                throw new ExtractionException("YoutubeVideoExtractor was unable to extract 'url_encoded_fmt_stream_map' in '_args'.");

            // Converts the '\u0026' characters back into '&' ampersand.
            return Regex.Unescape(strmMap);
        }

        private string ExtractAdaptiveStreamMap()
        {
            var adpStrmMap = GetBetween(_args, "adaptive_fmts\":\"", "\"");
            if (adpStrmMap == null)
                throw new ExtractionException("YoutubeVideoExtractor was unable to extract 'adaptive_fmts' in '_args'.");

            // Converts the '\u0026' characters back into '&' ampersand.
            return Regex.Unescape(adpStrmMap);
        }
    }
}
