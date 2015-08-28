using com.github.javaparser.ast.stmt;
using JavaToCSharp.Expressions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace JavaToCSharp.Statements
{
    public class ThrowStatementVisitor : StatementVisitor<ThrowStmt>
    {
        public override StatementSyntax Visit(ConversionContext context, ThrowStmt throwStmt)
        {
            var expr = throwStmt.getExpr();

            var exprSyntax = ExpressionVisitor.VisitExpression(context, expr);

            var statementSyntax = SyntaxFactory.ThrowStatement(exprSyntax).AddComments(context, throwStmt);
            return statementSyntax;
        }
    }
}
