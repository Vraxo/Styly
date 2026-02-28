using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Styly.Configuration;

namespace Styly.Rewriters;

internal class LogicalExpressionRewriter : CSharpSyntaxRewriter
{
    private readonly LogicalExpressionOptions _options;
    private const int IndentSize = 4;
    public LogicalExpressionRewriter(LogicalExpressionOptions options)
    {
        _options = options;
    }

    public override SyntaxNode? VisitBinaryExpression(BinaryExpressionSyntax node)
    {
        if (_options.Style != LogicalExpressionStyle.MultiLine 
            || !IsLogical(node.Kind()))
        {
            return base.VisitBinaryExpression(node);
        }

        // Only process from the root of a logical chain to avoid redundant work
        return IsLogical(node.Parent?.Kind() ?? SyntaxKind.None)
            ? base.VisitBinaryExpression(node)
            : FormatChain(node);
    }

    public override SyntaxNode? VisitBinaryPattern(BinaryPatternSyntax node)
    {
        if (_options.Style != LogicalExpressionStyle.MultiLine)
        {
            return base.VisitBinaryPattern(node);
        }

        // Only process from the root of a pattern chain
        return node.Parent is BinaryPatternSyntax
            ? base.VisitBinaryPattern(node)
            : FormatChain(node);
    }

    private static SyntaxNode FormatChain(SyntaxNode root)
    {
        SyntaxTriviaList parentIndent = GetParentIndentation(root);
        SyntaxTriviaList itemIndent = parentIndent.Add(SyntaxFactory.Whitespace(new string (' ', IndentSize)));
        // Delegate the actual token wrapping to the specialized visitor
        return new LogicalTriviaApplier(itemIndent).Visit(root);
    }

    private static bool IsLogical(SyntaxKind kind)
    {
        return kind is SyntaxKind.LogicalAndExpression 
            or SyntaxKind.LogicalOrExpression;
    }

    private static SyntaxTriviaList GetParentIndentation(SyntaxNode node)
    {
        SyntaxNode? container = node.FirstAncestorOrSelf<SyntaxNode>(n => n is StatementSyntax 
            or MemberDeclarationSyntax);

        if (container is null)
        {
            return SyntaxFactory.TriviaList();
        }

        SyntaxTriviaList leading = container.GetLeadingTrivia();
        SyntaxTrivia lastWhitespace = leading.LastOrDefault(t => t.IsKind(SyntaxKind.WhitespaceTrivia));

        return !lastWhitespace.IsKind(SyntaxKind.None)
            ? SyntaxFactory.TriviaList(lastWhitespace)
            : SyntaxFactory.TriviaList();
    }
}