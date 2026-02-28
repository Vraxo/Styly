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
            return wasSingleLine
                ? FormattingAction.SingleLine
                : FormattingAction.None;
        }

        return style switch
        {
            InitializerStyle.SingleLine => FormattingAction.SingleLine,
            InitializerStyle.MultiLine => FormattingAction.MultiLine,
            // Preserve mode: only force SingleLine if it was already single-line (recovery)
            InitializerStyle.Preserve => wasSingleLine
                ? FormattingAction.SingleLine
                : FormattingAction.None,
            _ => wasSingleLine
                ? FormattingAction.SingleLine
                : FormattingAction.None,
        };
    }

    public override SyntaxNode? VisitAnonymousObjectCreationExpression(AnonymousObjectCreationExpressionSyntax node)
    {
        FormattingAction action = GetFormattingAction(node);

        return action switch
        {
            FormattingAction.SingleLine => NameMe_6(node),
            FormattingAction.MultiLine => NameMe_5(node),
            FormattingAction.None => throw new NotImplementedException(),
            _ => base.VisitAnonymousObjectCreationExpression(node)
        };
    }

    private FormattingAction GetFormattingAction(AnonymousObjectCreationExpressionSyntax node)
    {
        bool hasComments = InitializerFormatter.HasComments(node);
        bool wasSingleLine = node.HasAnnotations(LayoutAnnotator.SingleLineAnnotationKind);
        bool hasItems = node.Initializers.Any();

        FormattingAction action = DetermineAction(_options.AnonymousType, hasComments, wasSingleLine, hasItems);
        return action;
    }

    private static AnonymousObjectCreationExpressionSyntax NameMe_6(AnonymousObjectCreationExpressionSyntax node)
    {
        SeparatedSyntaxList<AnonymousObjectMemberDeclaratorSyntax> newMembers = InitializerFormatter.FormatListSingleLine(node.Initializers);
        AnonymousObjectCreationExpressionSyntax cleanNode = node.WithNewKeyword(node.NewKeyword.WithTrailingTrivia(SyntaxFactory.TriviaList()));

        return GetSingleLineCleanNode(newMembers, cleanNode);
    }

    private static AnonymousObjectCreationExpressionSyntax NameMe_5(AnonymousObjectCreationExpressionSyntax node)
    {
        SyntaxTriviaList parentIndent = InitializerFormatter.GetParentIndentation(node);
        SeparatedSyntaxList<AnonymousObjectMemberDeclaratorSyntax> newMembers = InitializerFormatter.FormatListMultiLine(node.Initializers, parentIndent);
        AnonymousObjectCreationExpressionSyntax cleanNode = node.WithNewKeyword(node.NewKeyword.WithTrailingTrivia(SyntaxFactory.TriviaList()));

        return GetMultiLineCleanNode(parentIndent, newMembers, cleanNode);
    }

    private static AnonymousObjectCreationExpressionSyntax GetMultiLineCleanNode(SyntaxTriviaList parentIndent, SeparatedSyntaxList<AnonymousObjectMemberDeclaratorSyntax> newMembers, AnonymousObjectCreationExpressionSyntax cleanNode)
    {
        return cleanNode
            .WithOpenBraceToken(InitializerFormatter.FormatOpenBraceMultiLine(cleanNode.OpenBraceToken, parentIndent))
            .WithInitializers(newMembers)
            .WithCloseBraceToken(InitializerFormatter.FormatCloseBraceMultiLine(cleanNode.CloseBraceToken, parentIndent));
    }

    private static AnonymousObjectCreationExpressionSyntax GetSingleLineCleanNode(SeparatedSyntaxList<AnonymousObjectMemberDeclaratorSyntax> newMembers, AnonymousObjectCreationExpressionSyntax cleanNode)
    {
        return cleanNode
            .WithOpenBraceToken(InitializerFormatter.FormatOpenBraceSingleLine(cleanNode.OpenBraceToken))
            .WithInitializers(newMembers)
            .WithCloseBraceToken(InitializerFormatter.FormatCloseBraceSingleLine(cleanNode.CloseBraceToken));
    }

    public override SyntaxNode? VisitCollectionExpression(CollectionExpressionSyntax node)
    {
        FormattingAction action = GetFormattingAction(node);

        return action switch
        {
            FormattingAction.SingleLine => NameMe_4(node),
            FormattingAction.MultiLine => NameMe_3(node),
            FormattingAction.None => throw new NotImplementedException(),
            _ => base.VisitCollectionExpression(node)
        };
    }

    private FormattingAction GetFormattingAction(CollectionExpressionSyntax node)
    {
        bool hasComments = InitializerFormatter.HasComments(node);
        bool wasSingleLine = node.HasAnnotations(LayoutAnnotator.SingleLineAnnotationKind);
        bool hasItems = node.Elements.Any();

        return DetermineAction(_options.Collection, hasComments, wasSingleLine, hasItems);
    }

    private static CollectionExpressionSyntax NameMe_4(CollectionExpressionSyntax node)
    {
        SeparatedSyntaxList<CollectionElementSyntax> newElements = InitializerFormatter.FormatListSingleLine(node.Elements);

        SyntaxToken openBracketToken = node.OpenBracketToken
            .WithLeadingTrivia(SyntaxFactory.TriviaList())
            .WithTrailingTrivia(SyntaxFactory.Space);

        return node
            .WithOpenBracketToken(openBracketToken)
            .WithElements(newElements)
            .WithCloseBracketToken(node.CloseBracketToken.WithLeadingTrivia(SyntaxFactory.Space));
    }

    private static CollectionExpressionSyntax NameMe_3(CollectionExpressionSyntax node)
    {
        SyntaxTriviaList parentIndent = InitializerFormatter.GetParentIndentation(node);
        SeparatedSyntaxList<CollectionElementSyntax> newElements = InitializerFormatter.FormatListMultiLine(node.Elements, parentIndent);

        return node
            .WithOpenBracketToken(InitializerFormatter.FormatOpenBraceMultiLine(node.OpenBracketToken, parentIndent))
            .WithElements(newElements)
            .WithCloseBracketToken(InitializerFormatter.FormatCloseBraceMultiLine(node.CloseBracketToken, parentIndent));
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
        if (initializer is null)
        {
            return VisitBaseExpression(node);
        }

        return GetFormattingAction(node, initializer) switch
        {
            FormattingAction.SingleLine => NameMe_1(node, initializer, withInitializer),
            FormattingAction.MultiLine => NameMe_2(node, initializer, withInitializer),
            FormattingAction.None => throw new NotImplementedException(),
            _ => VisitBaseExpression(node)
        };
    }

    private FormattingAction GetFormattingAction<TNode>(TNode node, InitializerExpressionSyntax initializer)
        where TNode : ExpressionSyntax
    {
        bool hasComments = InitializerFormatter.HasComments(node);
        bool wasSingleLine = initializer.HasAnnotations(LayoutAnnotator.SingleLineAnnotationKind);
        bool hasItems = initializer.Expressions.Any();

        InitializerStyle style = GetInitializerStyle(initializer);
        FormattingAction action = DetermineAction(style, hasComments, wasSingleLine, hasItems);
        return action;
    }

    private static SyntaxNode NameMe_2<TNode>(TNode node, InitializerExpressionSyntax initializer, Func<TNode, InitializerExpressionSyntax, TNode> withInitializer)
        where TNode : ExpressionSyntax
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

    private static SyntaxNode NameMe_1<TNode>(TNode node, InitializerExpressionSyntax initializer, Func<TNode, InitializerExpressionSyntax, TNode> withInitializer)
        where TNode : ExpressionSyntax
    {
        SeparatedSyntaxList<ExpressionSyntax> newExps = InitializerFormatter.FormatListSingleLine(initializer.Expressions);
        TNode cleanNode = InitializerFormatter.StripPrecedingTrivia(node);

        InitializerExpressionSyntax newInit = initializer
            .WithOpenBraceToken(InitializerFormatter.FormatOpenBraceSingleLine(initializer.OpenBraceToken))
            .WithExpressions(newExps)
            .WithCloseBraceToken(InitializerFormatter.FormatCloseBraceSingleLine(initializer.CloseBraceToken));

        return withInitializer(cleanNode, newInit);
    }

    private InitializerStyle GetInitializerStyle(InitializerExpressionSyntax initializer)
    {
        return IsCollectionInitializer(initializer)
            ? _options.Collection
            : _options.Object;
    }

    private static bool IsCollectionInitializer(InitializerExpressionSyntax initializer)
    {
        return initializer.IsKind(SyntaxKind.CollectionInitializerExpression) || initializer.IsKind(SyntaxKind.ArrayInitializerExpression);
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