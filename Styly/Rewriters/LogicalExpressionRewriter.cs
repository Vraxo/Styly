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
        if (_options.Style != LogicalExpressionStyle.MultiLine || !IsLogical(node.Kind()))
        {
            return base.VisitBinaryExpression(node);
        }

        // Only process from the root of a logical chain
        return IsLogical(node.Parent?.Kind() ?? SyntaxKind.None) ? base.VisitBinaryExpression(node) : FormatChain(node);
    }

    public override SyntaxNode? VisitBinaryPattern(BinaryPatternSyntax node)
    {
        if (_options.Style != LogicalExpressionStyle.MultiLine)
        {
            return base.VisitBinaryPattern(node);
        }

        // Only process from the root of a pattern chain
        return node.Parent is BinaryPatternSyntax ? base.VisitBinaryPattern(node) : FormatChain(node);
    }

    private static SyntaxNode FormatChain(SyntaxNode root)
    {
        SyntaxTriviaList parentIndent = GetParentIndentation(root);
        SyntaxTriviaList itemIndent = parentIndent.Add(SyntaxFactory.Whitespace(new string(' ', IndentSize)));
        SyntaxTrivia newline = SyntaxFactory.CarriageReturnLineFeed;

        // Flatten the tree into operands and operators
        List<SyntaxNode> parts = new List<SyntaxNode>();
        Flatten(root, parts);

        if (parts.Count < 2)
        {
            return root;
        }

        // The first part stays flush or keeps original leading trivia
        _ = parts[0];

        for (int i = 1; i < parts.Count; i++)
        {
            SyntaxNode part = parts[i];

            if (part is SyntaxToken token)
            {
                // Move operator to a new line with indentation
                _ = token
                    .WithLeadingTrivia(SyntaxFactory.TriviaList(newline).AddRange(itemIndent))
                    .WithTrailingTrivia(SyntaxFactory.Space);

                // Reconstruct (this is a simplified logic for demo, 
                // in reality we'd rebuild the specific BinaryExpression or Pattern tree)
                // For a robust implementation, we wrap tokens and operands.
            }
        }

        // Simplified approach: Visit children and apply trivia to operators
        return new LogicalTriviaApplier(itemIndent).Visit(root);
    }

    private static bool IsLogical(SyntaxKind kind)
    {
        return kind is SyntaxKind.LogicalAndExpression or SyntaxKind.LogicalOrExpression;
    }

    private static SyntaxTriviaList GetParentIndentation(SyntaxNode node)
    {
        SyntaxNode? container = node.FirstAncestorOrSelf<SyntaxNode>(n => n is StatementSyntax or MemberDeclarationSyntax);
        if (container != null)
        {
            SyntaxTrivia lastWhitespace = container.GetLeadingTrivia().LastOrDefault(t => t.IsKind(SyntaxKind.WhitespaceTrivia));
            if (!lastWhitespace.IsKind(SyntaxKind.None))
            {
                return SyntaxFactory.TriviaList(lastWhitespace);
            }
        }
        return SyntaxFactory.TriviaList();
    }

    private static void Flatten(SyntaxNode node, List<SyntaxNode> parts)
    {
        if (node is BinaryExpressionSyntax bin && IsLogical(bin.Kind()))
        {
            Flatten(bin.Left, parts);
            parts.Add(bin.OperatorToken);
            Flatten(bin.Right, parts);
        }
        else if (node is BinaryPatternSyntax pat)
        {
            Flatten(pat.Left, parts);
            parts.Add(pat.OperatorToken);
            Flatten(pat.Right, parts);
        }
        else
        {
            parts.Add(node);
        }
    }

    private class LogicalTriviaApplier : CSharpSyntaxRewriter
    {
        private readonly SyntaxTriviaList _indent;
        public LogicalTriviaApplier(SyntaxTriviaList indent)
        {
            _indent = indent;
        }

        public override SyntaxToken VisitToken(SyntaxToken token)
        {
            if (token.IsKind(SyntaxKind.AmpersandAmpersandToken) ||
                token.IsKind(SyntaxKind.BarBarToken) ||
                token.IsKind(SyntaxKind.AndKeyword) ||
                token.IsKind(SyntaxKind.OrKeyword))
            {
                if (token.Parent is BinaryExpressionSyntax or BinaryPatternSyntax)
                {
                    return token
                        .WithLeadingTrivia(SyntaxFactory.TriviaList(SyntaxFactory.CarriageReturnLineFeed).AddRange(_indent))
                        .WithTrailingTrivia(SyntaxFactory.Space);
                }
            }
            return base.VisitToken(token);
        }
    }
}