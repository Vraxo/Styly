using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Styly.Rewriters.Indentation;

internal class IndentationRemover : CSharpSyntaxRewriter
{
    private const int IndentSize = 4;
    public override SyntaxToken VisitToken(SyntaxToken token)
    {
        if (!token.HasLeadingTrivia)
        {
            return base.VisitToken(token);
        }

        SyntaxTriviaList newTrivia = AdjustLeadingTrivia(token.LeadingTrivia);
        return token.WithLeadingTrivia(newTrivia);
    }

    private static SyntaxTriviaList AdjustLeadingTrivia(SyntaxTriviaList trivia)
    {
        int lastNewlineIndex = FindLastNewlineIndex(trivia);

        return lastNewlineIndex != -1
            ? ReduceIndentationAfterNewline(trivia, lastNewlineIndex)
            : ReduceIndentationAtStart(trivia);
    }

    private static int FindLastNewlineIndex(SyntaxTriviaList trivia)
    {
        for (int i = trivia.Count - 1; i >= 0; i--)
        {
            if (trivia[i].IsKind(SyntaxKind.EndOfLineTrivia))
            {
                return i;
            }
        }

        return -1;
    }

    private static SyntaxTriviaList ReduceIndentationAfterNewline(SyntaxTriviaList trivia, int newlineIndex)
    {
        int indentIndex = newlineIndex + 1;

        if (indentIndex >= trivia.Count)
        {
            return trivia;
        }

        SyntaxTrivia candidate = trivia[indentIndex];

        return candidate.IsKind(SyntaxKind.WhitespaceTrivia)
            ? trivia.Replace(candidate, ReduceWhitespace(candidate))
            : trivia;
    }

    private static SyntaxTriviaList ReduceIndentationAtStart(SyntaxTriviaList trivia)
    {
        if (!trivia.Any())
        {
            return trivia;
        }

        SyntaxTrivia firstTrivia = trivia.First();

        return firstTrivia.IsKind(SyntaxKind.WhitespaceTrivia)
            ? trivia.Replace(firstTrivia, ReduceWhitespace(firstTrivia))
            : trivia;
    }

    private static SyntaxTrivia ReduceWhitespace(SyntaxTrivia whitespace)
    {
        string text = whitespace.ToString();

        string newText = text.Length >= IndentSize
            ? text[IndentSize..]
            : string.Empty;

        return SyntaxFactory.Whitespace(newText);
    }
}