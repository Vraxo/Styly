using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Styly.Rewriters;

internal class StaticModifierRewriter : CSharpSyntaxRewriter
{
    private readonly SemanticModel _semanticModel;
    public StaticModifierRewriter(SemanticModel semanticModel)
    {
        _semanticModel = semanticModel;
    }

    public override SyntaxNode? VisitMethodDeclaration(MethodDeclarationSyntax node)
    {
        return node.Body is null 
            && node.ExpressionBody is null
            ? base.VisitMethodDeclaration(node)
            : node.Modifiers.Any(m => m.IsKind(SyntaxKind.StaticKeyword) 
            || m.IsKind(SyntaxKind.AbstractKeyword) 
            || m.IsKind(SyntaxKind.VirtualKeyword) 
            || m.IsKind(SyntaxKind.OverrideKeyword) 
            || m.IsKind(SyntaxKind.NewKeyword) 
            || m.IsKind(SyntaxKind.PartialKeyword)) ? base.VisitMethodDeclaration(node) : node.ExplicitInterfaceSpecifier is not null ? base.VisitMethodDeclaration(node) : AccessesInstanceData(node) 
            || IsInterfaceImplementation(node) ? base.VisitMethodDeclaration(node) : MakeStatic(node);
    }

    private bool IsInterfaceImplementation(MethodDeclarationSyntax node)
    {
        ISymbol? symbol = _semanticModel.GetDeclaredSymbol(node);

        if (symbol is not IMethodSymbol methodSymbol)
        {
            // Safety: If we can't resolve the symbol, assume it might be important.
            return true;
        }

        INamedTypeSymbol type = methodSymbol.ContainingType;

        if (type is null)
        {
            return true;
        }

        // Check if this method implements ANY member of ANY interface the type implements
        foreach (INamedTypeSymbol interfaceType in type.AllInterfaces)
        {
            // Optimization: Only check interface members with the same name
            foreach (ISymbol interfaceMember in interfaceType.GetMembers(methodSymbol.Name))
            {
                if (interfaceMember is IMethodSymbol interfaceMethod)
                {
                    // Ask Roslyn: "Which method in 'type' implements this 'interfaceMethod'?"
                    ISymbol? implementation = type.FindImplementationForInterfaceMember(interfaceMethod);
                    // If the answer is "this method", then we cannot make it static.
                    if (SymbolEqualityComparer.Default.Equals(implementation, methodSymbol))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    private bool AccessesInstanceData(MethodDeclarationSyntax method)
    {
        SyntaxNode body = (SyntaxNode? )method.Body ?? method.ExpressionBody!;
        // 1. Check for 'this' or 'base' keywords
        if (body.DescendantTokens().Any(t => t.IsKind(SyntaxKind.ThisKeyword) 
            || t.IsKind(SyntaxKind.BaseKeyword)))
        {
            return true;
        }

        // 2. Check for implicit instance member access
        IEnumerable<IdentifierNameSyntax> identifiers = body.DescendantNodes().OfType<IdentifierNameSyntax>();

        ISymbol? methodSymbol = _semanticModel.GetDeclaredSymbol(method);
        INamedTypeSymbol? containingType = methodSymbol?.ContainingType;

        if (containingType is null)
        {
            // If we can't determine the containing type, assume it's unsafe to change.
            return true;
        }

        foreach (IdentifierNameSyntax identifier in identifiers)
        {
            // Optimization: Skip 'var' keyword used as type inference
            if (identifier.Identifier.ValueText == "var")
            {
                continue;
            }

            // Optimization: Skip 'nameof(X)' - it relies on metadata, not instance state
            if (IsInNameOf(identifier))
            {
                continue;
            }

            // If identifier is the right side of a member access (e.g. obj.Prop), we check 'obj'.
            // If 'obj' is 'this' (implicit or explicit), then 'Prop' is instance access.
            // If identifier is standalone (e.g. Prop = 1), it's implicit 'this'.
            if (IsMemberAccessOnOtherInstance(identifier))
            {
                continue;
            }

            SymbolInfo symbolInfo = _semanticModel.GetSymbolInfo(identifier);
            ISymbol? symbol = symbolInfo.Symbol;

            if (symbol is null)
            {
                // CRITICAL SAFETY CHECK:
                // If we cannot resolve the symbol, we MUST assume it might be an instance member.
                // Ignoring it (continuing) risks breaking code if dependencies are missing (e.g. base classes in other DLLs).
                // It is better to leave a method as instance (safe) than to incorrectly make it static (compile error).
                return true;
            }

            if (symbol.IsStatic)
            {
                continue;
            }

            // If the symbol is a member of the containing class (or base), and it's not static, 
            // then we are accessing instance data.
            if (IsInstanceMember(symbol, containingType))
            {
                return true;
            }
        }

        return false;
    }

    private static bool IsInNameOf(SyntaxNode node)
    {
        // Traverse up to find if we are inside a nameof(...) expression
        SyntaxNode? current = node.Parent;

        while (current is not null)
        {
            if (current is InvocationExpressionSyntax invocation 
                && invocation.Expression is IdentifierNameSyntax name 
                && name.Identifier.ValueText == "nameof")
            {
                return true;
            }

            // Stop at statement boundaries to avoid deep walks
            if (current is StatementSyntax 
                or MemberDeclarationSyntax)
            {
                break;
            }

            current = current.Parent;
        }

        return false;
    }

    private static bool IsMemberAccessOnOtherInstance(IdentifierNameSyntax identifier)
    {
        // Check if this identifier is the 'Name' part of a MemberAccessExpression (e.g. "other.Name")
        if (identifier.Parent is not MemberAccessExpressionSyntax memberAccess 
            || memberAccess.Name != identifier)
        {
            return false;
        }

        // If the expression on the left (Expression) is NOT 'this' or 'base' (explicitly or implicitly),
        // then it's access on a different object.
        if (memberAccess.Expression is not ThisExpressionSyntax 
            and not BaseExpressionSyntax)
        {
            // It's something like "obj.Prop". 
            // CAUTION: If "obj" itself resolves to an instance field of 'this', checking "obj" in the loop
            // will catch it. So we can safely ignore the "Prop" part here.
            return true;
        }

        return false;
    }

    private static bool IsInstanceMember(ISymbol symbol, INamedTypeSymbol containingType)
    {
        if (symbol.Kind is not (SymbolKind.Field 
            or SymbolKind.Property 
            or SymbolKind.Method 
            or SymbolKind.Event))
        {
            return false;
        }

        // Check if the symbol belongs to the containing type or its base types
        ITypeSymbol? current = containingType;

        while (current is not null)
        {
            // Use OriginalDefinition to handle generic types correctly
            if (SymbolEqualityComparer.Default.Equals(current.OriginalDefinition, symbol.ContainingType?.OriginalDefinition))
            {
                return true;
            }

            current = current.BaseType;
        }

        return false;
    }

    private static MethodDeclarationSyntax MakeStatic(MethodDeclarationSyntax node)
    {
        SyntaxToken staticToken = SyntaxFactory.Token(SyntaxKind.StaticKeyword).WithTrailingTrivia(SyntaxFactory.Space);

        int insertIndex = -1;

        for (int i = 0; i < node.Modifiers.Count; i++)
        {
            SyntaxKind kind = node.Modifiers[i].Kind();

            if (kind is SyntaxKind.PublicKeyword 
                or SyntaxKind.PrivateKeyword 
                or SyntaxKind.ProtectedKeyword 
                or SyntaxKind.InternalKeyword)
            {
                insertIndex = i;
            }
        }

        if (insertIndex != -1)
        {
            // Insert after visibility modifiers (e.g. public static).
            // The previous modifier likely holds the indentation/trivia, which we preserve.
            return node.WithModifiers(node.Modifiers.Insert(insertIndex + 1, staticToken));
        }

        // Insert at the start of the modifiers list.
        if (node.Modifiers.Count > 0)
        {
            // We need to move the leading trivia (indentation/comments) from the first existing modifier to our new 'static' token.
            SyntaxToken firstMod = node.Modifiers[0];

            staticToken = staticToken.WithLeadingTrivia(firstMod.LeadingTrivia);
            // Strip the trivia from the old first modifier so it doesn't duplicate.
            SyntaxTokenList newModifiers = node.Modifiers.Replace(firstMod, firstMod.WithLeadingTrivia(SyntaxFactory.TriviaList()));

            return node.WithModifiers(newModifiers.Insert(0, staticToken));
        }
        else
        {
            // No existing modifiers. The leading trivia is currently attached to the ReturnType.
            // We move it to 'static'.
            staticToken = staticToken.WithLeadingTrivia(node.ReturnType.GetLeadingTrivia());
            // Strip trivia from ReturnType.
            MethodDeclarationSyntax newNode = node.WithReturnType(node.ReturnType.WithLeadingTrivia(SyntaxFactory.TriviaList()));

            return newNode.WithModifiers(SyntaxFactory.TokenList(staticToken));
        }
    }
}