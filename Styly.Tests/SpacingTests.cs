using Styly.Configuration;
using Xunit;

namespace Styly.Tests;

public class SpacingTests : FormatterTestBase
{
    [Fact]
    public static void Spacing_EmptyLineBeforeControlFlow_If()
    {
        string input = """

                            void M()
                            {
                                Console.WriteLine();
                                if (true) { }
                            }
        """;

        string expected = """

                            void M()
                            {
                                Console.WriteLine();

                                if (true)
                                {
                                }
                            }
        """;

        FormatOptions options = new();
        options.Spacing.EmptyLineBeforeControlFlow = true;
        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void Spacing_EmptyLineBeforeControlFlow_AlreadyExists_DoesNotDuplicate()
    {
        string input = """

                            void M()
                            {
                                Console.WriteLine();

                                if (true) { }
                            }
        """;

        string expected = """

                            void M()
                            {
                                Console.WriteLine();

                                if (true)
                                {
                                }
                            }
        """;

        FormatOptions options = new();
        options.Spacing.EmptyLineBeforeControlFlow = true;
        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void Spacing_EmptyLineBeforeControlFlow_MultipleTypes()
    {
        string input = """

                            void M()
                            {
                                int x = 1;
                                while (x < 10) x++;
                                x++;
                                for (int i = 0; i < 5; i++) { }
                                x++;
                                foreach (var y in new[] { 1 }) { }
                            }
        """;

        string expected = """

                            void M()
                            {
                                int x = 1;

                                while (x < 10)
                                    x++;
                                x++;

                                for (int i = 0; i < 5; i++)
                                {
                                }

                                x++;

                                foreach (var y in new[] { 1 })
                                {
                                }
                            }
        """;

        FormatOptions options = new();
        options.Spacing.EmptyLineBeforeControlFlow = true;
        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void Spacing_EmptyLineBeforeControlFlow_WithComments()
    {
        string input = """

                            void M()
                            {
                                DoSomething();
                                // Check condition
                                if (true) { }
                            }
        """;

        string expected = """

                            void M()
                            {
                                DoSomething();

                                // Check condition
                                if (true)
                                {
                                }
                            }
        """;

        FormatOptions options = new();
        options.Spacing.EmptyLineBeforeControlFlow = true;
        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void Spacing_EmptyLineBeforeControlFlow_SwitchCase()
    {
        string input = """

                            void M()
                            {
                                switch (x)
                                {
                                    case 1:
                                        Do();
                                        if (true) break;
                                        break;
                                }
                            }
        """;

        string expected = """

                            void M()
                            {
                                switch (x)
                                {
                                    case 1:
                                        Do();

                                        if (true)
                                            break;
                                        break;
                                }
                            }
        """;

        FormatOptions options = new();
        options.Spacing.EmptyLineBeforeControlFlow = true;
        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void Spacing_EmptyLineAfterControlFlow_If()
    {
        string input = """

                            void M()
                            {
                                if (true) { }
                                Console.WriteLine();
                            }
        """;

        string expected = """

                            void M()
                            {
                                if (true)
                                {
                                }

                                Console.WriteLine();
                            }
        """;

        FormatOptions options = new();
        options.Spacing.EmptyLineAfterControlFlow = true;
        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void Spacing_BeforeAndAfter_ControlFlow()
    {
        string input = """

                            void M()
                            {
                                Start();
                                if (true) { }
                                End();
                            }
        """;

        string expected = """

                            void M()
                            {
                                Start();

                                if (true)
                                {
                                }

                                End();
                            }
        """;

        FormatOptions options = new();
        options.Spacing.EmptyLineBeforeControlFlow = true;
        options.Spacing.EmptyLineAfterControlFlow = true;
        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void Spacing_TopLevelStatements()
    {
        string input = """

                            Console.WriteLine("Start");
                            if (args.Length > 0)
                            {
                                Console.WriteLine("Args");
                            }
        """;

        string expected = """

                            Console.WriteLine("Start");

                            if (args.Length > 0)
                            {
                                Console.WriteLine("Args");
                            }
        """;

        FormatOptions options = new();
        options.Spacing.EmptyLineBeforeControlFlow = true;
        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void Spacing_DisabledByDefault()
    {
        string input = """

                            void M()
                            {
                                Do();
                                if (true) { }
                            }
        """;

        string expected = """

                            void M()
                            {
                                Do();
                                if (true)
                                {
                                }
                            }
        """;

        FormatOptions options = new(); // Default is false
        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void Spacing_MultiLineInitializer_AddsBlankLine()
    {
        string input = """

                            void M()
                            {
                                SomeClass modes = new() { A = 1, B = 2 };
                                int integer = 10;
                                var x = new { X = 1 };
                                return;
                            }
        """;

        string expected = """

                            void M()
                            {
                                SomeClass modes = new()
                                {
                                    A = 1,
                                    B = 2
                                };

                                int integer = 10;
                                var x = new { X = 1 };
                                return;
                            }
        """;

        FormatOptions options = new();
        options.Initializers.Object = InitializerStyle.MultiLine;
        options.Initializers.AnonymousType = InitializerStyle.SingleLine;

        AssertFormatting(input, expected, options);
    }
}