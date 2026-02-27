using Styly.Configuration;

namespace Styly.Tests;

public class BasicFormattingTests : FormatterTestBase
{
    [Fact]
    public static void Cleanup_FixesSpacing_ControlFlow()
    {
        // Checks if 'if(true)' becomes 'if (true)'
        string input = "void M(){if(true){}}";
        string expected = """

                void M()
                {
                    if (true)
                    {
                    }
                }
        """;
        AssertFormatting(input, expected, new FormatOptions());
    }

    [Fact]
    public static void Cleanup_FixesIndentation()
    {
        string input = """

                void M()
                {
                Console.WriteLine("test");
                      if (true)
                    {
                        return;
                    }
                }
        """;
        string expected = """

                void M()
                {
                    Console.WriteLine("test");
                    if (true)
                    {
                        return;
                    }
                }
        """;
        AssertFormatting(input, expected, new FormatOptions());
    }

    [Fact]
    public static void Cleanup_RemovesExcessSpaces()
    {
        string input = """

                void M()
                {
                    int    x  =   1;
                }
        """;
        string expected = """

                void M()
                {
                    int x = 1;
                }
        """;
        AssertFormatting(input, expected, new FormatOptions());
    }
}