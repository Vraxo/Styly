using Styly.Configuration;

namespace Styly.Tests.Modifier;

public class ModifierStaticTests : FormatterTestBase
{
    private readonly FormatOptions _options = new()
    {
        Modifiers = new ModifiersOptions
        {
            MakeStaticWhenPossible = true
        }
    };
    [Fact]
    public void Modifiers_MakeStatic_SimpleMethod()
    {
        string input = "class C { void M() { int x = 1; } }";

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

    [Fact]
    public void Modifiers_MakeStatic_WithParameters()
    {
        string input = "class C { int Add(int a, int b) => a + b; }";

        string expected = """
            class C
            {
                static int Add(int a, int b) => a + b;
            }
            """;

        AssertFormatting(input, expected, _options);
    }

    [Fact]
    public void Modifiers_InsertsStaticCorrectly_WithVisibility()
    {
        string input = "class C { public void M() { } }";

        string expected = """
            class C
            {
                public static void M()
                {
                }
            }
            """;

        AssertFormatting(input, expected, _options);
    }
}