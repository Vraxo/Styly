using Styly.Configuration;

namespace Styly.Tests;

public class ModifiersTests : FormatterTestBase
{
    private readonly FormatOptions _options = new()
    {
        Modifiers = new ModifiersOptions { MakeStaticWhenPossible = true }
    };

    [Fact]
    public void Modifiers_MakeStatic_SimpleMethod()
    {
        string input = """
            class C
            {
                void M()
                {
                    int x = 1;
                }
            }
            """;
        string expected = """
            class C
            {
                static void M()
                {
                    int x = 1;
                }
            }
            """;
        AssertFormatting(input, expected, _options);
    }
}