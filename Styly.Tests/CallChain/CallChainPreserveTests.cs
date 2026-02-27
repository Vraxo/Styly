using Styly.Configuration;

namespace Styly.Tests.CallChain;

public class CallChainPreserveTests : FormatterTestBase
{
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
}