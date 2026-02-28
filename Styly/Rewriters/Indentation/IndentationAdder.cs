using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Styly.Rewriters.Indentation;

internal class IndentationAdder : CSharpSyntaxRewriter
{
    private const string Indent = "    ";
    public override SyntaxToken VisitToken(SyntaxToken token)
    {
        SyntaxToken processedToken = token;

        if (processedToken.HasLeadingTrivia)
        {
            processedToken = processedToken.WithLeadingTrivia(ProcessTriviaList(processedToken.LeadingTrivia));
        }

        if (processedToken.HasTrailingTrivia)
        {
            processedToken = processedToken.WithTrailingTrivia(ProcessTriviaList(processedToken.TrailingTrivia));
        }

        return processedToken;
    }

    private static SyntaxTriviaList ProcessTriviaList(SyntaxTriviaList originalTrivia)
    {
        List<SyntaxTrivia> newTrivia = [];

        for (int i = 0; i < originalTrivia.Count; i++)
        {
            SyntaxTrivia current = originalTrivia[i];
            newTrivia.Add(current);

            if (!current.IsKind(SyntaxKind.EndOfLineTrivia))
            {
                continue;
            }

            // We just added a newline. We need to add indentation now.
            // Check if the NEXT trivia in the original list is whitespace.
            if (i + 1 < originalTrivia.Count 
                && originalTrivia[i + 1].IsKind(SyntaxKind.WhitespaceTrivia))
            {
                // The next trivia is whitespace (existing indentation).
                // We combine our indent with it.
                SyntaxTrivia existingWhitespace = originalTrivia[i + 1];
                string combinedIndent = existingWhitespace.ToString() + Indent;

                newTrivia.Add(SyntaxFactory.Whitespace(combinedIndent));
                // Skip the next trivia since we've merged it.
                i++;
            }
            else
            {
                // The next trivia is NOT whitespace (or we are at the end of the list).
                // Just insert our indent.
                newTrivia.Add(SyntaxFactory.Whitespace(Indent));
            }
        }

        return SyntaxFactory.TriviaList(newTrivia);
    }
}