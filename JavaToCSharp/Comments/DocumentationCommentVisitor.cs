using System;
using System.Linq;
using System.Text;
using com.github.javaparser.ast.comments;
using JavaToCSharp.Comments.Parsers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace JavaToCSharp.Comments
{
    public class DocumentationCommentVisitor : CommentVisitor<JavadocComment>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        public override SyntaxTriviaList Visit(ConversionContext context, JavadocComment comment)
        {
            var javaDoc = JavaDocParser.Parse(comment);
            var builder = new StringBuilder();

            if (!String.IsNullOrWhiteSpace(javaDoc.Summary))
            {
                builder.AppendLine(@"/// <summary>");
                foreach (var line in javaDoc.Summary.Lines())
                {
                    builder.AppendLine($"/// {line}");
                }
                builder.AppendLine(@"/// </summary>");
            }

            if (javaDoc.Parameters.Any())
            {
                foreach (var parameter in javaDoc.Parameters)
                {
                    builder.AppendLine($"/// <param name=\"{parameter}\"");
                }
            }

            return SyntaxFactory.ParseLeadingTrivia(builder.ToString());

            //return SyntaxFactory.DocumentationCommentTrivia(
            //    SyntaxKind.SingleLineDocumentationCommentTrivia,
            //    new SyntaxList<XmlNodeSyntax>
            //    {
            //        SyntaxFactory.XmlSummaryElement(
            //            SyntaxFactory.XmlNewLine("\r\n"),
            //            SyntaxFactory.XmlText("This class provides extension methods for the "),
            //            SyntaxFactory.XmlSeeElement(
            //                SyntaxFactory.TypeCref(SyntaxFactory.ParseTypeName("TypeName"))
            //                ),
            //            SyntaxFactory.XmlText(" class."),
            //            SyntaxFactory.XmlNewLine("\r\n")
            //            ),
            //        SyntaxFactory.XmlNewLine("\r\n"),
            //        SyntaxFactory.XmlThreadSafetyElement(),
            //        SyntaxFactory.XmlNewLine("\r\n"),
            //        SyntaxFactory.XmlPreliminaryElement()
            //    },
            //    SyntaxFactory.Token(SyntaxKind.EndOfDocumentationCommentToken));


            //var documentation = SyntaxFactory.DocumentationComment(
            //    SyntaxFactory.XmlSummaryElement(
            //        SyntaxFactory.XmlNewLine("\r\n"),
            //        SyntaxFactory.XmlText("This class provides extension methods for the "),
            //        SyntaxFactory.XmlSeeElement(
            //            SyntaxFactory.TypeCref(SyntaxFactory.ParseTypeName("TypeName"))
            //        ),
            //        SyntaxFactory.XmlText(" class."),
            //        SyntaxFactory.XmlNewLine("\r\n")
            //    ),
            //    SyntaxFactory.XmlNewLine("\r\n"),
            //    SyntaxFactory.XmlThreadSafetyElement(),
            //    SyntaxFactory.XmlNewLine("\r\n"),
            //    SyntaxFactory.XmlPreliminaryElement()
            //);
            

            //return documentation;
        }
    }
}
