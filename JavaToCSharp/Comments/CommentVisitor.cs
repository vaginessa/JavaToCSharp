using System;
using System.Collections.Generic;
using com.github.javaparser.ast.comments;
using Microsoft.CodeAnalysis;

namespace JavaToCSharp.Comments
{
    public abstract class CommentVisitor<T> : CommentVisitor where T : Comment
    {
        public abstract SyntaxTriviaList Visit(ConversionContext context, T comment);

        protected sealed override SyntaxTriviaList Visit(ConversionContext context, Comment comment)
        {
            return this.Visit(context, (T)comment);
        }
    }

    public abstract class CommentVisitor
    {
        private static readonly IDictionary<Type, CommentVisitor> visitors;

        static CommentVisitor()
        {
            CommentVisitor.visitors = new Dictionary<Type, CommentVisitor>
            {
                { typeof(JavadocComment), new DocumentationCommentVisitor() }
            };
        }

        protected abstract SyntaxTriviaList Visit(ConversionContext context, Comment comment);

        public static SyntaxTriviaList VisitComment(ConversionContext context, Comment comment)
        {
            CommentVisitor visitor;

            if (!CommentVisitor.visitors.TryGetValue(comment.GetType(), out visitor))
                throw new InvalidOperationException("No visitor has been implemented for comment type " + comment.GetType().Name);

            return visitor.Visit(context, comment);
        }
    }
}
