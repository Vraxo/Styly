using Styly.Configuration;

namespace Styly.Tests;

public class SpacingTests : FormatterTestBase
{
    [Fact]
    public void Spacing_BeforeAndAfter_ControlFlow()
    {
        string input = """
            void M()
            {
                Start();
                if (true) { }
                End();
            }
            """;
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