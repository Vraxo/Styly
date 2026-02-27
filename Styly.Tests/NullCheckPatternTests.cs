using Styly.Configuration;

namespace Styly.Tests;

public class NullCheckPatternTests : FormatterTestBase
{
    [Fact]
    public void NullCheck_NotEqualsNull_IsConvertedTo_IsNotNull()
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
        FormatOptions options = new() { Optimization.PreferNullPatterns = true };
        AssertFormatting(input, expected, options);
    }
}