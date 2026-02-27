using Styly.Configuration;

namespace Styly.Tests.Initializer;

public class InitializerObjectTests : FormatterTestBase
{
    [Fact]
    public static void Initializers_Object_MultiLine()
    {
        string input = "var o = new Obj { A = 1, B = 2 };";

        string expected = """
            var o = new Obj
            {
                A = 1,
                B = 2
            };
            """;

        FormatOptions options = new();
        options.Initializers.Object = InitializerStyle.MultiLine;

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void Initializers_Object_SingleLine()
    {
        string input = """
            var o = new Obj
            {
                A = 1,
                B = 2
            };
            """;

        string expected = "var o = new Obj { A = 1, B = 2 };";

        FormatOptions options = new();
        options.Initializers.Object = InitializerStyle.SingleLine;

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void Initializers_Object_Preserve_Comments()
    {
        string input = "var o = new Obj { /* comment */ A = 1 };";
        string expected = "var o = new Obj { /* comment */ A = 1 };";

        FormatOptions options = new();
        options.Initializers.Object = InitializerStyle.MultiLine;

        AssertFormatting(input, expected, options);
    }
}