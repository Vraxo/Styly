using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Styly.Configuration;

namespace Styly.Rewriters;

internal class TernaryRewriter : CSharpSyntaxRewriter
{
    private readonly TernaryOptions _options;
    private const int IndentSize = 4;
    public TernaryRewriter(TernaryOptions options)
    {
        _options = options;
    }

    public override SyntaxNode? VisitConditionalExpression(ConditionalExpressionSyntax node)
    {
        return _options.Style == TernaryStyle.Preserve
            ? base.VisitConditionalExpression(node)
            : _options.Style == TernaryStyle.MultiLine ? FormatMultiLine(node) : _options.Style == TernaryStyle.SingleLine ? FormatSingleLine(node) : base.VisitConditionalExpression(node);
    }

    private static ConditionalExpressionSyntax FormatMultiLine(ConditionalExpressionSyntax node)
    {
        SyntaxTriviaList parentIndent = GetParentIndentation(node);
        SyntaxTriviaList itemIndent = parentIndent.Add(SyntaxFactory.Whitespace(new string (' ', IndentSize)));
        SyntaxTrivia newline = SyntaxFactory.CarriageReturnLineFeed;
        // Ensure the condition doesn't have trailing spaces
        ExpressionSyntax condition = node.Condition.WithoutTrailingTrivia();
        // Question mark on new line with indent
        SyntaxToken questionToken = SyntaxFactory.Token(SyntaxKind.QuestionToken).WithLeadingTrivia(SyntaxFactory.TriviaList(newline).AddRange(itemIndent)).WithTrailingTrivia(SyntaxFactory.Space);
        // Result branches cleaned up
        ExpressionSyntax whenTrue = node.WhenTrue.WithoutLeadingTrivia().WithoutTrailingTrivia();

        SyntaxToken colonToken = SyntaxFactory.Token(SyntaxKind.ColonToken).WithLeadingTrivia(SyntaxFactory.TriviaList(newline).AddRange(itemIndent)).WithTrailingTrivia(SyntaxFactory.Space);

        ExpressionSyntax whenFalse = node.WhenFalse.WithoutLeadingTrivia().WithoutTrailingTrivia();

        return node.WithCondition(condition).WithQuestionToken(questionToken).WithWhenTrue(whenTrue).WithColonToken(colonToken).WithWhenFalse(whenFalse);
    }

    private static ConditionalExpressionSyntax FormatSingleLine(ConditionalExpressionSyntax node)
    {
        return node.WithCondition(node.Condition.WithoutTrailingTrivia().WithTrailingTrivia(SyntaxFactory.Space)).WithQuestionToken(SyntaxFactory.Token(SyntaxKind.QuestionToken).WithTrailingTrivia(SyntaxFactory.Space)).WithWhenTrue(node.WhenTrue.WithoutLeadingTrivia().WithoutTrailingTrivia().WithTrailingTrivia(SyntaxFactory.Space)).WithColonToken(SyntaxFactory.Token(SyntaxKind.ColonToken).WithTrailingTrivia(SyntaxFactory.Space)).WithWhenFalse(node.WhenFalse.WithoutLeadingTrivia().WithoutTrailingTrivia());
    }

    private static SyntaxTriviaList GetParentIndentation(SyntaxNode node)
    {
        SyntaxNode? container = node.FirstAncestorOrSelf<SyntaxNode>(n => n is StatementSyntax or MemberDeclarationSyntax);

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