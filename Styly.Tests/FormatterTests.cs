using Styly.Configuration;
using Styly.Core;

namespace Styly.Tests;

public abstract class FormatterTestBase
{
    protected static void AssertFormatting(string input, string expected, FormatOptions options)
    {
        // 1. Run the formatter
        string result = CodeFormatter.Reformat(input.Trim(), options);
        // 2. Normalize both strings for comparison
        string normalizedResult = CleanString(result);
        string normalizedExpected = CleanString(expected);
        // 3. Compare the "clean" versions
        Assert.Equal(normalizedExpected, normalizedResult);
    }

    private static string CleanString(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return string.Empty;
        }

        // Convert to common line ending
        string normalized = text.Replace("""


        """, """


        """).Trim();
        // Split into lines
        string[] lines = normalized.Split('\n');
        // Find the minimum indentation across all non-empty lines
        int minIndent = int.MaxValue;

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            int indent = line.TakeWhile(char.IsWhiteSpace).Count();

            if (indent < minIndent)
            {
                minIndent = indent;
            }
        }

        if (minIndent == int.MaxValue)
        {
            minIndent = 0;
        }

        // Strip that indentation from every line to ensure "flush-left" comparison
        IEnumerable<string> cleanedLines = lines.Select(line => line.Length >= minIndent
            ? line[minIndent..].TrimEnd()
            : line.TrimEnd());

        return string.Join("""


        """, cleanedLines).Trim();
    }
}