using Styly.Configuration;

namespace Styly.Tests.VerticalRhythm;

public class VerticalRhythmExpressionTests : FormatterTestBase
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

    [Fact]
    public static void Isolation_MultiLineObjectInitializer_AddsBlankLine()
    {
        string input = """
            void M()
            {
                var x = 10;
                var o = new Obj { A = 1, B = 2 };
                var y = 20;
            }
            """;

        string expected = """
            void M()
            {
                var x = 10;

                var o = new Obj
                {
                    A = 1,
                    B = 2
                };

                var y = 20;
            }
            """;

        FormatOptions options = new();
        options.Initializers.Object = InitializerStyle.MultiLine;
        options.Spacing.EmptyLineAroundMultiLineExpression = true;

        AssertFormatting(input, expected, options);
    }
}