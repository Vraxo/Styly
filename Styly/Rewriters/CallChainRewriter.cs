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
        if (node.Parent is MemberAccessExpressionSyntax)
        {
            return base.VisitMemberAccessExpression(node);
        }

        if (_options.Style == CallChainStyle.Preserve)
        {
            SyntaxNode syntaxNode = node.HasAnnotations(LayoutAnnotator.MultiLineCallChainAnnotationKind)
                ? FormatMultiLine(node)
                : node;

            return (SyntaxNode? )syntaxNode;
        }

        if (!IsChained(node))
        {
            return base.VisitMemberAccessExpression(node);
        }

        ExpressionSyntax expressionSyntax = _options.Style == CallChainStyle.MultiLine
            ? FormatMultiLine(node)
            : FormatSingleLine(node);

        return (SyntaxNode? )expressionSyntax;
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

    private static ExpressionSyntax FormatMultiLine(MemberAccessExpressionSyntax node)
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
            return NameMeToo(indent, newline, invocation);
        }

        if (node is not MemberAccessExpressionSyntax memberAccess)
        {
            return node.WithTrailingTrivia(StripTrailingNewlines(node.GetTrailingTrivia()));
        }

        return NameMeAsap(indent, newline, memberAccess);
    }

    private static ExpressionSyntax NameMeToo(SyntaxTriviaList indent, SyntaxTrivia newline, InvocationExpressionSyntax invocation)
    {
        ExpressionSyntax wrappedExpression = WrapChainRecursive(invocation.Expression, indent, newline);

        return SyntaxFactory
            .InvocationExpression(wrappedExpression, invocation.ArgumentList)
            .WithTrailingTrivia(StripTrailingNewlines(invocation.GetTrailingTrivia()));
    }

    private static MemberAccessExpressionSyntax NameMeAsap(SyntaxTriviaList indent, SyntaxTrivia newline, MemberAccessExpressionSyntax memberAccess)
    {
        ExpressionSyntax wrappedInner = WrapChainRecursive(memberAccess.Expression, indent, newline);
        SyntaxToken dotToken = GetDotToken(indent, newline);

        SimpleNameSyntax name = memberAccess.Name.WithoutLeadingTrivia();
        name = name.WithTrailingTrivia(StripTrailingNewlines(name.GetTrailingTrivia()));

        return SyntaxFactory.MemberAccessExpression(memberAccess.Kind(), wrappedInner, dotToken, name);
    }

    private static SyntaxToken GetDotToken(SyntaxTriviaList indent, SyntaxTrivia newline)
    {
        return SyntaxFactory
            .Token(SyntaxKind.DotToken)
            .WithLeadingTrivia(SyntaxFactory.TriviaList(newline).AddRange(indent));
    }

    private static SyntaxTriviaList StripTrailingNewlines(SyntaxTriviaList trivia)
    {
        return SyntaxFactory.TriviaList(trivia.Where(t => !t.IsKind(SyntaxKind.EndOfLineTrivia)));
    }

    private static ExpressionSyntax FormatSingleLine(MemberAccessExpressionSyntax node)
    {
        return FlattenChain(node)
            .WithLeadingTrivia(node.GetLeadingTrivia())
            .WithTrailingTrivia(node.GetTrailingTrivia());
    }

    private static ExpressionSyntax FlattenChain(ExpressionSyntax node)
    {
        if (node is InvocationExpressionSyntax invocation)
        {
            return SyntaxFactory.InvocationExpression(FlattenChain(invocation.Expression), invocation.ArgumentList);
        }

        if (node is not MemberAccessExpressionSyntax memberAccess)
        {
            return node.WithoutTrailingTrivia();
        }

        ExpressionSyntax expression = FlattenChain(memberAccess.Expression);
        SyntaxToken dotToken = MakeDotToken();

        SimpleNameSyntax name = memberAccess
            .Name
            .WithoutLeadingTrivia()
            .WithoutTrailingTrivia();

        return SyntaxFactory
            .MemberAccessExpression(memberAccess.Kind(), expression, dotToken, name)
            .WithoutLeadingTrivia()
            .WithoutTrailingTrivia();
    }

    private static SyntaxToken MakeDotToken()
    {
        return SyntaxFactory
            .Token(SyntaxKind.DotToken)
            .WithLeadingTrivia(SyntaxFactory.TriviaList())
            .WithTrailingTrivia(SyntaxFactory.TriviaList());
    }

    private static SyntaxTriviaList GetParentIndentation(SyntaxNode node)
    {
        SyntaxNode? container = node.FirstAncestorOrSelf<SyntaxNode>(n =>
        {
            return n is StatementSyntax or MemberDeclarationSyntax;
        });

        if (container is null)
        {
            return SyntaxFactory.TriviaList();
        }

        SyntaxTriviaList leading = container.GetLeadingTrivia();
        SyntaxTrivia lastWhitespace = leading.LastOrDefault(t => t.IsKind(SyntaxKind.WhitespaceTrivia));

        return !lastWhitespace.IsKind(SyntaxKind.None)
            ? SyntaxFactory.TriviaList(lastWhitespace)
            : SyntaxFactory.TriviaList();
    }
}