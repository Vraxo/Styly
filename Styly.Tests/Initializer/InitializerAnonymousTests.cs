using Styly.Configuration;

namespace Styly.Tests.Initializer;

public class InitializerAnonymousTests : FormatterTestBase
{
    [Fact]
    public static void Initializers_Anonymous_MultiLine()
    {
        string input = "var a = new { X = 1, Y = 2 };";

        string expected = """
            var a = new
            {
                X = 1,
                Y = 2
            };
            """;

        FormatOptions options = new();
        options.Initializers.AnonymousType = InitializerStyle.MultiLine;
        AssertFormatting(input, expected, options);
    }
}