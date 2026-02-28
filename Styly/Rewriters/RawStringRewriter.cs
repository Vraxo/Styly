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

    [GeneratedRegex(@"\r?\n")]
    private static partial Regex MyRegex1();

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
        if (IsRawString(node))
        {
            return base.VisitLiteralExpression(node);
        }

        string value = node.Token.ValueText;
        // Only convert if the string contains actual newline characters
        return value.Contains('\n') || value.Contains('\r')
            ? ConvertToRawString(node, value)
            : base.VisitLiteralExpression(node);
    }

    private static bool IsRawString(LiteralExpressionSyntax node)
    {
        return node.Token.IsKind(SyntaxKind.MultiLineRawStringLiteralToken) || node.Token.IsKind(SyntaxKind.SingleLineRawStringLiteralToken);
    }

    private static LiteralExpressionSyntax ConvertToRawString(LiteralExpressionSyntax node, string value)
    {
        // 1. Determine delimiter depth (min 3 quotes)
        string delimiter = GetDelimiter(value);

        // 2. Get statement indentation to align the delimiter
        SyntaxTriviaList parentIndent = GetParentIndentation(node);
        string indentStr = parentIndent.ToString();
        string contentIndent = indentStr + new string(' ', IndentSize);

        // 3. Construct the raw string block
        // The closing delimiter must be aligned with the parent statement.
        // The content lines are indented relative to that delimiter.
        StringBuilder sb = GetRawStringBlock(value, delimiter, indentStr, contentIndent);

        // 4. Parse the generated text into a valid RawStringLiteralToken
        return GetRawStringLiteralToken(node, sb);
    }

    private static LiteralExpressionSyntax GetRawStringLiteralToken(LiteralExpressionSyntax node, StringBuilder sb)
    {
        SyntaxToken rawToken = SyntaxFactory.ParseToken(sb.ToString());

        return SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, rawToken)
            .WithLeadingTrivia(node.GetLeadingTrivia())
            .WithTrailingTrivia(node.GetTrailingTrivia());
    }

    private static StringBuilder GetRawStringBlock(string value, string delimiter, string indentStr, string contentIndent)
    {
        string[] lines = MyRegex1().Split(value);
        StringBuilder sb = new();
        sb.AppendLine(delimiter);

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                sb.AppendLine();
                continue;
            }

            sb.Append(contentIndent);
            sb.AppendLine(line);
        }

        sb.Append(indentStr);
        sb.Append(delimiter);

        return sb;
    }

    private static string GetDelimiter(string value)
    {
        int maxSequentialQuotes = GetMaxSequentialQuotes(value);
        int quotesNeeded = int.Max(3, maxSequentialQuotes + 1);
        string delimiter = new('"', quotesNeeded);

        return delimiter;
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
        var container = node.FirstAncestorOrSelf<SyntaxNode>(n => n is StatementSyntax or MemberDeclarationSyntax);

        if (container is null)
        {
            return SyntaxFactory.TriviaList();
        }

        SyntaxTriviaList leading = container.GetLeadingTrivia();
        SyntaxTrivia lastWhitespace = leading.LastOrDefault(t => t.IsKind(SyntaxKind.WhitespaceTrivia));

        if (lastWhitespace.IsKind(SyntaxKind.None))
        {
            return SyntaxFactory.TriviaList();
        }

        return SyntaxFactory.TriviaList(lastWhitespace);
    }
}