using Styly.Configuration;

namespace Styly.Tests;

public class CollectionTests : FormatterTestBase
{
    [Fact]
    public void Collections_PreferExpression_ListAndArray()
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