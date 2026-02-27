using Styly.Configuration;

namespace Styly.Tests.VerticalRhythm;

public class VerticalRhythmControlFlowTests : FormatterTestBase
{
    [Fact]
    public static void Spacing_EmptyLineBeforeControlFlow_If()
    {
        string input = """
            void M()
            {
                Console.WriteLine();
                if (true) { }
            }
            """;

        string expected = """
            void M()
            {
                Console.WriteLine();

                if (true)
                {
                }
            }
            """;

        FormatOptions options = new();
        options.Spacing.EmptyLineBeforeControlFlow = true;
        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void Spacing_BeforeAndAfter_ControlFlow()
    {
        string input = "void M() { Start(); if (true) { } End(); }";

        string expected = """
            void M()
            {
                Start();

                if (true)
                {
                }

                End();
            }
            """;

        FormatOptions options = new();
        options.Spacing.EmptyLineBeforeControlFlow = true;
        options.Spacing.EmptyLineAfterControlFlow = true;
        AssertFormatting(input, expected, options);
    }
}