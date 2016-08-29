using System.Collections.Generic;

namespace System
{
    internal static class StringExtentions
    {
        public static IList<String> Lines(this String value)
        {
            return value.Split(new[] { Environment.NewLine, "\n", "\r" }, StringSplitOptions.None);
        }
        
        public static String RemoveFirst(this String value, String find)
        {
            if (value.Length < find.Length)
            {
                return value;
            }

            return value.Substring(value.IndexOf(find, StringComparison.Ordinal) + 1);
        }
    }
}