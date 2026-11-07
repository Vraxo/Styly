using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Microsoft.Extensions.FileSystemGlobbing;
using Styly.Configuration;
using Styly.Core;
using System.CommandLine;
using System.Text.Json;
using Tomlyn;

namespace Styly.Tests;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        RootCommand rootCommand = new("A .NET code formatter. Automatically finds .styly.toml and processes C# files.");

        rootCommand.SetAction(async (parseResult, cancellationToken) => await HandleProjectFormatting());

        Argument<FileInfo> fileArgument = new("file") { Description = "The C# file to format." };
        fileArgument.AcceptExistingOnly();

        Option<NamespaceFormat> namespaceOption = new("--namespace-format")
        {
            Description = "Namespace format override.",
            DefaultValueFactory = _ => NamespaceFormat.File
        };

        Command formatCommand = new("format", "Formats a single file without project context.");
        formatCommand.Arguments.Add(fileArgument);
        formatCommand.Options.Add(namespaceOption);

        formatCommand.SetAction(async (parseResult, cancellationToken) =>
        {
            FileInfo? file = parseResult.GetValue(fileArgument);
            NamespaceFormat ns = parseResult.GetValue(namespaceOption);
            if (file is not null)
            {
                await HandleSingleFileReformat(file, new FormatOptions { Namespace = ns });
            }
        });

        rootCommand.Subcommands.Add(formatCommand);

        Command initCommand = new("init", "Initializes a default .styly.toml configuration file.");
        initCommand.SetAction(parseResult => HandleInitConfig());
        rootCommand.Subcommands.Add(initCommand);

        Command installPathCommand = new("install-path", "Adds the tool to the user's PATH.");
        installPathCommand.SetAction(parseResult => CliInstaller.InstallPath());
        rootCommand.Subcommands.Add(installPathCommand);

        return await rootCommand.Parse(args).InvokeAsync();
    }

    private static void HandleInitConfig()
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), ".styly.toml");

        if (File.Exists(path))
        {
            WriteError($"Error: Configuration file already exists at '{path}'.");
            return;
        }

        try
        {
            StylyConfig defaultConfig = new();
            TomlSerializerOptions options = new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            string toml = TomlSerializer.Serialize(defaultConfig, options);
            File.WriteAllText(path, toml);

            Console.WriteLine($"Successfully created '.styly.toml' at '{path}'.");
        }
        catch (Exception ex)
        {
            WriteError($"Error creating configuration file: {ex.Message}");
        }
    }

    private static async Task HandleProjectFormatting()
    {
        string currentDir = Directory.GetCurrentDirectory();
        string? configPath = FindConfigFile(currentDir);

        if (configPath is null)
        {
            WriteError("Error: Configuration file '.styly.toml' not found.");
            return;
        }

        StylyConfig config = LoadConfig(configPath);
        string baseDir = Path.GetDirectoryName(configPath)!;

        bool flowControl = FindAllMatchingFiles(config, baseDir, out List<string> filePaths);

        if (!flowControl)
        {
            return;
        }

        Console.WriteLine($"Found {filePaths.Count} files. Loading semantic context...");
        using AdhocWorkspace workspace = CreateSemanticWorkspace();
        Project project = workspace.CurrentSolution.GetProject(workspace.CurrentSolution.ProjectIds[0])!;

        project = await AddFilesToTheProject(config, filePaths, project);
    }

    private static bool FindAllMatchingFiles(StylyConfig config, string baseDir, out List<string> filePaths)
    {
        Matcher matcher = new();

        if (config.Include.Count == 0)
        {
            _ = matcher.AddInclude("**/*.cs");
        }
        else
        {
            config.Include.ForEach(p => matcher.AddInclude(p));
        }

        config.Exclude.ForEach(p => matcher.AddExclude(p));

        filePaths = [.. matcher.GetResultsInFullPath(baseDir)];

        if (filePaths.Count == 0)
        {
            Console.WriteLine("No files matched the inclusion patterns.");
            return false;
        }

        return true;
    }

    private static async Task<Project> AddFilesToTheProject(StylyConfig config, List<string> filePaths, Project project)
    {
        Dictionary<DocumentId, string> documentIds = [];

        foreach (string path in filePaths)
        {
            SourceText sourceText = SourceText.From(await File.ReadAllTextAsync(path));
            Document document = project.AddDocument(Path.GetFileName(path), sourceText, filePath: path);
            project = document.Project;

            documentIds.Add(document.Id, path);
        }

        Console.WriteLine("Formatting files...");

        foreach (KeyValuePair<DocumentId, string> entry in documentIds)
        {
            Document doc = project.GetDocument(entry.Key)!;
            await ProcessDocument(doc, config.Format, entry.Value);
            project = doc.Project.Solution.GetProject(project.Id)!;
        }

        Console.WriteLine("Done.");
        return project;
    }

    private static AdhocWorkspace CreateSemanticWorkspace()
    {
        AdhocWorkspace workspace = new();
        IEnumerable<PortableExecutableReference> references = ((string?)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES") ?? "")
            .Split(Path.PathSeparator)
            .Where(path => !string.IsNullOrEmpty(path) && File.Exists(path))
            .Select(path => MetadataReference.CreateFromFile(path));

        _ = workspace.AddProject("StylyContext", LanguageNames.CSharp).WithMetadataReferences(references);

        return workspace;
    }

    private static async Task ProcessDocument(Document document, FormatOptions options, string filePath)
    {
        string fileName = Path.GetFileName(filePath);

        try
        {
            string originalText = (await document.GetTextAsync()).ToString();
            Document formattedDoc = await CodeFormatter.ReformatAsync(document, options);
            string newText = (await formattedDoc.GetTextAsync()).ToString();

            if (originalText != newText)
            {
                await File.WriteAllTextAsync(filePath, newText);
                Console.WriteLine($"  Formatted: {fileName}");
            }
            else
            {
                Console.WriteLine($"  Unchanged: {fileName}");
            }
        }
        catch (Exception ex)
        {
            WriteError($"  Failed:    {fileName} ({ex.Message})");
        }
    }

    private static async Task HandleSingleFileReformat(FileInfo file, FormatOptions options)
    {
        Console.WriteLine($"Processing {file.Name}...");
        string source = await File.ReadAllTextAsync(file.FullName);
        string formatted = await CodeFormatter.ReformatScriptAsync(source, options);

        if (source != formatted)
        {
            await File.WriteAllTextAsync(file.FullName, formatted);
            Console.WriteLine("  Status: Formatted");
        }
        else
        {
            Console.WriteLine("  Status: Unchanged");
        }
    }

    private static StylyConfig LoadConfig(string path)
    {
        string toml = File.ReadAllText(path);
        TomlSerializerOptions options = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        return TomlSerializer.Deserialize<StylyConfig>(toml, options) ?? new StylyConfig();
    }

    private static string? FindConfigFile(string dir)
    {
        DirectoryInfo? current = new(dir);

        while (current is not null)
        {
            string path = Path.Combine(current.FullName, ".styly.toml");

            if (File.Exists(path))
            {
                return path;
            }

            current = current.Parent;
        }

        return null;
    }

    private static void WriteError(string msg)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(msg);
        Console.ResetColor();
    }
}