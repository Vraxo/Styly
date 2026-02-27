using Styly.Configuration;

namespace Styly.Tests;

public class ExpressionIsolationTests : FormatterTestBase
{
    [Fact]
    public static void Isolation_MultiLineTernary_AddsBlankLines()
    {
        string input = """

                                    void M()
                                    {
                                        var a = 1;
                                        var b = true ? "long_branch_1" : "long_branch_2";
                                        var c = 2;
                                    }
        """;

        string expected = """

                                    void M()
                                    {
                                        var a = 1;

                                        var b = true
                                            ? "long_branch_1"
                                            : "long_branch_2";

                                        var c = 2;
                                    }
        """;

        FormatOptions options = new();
        options.Ternary.Style = TernaryStyle.MultiLine;
        options.Spacing.EmptyLineAroundMultiLineExpression = true;

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void Isolation_MultiLineObjectInitializer_AddsBlankLines()
    {
        string input = """

                                    void M()
                                    {
                                        var x = 10;
                                        var o = new Obj { A = 1, B = 2 };
                                        var y = 20;
                                    }
        """;

        string expected = """

                                    void M()
                                    {
                                        var x = 10;

                                        var o = new Obj
                                        {
                                            A = 1,
                                            B = 2
                                        };

                                        var y = 20;
                                    }
        """;

        FormatOptions options = new();
        options.Initializers.Object = InitializerStyle.MultiLine;
        options.Spacing.EmptyLineAroundMultiLineExpression = true;

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void Isolation_Disabled_KeepsDensity()
    {
        string input = """

                                    void M()
                                    {
                                        var a = 1;
                                        var b = true ? "1" : "2";
                                        var c = 2;
                                    }
        """;
        // Even with MultiLine ternary, no blank lines are added if isolation is false.
        string expected = """

                                    void M()
                                    {
                                        var a = 1;
                                        var b = true
                                            ? "1"
                                            : "2";
                                        var c = 2;
                                    }
        """;

        FormatOptions options = new();
        options.Ternary.Style = TernaryStyle.MultiLine;
        options.Spacing.EmptyLineAroundMultiLineExpression = false;

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void Isolation_MultipleMixed_HandlesFlow()
    {
        string input = """

                                    void M()
                                    {
                                        var a = 1;
                                        var b = true ? "1" : "2";
                                        var c = new { X = 1, Y = 2 };
                                        var d = 4;
                                    }
        """;

        string expected = """

                                    void M()
                                    {
                                        var a = 1;

                                        var b = true
                                            ? "1"
                                            : "2";

                                        var c = new
                                        {
                                            X = 1,
                                            Y = 2
                                        };

                                        var d = 4;
                                    }
        """;

        FormatOptions options = new();
        options.Ternary.Style = TernaryStyle.MultiLine;
        options.Initializers.AnonymousType = InitializerStyle.MultiLine;
        options.Spacing.EmptyLineAroundMultiLineExpression = true;

        AssertFormatting(input, expected, options);
    }
}