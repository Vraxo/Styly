using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Styly.Rewriters;

internal static class SpacingUtility
{
    public static TNode EnsureBlankLine<TNode>(SyntaxNode prev, TNode curr) where TNode : SyntaxNode
    {
        return EnsureBlankLine(prev.GetLastToken(), curr);
    }

    public static TNode EnsureBlankLine<TNode>(SyntaxToken prevToken, TNode curr) where TNode : SyntaxNode
    {
        int newlineCount = CountNewlines(prevToken.TrailingTrivia) + CountNewlines(curr.GetLeadingTrivia());

        // A blank line exists if there are at least 2 newline characters between tokens.
        return newlineCount < 2
            ? curr.WithLeadingTrivia(curr.GetLeadingTrivia().Insert(0, SyntaxFactory.CarriageReturnLineFeed))
            : curr;
    }

    private static int CountNewlines(SyntaxTriviaList trivia)
    {
        return trivia.Count(t => t.IsKind(SyntaxKind.EndOfLineTrivia));
    }
}