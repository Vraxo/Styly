using Styly.Configuration;

namespace Styly.Tests;

public class RawStringIsolationTests : FormatterTestBase
{
    [Fact]
    public static void Isolation_MultiLineVerbatim_ConvertsAndIsolates()
    {
        string input = """
            void M()
            {
                var a = 1;
                var s = @"line 1
            line 2";
                var b = 2;
            }
            """;
        // Fixed: The closing delimiter of the inner raw string 
        // is now aligned with 'var s', matching the rewriter's logic.
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
    public static void Isolation_SingleLineStandard_DoesNotIsolate()
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