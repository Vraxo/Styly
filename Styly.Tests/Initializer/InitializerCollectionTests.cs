using Styly.Configuration;

namespace Styly.Tests.Initializer;

public class InitializerCollectionTests : FormatterTestBase
{
    [Fact]
    public static void Initializers_Collection_MultiLine()
    {
        string input = "var l = new List<int> { 1, 2, 3 };";

        string expected = """
            var l = new List<int>
            {
                1,
                2,
                3
            };
            """;

        FormatOptions options = new();
        options.Initializers.Collection = InitializerStyle.MultiLine;
        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void Initializers_CollectionExpression_MultiLine()
    {
        string input = "var x = [1, 2];";

        string expected = """
            var x =
            [
                1,
                2
            ];
            """;

        FormatOptions options = new();
        options.Initializers.Collection = InitializerStyle.MultiLine;
        AssertFormatting(input, expected, options);
    }
}