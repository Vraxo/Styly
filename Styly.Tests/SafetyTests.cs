using Styly.Configuration;
using Styly.Core;

namespace Styly.Tests;

public class SafetyTests : FormatterTestBase
{
    [Fact]
    public void Safety_ThrowsOnSyntaxErrors()
    {
        string input = "public class C { int x = ; }"; // Syntax error

        _ = Assert.Throws<InvalidOperationException>(() => CodeFormatter.Reformat(input, new FormatOptions()));
    }
}