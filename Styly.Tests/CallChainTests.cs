using Styly.Configuration;

namespace Styly.Tests;

public class CallChainTests : FormatterTestBase
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
    public static void CallChain_Preserve_DoesNotModify()
    {
        string input = """
            void M()
            {
                var a = obj.Method1().Method2();
                var b = obj
                    .Method1()
                    .Method2();
            }
            """;

        // Note: var b correctly receives a blank line because it is identified
        // as a 'Heavy' (multi-line) expression by the VerticalRhythmRewriter.
        string expected = """
            void M()
            {
                var a = obj.Method1().Method2();

                var b = obj
                    .Method1()
                    .Method2();
            }
            """;

        FormatOptions options = new()
        {
            CallChain = new CallChainOptions
            {
                Style = CallChainStyle.Preserve
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
    public static void CallChain_Preserve_DoesNotBreakComments()
    {
        string input = """
            var result = obj
                .Method1() // first step
                .Method2(); // second step
            """;

        string expected = """
            var result = obj
                .Method1() // first step
                .Method2(); // second step
            """;

        FormatOptions options = new()
        {
            CallChain = new CallChainOptions
            {
                Style = CallChainStyle.Preserve
            }
        };

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void CallChain_SingleLine_TwoPartChainIsFlattened()
    {
        string input = "var x = obj\n    .Method();";
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