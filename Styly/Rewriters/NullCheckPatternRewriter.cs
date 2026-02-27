using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Styly.Rewriters;

internal class NullCheckPatternRewriter : CSharpSyntaxRewriter
{
    public override SyntaxNode? VisitBinaryExpression(BinaryExpressionSyntax node)
    {
        bool isEquals = node.IsKind(SyntaxKind.EqualsExpression);
        bool isNotEquals = node.IsKind(SyntaxKind.NotEqualsExpression);

        if (!isEquals 
            && !isNotEquals)
        {
            return base.VisitBinaryExpression(node);
        }

        ExpressionSyntax? expression = null;

        if (node.Left.IsKind(SyntaxKind.NullLiteralExpression))
        {
            expression = node.Right;
        }
        else if (node.Right.IsKind(SyntaxKind.NullLiteralExpression))
        {
            expression = node.Left;
        }

        if (expression is null)
        {
            return base.VisitBinaryExpression(node);
        }

        // x is null
        // Added leading space to prevent 'isnull'
        ConstantPatternSyntax nullPattern = SyntaxFactory.ConstantPattern(SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression)).WithLeadingTrivia(SyntaxFactory.Space);

        if (isEquals)
        {
            return SyntaxFactory.IsPatternExpression(expression, nullPattern).WithLeadingTrivia(node.GetLeadingTrivia()).WithTrailingTrivia(node.GetTrailingTrivia());
        }

        // x is not null
        // The 'not' keyword provides the space after 'is'
        UnaryPatternSyntax notPattern = SyntaxFactory.UnaryPattern(SyntaxFactory.Token(SyntaxKind.NotKeyword).WithLeadingTrivia(SyntaxFactory.Space).WithTrailingTrivia(SyntaxFactory.Space), nullPattern.WithoutLeadingTrivia()); // Remove leading space from null pattern here to prevent 'not  null'

        return SyntaxFactory.IsPatternExpression(expression, notPattern).WithLeadingTrivia(node.GetLeadingTrivia()).WithTrailingTrivia(node.GetTrailingTrivia());
    }
}