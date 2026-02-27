using Styly.Configuration;

namespace Styly.Tests;

public class ModifiersTests : FormatterTestBase
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

    [Fact]
    public void Modifiers_MakeStatic_WithParameters()
    {
        string input = """
            class C
            {
                int Add(int a, int b) => a + b;
            }
            """;

        string expected = """
            class C
            {
                static int Add(int a, int b) => a + b;
            }
            """;

        AssertFormatting(input, expected, _options);
    }

    [Fact]
    public void Modifiers_MakeStatic_AccessingOtherInstance()
    {
        string input = """
            class C
            {
                public int Val;
                void Update(C other) => other.Val = 5;
            }
            """;

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
    public void Modifiers_MakeStatic_AccessingStaticMember()
    {
        string input = """
            class C
            {
                static int G = 0;
                void M() => G++;
            }
            """;

        string expected = """
            class C
            {
                static int G = 0;
                static void M() => G++;
            }
            """;

        AssertFormatting(input, expected, _options);
    }

    [Fact]
    public void Modifiers_KeepInstance_AccessingThisField()
    {
        string input = """
            class C
            {
                int _f;
                void M() => _f = 1;
            }
            """;

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
    public void Modifiers_KeepInstance_AccessingBaseMember()
    {
        string input = """
            class Base { public int B; }
            class C : Base
            {
                void M() => B = 2;
            }
            """;

        string expected = """
            class Base
            {
                public int B;
            }

            class C : Base
            {
                void M() => B = 2;
            }
            """;

        AssertFormatting(input, expected, _options);
    }

    [Fact]
    public void Modifiers_KeepInstance_VirtualMethods()
    {
        string input = """
            class C
            {
                public virtual void M() { }
            }
            """;

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

    [Fact]
    public void Modifiers_KeepInstance_InterfaceImplementation()
    {
        string input = """
            interface I { void M(); }
            class C : I
            {
                void I.M() { }
            }
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
    public void Modifiers_InsertsStaticCorrectly_WithVisibility()
    {
        string input = """
            class C
            {
                public void M() { }
            }
            """;

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