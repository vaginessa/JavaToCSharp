using com.github.javaparser.ast.comments;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace JavaToCSharp.Comments
{
    public class LineCommentVisitor : CommentVisitor<LineComment>
    {
        public override SyntaxTriviaList Visit(ConversionContext context, LineComment comment)
        {
            return SyntaxFactory.ParseLeadingTrivia(comment.toString());
        }
    }
}