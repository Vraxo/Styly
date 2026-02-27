using Styly.Configuration;
using Xunit;

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
        // Method using only locals should become static.
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
        // Method using parameters (but no instance data) should become static.
        string input = """

                            class C
                            {
                                int Add(int a, int b)
                                {
                                    return a + b;
                                }
                            }
        """;

        string expected = """

                            class C
                            {
                                static int Add(int a, int b)
                                {
                                    return a + b;
                                }
                            }
        """;

        AssertFormatting(input, expected, _options);
    }

    [Fact]
    public void Modifiers_MakeStatic_AccessingOtherInstance()
    {
        // Accessing a member on *another* instance (param) is fine.
        string input = """

                            class C
                            {
                                public int Val;
                                void Update(C other)
                                {
                                    other.Val = 5;
                                }
                            }
        """;

        string expected = """

                            class C
                            {
                                public int Val;
                                static void Update(C other)
                                {
                                    other.Val = 5;
                                }
                            }
        """;

        AssertFormatting(input, expected, _options);
    }

    [Fact]
    public void Modifiers_MakeStatic_AccessingStaticMember()
    {
        // Accessing a static field of the class is fine.
        string input = """

                            class C
                            {
                                static int Global = 0;
                                void M()
                                {
                                    Global++;
                                }
                            }
        """;

        string expected = """

                            class C
                            {
                                static int Global = 0;
                                static void M()
                                {
                                    Global++;
                                }
                            }
        """;

        AssertFormatting(input, expected, _options);
    }

    [Fact]
    public void Modifiers_KeepInstance_AccessingThisField()
    {
        // Implicit 'this.Field' access -> Should NOT be static.
        string input = """

                            class C
                            {
                                int _f;
                                void M()
                                {
                                    _f = 1;
                                }
                            }
        """;

        string expected = """

                            class C
                            {
                                int _f;
                                void M()
                                {
                                    _f = 1;
                                }
                            }
        """;

        AssertFormatting(input, expected, _options);
    }

    [Fact]
    public void Modifiers_KeepInstance_AccessingBaseMember()
    {
        // Accessing base member -> Should NOT be static.
        string input = """

                            class Base { public int B; }
                            class C : Base
                            {
                                void M()
                                {
                                    B = 2;
                                }
                            }
        """;

        string expected = """

                            class Base
                            {
                                public int B;
                            }

                            class C : Base
                            {
                                void M()
                                {
                                    B = 2;
                                }
                            }
        """;

        AssertFormatting(input, expected, _options);
    }

    [Fact]
    public void Modifiers_KeepInstance_ExplicitThis()
    {
        string input = """

                            class C
                            {
                                void M()
                                {
                                    var t = this;
                                }
                            }
        """;

        string expected = """

                            class C
                            {
                                void M()
                                {
                                    var t = this;
                                }
                            }
        """;

        AssertFormatting(input, expected, _options);
    }

    [Fact]
    public void Modifiers_KeepInstance_VirtualMethods()
    {
        // Virtual methods cannot be static.
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
        // Explicit interface implementation cannot be static.
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
        // Should insert 'static' after 'public'.
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