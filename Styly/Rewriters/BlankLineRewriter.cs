using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Styly.Configuration;

namespace Styly.Rewriters;

internal class BlankLineRewriter : CSharpSyntaxRewriter
{
    private readonly SpacingOptions _options;
    public BlankLineRewriter(SpacingOptions options)
    {
        _options = options;
    }

    public override SyntaxNode? VisitCompilationUnit(CompilationUnitSyntax node)
    {
        // 1. Enforce spacing between members (e.g. Global Statements)
        SyntaxList<MemberDeclarationSyntax> members = node.Members;
        SyntaxList<MemberDeclarationSyntax> newMembers = ProcessList(members, GetStatement);

        if (members != newMembers)
        {
            node = node.WithMembers(newMembers);
        }

        // 2. Enforce spacing between Usings and the first Member
        node = EnsureUsingSeparator(node, node.Usings, node.Members, (n, m) => n.WithMembers(m));

        return base.VisitCompilationUnit(node);
    }

    public override SyntaxNode? VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
    {
        // 1. Enforce spacing between members inside the namespace
        SyntaxList<MemberDeclarationSyntax> members = node.Members;
        SyntaxList<MemberDeclarationSyntax> newMembers = ProcessList(members, GetStatement);

        if (members != newMembers)
        {
            node = node.WithMembers(newMembers);
        }

        // 2. Enforce spacing between Usings and the first Member
        node = EnsureUsingSeparator(node, node.Usings, node.Members, (n, m) => n.WithMembers(m));

        return base.VisitNamespaceDeclaration(node);
    }

    public override SyntaxNode? VisitFileScopedNamespaceDeclaration(FileScopedNamespaceDeclarationSyntax node)
    {
        // 1. Enforce spacing between members inside the namespace
        SyntaxList<MemberDeclarationSyntax> members = node.Members;
        SyntaxList<MemberDeclarationSyntax> newMembers = ProcessList(members, GetStatement);

        if (members != newMembers)
        {
            node = node.WithMembers(newMembers);
        }

        // 2. Enforce spacing between Usings and the first Member
        node = EnsureUsingSeparator(node, node.Usings, node.Members, (n, m) => n.WithMembers(m));

        // 3. If no usings exist, enforce spacing between the namespace semicolon and the first member
        if (!node.Usings.Any() && node.Members.Any())
        {
            MemberDeclarationSyntax firstMember = node.Members[0];
            MemberDeclarationSyntax newFirstMember = EnsureBlankLine(node.SemicolonToken, firstMember);

            if (newFirstMember != firstMember)
            {
                node = node.WithMembers(node.Members.Replace(firstMember, newFirstMember));
            }
        }

        return base.VisitFileScopedNamespaceDeclaration(node);
    }

    public override SyntaxNode? VisitBlock(BlockSyntax node)
    {
        SyntaxList<StatementSyntax> statements = node.Statements;
        SyntaxList<StatementSyntax> newStatements = ProcessList(statements, s => s);

        if (statements != newStatements)
        {
            node = node.WithStatements(newStatements);
        }

        return base.VisitBlock(node);
    }

    public override SyntaxNode? VisitSwitchSection(SwitchSectionSyntax node)
    {
        SyntaxList<StatementSyntax> statements = node.Statements;
        SyntaxList<StatementSyntax> newStatements = ProcessList(statements, s => s);

        if (statements != newStatements)
        {
            node = node.WithStatements(newStatements);
        }

        return base.VisitSwitchSection(node);
    }

    // --- Helpers ---
    private static TNode EnsureUsingSeparator<TNode>(TNode node, SyntaxList<UsingDirectiveSyntax> usings, SyntaxList<MemberDeclarationSyntax> members, Func<TNode, SyntaxList<MemberDeclarationSyntax>, TNode> withMembers)
        where TNode : SyntaxNode
    {
        if (!usings.Any() || !members.Any())
        {
            return node;
        }

        UsingDirectiveSyntax lastUsing = usings.Last();
        MemberDeclarationSyntax firstMember = members.First();

        MemberDeclarationSyntax newFirstMember = EnsureBlankLine(lastUsing, firstMember);

        return newFirstMember != firstMember
            ? withMembers(node, members.Replace(firstMember, newFirstMember))
            : node;
    }

