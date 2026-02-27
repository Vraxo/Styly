using System.CommandLine;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Microsoft.Extensions.FileSystemGlobbing;
using Styly.Configuration;
using YamlDotNet.Serialization;

namespace Styly.Core;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        RootCommand rootCommand = new("A .NET code formatter. Automatically finds .styly.yaml and processes C# files.");

        rootCommand.SetHandler(HandleProjectFormatting);
        // Standalone 'format' command for single files
        Argument<FileInfo> fileArgument = new Argument<FileInfo>("file", "The C# file to format.").ExistingOnly();

        Option<NamespaceFormat> namespaceOption = new("--namespace-format", () => NamespaceFormat.File, "Namespace format override.");
        Command formatCommand = new("format", "Formats a single file without project context.");
        formatCommand.AddArgument(fileArgument);
        formatCommand.AddOption(namespaceOption);

        formatCommand.SetHandler(async (file, ns) => await HandleSingleFileReformat(file, new FormatOptions
        {
            Namespace = ns
        }), fileArgument, namespaceOption);

        rootCommand.AddCommand(formatCommand);
        // Path installation helper
        Command installPathCommand = new("install-path", "Adds the tool to the user's PATH.");

        installPathCommand.SetHandler(CliInstaller.InstallPath);
        rootCommand.AddCommand(installPathCommand);

        return await rootCommand.InvokeAsync(args);
    }

    private static async Task HandleProjectFormatting()
    {
        string currentDir = Directory.GetCurrentDirectory();
        string? configPath = FindConfigFile(currentDir);

        if (configPath is null)
        {
            WriteError("Error: Configuration file '.styly.yaml' not found.");
            return;
        }

        StylyConfig config = LoadConfig(configPath);
        string baseDir = Path.GetDirectoryName(configPath)!;
        // 1. Find all matching files
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

        List<string> filePaths = matcher.GetResultsInFullPath(baseDir).ToList();

        if (filePaths.Count == 0)
        {
            Console.WriteLine("No files matched the inclusion patterns.");
            return;
        }

        // 2. Build the Semantic Workspace
        Console.WriteLine($"Found {filePaths.Count} files. Loading semantic context...");

        using AdhocWorkspace workspace = CreateSemanticWorkspace();
        Project project = workspace.CurrentSolution.GetProject(workspace.CurrentSolution.ProjectIds[0])!;
        // 3. Add files to the project
        Dictionary<DocumentId, string> documentIds = new();

        foreach (string path in filePaths)
        {
            SourceText sourceText = SourceText.From(await File.ReadAllTextAsync(path));
            Document document = project.AddDocument(Path.GetFileName(path), sourceText, filePath: path);
            project = document.Project;
            documentIds.Add(document.Id, path);
        }

        // 4. Process each document
        Console.WriteLine("Formatting files...");

        foreach (KeyValuePair<DocumentId, string> entry in documentIds)
        {
            Document doc = project.GetDocument(entry.Key)!;
            await ProcessDocument(doc, config.Format, entry.Value);
            // Refresh project state to maintain semantic accuracy across rewrites
            project = doc.Project.Solution.GetProject(project.Id)!;
        }

        Console.WriteLine("Done.");
    }

    private static AdhocWorkspace CreateSemanticWorkspace()
    {
        AdhocWorkspace workspace = new();
        // Feed the workspace the same DLLs this tool is running on (.NET 10 Core)
        IEnumerable<PortableExecutableReference> references = ((string? )AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES") ?? "").Split(Path.PathSeparator).Where(path => !string.IsNullOrEmpty(path) 
            && File.Exists(path)).Select(path => MetadataReference.CreateFromFile(path));

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
        string yaml = File.ReadAllText(path);
        return new DeserializerBuilder().Build().Deserialize<StylyConfig>(yaml);
    }

    private static string? FindConfigFile(string dir)
    {
        DirectoryInfo? current = new(dir);

        while (current is not null)
        {
            string p1 = Path.Combine(current.FullName, ".styly.yaml");

            if (File.Exists(p1))
            {
                return p1;
            }

            string p2 = Path.Combine(current.FullName, ".styly.yml");

            if (File.Exists(p2))
            {
                return p2;
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