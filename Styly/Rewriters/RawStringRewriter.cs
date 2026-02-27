using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Styly.Configuration;

namespace Styly.Rewriters;

internal partial class RawStringRewriter : CSharpSyntaxRewriter
{
    private readonly RawStringsOptions _options;
    private const int IndentSize = 4;
    public RawStringRewriter(RawStringsOptions options)
    {
        _options = options;
    }

    public override SyntaxNode? VisitLiteralExpression(LiteralExpressionSyntax node)
    {
        if (!_options.PreferRawForMultiline || !node.IsKind(SyntaxKind.StringLiteralExpression))
        {
            return base.VisitLiteralExpression(node);
        }

        string value = node.Token.ValueText;

        // "Truly Multiline" check: must contain a newline
        return !value.Contains('\n') && !value.Contains('\r')
            ? base.VisitLiteralExpression(node)
            : ConvertToRawString(node, value);
    }

    private static SyntaxNode ConvertToRawString(LiteralExpressionSyntax node, string value)
    {
        // 1. Determine how many quotes we need (minimum 3)
        int maxSequentialQuotes = GetMaxSequentialQuotes(value);
        int quotesNeeded = Math.Max(3, maxSequentialQuotes + 1);
        string delimiter = new('"', quotesNeeded);

        // 2. Get indentation
        SyntaxTriviaList parentIndent = GetParentIndentation(node);
        string indentStr = parentIndent.ToString();
        string contentIndent = indentStr + new string (' ', IndentSize);

        // 3. Format lines
        string[] lines = MyRegex().Split(value);
        StringBuilder sb = new();
        _ = sb.AppendLine(delimiter);

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                _ = sb.AppendLine();
            }
            else
            {
                _ = sb.Append(contentIndent);
                _ = sb.AppendLine(line);
            }
        }

        _ = sb.Append(indentStr);
        _ = sb.Append(delimiter);

        // 4. Create the new token
        // We use ParseToken because constructing a RawStringLiteralToken manually is complex 
        // and requires internal Roslyn bits or very specific factory calls.
        SyntaxToken rawToken = SyntaxFactory.ParseToken(sb.ToString());

        return SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, rawToken).WithLeadingTrivia(node.GetLeadingTrivia()).WithTrailingTrivia(node.GetTrailingTrivia());
    }

    private static int GetMaxSequentialQuotes(string text)
    {
        int max = 0;
        int current = 0;

        foreach (char c in text)
        {
            if (c == '"')
            {
                current++;
                max = Math.Max(max, current);
            }
            else
            {
                current = 0;
            }
        }

        return max;
    }

    private static SyntaxTriviaList GetParentIndentation(SyntaxNode node)
    {
        SyntaxNode? container = node.FirstAncestorOrSelf<SyntaxNode>(n => n is StatementSyntax or MemberDeclarationSyntax);

        if (container != null)
        {
            SyntaxTriviaList leading = container.GetLeadingTrivia();
            SyntaxTrivia lastWhitespace = leading.LastOrDefault(t => t.IsKind(SyntaxKind.WhitespaceTrivia));

            if (!lastWhitespace.IsKind(SyntaxKind.None))
            {
                return SyntaxFactory.TriviaList(lastWhitespace);
            }
        }

        return SyntaxFactory.TriviaList();
    }

    [GeneratedRegex(@"\r?\n")]
    private static partial Regex MyRegex();
}