using System;
using System.Collections;

namespace Utube
{
    internal static class Utils
    {
        // Returns true if the specified URL is a valid Youtube video URL.
        public static bool ValidVideoUrl(Uri url)
        {
            var host = url.Host;
            switch (host)
            {
                case "www.youtube.com":
                case "youtube.com":
                    if (url.LocalPath == "/watch")
                        return true;
                    return false;

                case "youtu.be":
                    return true;

                default:
                    return false;
            }
        }

        // Returns true if the specified URL is a valid Youtube playlist URL.
        public static bool ValidPlaylistUrl(Uri url)
        {
            if (url.Host != "www.youtube.com")
                return false;
            if (url.LocalPath != "/playlist")
            {
                if (url.AbsoluteUri.Contains("&list="))
                    return true;
                return false;
            }
            return true;
        }

        // Returns a Hashtable of the parameters specified Query string.
        public static Hashtable ParseQuery(string query)
        {
            if (string.IsNullOrWhiteSpace("query"))
                throw new ArgumentNullException("query");

            if (query[0] == '?')
                query = query.Substring(1);

            var table = new Hashtable();
            var parameters = query.Split('&');
            for (int i = 0; i < parameters.Length; i++)
            {
                var paramNameVal = parameters[i].Split('=');
                table.Add(paramNameVal[0], paramNameVal[1]);
            }

            return table;
        }

        // Valid characters in a Youtube ID.
        private const string ValidYoutubeIDCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890-_";

        // Returns true if the Youtube ID specified is valid; otherwise false.
        public static bool ValidYoutubeID(string id)
        {
            for (int i = 0; i < id.Length; i++)
            {
                if (!ValidYoutubeIDCharacters.Contains(id[i].ToString()))
                    return false;
            }
            return true;
        }

        // Returns the string between the specified strings a & b with the specified baseIndex.
        public static string GetBetween(string value, string a, string b, int baseIndex)
        {
            var index1 = value.IndexOf(a, baseIndex);
            if (index1 == -1)
                return null;

            var index2 = value.IndexOf(b, index1 + a.Length + baseIndex);
            if (index2 == -1)
                return null;

            return value.Substring(index1 + a.Length, index2 - index1 - a.Length);
        }

        public static string GetBetween2(string value, string a, string b, int startIndex)
        {
            var index1 = value.IndexOf(a, startIndex);
            if (index1 == -1)
                return null;

            var index2 = value.IndexOf(b, index1 + a.Length);
            if (index2 == -1)
                return null;

            return value.Substring(index1 + a.Length, index2 - index1 - a.Length);
        }

        public static int CountOccurences(this string str, string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("value");

            var index = str.IndexOf(value);
            if (index == -1)
                return 0;

            //var count = 1;
            var count = 0;
            while (index != -1)
            {
                count++;
                index = str.IndexOf(value, index + value.Length);
            }
            return count;
        }
    }
}
