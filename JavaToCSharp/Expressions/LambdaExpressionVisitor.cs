using com.github.javaparser.ast.expr;
using JavaToCSharp.Statements;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace JavaToCSharp.Expressions
{
    public class LambdaExpressionVisitor : ExpressionVisitor<LambdaExpr>
    {
        public override ExpressionSyntax Visit(ConversionContext context, LambdaExpr expr)
        {
            var parameters = expr.getParameters();
            var parameterList = parameters.toArray();
            var parameterNodes = new List<SyntaxNode>();

            foreach (var parameter in parameterList)
            {
                var identifier = TypeHelper.ConvertIdentifierName(parameter.ToString());
                var identifierToken = SyntaxFactory.ParseToken(identifier);
                parameterNodes.Add(SyntaxFactory.Parameter(identifierToken));
            }

            var body = expr.getBody();
            var syntax = StatementVisitor.VisitStatement(context, body);

            return SyntaxFactory.ParenthesizedLambdaExpression(AsParameterList(parameterNodes), syntax);
        }

        private static ParameterListSyntax AsParameterList(IEnumerable<SyntaxNode> parameters)
        {
            return parameters != null
                ? SyntaxFactory.ParameterList(SyntaxFactory.SeparatedList(parameters.Cast<ParameterSyntax>()))
                : SyntaxFactory.ParameterList();
        }
    }
}