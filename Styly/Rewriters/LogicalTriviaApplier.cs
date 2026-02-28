using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Styly.Rewriters;

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
        bool isLogicalOperator = token.IsKind(SyntaxKind.AmpersandAmpersandToken) 
            || token.IsKind(SyntaxKind.BarBarToken) 
            || token.IsKind(SyntaxKind.AndKeyword) 
            || token.IsKind(SyntaxKind.OrKeyword);
        // If it's a logical operator within a binary expression or pattern, wrap it
        return isLogicalOperator 
            && (token.Parent is BinaryExpressionSyntax 
            or BinaryPatternSyntax)
            ? token.WithLeadingTrivia(SyntaxFactory.TriviaList(_newline).AddRange(_indent)).WithTrailingTrivia(SyntaxFactory.Space)
            : base.VisitToken(token);
    }
}