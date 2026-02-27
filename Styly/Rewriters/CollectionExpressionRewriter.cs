using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Styly.Rewriters;

internal class CollectionExpressionRewriter : CSharpSyntaxRewriter
{
    private readonly SemanticModel _semanticModel;
    public CollectionExpressionRewriter(SemanticModel semanticModel)
    {
        _semanticModel = semanticModel;
    }

    private bool IsCollectionLike(ExpressionSyntax node)
    {
        TypeInfo typeInfo = _semanticModel.GetTypeInfo(node);
        ITypeSymbol? type = typeInfo.ConvertedType;

        if (type is null 
            || type.TypeKind == TypeKind.Error)
        {
            return false;
        }

        // Arrays are collection-like
        if (type.TypeKind == TypeKind.Array)
        {
            return true;
        }

        // string is special, but we are visiting creation expressions, so we can ignore it.
        if (type.SpecialType == SpecialType.System_String)
        {
            return false;
        }

        // Check if it's a generic collection.
        return type.AllInterfaces.Any(i => i.IsGenericType 
            && i.OriginalDefinition.SpecialType == SpecialType.System_Collections_Generic_IEnumerable_T);
    }

    private static CollectionExpressionSyntax? CreateCollectionExpression(InitializerExpressionSyntax? initializer, SyntaxNode originalNode)
    {
        CollectionExpressionSyntax newExpression;

        if (initializer is null 
            || initializer.Expressions.Count == 0)
        {
            // Empty collection: []
            newExpression = SyntaxFactory.CollectionExpression();
        }
        else
        {
            // Rebuild the list of elements as an IEnumerable, stripping trailing trivia from each.
            // This is crucial for removing the unwanted space from the last element.
            // We also strip leading trivia to remove excessive indentation added by NormalizeWhitespace.
            IEnumerable<ExpressionElementSyntax> cleanElements = initializer.Expressions.Select(expr => SyntaxFactory.ExpressionElement(expr.WithoutLeadingTrivia().WithoutTrailingTrivia()));
            // Create new comma separators, each with a standard trailing space for correct formatting like `1, 2, 3`.
            IEnumerable<SyntaxToken> separators = Enumerable.Range(0, initializer.Expressions.Count - 1).Select(_ => SyntaxFactory.Token(SyntaxKind.CommaToken).WithTrailingTrivia(SyntaxFactory.Space));
            // Explicitly create a SeparatedSyntaxList of the base type `CollectionElementSyntax`.
            // This works because IEnumerable<T> is covariant, allowing our IEnumerable<ExpressionElementSyntax> to be used.
            SeparatedSyntaxList<CollectionElementSyntax> separatedList = SyntaxFactory.SeparatedList<CollectionElementSyntax>(cleanElements, separators);

            newExpression = SyntaxFactory.CollectionExpression(separatedList);
        }

        // Preserve the original leading and trailing trivia from the entire original expression.
        return newExpression.WithLeadingTrivia(originalNode.GetLeadingTrivia()).WithTrailingTrivia(originalNode.GetTrailingTrivia());
    }

    public override SyntaxNode? VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
    {
        // Must have an initializer `{ ... }` or be parameterless `()`
        if (node.ArgumentList is not null 
            && node.ArgumentList.Arguments.Any())
        {
            return base.VisitObjectCreationExpression(node);
        }

        if (!IsCollectionLike(node))
        {
            return base.VisitObjectCreationExpression(node);
        }

        // This handles both `new List<T>()` and `new List<T> { ... }`
        return CreateCollectionExpression(node.Initializer, node);
    }

    public override SyntaxNode? VisitArrayCreationExpression(ArrayCreationExpressionSyntax node)
    {
        // Handle `new int[0]`.
        if (node.Initializer is not null)
        {
            return CreateCollectionExpression(node.Initializer, node);
        }

        if (node.Type.RankSpecifiers.Count != 1 
            || node.Type.RankSpecifiers[0].Sizes.Count != 1 
            || node.Type.RankSpecifiers[0].Sizes[0] is not LiteralExpressionSyntax literal 
            || literal.Token.ValueText != "0")
        {
            // Cannot convert things like `new int[5]` which are not initializers
            return base.VisitArrayCreationExpression(node);
        }

        CollectionExpressionSyntax emptyCollection = SyntaxFactory.CollectionExpression().WithLeadingTrivia(node.GetLeadingTrivia()).WithTrailingTrivia(node.GetTrailingTrivia());

        return emptyCollection;
    }

    public override SyntaxNode? VisitImplicitObjectCreationExpression(ImplicitObjectCreationExpressionSyntax node)
    {
        // new() { ... } or new()
        return node.ArgumentList.Arguments.Any()
            ? base.VisitImplicitObjectCreationExpression(node)
            : !IsCollectionLike(node) ? base.VisitImplicitObjectCreationExpression(node) : CreateCollectionExpression(node.Initializer, node);
    }
}