using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Styly.Configuration;

namespace Styly.Rewriters.Initializer;

internal class InitializerRewriter : CSharpSyntaxRewriter
{
    private readonly InitializerOptions _options;
    private enum FormattingAction
    {
        None,
        SingleLine,
        MultiLine
    }

    public InitializerRewriter(InitializerOptions options)
    {
        _options = options;
    }

    /// <summary>
    /// Centralizes the logic for deciding whether to format as SingleLine, MultiLine, or Preserve (None).
    /// </summary>
    private static FormattingAction DetermineAction(InitializerStyle style, bool hasComments, bool wasSingleLine, bool hasItems)
    {
        if (!hasItems)
        {
            return FormattingAction.None;
        }

        // If comments exist, we generally preserve the layout to avoid breaking comment placement.
        if (hasComments)
        {
            // However, if it was originally single-line, NormalizeWhitespace likely expanded it awkwardly.
            // We force it back to SingleLine to recover the original intent while keeping comments inline.
            return wasSingleLine ? FormattingAction.SingleLine : FormattingAction.None;
        }

        // If no comments exist, we follow the configuration strictness.
        return style switch
        {
            InitializerStyle.SingleLine => FormattingAction.SingleLine,
            InitializerStyle.MultiLine => FormattingAction.MultiLine,
            InitializerStyle.Preserve => throw new NotImplementedException(),
            _ => wasSingleLine ? FormattingAction.SingleLine : FormattingAction.None, // In Preserve mode, we only enforce SingleLine if it was already single-line
        // (recovering from NormalizeWhitespace). Otherwise, we leave it alone.
        };
    }

    public override SyntaxNode? VisitAnonymousObjectCreationExpression(AnonymousObjectCreationExpressionSyntax node)
    {
        bool hasComments = InitializerFormatter.HasComments(node);
        bool wasSingleLine = node.HasAnnotations(LayoutAnnotator.SingleLineAnnotationKind);
        bool hasItems = node.Initializers.Any();

        FormattingAction action = DetermineAction(_options.AnonymousType, hasComments, wasSingleLine, hasItems);

        if (action == FormattingAction.SingleLine)
        {
            SeparatedSyntaxList<AnonymousObjectMemberDeclaratorSyntax> newMembers = InitializerFormatter.FormatListSingleLine(node.Initializers);

            AnonymousObjectCreationExpressionSyntax cleanNode = node.WithNewKeyword(node.NewKeyword.WithTrailingTrivia(SyntaxFactory.TriviaList()));

            return cleanNode.WithOpenBraceToken(InitializerFormatter.FormatOpenBraceSingleLine(cleanNode.OpenBraceToken)).WithInitializers(newMembers).WithCloseBraceToken(InitializerFormatter.FormatCloseBraceSingleLine(cleanNode.CloseBraceToken));
        }

        if (action == FormattingAction.MultiLine)
        {
            SyntaxTriviaList parentIndent = InitializerFormatter.GetParentIndentation(node);
            SeparatedSyntaxList<AnonymousObjectMemberDeclaratorSyntax> newMembers = InitializerFormatter.FormatListMultiLine(node.Initializers, parentIndent);

            AnonymousObjectCreationExpressionSyntax cleanNode = node.WithNewKeyword(node.NewKeyword.WithTrailingTrivia(SyntaxFactory.TriviaList()));

            return cleanNode.WithOpenBraceToken(InitializerFormatter.FormatOpenBraceMultiLine(cleanNode.OpenBraceToken, parentIndent)).WithInitializers(newMembers).WithCloseBraceToken(InitializerFormatter.FormatCloseBraceMultiLine(cleanNode.CloseBraceToken, parentIndent));
        }

        return base.VisitAnonymousObjectCreationExpression(node);
    }

    public override SyntaxNode? VisitCollectionExpression(CollectionExpressionSyntax node)
    {
        bool hasComments = InitializerFormatter.HasComments(node);
        bool wasSingleLine = node.HasAnnotations(LayoutAnnotator.SingleLineAnnotationKind);
        bool hasItems = node.Elements.Any();

        FormattingAction action = DetermineAction(_options.Collection, hasComments, wasSingleLine, hasItems);

        if (action == FormattingAction.SingleLine)
        {
            SeparatedSyntaxList<CollectionElementSyntax> newElements = InitializerFormatter.FormatListSingleLine(node.Elements);

            return node.WithOpenBracketToken(node.OpenBracketToken.WithLeadingTrivia(SyntaxFactory.TriviaList()).WithTrailingTrivia(SyntaxFactory.Space)).WithElements(newElements).WithCloseBracketToken(node.CloseBracketToken.WithLeadingTrivia(SyntaxFactory.Space));
        }

        if (action == FormattingAction.MultiLine)
        {
            SyntaxTriviaList parentIndent = InitializerFormatter.GetParentIndentation(node);
            SeparatedSyntaxList<CollectionElementSyntax> newElements = InitializerFormatter.FormatListMultiLine(node.Elements, parentIndent);

            return node.WithOpenBracketToken(InitializerFormatter.FormatOpenBraceMultiLine(node.OpenBracketToken, parentIndent)).WithElements(newElements).WithCloseBracketToken(InitializerFormatter.FormatCloseBraceMultiLine(node.CloseBracketToken, parentIndent));
        }

        return base.VisitCollectionExpression(node);
    }