    private SyntaxList<T> ProcessList<T>(SyntaxList<T> items, Func<T, StatementSyntax?> getStatement)
        where T : SyntaxNode
    {
        if (items.Count < 2)
        {
            return items;
        }

        List<T> newItems = new(items.Count) { items[0] };

        for (int i = 1; i < items.Count; i++)
        {
            T prevItem = items[i - 1];
            T currentItem = items[i];

            StatementSyntax? prevStmt = getStatement(prevItem);
            StatementSyntax? currStmt = getStatement(currentItem);

            bool isPrevControlFlow = prevStmt != null && IsControlFlowStatement(prevStmt);
            bool isCurrControlFlow = currStmt != null && IsControlFlowStatement(currStmt);

            bool isPrevMultiLineInit = prevStmt != null && ContainsMultiLineInitializer(prevStmt);
            bool isCurrMultiLineInit = currStmt != null && ContainsMultiLineInitializer(currStmt);

            bool hasPreserveAnnotation = currentItem.HasAnnotations(LayoutAnnotator.PreserveBlankLineAnnotationKind);

            bool shouldEnsure = (_options.EmptyLineBeforeControlFlow && isCurrControlFlow) || (_options.EmptyLineAfterControlFlow && isPrevControlFlow) || isPrevMultiLineInit || isCurrMultiLineInit || hasPreserveAnnotation;

            if (shouldEnsure)
            {
                currentItem = EnsureBlankLine(prevItem, currentItem);
            }

            newItems.Add(currentItem);
        }

        return SyntaxFactory.List(newItems);
    }

    private static StatementSyntax? GetStatement(MemberDeclarationSyntax member)
    {
        return member is GlobalStatementSyntax global
            ? global.Statement
            : null;
    }

    private static bool IsControlFlowStatement(StatementSyntax statement)
    {
        return statement is IfStatementSyntax or SwitchStatementSyntax or WhileStatementSyntax or DoStatementSyntax or ForStatementSyntax or ForEachStatementSyntax or TryStatementSyntax or LocalFunctionStatementSyntax;
    }

    private static bool ContainsMultiLineInitializer(StatementSyntax statement)
    {
        IEnumerable<SyntaxNode> nodes = statement.DescendantNodes().Where(n => n is InitializerExpressionSyntax or CollectionExpressionSyntax or AnonymousObjectCreationExpressionSyntax);

        foreach (SyntaxNode? node in nodes)
        {
            if (node.DescendantTrivia().Any(t => t.IsKind(SyntaxKind.EndOfLineTrivia)))
            {
                return true;
            }
        }

        return false;
    }

    private static TNode EnsureBlankLine<TNode>(SyntaxNode prev, TNode curr)
        where TNode : SyntaxNode
    {
        return EnsureBlankLine(prev.GetLastToken(), curr);
    }

    private static TNode EnsureBlankLine<TNode>(SyntaxToken prevToken, TNode curr)
        where TNode : SyntaxNode
    {
        SyntaxTriviaList prevTrailing = prevToken.TrailingTrivia;
        SyntaxTriviaList currLeading = curr.GetLeadingTrivia();

        int newlineCount = CountEndingNewlines(prevTrailing) + CountStartingNewlines(currLeading);

        return newlineCount < 2
            ? curr.WithLeadingTrivia(currLeading.Insert(0, SyntaxFactory.CarriageReturnLineFeed))
            : curr;
    }

    private static int CountEndingNewlines(SyntaxTriviaList trivia)
    {
        int count = 0;

        for (int i = trivia.Count - 1; i >= 0; i--)
        {
            SyntaxKind kind = trivia[i].Kind();

            if (kind == SyntaxKind.EndOfLineTrivia)
            {
                count++;
            }
            else if (kind != SyntaxKind.WhitespaceTrivia)
            {
                break;
            }
        }

        return count;
    }

    private static int CountStartingNewlines(SyntaxTriviaList trivia)
    {
        int count = 0;

        for (int i = 0; i < trivia.Count; i++)
        {
            SyntaxKind kind = trivia[i].Kind();

            if (kind == SyntaxKind.EndOfLineTrivia)
            {
                count++;
            }
            else if (kind != SyntaxKind.WhitespaceTrivia)
            {
                break;
            }
        }

        return count;
    }
}