using Styly.Configuration;
using Xunit;

namespace Styly.Tests;

public class EnumerableAnyTests : FormatterTestBase
{
    [Fact]
    public static void Any_To_Count_List()
    {
        string input = """

                            using System.Collections.Generic;
                            using System.Linq;

                            class C
                            {
                                void M()
                                {
                                    List<int> list = new List<int>();
                                    if (list.Any()) { }
                                }
                            }
        """;
        // Expect formatting (braces/newlines) + optimization (Count != 0)
        string expected = """

                            using System.Collections.Generic;
                            using System.Linq;

                            class C
                            {
                                void M()
                                {
                                    List<int> list = new List<int>();
                                    if (list.Count != 0)
                                    {
                                    }
                                }
                            }
        """;

        AssertFormatting(input, expected, new FormatOptions());
    }

    [Fact]
    public static void Any_To_Length_Array()
    {
        string input = """

                            using System.Linq;

                            class C
                            {
                                void M()
                                {
                                    int[] arr = new int[0];
                                    if (arr.Any()) { }
                                }
                            }
        """;

        string expected = """

                            using System.Linq;

                            class C
                            {
                                void M()
                                {
                                    int[] arr = new int[0];
                                    if (arr.Length != 0)
                                    {
                                    }
                                }
                            }
        """;

        AssertFormatting(input, expected, new FormatOptions());
    }

    [Fact]
    public static void Any_Ignored_When_No_Count_Or_Length()
    {
        // IEnumerable<T> does not have Count/Length, so Any() must stay.
        string input = """

                            using System.Collections.Generic;
                            using System.Linq;

                            class C
                            {
                                void M(IEnumerable<int> seq)
                                {
                                    if (seq.Any()) { }
                                }
                            }
        """;

        string expected = """

                            using System.Collections.Generic;
                            using System.Linq;

                            class C
                            {
                                void M(IEnumerable<int> seq)
                                {
                                    if (seq.Any())
                                    {
                                    }
                                }
                            }
        """;

        AssertFormatting(input, expected, new FormatOptions());
    }

    [Fact]
    public static void Any_Ignored_With_Predicate()
    {
        // Any(predicate) cannot be simply converted to Count != 0.
        string input = """

                            using System.Collections.Generic;
                            using System.Linq;

                            class C
                            {
                                void M()
                                {
                                    List<int> list = new List<int>();
                                    if (list.Any(x => x > 5)) { }
                                }
                            }
        """;

        string expected = """

                            using System.Collections.Generic;
                            using System.Linq;

                            class C
                            {
                                void M()
                                {
                                    List<int> list = new List<int>();
                                    if (list.Any(x => x > 5))
                                    {
                                    }
                                }
                            }
        """;

        AssertFormatting(input, expected, new FormatOptions());
    }

    [Fact]
    public static void Any_Is_Converted_And_UnusedUsing_Removed()
    {
        // Verify integration: Optimization happens BEFORE unused using removal.
        // If Any() is the only Linq usage, System.Linq should be removed.
        string input = """

                            using System.Collections.Generic;
                            using System.Linq;

                            class C
                            {
                                void M()
                                {
                                    List<int> list = new List<int>();
                                    if (list.Any()) { }
                                }
                            }
        """;

        string expected = """

                            using System.Collections.Generic;

                            class C
                            {
                                void M()
                                {
                                    List<int> list = new List<int>();
                                    if (list.Count != 0)
                                    {
                                    }
                                }
                            }
        """;

        FormatOptions options = new()
        {
            Usings = new UsingsOptions
            {
                RemoveUnused = true
            }
        };

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void Any_To_Count_Operator_Spacing()
    {
        // Explicit regression test: checks that " != " has spaces around it.
        string input = """

                            using System.Collections.Generic;
                            using System.Linq;

                            class C
                            {
                                void M()
                                {
                                    List<int> list = [];
                                    if(list.Any()){}
                                }
                            }
        """;

        string expected = """

                            using System.Collections.Generic;

                            class C
                            {
                                void M()
                                {
                                    List<int> list = [];
                                    if (list.Count != 0)
                                    {
                                    }
                                }
                            }
        """;

        FormatOptions options = new()
        {
            Usings = new UsingsOptions
            {
                RemoveUnused = true
            }
        };

        AssertFormatting(input, expected, options);
    }
}