using Styly.Configuration;

namespace Styly.Tests;

public class ExpressionIsolationTests : FormatterTestBase
{
    [Fact]
    public static void Isolation_MultiLineTernary_AddsBlankLines()
    {
        string input = """
            void M()
            {
                var a = 1;
                var b = true ? "long_branch_1" : "long_branch_2";
                var c = 2;
            }
            """;

        string expected = """
            void M()
            {
                var a = 1;

                var b = true
                    ? "long_branch_1"
                    : "long_branch_2";

                var c = 2;
            }
            """;

        FormatOptions options = new();
        options.Ternary.Style = TernaryStyle.MultiLine;
        options.Spacing.EmptyLineAroundMultiLineExpression = true;

        AssertFormatting(input, expected, options);
    }
}