namespace Styly.Core;

public static class CliInstaller
{
    public static void InstallPath()
    {
        try
        {
            string? exeDir = AppContext.BaseDirectory;

            if (string.IsNullOrEmpty(exeDir))
            {
                WriteError("Error: Could not determine the application's directory.");
                return;
            }

            // Normalize the directory path by removing any trailing slashes.
            exeDir = exeDir.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

            Console.WriteLine($"Application directory: {exeDir}");

            const EnvironmentVariableTarget scope = EnvironmentVariableTarget.User;
            string pathVar = Environment.GetEnvironmentVariable("PATH", scope) ?? string.Empty;
            string[] paths = pathVar.Split(Path.PathSeparator, StringSplitOptions.RemoveEmptyEntries);

            if (paths.Contains(exeDir, StringComparer.OrdinalIgnoreCase))
            {
                ConsoleColor originalColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("The application directory is already in your PATH.");
                Console.ForegroundColor = originalColor;
                return;
            }

            string newPath = string.IsNullOrEmpty(pathVar)
                ? exeDir
                : $"{pathVar}{Path.PathSeparator}{exeDir}";

            Environment.SetEnvironmentVariable("PATH", newPath, scope);

            WriteSuccess("""

                                        Successfully added the application directory to your user PATH.
            """);

            WriteSuccess("Please restart your terminal session for the changes to take effect.");
        }
        catch (Exception ex)
        {
            WriteError($"An unexpected error occurred: {ex.Message}");
        }
    }

    private static void WriteError(string message)
    {
        ConsoleColor originalColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Error.WriteLine(message);
        Console.ForegroundColor = originalColor;
    }

    private static void WriteSuccess(string message)
    {
        ConsoleColor originalColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ForegroundColor = originalColor;
    }
}