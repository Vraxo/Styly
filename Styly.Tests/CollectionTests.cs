using Styly.Configuration;
using Xunit;

namespace Styly.Tests;

public class CollectionTests : FormatterTestBase
{
    [Fact]
    public static void Collections_PreferExpression_ListAndArray()
    {
        string input = """

                    using System.Collections.Generic;
                    void M()
                    {
                        List<int> l = new List<int> { 1, 2 };
                        int[] a = new int[] { 3, 4 };
                        int[] b = new int[0];
                    }
        """;
        // Note: formatting of the resulting expression [1, 2] depends on Initializer options.
        // Default is Preserve, but the rewriter might output single line by default if untouched.
        // We assume standard single-line output from the CollectionExpressionRewriter before formatting applies.
        // NormalizeWhitespace inserts a blank line between usings and the first member.
        string expected = """

                    using System.Collections.Generic;

                    void M()
                    {
                        List<int> l = [1, 2];
                        int[] a = [3, 4];
                        int[] b = [];
                    }
        """;

        FormatOptions options = new()
        {
            Collections = new CollectionsOptions
            {
                PreferExpression = true
            }
        };

        AssertFormatting(input, expected, options);
    }
}