using Styly.Configuration;
using Styly.Core;
using Xunit;

namespace Styly.Tests;

public class UsingsTests : FormatterTestBase
{
    [Fact]
    public static void Usings_SortAlphabetical_SystemFirst()
    {
        string input = @"
using Styly;
using System.Linq;
using System;
using Xunit;
";
        string expected = @"
using System;
using System.Linq;
using Styly;
using Xunit;
";

        FormatOptions options = new()
        {
            Usings = new UsingsOptions
            {
                Sort = UsingSortOrder.Alphabetical
            }
        };

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void Usings_SortAlphabetical_EnsuresBlankLineBeforeNamespace()
    {
        string input = @"
using System.Text;
using System;
namespace MyTestApp;
";

        // Expect a blank line between usings and namespace
        string expected = @"
using System;
using System.Text;

namespace MyTestApp;
";

        FormatOptions options = new()
        {
            Usings = new UsingsOptions
            {
                Sort = UsingSortOrder.Alphabetical
            },
            Namespace = NamespaceFormat.File
        };

        AssertFormatting(input, expected, options);
    }
}