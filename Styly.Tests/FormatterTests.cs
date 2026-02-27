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
        string normalizedResult = result.Trim().Replace("\r\n", "\n");
        string normalizedExpected = expected.Trim().Replace("\r\n", "\n");

        Assert.Equal(normalizedExpected, normalizedResult);
    }
}