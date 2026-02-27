using Styly.Configuration;

namespace Styly.Tests;

public class NullCheckPatternTests : FormatterTestBase
{
    [Fact]
    public static void NullCheck_EqualsNull_IsConvertedTo_IsNull()
    {
        string input = """
            void M(object x)
            {
                if (x == null) { }
            }
            """;

        string expected = """
            void M(object x)
            {
                if (x is null)
                {
                }
            }
            """;

        FormatOptions options = new();
        options.Optimization.PreferNullPatterns = true;

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void NullCheck_NotEqualsNull_IsConvertedTo_IsNotNull()
    {
        string input = """
            void M(object x)
            {
                if (x != null) { }
            }
            """;

        string expected = """
            void M(object x)
            {
                if (x is not null)
                {
                }
            }
            """;

        FormatOptions options = new();
        options.Optimization.PreferNullPatterns = true;

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void NullCheck_ReverseOrder_IsConverted()
    {
        string input = """
            void M(object x)
            {
                if (null == x) { }
            }
            """;

        string expected = """
            void M(object x)
            {
                if (x is null)
                {
                }
            }
            """;

        FormatOptions options = new();
        options.Optimization.PreferNullPatterns = true;

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void NullCheck_ComplexExpression_IsConverted()
    {
        string input = """
            void M(string s)
            {
                if (s.Length.ToString() == null) { }
            }
            """;

        string expected = """
            void M(string s)
            {
                if (s.Length.ToString() is null)
                {
                }
            }
            """;

        FormatOptions options = new();
        options.Optimization.PreferNullPatterns = true;

        AssertFormatting(input, expected, options);
    }
}