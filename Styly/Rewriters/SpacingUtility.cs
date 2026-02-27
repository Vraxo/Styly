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
        return newlineCount < 2
            ? curr.WithLeadingTrivia(curr.GetLeadingTrivia().Insert(0, SyntaxFactory.CarriageReturnLineFeed))
            : curr;
    }

    private static int CountNewlines(SyntaxTriviaList trivia)
    {
        int count = 0;
        foreach (SyntaxTrivia t in trivia)
        {
            if (t.IsKind(SyntaxKind.EndOfLineTrivia))
            {
                count++;
            }
        }
        return count;
    }
}