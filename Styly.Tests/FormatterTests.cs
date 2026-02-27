using Styly.Configuration;
using Styly.Core;
using Xunit;

namespace Styly.Tests;

public abstract class FormatterTestBase
{
    protected static void AssertFormatting(string input, string expected, FormatOptions options)
    {
        // CodeFormatter.Reformat now internally uses ReformatScriptAsync (AdhocWorkspace),
        // which mimics the standalone behavior suitable for snippets.
        string result = CodeFormatter.Reformat(input.Trim(), options);

        // Normalize line endings for cross-platform assertion
        string normalizedResult = result.Trim().Replace("""


        """, """


        """);
        string normalizedExpected = expected.Trim().Replace("""


        """, """


        """);

        Assert.Equal(normalizedExpected, normalizedResult);
    }
}