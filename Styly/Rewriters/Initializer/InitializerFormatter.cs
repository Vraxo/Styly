using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Styly.Rewriters.Initializer;

internal static class InitializerFormatter
{
    private const int IndentSize = 4;
    public static bool HasComments(SyntaxNode node)
    {
        return node.DescendantTrivia().Any(t =>
        {
            return t.IsKind(SyntaxKind.SingleLineCommentTrivia) || t.IsKind(SyntaxKind.MultiLineCommentTrivia);
        });
    }

    public static SyntaxTriviaList GetParentIndentation(SyntaxNode node)
    {
        SyntaxNode? container = node.FirstAncestorOrSelf<SyntaxNode>(n => n is StatementSyntax or MemberDeclarationSyntax);

        if (container is not null)
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

    private static T RemoveWhitespaceOnly<T>(T node)
        where T : SyntaxNode
    {
        // Remove leading whitespace, keep comments
        SyntaxTriviaList leading = node.GetLeadingTrivia();

        IEnumerable<SyntaxTrivia> newLeading = leading.Where(t =>
        {
            return !t.IsKind(SyntaxKind.WhitespaceTrivia) && !t.IsKind(SyntaxKind.EndOfLineTrivia);
        });
        // Remove trailing whitespace, keep comments
        SyntaxTriviaList trailing = node.GetTrailingTrivia();

        IEnumerable<SyntaxTrivia> newTrailing = trailing.Where(t =>
        {
            return !t.IsKind(SyntaxKind.WhitespaceTrivia) && !t.IsKind(SyntaxKind.EndOfLineTrivia);
        });

        return node.WithLeadingTrivia(newLeading).WithTrailingTrivia(newTrailing);
    }

    public static SeparatedSyntaxList<T> FormatListSingleLine<T>(SeparatedSyntaxList<T> items)
        where T : SyntaxNode
    {
        // Use RemoveWhitespaceOnly instead of WithoutLeadingTrivia/WithoutTrailingTrivia to preserve comments.
        IEnumerable<T> nodes = items.Select(i => RemoveWhitespaceOnly(i));

        IEnumerable<SyntaxToken> separators = Enumerable.Repeat(SyntaxFactory
            .Token(SyntaxKind.CommaToken)
            .WithTrailingTrivia(SyntaxFactory.Space), items.Count - 1);

        return SyntaxFactory.SeparatedList(nodes, separators);
    }

    public static SeparatedSyntaxList<T> FormatListMultiLine<T>(SeparatedSyntaxList<T> items, SyntaxTriviaList parentIndent)
        where T : SyntaxNode
    {
        SyntaxTriviaList itemIndent = parentIndent.Add(SyntaxFactory.Whitespace(new string (' ', IndentSize)));
        SyntaxTrivia newline = SyntaxFactory.CarriageReturnLineFeed;

        IEnumerable<T> nodes = items.Select(i =>
        {
            return i
                .WithoutLeadingTrivia()
                .WithoutTrailingTrivia()
                .WithLeadingTrivia(itemIndent);
        });

        IEnumerable<SyntaxToken> separators = Enumerable.Repeat(SyntaxFactory
            .Token(SyntaxKind.CommaToken)
            .WithTrailingTrivia(newline), items.Count - 1);

        return SyntaxFactory.SeparatedList(nodes, separators);
    }

    public static TNode StripPrecedingTrivia<TNode>(TNode node)
        where TNode : SyntaxNode
    {
        if (node is ObjectCreationExpressionSyntax oce)
        {
            return (TNode)(object)(oce.ArgumentList is not null
                ? oce.WithArgumentList(oce.ArgumentList.WithoutTrailingTrivia())
                : oce.WithType(oce.Type.WithoutTrailingTrivia()));
        }

        if (node is ImplicitObjectCreationExpressionSyntax ioce)
        {
            return (TNode)(object)ioce.WithArgumentList(ioce.ArgumentList.WithoutTrailingTrivia());
        }

        if (node is ArrayCreationExpressionSyntax ace)
        {
            return (TNode)(object)ace.WithType(ace.Type.WithoutTrailingTrivia());
        }

        if (node is ImplicitArrayCreationExpressionSyntax iace)
        {
            return (TNode)(object)iace.WithCloseBracketToken(iace.CloseBracketToken.WithTrailingTrivia(SyntaxFactory.TriviaList()));
        }

        return node;
    }

    public static SyntaxToken FormatOpenBraceSingleLine(SyntaxToken openBrace)
    {
        // Ensure space around brace, but preserve existing comments.
        // Flatten leading/trailing to remove newlines, but keep non-whitespace.
        IEnumerable<SyntaxTrivia> leading = openBrace.LeadingTrivia.Where(t =>
        {
            return !t.IsKind(SyntaxKind.WhitespaceTrivia) && !t.IsKind(SyntaxKind.EndOfLineTrivia);
        });

        IEnumerable<SyntaxTrivia> trailing = openBrace.TrailingTrivia.Where(t =>
        {
            return !t.IsKind(SyntaxKind.WhitespaceTrivia) && !t.IsKind(SyntaxKind.EndOfLineTrivia);
        });
        // Start with a space, then comments
        SyntaxTriviaList newLeading = SyntaxFactory.TriviaList(SyntaxFactory.Space).AddRange(leading);
        // Ensure space inside the brace.
        // If there are comments (trailing trivia of open brace), ensure space before AND after them.
        SyntaxTriviaList newTrailing = SyntaxFactory.TriviaList(SyntaxFactory.Space).AddRange(trailing);

        if (trailing.Any())
        {
            newTrailing = newTrailing.Add(SyntaxFactory.Space);
        }

        return openBrace.WithLeadingTrivia(newLeading).WithTrailingTrivia(newTrailing);
    }

    public static SyntaxToken FormatCloseBraceSingleLine(SyntaxToken closeBrace)
    {
        IEnumerable<SyntaxTrivia> leading = closeBrace.LeadingTrivia.Where(t =>
        {
            return !t.IsKind(SyntaxKind.WhitespaceTrivia) && !t.IsKind(SyntaxKind.EndOfLineTrivia);
        });
        // Prepend space
        SyntaxTriviaList newLeading = SyntaxFactory.TriviaList(SyntaxFactory.Space).AddRange(leading);
        // If there are comments, ensure space after them as well (before the brace)
        if (leading.Any())
        {
            newLeading = newLeading.Add(SyntaxFactory.Space);
        }

        // Clean trailing trivia too (remove whitespace/newlines) to ensure compact formatting
        IEnumerable<SyntaxTrivia> trailing = closeBrace.TrailingTrivia.Where(t =>
        {
            return !t.IsKind(SyntaxKind.WhitespaceTrivia) & !t.IsKind(SyntaxKind.EndOfLineTrivia);
        });

        SyntaxTriviaList newTrailing = SyntaxFactory.TriviaList(trailing);

        return closeBrace.WithLeadingTrivia(newLeading).WithTrailingTrivia(newTrailing);
    }

    public static SyntaxToken FormatOpenBraceMultiLine(SyntaxToken openBrace, SyntaxTriviaList parentIndent)
    {
        SyntaxTriviaList trivia = SyntaxFactory.TriviaList(SyntaxFactory.CarriageReturnLineFeed).AddRange(parentIndent);

        return openBrace.WithLeadingTrivia(trivia).WithTrailingTrivia(SyntaxFactory.CarriageReturnLineFeed);
    }

    public static SyntaxToken FormatCloseBraceMultiLine(SyntaxToken closeBrace, SyntaxTriviaList parentIndent)
    {
        SyntaxTriviaList trivia = SyntaxFactory.TriviaList(SyntaxFactory.CarriageReturnLineFeed).AddRange(parentIndent);

        return closeBrace.WithLeadingTrivia(trivia);
    }
}