    public override SyntaxNode? VisitEqualsValueClause(EqualsValueClauseSyntax node)
    {
        EqualsValueClauseSyntax visited = (EqualsValueClauseSyntax)base.VisitEqualsValueClause(node)!;
        return _options.Collection == InitializerStyle.MultiLine && visited.Value is CollectionExpressionSyntax ? visited.WithEqualsToken(visited.EqualsToken.WithTrailingTrivia(SyntaxFactory.TriviaList())) : visited;
    }

    public override SyntaxNode? VisitAssignmentExpression(AssignmentExpressionSyntax node)
    {
        AssignmentExpressionSyntax visited = (AssignmentExpressionSyntax)base.VisitAssignmentExpression(node)!;
        return _options.Collection == InitializerStyle.MultiLine && visited.Right is CollectionExpressionSyntax ? visited.WithOperatorToken(visited.OperatorToken.WithTrailingTrivia(SyntaxFactory.TriviaList())) : visited;
    }

    public override SyntaxNode? VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
    {
        return ProcessObjectCreation(node, node.Initializer, (n, i) => n.WithInitializer(i));
    }

    public override SyntaxNode? VisitImplicitObjectCreationExpression(ImplicitObjectCreationExpressionSyntax node)
    {
        return ProcessObjectCreation(node, node.Initializer, (n, i) => n.WithInitializer(i));
    }

    public override SyntaxNode? VisitArrayCreationExpression(ArrayCreationExpressionSyntax node)
    {
        return ProcessObjectCreation(node, node.Initializer, (n, i) => n.WithInitializer(i));
    }

    public override SyntaxNode? VisitImplicitArrayCreationExpression(ImplicitArrayCreationExpressionSyntax node)
    {
        return ProcessObjectCreation(node, node.Initializer, (n, i) => n.WithInitializer(i));
    }

    private SyntaxNode ProcessObjectCreation<TNode>(TNode node, InitializerExpressionSyntax? initializer, Func<TNode, InitializerExpressionSyntax, TNode> withInitializer)
        where TNode : ExpressionSyntax
    {
        if (initializer == null)
        {
            return VisitBaseExpression(node);
        }

        bool hasComments = InitializerFormatter.HasComments(node);
        bool wasSingleLine = initializer.HasAnnotations(LayoutAnnotator.SingleLineAnnotationKind);
        bool hasItems = initializer.Expressions.Any();

        bool isCollection = initializer.IsKind(SyntaxKind.CollectionInitializerExpression) || initializer.IsKind(SyntaxKind.ArrayInitializerExpression);

        InitializerStyle style = isCollection ? _options.Collection : _options.Object;

        FormattingAction action = DetermineAction(style, hasComments, wasSingleLine, hasItems);

        if (action == FormattingAction.SingleLine)
        {
            SeparatedSyntaxList<ExpressionSyntax> newExpressions = InitializerFormatter.FormatListSingleLine(initializer.Expressions);
            TNode cleanNode = InitializerFormatter.StripPrecedingTrivia(node);

            InitializerExpressionSyntax newInitializer = initializer.WithOpenBraceToken(InitializerFormatter.FormatOpenBraceSingleLine(initializer.OpenBraceToken)).WithExpressions(newExpressions).WithCloseBraceToken(InitializerFormatter.FormatCloseBraceSingleLine(initializer.CloseBraceToken));

            return withInitializer(cleanNode, newInitializer);
        }

        if (action == FormattingAction.MultiLine)
        {
            SyntaxTriviaList parentIndent = InitializerFormatter.GetParentIndentation(node);
            SeparatedSyntaxList<ExpressionSyntax> newExpressions = InitializerFormatter.FormatListMultiLine(initializer.Expressions, parentIndent);
            TNode cleanNode = InitializerFormatter.StripPrecedingTrivia(node);

            InitializerExpressionSyntax newInitializer = initializer.WithOpenBraceToken(InitializerFormatter.FormatOpenBraceMultiLine(initializer.OpenBraceToken, parentIndent)).WithExpressions(newExpressions).WithCloseBraceToken(InitializerFormatter.FormatCloseBraceMultiLine(initializer.CloseBraceToken, parentIndent));

            return withInitializer(cleanNode, newInitializer);
        }

        return VisitBaseExpression(node);
    }

    private SyntaxNode VisitBaseExpression(SyntaxNode node)
    {
        return node switch
        {
            ObjectCreationExpressionSyntax oce => base.VisitObjectCreationExpression(oce)!,
            ImplicitObjectCreationExpressionSyntax ioce => base.VisitImplicitObjectCreationExpression(ioce)!,
            ArrayCreationExpressionSyntax ace => base.VisitArrayCreationExpression(ace)!,
            ImplicitArrayCreationExpressionSyntax iace => base.VisitImplicitArrayCreationExpression(iace)!,
            _ => node
        };
    }
}