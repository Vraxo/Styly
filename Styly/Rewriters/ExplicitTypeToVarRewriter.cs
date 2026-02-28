using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Styly.Configuration;

namespace Styly.Rewriters;

internal class ExplicitTypeToVarRewriter : CSharpSyntaxRewriter
{
    private readonly SemanticModel _semanticModel;
    private readonly UseVarOption _option;
    public ExplicitTypeToVarRewriter(SemanticModel semanticModel, UseVarOption option)
    {
        _semanticModel = semanticModel;
        _option = option;
    }

    public override SyntaxNode? VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
    {
        // var is only valid if the declaration is not a constant
        if (node.IsConst)
        {
            return base.VisitLocalDeclarationStatement(node);
        }

        VariableDeclarationSyntax declaration = node.Declaration;
        // If it's already var, do nothing
        if (declaration.Type.IsVar)
        {
            return base.VisitLocalDeclarationStatement(node);
        }

        // var requires an initializer for every variable in the declaration
        // e.g. 'int x = 1, y = 2;' can be 'var', but 'int x;' cannot.
        // Also, 'var' cannot be used if there are multiple variables: 'var x = 1, y = 2' is valid C#,
        // but we simplify by only handling single variable declarations for safety and clarity.
        if (declaration.Variables.Count != 1)
        {
            return base.VisitLocalDeclarationStatement(node);
        }

        VariableDeclaratorSyntax variable = declaration.Variables[0];

        if (variable.Initializer is null)
        {
            return base.VisitLocalDeclarationStatement(node);
        }

        // Check if 'var' is semantically valid here (e.g., not assigning null without cast)
        TypeInfo typeInfo = _semanticModel.GetTypeInfo(declaration.Type);
        ITypeSymbol? typeSymbol = typeInfo.Type;

        TypeInfo initializerTypeInfo = _semanticModel.GetTypeInfo(variable.Initializer.Value);
        ITypeSymbol? initializerType = initializerTypeInfo.Type;
        // If we can't resolve types, we can't safely change to var
        if (typeSymbol is null || typeSymbol.TypeKind == TypeKind.Error)
        {
            return base.VisitLocalDeclarationStatement(node);
        }

        // If assigning null (without cast), var is illegal: var x = null; // Error
        if (initializerType is null && variable.Initializer.Value.IsKind(SyntaxKind.NullLiteralExpression))
        {
            return base.VisitLocalDeclarationStatement(node);
        }

        // Determine if we should replace based on the strategy
        bool shouldReplace = _option == UseVarOption.Always || IsApparent(variable.Initializer.Value);

        if (shouldReplace)
        {
            // Replace the explicit type with 'var'
            // Preserve leading/trailing trivia (indentation/comments)
            TypeSyntax varType = SyntaxFactory.IdentifierName("var").WithLeadingTrivia(declaration.Type.GetLeadingTrivia()).WithTrailingTrivia(declaration.Type.GetTrailingTrivia());

            VariableDeclarationSyntax newDeclaration = declaration.WithType(varType);
            return node.WithDeclaration(newDeclaration);
        }

        return base.VisitLocalDeclarationStatement(node);
    }

    private static bool IsApparent(ExpressionSyntax expression)
    {
        // 1. Object Creation: new Customer()
        if (expression is ObjectCreationExpressionSyntax)
        {
            return true;
        }

        // 2. Array Creation: new int[] { ... }
        if (expression is ArrayCreationExpressionSyntax)
        {
            return true;
        }

        // 3. Cast Expression: (int)x
        if (expression is CastExpressionSyntax)
        {
            return true;
        }

        // 4. Binary Expression using 'as': x as Customer
        if (expression is BinaryExpressionSyntax binary && binary.IsKind(SyntaxKind.AsExpression))
        {
            return true;
        }

        // 5. Literals: 5, "string", true, 10.5
        if (expression is LiteralExpressionSyntax)
        {
            return true;
        }

        // 6. Default expression: default(int) - (But NOT just 'default')
        return expression is DefaultExpressionSyntax;
    }
}