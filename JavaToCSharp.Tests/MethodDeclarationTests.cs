using com.github.javaparser;
using com.github.javaparser.ast.body;
using JavaToCSharp.Declarations;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace JavaToCSharp.Tests
{
    public class MethodDeclarationTests
    {
        [Fact]
        public void TestSimpleMethodDeclaration()
        {
            var java = @"public static void write() {}";
            var csharp = @"public static void Write()
{
}";

            var converted = ConvertStatement(java);
            Assert.Equal(csharp, converted);
        }

        private static string ConvertStatement(string java)
        {
            var javaClassDeclaration = @"
            class A
            {
                " + java + @"
            }";
            var declaration = JavaParser.parseBodyDeclaration(javaClassDeclaration);
            var options = new JavaConversionOptions();
            var context = new ConversionContext(options);
            var classDeclaration = SyntaxFactory.ClassDeclaration("A");
            var body = declaration.getChildrenNodes().get(1);
            var statementSyntax = BodyDeclarationVisitor.VisitBodyDeclarationForClass(context,
                classDeclaration, (BodyDeclaration)body)
                .NormalizeWhitespace();

            var tree = CSharpSyntaxTree.Create(statementSyntax);
            return tree.GetText().ToString();
        }
    }
}