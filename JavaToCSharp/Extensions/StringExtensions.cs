using System;
using System.Collections.Generic;

namespace JavaToCSharp.Extensions
{
    internal static class StringExtentions
    {
        public static IList<string> Lines(this string value)
        {
            return value.Split(new[] { Environment.NewLine, "\n", "\r" }, StringSplitOptions.None);
        }
        
        public static string RemoveFirst(this string value, string find)
        {
            if (value.Length < find.Length)
            {
                return value;
            }

            return value.Substring(value.IndexOf(find, StringComparison.Ordinal) + 1);
        }
    }
}