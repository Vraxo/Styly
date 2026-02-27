using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Styly.Configuration;
using System.Text;
using System.Text.RegularExpressions;

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

        // CRITICAL: Do not process existing raw strings.
        // Re-processing raw strings changes their semantic value by baking in current indentation.
        if (node.Token.IsKind(SyntaxKind.MultiLineRawStringLiteralToken) ||
            node.Token.IsKind(SyntaxKind.SingleLineRawStringLiteralToken))
        {
            return base.VisitLiteralExpression(node);
        }

        string value = node.Token.ValueText;

        // Only convert if the string contains actual newline characters
        return !value.Contains('\n') && !value.Contains('\r') ? base.VisitLiteralExpression(node) : ConvertToRawString(node, value);
    }

    private static SyntaxNode ConvertToRawString(LiteralExpressionSyntax node, string value)
    {
        // 1. Determine delimiter depth (min 3 quotes)
        int maxSequentialQuotes = GetMaxSequentialQuotes(value);
        int quotesNeeded = Math.Max(3, maxSequentialQuotes + 1);
        string delimiter = new('"', quotesNeeded);

        // 2. Get statement indentation to align the delimiter
        SyntaxTriviaList parentIndent = GetParentIndentation(node);
        string indentStr = parentIndent.ToString();
        string contentIndent = indentStr + new string(' ', IndentSize);

        // 3. Construct the raw string block
        // The closing delimiter must be aligned with the parent statement.
        // The content lines are indented relative to that delimiter.
        string[] lines = MyRegex1().Split(value);
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

        // 4. Parse the generated text into a valid RawStringLiteralToken
        SyntaxToken rawToken = SyntaxFactory.ParseToken(sb.ToString());

        return SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, rawToken)
            .WithLeadingTrivia(node.GetLeadingTrivia())
            .WithTrailingTrivia(node.GetTrailingTrivia());
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
    private static partial Regex MyRegex1();
}