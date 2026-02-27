using Styly.Configuration;

namespace Styly.Tests;

public class UsingsTests : FormatterTestBase
{
    [Fact]
    public static void Usings_SortAlphabetical_SystemFirst()
    {
        string input = """
            using Styly;
            using System.Linq;
            using System;
            using Xunit;
            """;
        string expected = """
            using System;
            using System.Linq;
            using Styly;
            using Xunit;
            """;

        FormatOptions options = new()
        {
            Usings = new UsingsOptions
            {
                Sort = UsingSortOrder.Alphabetical
            }
        };

        AssertFormatting(input, expected, options);
    }
}