using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Styly.Rewriters;

internal class UsingSorterRewriter : CSharpSyntaxRewriter
{
    private static int CompareUsings(UsingDirectiveSyntax a, UsingDirectiveSyntax b)
    {
        string nameA = a.Name?.ToString() ?? string.Empty;
        string nameB = b.Name?.ToString() ?? string.Empty;

        bool isSystemA = nameA.StartsWith("System");
        bool isSystemB = nameB.StartsWith("System");

        return isSystemA != isSystemB
            ? isSystemA ? -1 : 1
            : string.Compare(nameA, nameB, StringComparison.Ordinal);
    }

    public override SyntaxNode? VisitCompilationUnit(CompilationUnitSyntax node)
    {
        CompilationUnitSyntax visited = (CompilationUnitSyntax)base.VisitCompilationUnit(node)!;

        if (!visited.Usings.Any())
        {
            return visited;
        }

        SyntaxList<UsingDirectiveSyntax> sortedUsings = ProcessUsings(visited.Usings);
        return visited.WithUsings(sortedUsings);
    }

    public override SyntaxNode? VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
    {
        NamespaceDeclarationSyntax visited = (NamespaceDeclarationSyntax)base.VisitNamespaceDeclaration(node)!;

        if (!visited.Usings.Any())
        {
            return visited;
        }

        SyntaxList<UsingDirectiveSyntax> sortedUsings = ProcessUsings(visited.Usings);
        return visited.WithUsings(sortedUsings);
    }

    public override SyntaxNode? VisitFileScopedNamespaceDeclaration(FileScopedNamespaceDeclarationSyntax node)
    {
        FileScopedNamespaceDeclarationSyntax visited = (FileScopedNamespaceDeclarationSyntax)base.VisitFileScopedNamespaceDeclaration(node)!;

        if (!visited.Usings.Any())
        {
            return visited;
        }

        SyntaxList<UsingDirectiveSyntax> sortedUsings = ProcessUsings(visited.Usings);
        return visited.WithUsings(sortedUsings);
    }

    private static SyntaxList<UsingDirectiveSyntax> ProcessUsings(SyntaxList<UsingDirectiveSyntax> usings)
    {
        // 1. Sort the list
        List<UsingDirectiveSyntax> sortedList = [ ..usings ];

        if (sortedList.Count > 1)
        {
            sortedList.Sort(CompareUsings);
        }

        // 2. Preserve File Header (Leading trivia of the *original* first item)
        // We attach it to the new first item.
        SyntaxTriviaList originalFirstTrivia = usings.First().GetLeadingTrivia();
        sortedList[0] = sortedList[0].WithLeadingTrivia(originalFirstTrivia);
        // 3. Normalize spacing for all items
        for (int i = 0; i < sortedList.Count; i++)
        {
            UsingDirectiveSyntax current = sortedList[i];
            // A. Strip Leading Newlines (for items 1..N)
            // This prevents gaps if the previous item already has a trailing newline.
            // The first item (0) keeps its header (assigned above).
            if (i > 0)
            {
                current = StripLeadingNewlines(current);
            }

            // B. Set Trailing Newlines
            // Last item gets 2 newlines (blank line separator).
            // All other items get 1 newline (standard list).
            int requiredNewlines = (i == sortedList.Count - 1)
                ? 2
                : 1;

            current = SetTrailingNewlines(current, requiredNewlines);

            sortedList[i] = current;
        }

        return SyntaxFactory.List(sortedList);
    }

    private static UsingDirectiveSyntax StripLeadingNewlines(UsingDirectiveSyntax u)
    {
        SyntaxTriviaList leading = u.GetLeadingTrivia();
        IEnumerable<SyntaxTrivia> newLeading = leading.SkipWhile(t => t.IsKind(SyntaxKind.EndOfLineTrivia));
        return u.WithLeadingTrivia(SyntaxFactory.TriviaList(newLeading));
    }

    private static UsingDirectiveSyntax SetTrailingNewlines(UsingDirectiveSyntax node, int count)
    {
        SyntaxTriviaList trailing = node.GetTrailingTrivia();
        // Find the last significant trivia (comment or non-whitespace/newline).
        // We want to keep comments, but strip existing whitespace/newlines at the end.
        int lastIndexToKeep = -1;

        for (int i = trailing.Count - 1; i >= 0; i--)
        {
            if (!trailing[i].IsKind(SyntaxKind.WhitespaceTrivia) && !trailing[i].IsKind(SyntaxKind.EndOfLineTrivia))
            {
                lastIndexToKeep = i;
                break;
            }
        }

        SyntaxTriviaList newTrivia = SyntaxFactory.TriviaList(trailing.Take(lastIndexToKeep + 1));

        for (int i = 0; i < count; i++)
        {
            newTrivia = newTrivia.Add(SyntaxFactory.CarriageReturnLineFeed);
        }

        return node.WithTrailingTrivia(newTrivia);
    }
}