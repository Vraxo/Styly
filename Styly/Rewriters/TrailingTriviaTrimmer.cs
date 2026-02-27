using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Styly.Rewriters;

public class TrailingTriviaTrimmer : CSharpSyntaxRewriter
{
    public override SyntaxNode? VisitCompilationUnit(CompilationUnitSyntax node)
    {
        SyntaxToken lastToken = node.GetLastToken();

        if (lastToken.IsKind(SyntaxKind.None) || !lastToken.TrailingTrivia.Any())
        {
            return node;
        }

        int lastSignificantIndex = GetLastSignificantTriviaIndex(lastToken.TrailingTrivia);

        IEnumerable<SyntaxTrivia> triviaToKeep = lastToken.TrailingTrivia.Take(lastSignificantIndex + 1);
        SyntaxTriviaList newTrailingTrivia = SyntaxFactory.TriviaList(triviaToKeep);

        return newTrailingTrivia.SequenceEqual(lastToken.TrailingTrivia)
            ? node
            : node.ReplaceToken(lastToken, lastToken.WithTrailingTrivia(newTrailingTrivia));
    }

    private static int GetLastSignificantTriviaIndex(SyntaxTriviaList trailingTrivia)
    {
        for (int i = trailingTrivia.Count - 1; i >= 0; i--)
        {
            SyntaxTrivia trivia = trailingTrivia[i];

            if (!IsWhitespaceOrNewline(trivia))
            {
                return i;
            }
        }

        return -1;
    }

    private static bool IsWhitespaceOrNewline(SyntaxTrivia trivia)
    {
        return trivia.IsKind(SyntaxKind.EndOfLineTrivia) || trivia.IsKind(SyntaxKind.WhitespaceTrivia);
    }
}