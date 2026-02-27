using Styly.Configuration;
using Styly.Core;
using Xunit;

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

    [Fact]
    public static void Namespace_FileToBlock_ConvertsAndAddsIndentation()
    {
        string input = """

                                    namespace MySpace;

                                    public class MyClass
                                    {
                                    }

        """;

        string expected = """

                                    namespace MySpace
                                    {
                                        public class MyClass
                                        {
                                        }
                                    }

        """;

        FormatOptions options = new()
        {
            Namespace = NamespaceFormat.Block
        };

        AssertFormatting(input, expected, options);
    }
}