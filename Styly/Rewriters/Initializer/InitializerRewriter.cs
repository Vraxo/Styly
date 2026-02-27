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

    private static FormattingAction DetermineAction(InitializerStyle style, bool hasComments, bool wasSingleLine, bool hasItems)
    {
        if (!hasItems)
        {
            return FormattingAction.None;
        }

        // If comments exist, we preserve layout to avoid breaking comment placement.
        if (hasComments)
        {
            // If it was originally single-line, we recover it from NormalizeWhitespace.
            return wasSingleLine ? FormattingAction.SingleLine : FormattingAction.None;
        }

        return style switch
        {
            InitializerStyle.SingleLine => FormattingAction.SingleLine,
            InitializerStyle.MultiLine => FormattingAction.MultiLine,
            // Preserve mode: only force SingleLine if it was already single-line (recovery)
            InitializerStyle.Preserve => wasSingleLine ? FormattingAction.SingleLine : FormattingAction.None,
            _ => wasSingleLine ? FormattingAction.SingleLine : FormattingAction.None,
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

            return cleanNode
                .WithOpenBraceToken(InitializerFormatter.FormatOpenBraceSingleLine(cleanNode.OpenBraceToken))
                .WithInitializers(newMembers)
                .WithCloseBraceToken(InitializerFormatter.FormatCloseBraceSingleLine(cleanNode.CloseBraceToken));
        }

        if (action == FormattingAction.MultiLine)
        {
            SyntaxTriviaList parentIndent = InitializerFormatter.GetParentIndentation(node);
            SeparatedSyntaxList<AnonymousObjectMemberDeclaratorSyntax> newMembers = InitializerFormatter.FormatListMultiLine(node.Initializers, parentIndent);
            AnonymousObjectCreationExpressionSyntax cleanNode = node.WithNewKeyword(node.NewKeyword.WithTrailingTrivia(SyntaxFactory.TriviaList()));

            return cleanNode
                .WithOpenBraceToken(InitializerFormatter.FormatOpenBraceMultiLine(cleanNode.OpenBraceToken, parentIndent))
                .WithInitializers(newMembers)
                .WithCloseBraceToken(InitializerFormatter.FormatCloseBraceMultiLine(cleanNode.CloseBraceToken, parentIndent));
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

            return node
                .WithOpenBracketToken(node.OpenBracketToken.WithLeadingTrivia(SyntaxFactory.TriviaList()).WithTrailingTrivia(SyntaxFactory.Space))
                .WithElements(newElements)
                .WithCloseBracketToken(node.CloseBracketToken.WithLeadingTrivia(SyntaxFactory.Space));
        }

        if (action == FormattingAction.MultiLine)
        {
            SyntaxTriviaList parentIndent = InitializerFormatter.GetParentIndentation(node);
            SeparatedSyntaxList<CollectionElementSyntax> newElements = InitializerFormatter.FormatListMultiLine(node.Elements, parentIndent);

            return node
                .WithOpenBracketToken(InitializerFormatter.FormatOpenBraceMultiLine(node.OpenBracketToken, parentIndent))
                .WithElements(newElements)
                .WithCloseBracketToken(InitializerFormatter.FormatCloseBraceMultiLine(node.CloseBracketToken, parentIndent));
        }

        return base.VisitCollectionExpression(node);
    }

    public override SyntaxNode? VisitEqualsValueClause(EqualsValueClauseSyntax node)
    {
        EqualsValueClauseSyntax visited = (EqualsValueClauseSyntax)base.VisitEqualsValueClause(node)!;
        return _options.Collection == InitializerStyle.MultiLine && visited.Value is CollectionExpressionSyntax
            ? visited.WithEqualsToken(visited.EqualsToken.WithTrailingTrivia(SyntaxFactory.TriviaList()))
            : visited;
    }

    public override SyntaxNode? VisitAssignmentExpression(AssignmentExpressionSyntax node)
    {
        AssignmentExpressionSyntax visited = (AssignmentExpressionSyntax)base.VisitAssignmentExpression(node)!;
        return _options.Collection == InitializerStyle.MultiLine && visited.Right is CollectionExpressionSyntax
            ? visited.WithOperatorToken(visited.OperatorToken.WithTrailingTrivia(SyntaxFactory.TriviaList()))
            : visited;
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

        bool isColl = initializer.IsKind(SyntaxKind.CollectionInitializerExpression) ||
                      initializer.IsKind(SyntaxKind.ArrayInitializerExpression);

        InitializerStyle style = isColl ? _options.Collection : _options.Object;
        FormattingAction action = DetermineAction(style, hasComments, wasSingleLine, hasItems);

        if (action == FormattingAction.SingleLine)
        {
            SeparatedSyntaxList<ExpressionSyntax> newExps = InitializerFormatter.FormatListSingleLine(initializer.Expressions);
            TNode cleanNode = InitializerFormatter.StripPrecedingTrivia(node);
            InitializerExpressionSyntax newInit = initializer
                .WithOpenBraceToken(InitializerFormatter.FormatOpenBraceSingleLine(initializer.OpenBraceToken))
                .WithExpressions(newExps)
                .WithCloseBraceToken(InitializerFormatter.FormatCloseBraceSingleLine(initializer.CloseBraceToken));

            return withInitializer(cleanNode, newInit);
        }

        if (action == FormattingAction.MultiLine)
        {
            SyntaxTriviaList parentIndent = InitializerFormatter.GetParentIndentation(node);
            SeparatedSyntaxList<ExpressionSyntax> newExps = InitializerFormatter.FormatListMultiLine(initializer.Expressions, parentIndent);
            TNode cleanNode = InitializerFormatter.StripPrecedingTrivia(node);
            InitializerExpressionSyntax newInit = initializer
                .WithOpenBraceToken(InitializerFormatter.FormatOpenBraceMultiLine(initializer.OpenBraceToken, parentIndent))
                .WithExpressions(newExps)
                .WithCloseBraceToken(InitializerFormatter.FormatCloseBraceMultiLine(initializer.CloseBraceToken, parentIndent));

            return withInitializer(cleanNode, newInit);
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