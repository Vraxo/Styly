using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Styly.Rewriters;

internal class LayoutAnnotator : CSharpSyntaxRewriter
{
    public const string SingleLineAnnotationKind = "Styly_SingleLine";
    public const string PreserveBlankLineAnnotationKind = "Styly_PreserveBlankLine";
    public const string MultiLineCallChainAnnotationKind = "Styly_MultiLineCallChain";
    private static bool IsSingleLine(SyntaxNode node)
    {
        FileLinePositionSpan lineSpan = node.GetLocation().GetLineSpan();
        return lineSpan.StartLinePosition.Line == lineSpan.EndLinePosition.Line;
    }

    private static SyntaxList<T> AnnotateBlankLines<T>(SyntaxList<T> items)
        where T : SyntaxNode
    {
        if (items.Count < 2)
        {
            return items;
        }

        List<T> newItems = new(items.Count) { items[0] };

        for (int i = 1; i < items.Count; i++)
        {
            T prev = items[i - 1];
            T curr = items[i];

            if (HasBlankLineBetween(prev, curr))
            {
                curr = curr.WithAdditionalAnnotations(new SyntaxAnnotation(PreserveBlankLineAnnotationKind));
            }

            newItems.Add(curr);
        }

        return SyntaxFactory.List(newItems);
    }

    private static bool HasBlankLineBetween(SyntaxNode prev, SyntaxNode curr)
    {
        int newlines = CountEndingNewlines(prev.GetTrailingTrivia()) + CountStartingNewlines(curr.GetLeadingTrivia());
        return newlines >= 2;
    }

    private static int CountEndingNewlines(SyntaxTriviaList trivia)
    {
        return trivia.Count(t => t.IsKind(SyntaxKind.EndOfLineTrivia));
    }

    private static int CountStartingNewlines(SyntaxTriviaList trivia)
    {
        return trivia.Count(t => t.IsKind(SyntaxKind.EndOfLineTrivia));
    }

    public override SyntaxNode? VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
    {
        // Annotate the root of a call chain if it is currently multi-line.
        // This allows 'Preserve' mode to restore the layout after NormalizeWhitespace flattens it.
        return node.Parent is not MemberAccessExpressionSyntax 
            && !IsSingleLine(node)
            ? base.VisitMemberAccessExpression(node)!.WithAdditionalAnnotations(new SyntaxAnnotation(MultiLineCallChainAnnotationKind))
            : base.VisitMemberAccessExpression(node);
    }

    public override SyntaxNode? VisitInitializerExpression(InitializerExpressionSyntax node)
    {
        return IsSingleLine(node)
            ? base.VisitInitializerExpression(node)!.WithAdditionalAnnotations(new SyntaxAnnotation(SingleLineAnnotationKind))
            : base.VisitInitializerExpression(node);
    }

    public override SyntaxNode? VisitCollectionExpression(CollectionExpressionSyntax node)
    {
        return IsSingleLine(node)
            ? base.VisitCollectionExpression(node)!.WithAdditionalAnnotations(new SyntaxAnnotation(SingleLineAnnotationKind))
            : base.VisitCollectionExpression(node);
    }

    public override SyntaxNode? VisitAnonymousObjectCreationExpression(AnonymousObjectCreationExpressionSyntax node)
    {
        return IsSingleLine(node)
            ? base.VisitAnonymousObjectCreationExpression(node)!.WithAdditionalAnnotations(new SyntaxAnnotation(SingleLineAnnotationKind))
            : base.VisitAnonymousObjectCreationExpression(node);
    }

    public override SyntaxNode? VisitCompilationUnit(CompilationUnitSyntax node)
    {
        CompilationUnitSyntax visited = (CompilationUnitSyntax)base.VisitCompilationUnit(node)!;
        return visited.WithMembers(AnnotateBlankLines(visited.Members));
    }

    public override SyntaxNode? VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
    {
        NamespaceDeclarationSyntax visited = (NamespaceDeclarationSyntax)base.VisitNamespaceDeclaration(node)!;
        return visited.WithMembers(AnnotateBlankLines(visited.Members));
    }

    public override SyntaxNode? VisitFileScopedNamespaceDeclaration(FileScopedNamespaceDeclarationSyntax node)
    {
        FileScopedNamespaceDeclarationSyntax visited = (FileScopedNamespaceDeclarationSyntax)base.VisitFileScopedNamespaceDeclaration(node)!;
        return visited.WithMembers(AnnotateBlankLines(visited.Members));
    }

    public override SyntaxNode? VisitBlock(BlockSyntax node)
    {
        BlockSyntax visited = (BlockSyntax)base.VisitBlock(node)!;
        return visited.WithStatements(AnnotateBlankLines(visited.Statements));
    }

    public override SyntaxNode? VisitSwitchSection(SwitchSectionSyntax node)
    {
        SwitchSectionSyntax visited = (SwitchSectionSyntax)base.VisitSwitchSection(node)!;
        return visited.WithStatements(AnnotateBlankLines(visited.Statements));
    }

    public override SyntaxNode? VisitClassDeclaration(ClassDeclarationSyntax node)
    {
        ClassDeclarationSyntax visited = (ClassDeclarationSyntax)base.VisitClassDeclaration(node)!;
        return visited.WithMembers(AnnotateBlankLines(visited.Members));
    }

    public override SyntaxNode? VisitStructDeclaration(StructDeclarationSyntax node)
    {
        StructDeclarationSyntax visited = (StructDeclarationSyntax)base.VisitStructDeclaration(node)!;
        return visited.WithMembers(AnnotateBlankLines(visited.Members));
    }

    public override SyntaxNode? VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
    {
        InterfaceDeclarationSyntax visited = (InterfaceDeclarationSyntax)base.VisitInterfaceDeclaration(node)!;
        return visited.WithMembers(AnnotateBlankLines(visited.Members));
    }

    public override SyntaxNode? VisitRecordDeclaration(RecordDeclarationSyntax node)
    {
        RecordDeclarationSyntax visited = (RecordDeclarationSyntax)base.VisitRecordDeclaration(node)!;
        return visited.WithMembers(AnnotateBlankLines(visited.Members));
    }
}