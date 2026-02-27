using Styly.Configuration;

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