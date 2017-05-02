using System;

namespace Utube.Extractors
{
    internal class BaseExtractor
    {
        public BaseExtractor(string data)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            Data = data;
        }

        protected string Data { get; set; }

        // Gets the string between the 2 specified string in Data.
        protected string GetBetween(string a, string b)
        {
            return GetBetween(Data, a, b);
        }

        // Gets the string between the 2 specified string in Data with given base index.
        protected string GetBetween(string a, string b, int baseIndex)
        {
            return GetBetween(Data, a, b, baseIndex);
        }

        // Gets the string between the 2 specified string in value.
        protected string GetBetween(string value, string a, string b)
        {
            return GetBetween(value, a, b, 0);
        }

        // Gets the string between the 2 specified string in value with given base index.
        protected string GetBetween(string value, string a, string b, int baseIndex)
        {
            var index1 = value.IndexOf(a, baseIndex);
            if (index1 == -1)
                return null;

            // Skips the value of a.
            var index2 = value.IndexOf(b, index1 + a.Length);
            if (index2 == -1)
                return null;

            return value.Substring(index1 + a.Length, index2 - index1 - a.Length);
        }
    }
}
