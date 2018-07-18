using com.github.javaparser;
using com.github.javaparser.ast;
using com.github.javaparser.ast.body;
using com.github.javaparser.ast.expr;
using JavaToCSharp.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace JavaToCSharp.Tests
{
    public class ArrayExpressionTests
    {
        [Fact]
        public void ArrayExpressionTest()
        {
            var java = @"class Program
            {
	            static string[] Main()
	            {
		            return new String[] {};
	            }
            }";
            var csharp = @"return new string[]{};";

            var converted = ConvertStatement(java);
            Assert.Equal(csharp, converted);
        }

        private static string ConvertStatement(string java)
        {
            var declaration = JavaParser.parseBodyDeclaration(java);
            var options = new JavaConversionOptions();
            var context = new ConversionContext(options);
            var arrayCreationExpression = (ArrayCreationExpr)((Node)((MethodDeclaration)declaration.getChildrenNodes().get(1)).getBody().getStmts().get(0)).getChildrenNodes().get(0);
            var expressionSyntax = ExpressionVisitor.VisitExpression(context, arrayCreationExpression).NormalizeWhitespace();

            var tree = CSharpSyntaxTree.Create(expressionSyntax);
            return tree.GetText().ToString();
        }
    }
}