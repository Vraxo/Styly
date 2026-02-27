using Styly.Configuration;

namespace Styly.Tests;

public class InitializerTests : FormatterTestBase
{
    [Fact]
    public void Initializers_Object_MultiLine()
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
    public void Initializers_Object_SingleLine()
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
    public void Initializers_Anonymous_MultiLine()
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

    [Fact]
    public void Initializers_Collection_MultiLine()
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
    public void Initializers_CollectionExpression_MultiLine()
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

    [Fact]
    public void Initializers_Preserve_Comments()
    {
        string input = "var o = new Obj { /* comment */ A = 1 };";
        string expected = "var o = new Obj { /* comment */ A = 1 };";

        FormatOptions options = new();
        options.Initializers.Object = InitializerStyle.MultiLine;

        AssertFormatting(input, expected, options);
    }
}