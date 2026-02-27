using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Styly.Configuration;

namespace Styly.Rewriters;

internal class CallChainRewriter : CSharpSyntaxRewriter
{
    private readonly CallChainOptions _options;
    private const int IndentSize = 4;
    public CallChainRewriter(CallChainOptions options)
    {
        _options = options;
    }

    public override SyntaxNode? VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
    {
        if (_options.Style == CallChainStyle.Preserve)
        {
            return base.VisitMemberAccessExpression(node);
        }

        // Only process the root of a call chain
        if (node.Parent is MemberAccessExpressionSyntax)
        {
            return base.VisitMemberAccessExpression(node);
        }

        // Check if this is actually a chain (at least 2 calls)
        return !IsChained(node)
            ? base.VisitMemberAccessExpression(node)
            : _options.Style == CallChainStyle.MultiLine ? FormatMultiLine(node) : _options.Style == CallChainStyle.SingleLine ? FormatSingleLine(node) : base.VisitMemberAccessExpression(node);
    }

    private static bool IsChained(MemberAccessExpressionSyntax node)
    {
        // Count depth of chain
        int depth = 0;
        ExpressionSyntax current = node;

        while (current is MemberAccessExpressionSyntax memberAccess)
        {
            depth++;

            if (depth >= 2)
            {
                return true;
            }

            current = memberAccess.Expression;
        }

        // Also check if any part is an invocation
        return ContainsInvocation(node);
    }

    private static bool ContainsInvocation(ExpressionSyntax node)
    {
        return node is InvocationExpressionSyntax 
            || (node is MemberAccessExpressionSyntax memberAccess 
            && (ContainsInvocation(memberAccess.Expression) 
            || ContainsInvocation(memberAccess.Name)));
    }

    private SyntaxNode FormatMultiLine(MemberAccessExpressionSyntax node)
    {
        SyntaxTriviaList parentIndent = GetParentIndentation(node);
        SyntaxTriviaList itemIndent = parentIndent.Add(SyntaxFactory.Whitespace(new string (' ', IndentSize)));
        SyntaxTrivia newline = SyntaxFactory.CarriageReturnLineFeed;
        // Build the chain from the root expression outward
        ExpressionSyntax? rootExpression = FindRootExpression(node);

        if (rootExpression is null)
        {
            return base.VisitMemberAccessExpression(node);
        }

        // Start with root on its own line, then wrap each access
        ExpressionSyntax wrapped = WrapChainRecursive(node, itemIndent, newline);

        return wrapped.WithLeadingTrivia(node.GetLeadingTrivia()).WithTrailingTrivia(node.GetTrailingTrivia());
    }

    private static ExpressionSyntax WrapChainRecursive(MemberAccessExpressionSyntax node, SyntaxTriviaList indent, SyntaxTrivia newline)
    {
        // If the expression is another member access, wrap it first
        if (node.Expression is MemberAccessExpressionSyntax innerMember)
        {
            ExpressionSyntax wrappedInner = WrapChainRecursive(innerMember, indent, newline);
            // Create the dot token with leading newline and indent
            SyntaxToken dotToken = SyntaxFactory.Token(SyntaxKind.DotToken).WithLeadingTrivia(SyntaxFactory.TriviaList(newline).AddRange(indent));
            // Get the name without leading trivia
            SimpleNameSyntax name = node.Name.WithoutLeadingTrivia();

            return SyntaxFactory.MemberAccessExpression(node.Kind(), wrappedInner, dotToken, name);
        }
        else
        {
            // Base case: first element in chain
            // The root expression keeps its position, dot goes on new line
            SyntaxToken dotToken = SyntaxFactory.Token(SyntaxKind.DotToken).WithLeadingTrivia(SyntaxFactory.TriviaList(newline).AddRange(indent));

            SimpleNameSyntax name = node.Name.WithoutLeadingTrivia();

            return SyntaxFactory.MemberAccessExpression(node.Kind(), node.Expression.WithoutTrailingTrivia(), dotToken, name);
        }
    }

    private static SyntaxNode FormatSingleLine(MemberAccessExpressionSyntax node)
    {
        // Flatten the chain to single line by removing all newlines between parts
        return FlattenChain(node).WithLeadingTrivia(node.GetLeadingTrivia()).WithTrailingTrivia(node.GetTrailingTrivia());
    }

    private static ExpressionSyntax FlattenChain(MemberAccessExpressionSyntax node)
    {
        // Recursively flatten inner expressions
        ExpressionSyntax expression = node.Expression is MemberAccessExpressionSyntax innerMember
            ? FlattenChain(innerMember)
            : node.Expression.WithoutTrailingTrivia();
        // Create dot with just a leading space (if needed) but no newlines
        SyntaxToken dotToken = SyntaxFactory.Token(SyntaxKind.DotToken).WithLeadingTrivia(SyntaxFactory.TriviaList()).WithTrailingTrivia(SyntaxFactory.TriviaList());

        SimpleNameSyntax name = node.Name.WithoutLeadingTrivia().WithoutTrailingTrivia();

        ExpressionSyntax result = SyntaxFactory.MemberAccessExpression(node.Kind(), expression, dotToken, name);

        return result;
    }

    private static ExpressionSyntax? FindRootExpression(MemberAccessExpressionSyntax node)
    {
        ExpressionSyntax current = node;

        while (current is MemberAccessExpressionSyntax memberAccess)
        {
            current = memberAccess.Expression;
        }

        return current;
    }

    private static SyntaxTriviaList GetParentIndentation(SyntaxNode node)
    {
        SyntaxNode? container = node.FirstAncestorOrSelf<SyntaxNode>(n => n is StatementSyntax 
            or MemberDeclarationSyntax);

        if (container is not null)
        {
            SyntaxTriviaList leading = container.GetLeadingTrivia();
            SyntaxTrivia lastWhitespace = leading.LastOrDefault(t => t.IsKind(SyntaxKind.WhitespaceTrivia));

            if (!lastWhitespace.IsKind(SyntaxKind.None))
            {
                return SyntaxFactory.TriviaList(lastWhitespace);
            }
        }

        return SyntaxFactory.TriviaList();
    }
}