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
        return node.Parent is MemberAccessExpressionSyntax
            ? base.VisitMemberAccessExpression(node)
            : _options.Style == CallChainStyle.Preserve ? node.HasAnnotations(LayoutAnnotator.MultiLineCallChainAnnotationKind) ? FormatMultiLine(node) : node : !IsChained(node) ? base.VisitMemberAccessExpression(node) : _options.Style == CallChainStyle.MultiLine ? FormatMultiLine(node) : FormatSingleLine(node);
    }

    private static bool IsChained(MemberAccessExpressionSyntax node)
    {
        int count = 1;

        if (node.Parent is InvocationExpressionSyntax)
        {
            count++;
        }

        ExpressionSyntax? current = node.Expression;

        while (true)
        {
            if (current is MemberAccessExpressionSyntax memberAccess)
            {
                count++;
                current = memberAccess.Expression;
            }
            else if (current is InvocationExpressionSyntax invocation)
            {
                count++;
                current = invocation.Expression;
            }
            else
            {
                break;
            }
        }

        return count >= 2;
    }

    private static SyntaxNode FormatMultiLine(MemberAccessExpressionSyntax node)
    {
        SyntaxTriviaList parentIndent = GetParentIndentation(node);
        SyntaxTriviaList itemIndent = parentIndent.Add(SyntaxFactory.Whitespace(new string (' ', IndentSize)));
        SyntaxTrivia newline = SyntaxFactory.CarriageReturnLineFeed;

        ExpressionSyntax wrapped = WrapChainRecursive(node, itemIndent, newline);

        return wrapped.WithLeadingTrivia(node.GetLeadingTrivia()).WithTrailingTrivia(node.GetTrailingTrivia());
    }

    private static ExpressionSyntax WrapChainRecursive(ExpressionSyntax node, SyntaxTriviaList indent, SyntaxTrivia newline)
    {
        if (node is InvocationExpressionSyntax invocation)
        {
            ExpressionSyntax wrappedExpression = WrapChainRecursive(invocation.Expression, indent, newline);

            return SyntaxFactory
                .InvocationExpression(wrappedExpression, invocation.ArgumentList)
                .WithTrailingTrivia(StripTrailingNewlines(invocation.GetTrailingTrivia()));
        }

        if (node is MemberAccessExpressionSyntax memberAccess)
        {
            ExpressionSyntax wrappedInner = WrapChainRecursive(memberAccess.Expression, indent, newline);

            SyntaxToken dotToken = SyntaxFactory
                .Token(SyntaxKind.DotToken)
                .WithLeadingTrivia(SyntaxFactory.TriviaList(newline).AddRange(indent));

            SimpleNameSyntax name = memberAccess.Name.WithoutLeadingTrivia();
            name = name.WithTrailingTrivia(StripTrailingNewlines(name.GetTrailingTrivia()));

            return SyntaxFactory.MemberAccessExpression(memberAccess.Kind(), wrappedInner, dotToken, name);
        }

        return node.WithTrailingTrivia(StripTrailingNewlines(node.GetTrailingTrivia()));
    }

    private static SyntaxTriviaList StripTrailingNewlines(SyntaxTriviaList trivia)
    {
        return SyntaxFactory.TriviaList(trivia.Where(t => !t.IsKind(SyntaxKind.EndOfLineTrivia)));
    }

    private static SyntaxNode FormatSingleLine(MemberAccessExpressionSyntax node)
    {
        return FlattenChain(node).WithLeadingTrivia(node.GetLeadingTrivia()).WithTrailingTrivia(node.GetTrailingTrivia());
    }

    private static ExpressionSyntax FlattenChain(ExpressionSyntax node)
    {
        if (node is InvocationExpressionSyntax invocation)
        {
            return SyntaxFactory.InvocationExpression(FlattenChain(invocation.Expression), invocation.ArgumentList);
        }

        if (node is MemberAccessExpressionSyntax memberAccess)
        {
            ExpressionSyntax expression = FlattenChain(memberAccess.Expression);

            SyntaxToken dotToken = SyntaxFactory
                .Token(SyntaxKind.DotToken)
                .WithLeadingTrivia(SyntaxFactory.TriviaList())
                .WithTrailingTrivia(SyntaxFactory.TriviaList());

            SimpleNameSyntax name = memberAccess.Name.WithoutLeadingTrivia().WithoutTrailingTrivia();

            return SyntaxFactory
                .MemberAccessExpression(memberAccess.Kind(), expression, dotToken, name)
                .WithoutLeadingTrivia()
                .WithoutTrailingTrivia();
        }

        return node.WithoutTrailingTrivia();
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