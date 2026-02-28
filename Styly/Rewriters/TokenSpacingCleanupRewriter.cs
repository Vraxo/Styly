using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Styly.Rewriters;

internal class TokenSpacingCleanupRewriter : CSharpSyntaxRewriter
{
    public override SyntaxNode? Visit(SyntaxNode? node)
    {
        return node is null
            ? null
            : CleanupTokenSpacing(node);
    }

    private static SyntaxNode CleanupTokenSpacing(SyntaxNode root)
    {
        List<SyntaxToken> tokens = [ ..root.DescendantTokens() ];
        Dictionary<SyntaxToken, SyntaxToken> replacements = [];

        for (int i = 1; i < tokens.Count; i++)
        {
            SyntaxToken prev = tokens[i - 1];
            SyntaxToken current = tokens[i];

            if (IsUnwantedSpaceAfterBrace(prev, current))
            {
                replacements[current] = RemoveLeadingWhitespace(current);
            }
        }

        return replacements.Count != 0
            ? root.ReplaceTokens(replacements.Keys, (original, _) => replacements[original])
            : root;
    }

    private static bool IsUnwantedSpaceAfterBrace(SyntaxToken prev, SyntaxToken current)
    {
        return IsBraceFollowedByCloserOrSeparator(prev, current) 
            && !AreSeparatedByNewline(prev, current);
    }

    private static bool IsBraceFollowedByCloserOrSeparator(SyntaxToken prev, SyntaxToken current)
    {
        return prev.IsKind(SyntaxKind.CloseBraceToken) 
            && (current.IsKind(SyntaxKind.CloseParenToken) 
            || current.IsKind(SyntaxKind.CloseBracketToken) 
            || current.IsKind(SyntaxKind.SemicolonToken) 
            || current.IsKind(SyntaxKind.CommaToken));
    }

    private static bool AreSeparatedByNewline(SyntaxToken prev, SyntaxToken current)
    {
        return prev.TrailingTrivia.Any(t => t.IsKind(SyntaxKind.EndOfLineTrivia)) 
            || current.LeadingTrivia.Any(t => t.IsKind(SyntaxKind.EndOfLineTrivia));
    }

    private static SyntaxToken RemoveLeadingWhitespace(SyntaxToken token)
    {
        return token.LeadingTrivia.Any(t => t.IsKind(SyntaxKind.WhitespaceTrivia))
            ? token.WithLeadingTrivia(GetNonWhitespaceTrivia(token.LeadingTrivia))
            : token;
    }

    private static SyntaxTriviaList GetNonWhitespaceTrivia(SyntaxTriviaList trivia)
    {
        return SyntaxFactory.TriviaList(trivia.Where(t => !t.IsKind(SyntaxKind.WhitespaceTrivia)));
    }
}