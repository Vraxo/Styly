using Styly.Configuration;

namespace Styly.Tests;

public class VariablesTests : FormatterTestBase
{
    [Fact]
    public static void Variables_VarToExplicit_BuiltInTypes()
    {
        string input = """
            void M()
            {
                var x = 10;
                var s = "hello";
            }
            """;

        string expected = """
            void M()
            {
                int x = 10;
                string s = "hello";
            }
            """;

        FormatOptions options = new()
        {
            Variables = new VariablesOptions
            {
                UseVar = UseVarOption.Never
            }
        };

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void Variables_VarToExplicit_PreservesAnonymousTypes()
    {
        string input = """
            void M()
            {
                var anon = new { Name = "Test" };
            }
            """;

        string expected = """
            void M()
            {
                var anon = new { Name = "Test" };
            }
            """;

        FormatOptions options = new()
        {
            Variables = new VariablesOptions
            {
                UseVar = UseVarOption.Never
            }
        };

        AssertFormatting(input, expected, options);
    }
}