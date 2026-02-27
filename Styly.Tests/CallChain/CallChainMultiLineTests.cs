using Styly.Configuration;

namespace Styly.Tests.CallChain;

public class CallChainMultiLineTests : FormatterTestBase
{
    [Fact]
    public static void CallChain_MultiLine_ExpandsChain()
    {
        string input = "var result = obj.Method1().Method2().Method3();";

        string expected = """
            var result = obj
                .Method1()
                .Method2()
                .Method3();
            """;

        FormatOptions options = new()
        {
            CallChain = new CallChainOptions
            {
                Style = CallChainStyle.MultiLine
            }
        };

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void CallChain_MultiLine_PreservesAlreadyMultiLine()
    {
        string input = """
            var result = obj
                .Method1()
                .Method2();
            """;

        string expected = """
            var result = obj
                .Method1()
                .Method2();
            """;

        FormatOptions options = new()
        {
            CallChain = new CallChainOptions
            {
                Style = CallChainStyle.MultiLine
            }
        };

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void CallChain_MultiLine_IgnoresSingleMemberAccess()
    {
        string input = "var x = obj.Property;";
        string expected = "var x = obj.Property;";

        FormatOptions options = new()
        {
            CallChain = new CallChainOptions
            {
                Style = CallChainStyle.MultiLine
            }
        };

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void CallChain_MultiLine_HandlesNestedChains()
    {
        string input = "var result = outer.GetInner().Items.Select(x => x.Value).ToList();";

        string expected = """
            var result = outer
                .GetInner()
                .Items
                .Select(x => x.Value)
                .ToList();
            """;

        FormatOptions options = new()
        {
            CallChain = new CallChainOptions
            {
                Style = CallChainStyle.MultiLine
            }
        };

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void CallChain_MultiLine_PreservesIndentationContext()
    {
        string input = """
            void M()
            {
                if (true)
                {
                    var x = obj.Method1().Method2();
                }
            }
            """;

        string expected = """
            void M()
            {
                if (true)
                {
                    var x = obj
                        .Method1()
                        .Method2();
                }
            }
            """;

        FormatOptions options = new()
        {
            CallChain = new CallChainOptions
            {
                Style = CallChainStyle.MultiLine
            }
        };

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void CallChain_MultiLine_WithInvocationInChain()
    {
        string input = "var r = factory.Create().Configure().Build().Execute();";

        string expected = """
            var r = factory
                .Create()
                .Configure()
                .Build()
                .Execute();
            """;

        FormatOptions options = new()
        {
            CallChain = new CallChainOptions
            {
                Style = CallChainStyle.MultiLine
            }
        };

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void CallChain_MultiLine_TwoPartChainIsExpanded()
    {
        string input = "var x = obj.Method();";

        string expected = """
            var x = obj
                .Method();
            """;

        FormatOptions options = new()
        {
            CallChain = new CallChainOptions
            {
                Style = CallChainStyle.MultiLine
            }
        };

        AssertFormatting(input, expected, options);
    }
}