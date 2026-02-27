using Styly.Configuration;

namespace Styly.Tests.Modifier;

public class ModifierStaticSafetyTests : FormatterTestBase
{
    private readonly FormatOptions _options = new()
    {
        Modifiers = new ModifiersOptions { MakeStaticWhenPossible = true }
    };

    [Fact]
    public void Modifiers_KeepInstance_AccessingThisField()
    {
        string input = "class C { int _f; void M() => _f = 1; }";
        string expected = """
            class C
            {
                int _f;
                void M() => _f = 1;
            }
            """;
        AssertFormatting(input, expected, _options);
    }

    [Fact]
    public void Modifiers_MakeStatic_AccessingOtherInstance()
    {
        string input = "class C { public int Val; void Update(C other) => other.Val = 5; }";
        string expected = """
            class C
            {
                public int Val;
                static void Update(C other) => other.Val = 5;
            }
            """;
        AssertFormatting(input, expected, _options);
    }

    [Fact]
    public void Modifiers_KeepInstance_InterfaceImplementation()
    {
        string input = """
            interface I { void M(); }
            class C : I { void I.M() { } }
            """;
        string expected = """
            interface I
            {
                void M();
            }

            class C : I
            {
                void I.M()
                {
                }
            }
            """;
        AssertFormatting(input, expected, _options);
    }

    [Fact]
    public void Modifiers_KeepInstance_VirtualMethods()
    {
        string input = "class C { public virtual void M() { } }";
        string expected = """
            class C
            {
                public virtual void M()
                {
                }
            }
            """;
        AssertFormatting(input, expected, _options);
    }
}