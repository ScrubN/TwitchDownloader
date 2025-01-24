using System;
using System.Buffers;

namespace TwitchDownloaderCore.Extensions
{
    public static class StringExtensions
    {
        public static string ReplaceAny(this string str, SearchValues<char> oldChars, char newChar)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            var index = str.AsSpan().IndexOfAny(oldChars);
            if (index == -1)
            {
                return str;
            }

            return string.Create(str.Length, (str, index, oldChars, newChar), static (span, state) =>
            {
                var (str, index, oldChars, newChar) = state;

                // A single CopyTo() followed by individual char replacements is significantly faster than many smaller CopyTo()s
                str.CopyTo(span);

                do
                {
                    span[index] = newChar;
                    span = span[(index + 1)..];

                    index = span.IndexOfAny(oldChars);
                } while (index != -1);
            });
        }

        public static string ReplaceAny(this string str, SearchValues<char> oldChars, Func<char, char> newCharCallback)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            var index = str.AsSpan().IndexOfAny(oldChars);
            if (index == -1)
            {
                return str;
            }

            return string.Create(str.Length, (str, index, oldChars, newCharCallback), static (span, state) =>
            {
                var (str, index, oldChars, newCharCallback) = state;

                // A single CopyTo() followed by individual char replacements is significantly faster than many smaller CopyTo()s
                str.CopyTo(span);

                do
                {
                    span[index] = newCharCallback(span[index]);
                    span = span[(index + 1)..];

                    index = span.IndexOfAny(oldChars);
                } while (index != -1);
            });
        }
    }
}