using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Styly.Core;
using Styly.Rewriters.Indentation;

namespace Styly.Rewriters;

internal class NamespaceRewriter : CSharpSyntaxRewriter
{
    private readonly NamespaceFormat _format;
    public NamespaceRewriter(NamespaceFormat format)
    {
        _format = format;
    }

    public override SyntaxNode? VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
    {
        if (_format == NamespaceFormat.File && node.Parent is CompilationUnitSyntax)
        {
            IndentationRemover indentationRemover = new();
            SyntaxList<MemberDeclarationSyntax> unindentedMembers = [];

            foreach (MemberDeclarationSyntax member in node.Members)
            {
                unindentedMembers = unindentedMembers.Add((MemberDeclarationSyntax)indentationRemover.Visit(member));
            }

            FileScopedNamespaceDeclarationSyntax fileScopedNamespace = SyntaxFactory.FileScopedNamespaceDeclaration(node.AttributeLists, node.Modifiers, node.Name.WithoutTrailingTrivia(), node.Externs, node.Usings, unindentedMembers).WithLeadingTrivia(node.GetLeadingTrivia());

            fileScopedNamespace = fileScopedNamespace.WithNamespaceKeyword(node.NamespaceKeyword.WithTrailingTrivia(SyntaxFactory.Space));

            // Add a blank line after the semicolon to separate it from the first member.
            SyntaxToken semicolon = SyntaxFactory.Token(SyntaxKind.SemicolonToken).WithTrailingTrivia(SyntaxFactory.CarriageReturnLineFeed, SyntaxFactory.CarriageReturnLineFeed);

            fileScopedNamespace = fileScopedNamespace.WithSemicolonToken(semicolon);

            SyntaxTriviaList trailingTrivia = node.GetTrailingTrivia();
            int lastNonWhitespaceIndex = -1;

            for (int i = trailingTrivia.Count - 1; i >= 0; i--)
            {
                SyntaxTrivia trivia = trailingTrivia[i];

                if (!trivia.IsKind(SyntaxKind.WhitespaceTrivia) && !trivia.IsKind(SyntaxKind.EndOfLineTrivia))
                {
                    lastNonWhitespaceIndex = i;
                    break;
                }
            }

            SyntaxTriviaList newTrailingTrivia = SyntaxFactory.TriviaList(trailingTrivia.Take(lastNonWhitespaceIndex + 1));
            return fileScopedNamespace.WithTrailingTrivia(newTrailingTrivia);
        }

        return base.VisitNamespaceDeclaration(node);
    }

    public override SyntaxNode? VisitFileScopedNamespaceDeclaration(FileScopedNamespaceDeclarationSyntax node)
    {
        if (_format == NamespaceFormat.Block)
        {
            IndentationAdder indentationAdder = new();
            SyntaxList<UsingDirectiveSyntax> indentedUsings = [];
            SyntaxList<MemberDeclarationSyntax> indentedMembers = [];
            bool isFirstItemProcessed = false;

            // Process Usings
            foreach (UsingDirectiveSyntax u in node.Usings)
            {
                UsingDirectiveSyntax rewritten = (UsingDirectiveSyntax)indentationAdder.Visit(u);

                if (!isFirstItemProcessed)
                {
                    rewritten = FixFirstItemIndentation(rewritten);
                    isFirstItemProcessed = true;
                }
                else
                {
                    rewritten = EnsureIndentation(rewritten);
                }

                indentedUsings = indentedUsings.Add(rewritten);
            }

            // Process Members
            foreach (MemberDeclarationSyntax m in node.Members)
            {
                MemberDeclarationSyntax rewritten = (MemberDeclarationSyntax)indentationAdder.Visit(m);

                if (!isFirstItemProcessed && !indentedUsings.Any())
                {
                    rewritten = FixFirstItemIndentation(rewritten);
                    isFirstItemProcessed = true;
                }
                else
                {
                    rewritten = EnsureIndentation(rewritten);
                }

                indentedMembers = indentedMembers.Add(rewritten);
            }

            NamespaceDeclarationSyntax blockScopedNamespace = SyntaxFactory.NamespaceDeclaration(node.AttributeLists, node.Modifiers, node.Name, node.Externs, indentedUsings, indentedMembers).WithLeadingTrivia(node.GetLeadingTrivia()).WithTrailingTrivia(node.GetTrailingTrivia());

            blockScopedNamespace = blockScopedNamespace.WithNamespaceKeyword(blockScopedNamespace.NamespaceKeyword.WithTrailingTrivia(SyntaxFactory.Space));

            // Ensure braces are properly formatted with newlines
            blockScopedNamespace = blockScopedNamespace.WithOpenBraceToken(SyntaxFactory.Token(SyntaxKind.OpenBraceToken).WithLeadingTrivia(SyntaxFactory.CarriageReturnLineFeed).WithTrailingTrivia(SyntaxFactory.CarriageReturnLineFeed)).WithCloseBraceToken(SyntaxFactory.Token(SyntaxKind.CloseBraceToken).WithLeadingTrivia(SyntaxFactory.CarriageReturnLineFeed));

            return blockScopedNamespace;
        }

        return base.VisitFileScopedNamespaceDeclaration(node);
    }

    private static T FixFirstItemIndentation<T>(T node)
        where T : SyntaxNode
    {
        SyntaxToken firstToken = node.GetFirstToken();
        SyntaxTriviaList leading = firstToken.LeadingTrivia;

        // Strip all initial newlines and whitespace to ensure the item sits flush after the brace's newline.
        IEnumerable<SyntaxTrivia> content = leading.SkipWhile(t => t.IsKind(SyntaxKind.EndOfLineTrivia) || t.IsKind(SyntaxKind.WhitespaceTrivia));

        // Force a single indentation level
        SyntaxTriviaList newTrivia = SyntaxFactory.TriviaList(SyntaxFactory.Whitespace("    "));
        newTrivia = newTrivia.AddRange(content);

        return node.ReplaceToken(firstToken, firstToken.WithLeadingTrivia(newTrivia));
    }

    private static T EnsureIndentation<T>(T node)
        where T : SyntaxNode
    {
        SyntaxToken firstToken = node.GetFirstToken();

        // If the token doesn't start with a newline, IndentationAdder wouldn't have added indentation.
        // We explicitly add it here to ensure alignment within the block.
        if (!firstToken.LeadingTrivia.Any(t => t.IsKind(SyntaxKind.EndOfLineTrivia)))
        {
            SyntaxTriviaList newTrivia = firstToken.LeadingTrivia.Insert(0, SyntaxFactory.Whitespace("    "));
            return node.ReplaceToken(firstToken, firstToken.WithLeadingTrivia(newTrivia));
        }

        return node;
    }
}