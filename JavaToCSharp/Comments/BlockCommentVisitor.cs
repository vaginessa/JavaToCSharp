using com.github.javaparser.ast.comments;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace JavaToCSharp.Comments
{
    public class BlockCommentVisitor : CommentVisitor<BlockComment>
    {
        public override SyntaxTriviaList Visit(ConversionContext context, BlockComment comment)
        {
            return SyntaxFactory.ParseLeadingTrivia(comment.toString());
        }
    }
}