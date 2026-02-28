using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Styly.Rewriters;

internal class EnumerableAnyRewriter : CSharpSyntaxRewriter
{
    private readonly SemanticModel _semanticModel;
    public EnumerableAnyRewriter(SemanticModel semanticModel)
    {
        _semanticModel = semanticModel;
    }

    public override SyntaxNode? VisitInvocationExpression(InvocationExpressionSyntax node)
    {
        if (node.Expression is not MemberAccessExpressionSyntax memberAccess 
            || memberAccess.Name.Identifier.ValueText != "Any" 
            || node.ArgumentList.Arguments.Count != 0)
        {
            return base.VisitInvocationExpression(node);
        }

        if (!IsLinqAny(node))
        {
            return base.VisitInvocationExpression(node);
        }

        TypeInfo typeInfo = _semanticModel.GetTypeInfo(memberAccess.Expression);

        if (typeInfo.Type is null)
        {
            return base.VisitInvocationExpression(node);
        }

        string? propertyName = GetLengthOrCountProperty(typeInfo.Type);

        if (propertyName is null)
        {
            return base.VisitInvocationExpression(node);
        }

        // Replace .Any() with .Count != 0
        return CreateCountComparison(memberAccess.Expression, propertyName);
    }

    private bool IsLinqAny(InvocationExpressionSyntax node)
    {
        SymbolInfo symbolInfo = _semanticModel.GetSymbolInfo(node);

        return symbolInfo.Symbol is IMethodSymbol methodSymbol 
            && methodSymbol.Name == "Any" 
            && methodSymbol.ContainingType.ToDisplayString() == "System.Linq.Enumerable";
    }

    private static string? GetLengthOrCountProperty(ITypeSymbol type)
    {
        // Prefer Length for Arrays
        if (type.TypeKind == TypeKind.Array)
        {
            return "Length";
        }

        // Check for explicit Count or Length properties on the type (e.g. List<T>, ICollection<T>)
        foreach (ISymbol member in type.GetMembers())
        {
            if (member is not IPropertySymbol prop 
                || prop.IsStatic 
                || prop.Type.SpecialType != SpecialType.System_Int32)
            {
                continue;
            }

            if (prop.Name is "Count" 
                or "Length")
            {
                return prop.Name;
            }
        }

        return null;
    }

    private static BinaryExpressionSyntax CreateCountComparison(ExpressionSyntax expression, string propertyName)
    {
        // Generates: expression.Count != 0
        // We use NormalizeWhitespace() to ensure correct spacing around the operator (e.g. " != ")
        // instead of manually constructing trivia, which is less robust.
        return SyntaxFactory
            .BinaryExpression(SyntaxKind.NotEqualsExpression, SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, expression, SyntaxFactory.IdentifierName(propertyName)), SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(0)))
            .NormalizeWhitespace();
    }
}