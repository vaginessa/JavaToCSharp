using com.github.javaparser.ast;
using com.github.javaparser.ast.comments;
using JavaToCSharp.Comments;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Linq;

namespace JavaToCSharp
{
    public static class SyntaxNodeExtensions
    {
        internal static T AddComments<T>(this T syntaxNode, ConversionContext context, Node methodDecl) where T : CSharpSyntaxNode
        {
            if (context.Options.IncludeComments)
            {
                var comment = methodDecl.getComment();
                if (comment != null)
                {
                    var trivia = CommentVisitor.VisitComment(context, comment);
                    syntaxNode = syntaxNode.WithLeadingTrivia(trivia);
                }
                else if(methodDecl.getOrphanComments().size() > 0)
                {
                    var orphanComments = methodDecl.getOrphanComments().AsEnumerable<Comment>();
                    var trivias = orphanComments.SelectMany(orphanComment => CommentVisitor.VisitComment(context, orphanComment)).ToList();
                    syntaxNode = syntaxNode.WithLeadingTrivia(trivias);
                }
            }

            return syntaxNode;
        }
    }
}
