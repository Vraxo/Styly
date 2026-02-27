using Styly.Configuration;

namespace Styly.Tests.CallChain;

public class CallChainSingleLineTests : FormatterTestBase
{
    [Fact]
    public static void CallChain_SingleLine_FlattensMultiLineChain()
    {
        string input = """
            void M()
            {
                var result = obj
                    .Method1()
                    .Method2()
                    .Method3();
            }
            """;

        string expected = """
            void M()
            {
                var result = obj.Method1().Method2().Method3();
            }
            """;

        FormatOptions options = new()
        {
            CallChain = new CallChainOptions
            {
                Style = CallChainStyle.SingleLine
            }
        };

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void CallChain_SingleLine_PreservesAlreadySingleLine()
    {
        string input = "var result = obj.Method1().Method2();";
        string expected = "var result = obj.Method1().Method2();";

        FormatOptions options = new()
        {
            CallChain = new CallChainOptions
            {
                Style = CallChainStyle.SingleLine
            }
        };

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void CallChain_SingleLine_IgnoresSingleMemberAccess()
    {
        string input = "var x = obj.Property;";
        string expected = "var x = obj.Property;";

        FormatOptions options = new()
        {
            CallChain = new CallChainOptions
            {
                Style = CallChainStyle.SingleLine
            }
        };

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void CallChain_SingleLine_HandlesNestedChains()
    {
        string input = """
            void M()
            {
                var result = outer
                    .GetInner()
                    .Items
                    .Select(x => x.Value)
                    .Where(x => x > 0)
                    .ToList();
            }
            """;

        string expected = """
            void M()
            {
                var result = outer.GetInner().Items.Select(x => x.Value).Where(x => x > 0).ToList();
            }
            """;

        FormatOptions options = new()
        {
            CallChain = new CallChainOptions
            {
                Style = CallChainStyle.SingleLine
            }
        };

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void CallChain_SingleLine_RemovesExtraWhitespace()
    {
        string input = """
            var x = obj
                .Method1 ( )
                .Method2( arg );
            """;

        string expected = "var x = obj.Method1().Method2(arg);";

        FormatOptions options = new()
        {
            CallChain = new CallChainOptions
            {
                Style = CallChainStyle.SingleLine
            }
        };

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void CallChain_SingleLine_TwoPartChainIsFlattened()
    {
        string input = """
            var x = obj
                .Method();
        """;

        string expected = "var x = obj.Method();";

        FormatOptions options = new()
        {
            CallChain = new CallChainOptions
            {
                Style = CallChainStyle.SingleLine
            }
        };

        AssertFormatting(input, expected, options);
    }
}