using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Styly.Rewriters.LogicalExpression;

internal class LogicalTriviaApplier : CSharpSyntaxRewriter
{
    private readonly SyntaxTriviaList _indent;
    private readonly SyntaxTrivia _newline = SyntaxFactory.CarriageReturnLineFeed;
    public LogicalTriviaApplier(SyntaxTriviaList indent)
    {
        _indent = indent;
    }

    public override SyntaxToken VisitToken(SyntaxToken token)
    {
        // If it's a logical operator within a binary expression or pattern, wrap it
        if (IsLogicalOperator(token) && (token.Parent is BinaryExpressionSyntax or BinaryPatternSyntax))
        {
            // If it's a logical operator within a binary expression or pattern, wrap it
            return NameMe(token);
        }

        // If it's a logical operator within a binary expression or pattern, wrap it
        return base.VisitToken(token);
    }

    private SyntaxToken NameMe(SyntaxToken token)
    {
        return token
            .WithLeadingTrivia(SyntaxFactory.TriviaList(_newline).AddRange(_indent))
            .WithTrailingTrivia(SyntaxFactory.Space);
    }

    private static bool IsLogicalOperator(SyntaxToken token)
    {
        return token.IsKind(SyntaxKind.AmpersandAmpersandToken) || token.IsKind(SyntaxKind.BarBarToken) || token.IsKind(SyntaxKind.AndKeyword) || token.IsKind(SyntaxKind.OrKeyword);
    }
}