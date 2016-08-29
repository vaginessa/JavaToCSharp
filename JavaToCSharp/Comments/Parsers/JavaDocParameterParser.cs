using System;
using JavaToCSharp.Comments.Syntax;

namespace JavaToCSharp.Comments.Parsers
{
    internal class JavaDocParameterParser
    {
        public static void Parse(JavaDoc context, String line)
        {
            line = line.Trim();

            if (!line.StartsWith(JavaDocSyntaxToken.Param))
            {
                return;
            }

            line = line.Replace(JavaDocSyntaxToken.Param, String.Empty).Trim();

            var values = line.Split(new[] { ' ', '\t' }, 2);

            context.Parameters.Add(new JavaDocParameter
            {
                Name = values[0],
                Description = values.Length == 1 ? values[1] : null
            });
        }
    }
}