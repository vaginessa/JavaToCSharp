using System;
using System.Text;
using com.github.javaparser.ast.comments;
using JavaToCSharp.Comments.Syntax;

namespace JavaToCSharp.Comments.Parsers
{
    internal static class JavaDocParser
    {
        public static JavaDoc Parse(Comment comment)
        {
            var lines = comment.getContent().Lines();

            var doc = new JavaDoc();
            var summaryBuilder = new StringBuilder();

            foreach (var line in lines)
            {
                var commentLine = line
                    .RemoveFirst("*/")
                    .RemoveFirst("/**")
                    .RemoveFirst("*")
                    .RemoveFirst("///")
                    .RemoveFirst("//");

                if (String.IsNullOrWhiteSpace(commentLine))
                {
                    continue;
                }

                //var trimmedCommentLine = commentLine.Trim();

                //if (trimmedCommentLine.StartsWith(JavaDocSyntaxToken.See))
                //{
                //    doc.See.Add(trimmedCommentLine.Replace(JavaDocSyntaxToken.See, String.Empty).Trim());
                //    continue;
                //}

                //if (trimmedCommentLine.StartsWith(JavaDocSyntaxToken.Author))
                //{
                //    doc.Author.Add(trimmedCommentLine.Replace(JavaDocSyntaxToken.Author, String.Empty).Trim());
                //    continue;
                //}

                //if (trimmedCommentLine.StartsWith(JavaDocSyntaxToken.Return))
                //{
                //    doc.Return.Add(trimmedCommentLine.Replace(JavaDocSyntaxToken.Return, String.Empty).Trim());
                //    continue;
                //}

                //if (trimmedCommentLine.StartsWith(JavaDocSyntaxToken.Param))
                //{
                //    JavaDocParameterParser.Parse(trimmedCommentLine);
                //    doc.Parameters.Add(trimmedCommentLine.Replace(JavaDocSyntaxToken.Param, String.Empty).Trim());
                //    continue;
                //}

                //if (trimmedCommentLine.StartsWith(JavaDocSyntaxToken.Throws) || trimmedCommentLine.StartsWith(JavaDocSyntaxToken.Exception))
                //{
                //    doc.Exceptions.Add(trimmedCommentLine.Replace(JavaDocSyntaxToken.Throws, String.Empty).Trim().Replace(JavaDocSyntaxToken.Exception, String.Empty).Trim());
                //    continue;
                //}

                //if (trimmedCommentLine.StartsWith(JavaDocSyntaxToken.Deprecated))
                //{
                //    doc.Depricated = trimmedCommentLine.Replace(JavaDocSyntaxToken.Deprecated, String.Empty).Trim();
                //    continue;
                //}

                //if (trimmedCommentLine.StartsWith(JavaDocSyntaxToken.Since))
                //{
                //    doc.Depricated = trimmedCommentLine.Replace(JavaDocSyntaxToken.Since, String.Empty).Trim();
                //    continue;
                //}

                //if (commentLine.Contains(JavaDocSyntaxToken.Link))
                //{
                //    //TODO: Replace @link Foo with <see cref="Foo" />
                //}

                summaryBuilder.AppendLine(commentLine);
            }

            doc.Summary = summaryBuilder.ToString().Trim();

            return doc;
        }
    }
}