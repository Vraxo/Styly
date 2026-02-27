using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Styly.Configuration;

namespace Styly.Rewriters;

internal class VerticalRhythmRewriter : CSharpSyntaxRewriter
{
    private readonly SpacingOptions _options;
    public VerticalRhythmRewriter(SpacingOptions options)
    {
        _options = options;
    }

    public override SyntaxNode? VisitCompilationUnit(CompilationUnitSyntax node)
    {
        SyntaxList<MemberDeclarationSyntax> newMembers = ProcessList(node.Members, m => m is GlobalStatementSyntax g
            ? g.Statement
            : null);

        return base.VisitCompilationUnit(node.WithMembers(newMembers));
    }

    public override SyntaxNode? VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
    {
        SyntaxList<MemberDeclarationSyntax> newMembers = ProcessList(node.Members, _ => null);
        return base.VisitNamespaceDeclaration(node.WithMembers(newMembers));
    }

    public override SyntaxNode? VisitFileScopedNamespaceDeclaration(FileScopedNamespaceDeclarationSyntax node)
    {
        SyntaxList<MemberDeclarationSyntax> newMembers = ProcessList(node.Members, _ => null);
        return base.VisitFileScopedNamespaceDeclaration(node.WithMembers(newMembers));
    }

    public override SyntaxNode? VisitBlock(BlockSyntax node)
    {
        SyntaxList<StatementSyntax> newStatements = ProcessList(node.Statements, s => s);
        return base.VisitBlock(node.WithStatements(newStatements));
    }

    public override SyntaxNode? VisitSwitchSection(SwitchSectionSyntax node)
    {
        SyntaxList<StatementSyntax> newStatements = ProcessList(node.Statements, s => s);
        return base.VisitSwitchSection(node.WithStatements(newStatements));
    }

    private SyntaxList<T> ProcessList<T>(SyntaxList<T> items, Func<T, StatementSyntax?> getStatement)
        where T : SyntaxNode
    {
        if (items.Count < 2)
        {
            return items;
        }

        List<T> newItems = new List<T> { items[0] };

        for (int i = 1; i < items.Count; i++)
        {
            T prev = items[i - 1];
            T curr = items[i];

            StatementSyntax? prevStmt = getStatement(prev);
            StatementSyntax? currStmt = getStatement(curr);

            bool shouldGap = (_options.EmptyLineBeforeControlFlow 
                && IsControlFlow(currStmt)) 
                || (_options.EmptyLineAfterControlFlow 
                && IsControlFlow(prevStmt)) 
                || (_options.EmptyLineAroundMultiLineExpression 
                && (IsMultiLine(prevStmt) 
                || IsMultiLine(currStmt))) 
                || curr.HasAnnotations(LayoutAnnotator.PreserveBlankLineAnnotationKind);

            if (shouldGap)
            {
                curr = SpacingUtility.EnsureBlankLine(prev, curr);
            }

            newItems.Add(curr);
        }

        return SyntaxFactory.List(newItems);
    }

    private static bool IsControlFlow(StatementSyntax? s)
    {
        return s is IfStatementSyntax 
            or SwitchStatementSyntax 
            or WhileStatementSyntax 
            or DoStatementSyntax 
            or ForStatementSyntax 
            or ForEachStatementSyntax 
            or TryStatementSyntax 
            or LocalFunctionStatementSyntax;
    }

    private static bool IsMultiLine(StatementSyntax? s)
    {
        return s is not null 
            && s.DescendantNodes().Any(n => (n is InitializerExpressionSyntax 
            or CollectionExpressionSyntax 
            or AnonymousObjectCreationExpressionSyntax 
            or ConditionalExpressionSyntax) 
            && n.DescendantTrivia().Any(t => t.IsKind(SyntaxKind.EndOfLineTrivia)));
    }
}