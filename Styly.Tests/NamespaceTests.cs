using Styly.Configuration;
using Styly.Core;

namespace Styly.Tests;

public class NamespaceTests : FormatterTestBase
{
    [Fact]
    public static void Namespace_BlockToFile_ConvertsAndRemovesIndentation()
    {
        string input = """
            namespace MySpace
            {
                public class MyClass
                {
                    public void M() { }
                }
            }
            """;

        string expected = """
            namespace MySpace;

            public class MyClass
            {
                public void M()
                {
                }
            }
            """;

        FormatOptions options = new()
        {
            Namespace = NamespaceFormat.File
        };

        AssertFormatting(input, expected, options);
    }
}