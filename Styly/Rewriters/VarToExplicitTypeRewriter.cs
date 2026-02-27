using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Styly.Rewriters;

internal class VarToExplicitTypeRewriter : CSharpSyntaxRewriter
{
    private readonly SemanticModel _semanticModel;
    public VarToExplicitTypeRewriter(SemanticModel semanticModel)
    {
        _semanticModel = semanticModel;
    }

    public override SyntaxNode? VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
    {
        VariableDeclarationSyntax declaration = node.Declaration;

        if (!declaration.Type.IsVar)
        {
            return base.VisitLocalDeclarationStatement(node);
        }

        TypeInfo typeInfo = _semanticModel.GetTypeInfo(declaration.Type);
        ITypeSymbol? typeSymbol = typeInfo.ConvertedType;

        // Skip rewriting if:
        // 1. Type symbol is not resolved (null or Error).
        // 2. Type is void (cannot be a variable type).
        // 3. Type is Anonymous (e.g. new { A = 1 }), as it has no explicit type name.
        if (typeSymbol == null || typeSymbol.TypeKind == TypeKind.Error || typeSymbol.SpecialType == SpecialType.System_Void || typeSymbol.IsAnonymousType)
        {
            return base.VisitLocalDeclarationStatement(node);
        }

        string typeName = typeSymbol.ToMinimalDisplayString(_semanticModel, declaration.Type.SpanStart);
        TypeSyntax explicitTypeSyntax = SyntaxFactory.ParseTypeName(typeName).WithLeadingTrivia(declaration.Type.GetLeadingTrivia()).WithTrailingTrivia(declaration.Type.GetTrailingTrivia());

        VariableDeclarationSyntax newDeclaration = declaration.WithType(explicitTypeSyntax);

        return node.WithDeclaration(newDeclaration);
    }
}