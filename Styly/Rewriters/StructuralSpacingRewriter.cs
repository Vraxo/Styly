using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Styly.Rewriters;

internal class StructuralSpacingRewriter : CSharpSyntaxRewriter
{
    public override SyntaxNode? VisitCompilationUnit(CompilationUnitSyntax node)
    {
        node = EnsureUsingSeparator(node, node.Usings, node.Members, (n, m) => n.WithMembers(m));
        return base.VisitCompilationUnit(node);
    }

    public override SyntaxNode? VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
    {
        node = EnsureUsingSeparator(node, node.Usings, node.Members, (n, m) => n.WithMembers(m));
        return base.VisitNamespaceDeclaration(node);
    }

    public override SyntaxNode? VisitFileScopedNamespaceDeclaration(FileScopedNamespaceDeclarationSyntax node)
    {
        node = EnsureUsingSeparator(node, node.Usings, node.Members, (n, m) => n.WithMembers(m));

        if (!node.Usings.Any() && node.Members.Any())
        {
            MemberDeclarationSyntax firstMember = node.Members[0];
            var newFirstMember = SpacingUtility.EnsureBlankLine(node.SemicolonToken, firstMember);

            if (newFirstMember != firstMember)
            {
                node = node.WithMembers(node.Members.Replace(firstMember, newFirstMember));
            }
        }

        return base.VisitFileScopedNamespaceDeclaration(node);
    }

    private static TNode EnsureUsingSeparator<TNode>(TNode node, SyntaxList<UsingDirectiveSyntax> usings, SyntaxList<MemberDeclarationSyntax> members, Func<TNode, SyntaxList<MemberDeclarationSyntax>, TNode> withMembers)
        where TNode : SyntaxNode
    {
        if (!usings.Any() || !members.Any())
        {
            return node;
        }

        UsingDirectiveSyntax lastUsing = usings.Last();
        MemberDeclarationSyntax firstMember = members.First();
        var newFirstMember = SpacingUtility.EnsureBlankLine(lastUsing, firstMember);

        return newFirstMember != firstMember ? withMembers(node, members.Replace(firstMember, newFirstMember)) : node;
    }
}