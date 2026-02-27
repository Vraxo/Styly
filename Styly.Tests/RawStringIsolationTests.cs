using Styly.Configuration;

namespace Styly.Tests;

public class RawStringIsolationTests : FormatterTestBase
{
    [Fact]
    public void Isolation_MultiLineVerbatim_ConvertsAndIsolates()
    {
        // Use 4 quotes for the outer delimiter because the content contains 3 quotes.
        string input = """
            void M()
            {
                var a = 1;
                var s = @"line 1
            line 2";
                var b = 2;
            }
            """;

        string expected = """"
            void M()
            {
                var a = 1;

                var s = """
                    line 1
                    line 2
                    """;

                var b = 2;
            }
            """";

        FormatOptions options = new();
        options.RawStrings.PreferRawForMultiline = true;
        options.Spacing.EmptyLineAroundMultiLineExpression = true;

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public void Isolation_SingleLineStandard_DoesNotIsolate()
    {
        string input = """
            void M()
            {
                var a = 1;
                var s = "just a single line";
                var b = 2;
            }
            """;

        string expected = """
            void M()
            {
                var a = 1;
                var s = "just a single line";
                var b = 2;
            }
            """;

        FormatOptions options = new();
        options.RawStrings.PreferRawForMultiline = true;
        options.Spacing.EmptyLineAroundMultiLineExpression = true;

        AssertFormatting(input, expected, options);
    }
}