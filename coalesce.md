### `Styly\Styly.csproj`

```xml
<Project Sdk="Microsoft.NET.Sdk">

	<PropertyGroup>
		<OutputType>Exe</OutputType>
		<TargetFramework>net10.0</TargetFramework>
		<ImplicitUsings>enable</ImplicitUsings>
		<Nullable>enable</Nullable>
		<SatelliteResourceLanguages>en</SatelliteResourceLanguages>
		<DisableMSBuildAssemblyCopyCheck>true</DisableMSBuildAssemblyCopyCheck>
		<CopyLocalLockFileAssemblies>true</CopyLocalLockFileAssemblies>
	</PropertyGroup>

	<PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Debug|AnyCPU'">
		<TreatWarningsAsErrors>False</TreatWarningsAsErrors>
	</PropertyGroup>

	<PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Release|AnyCPU'">
		<TreatWarningsAsErrors>False</TreatWarningsAsErrors>
	</PropertyGroup>

	<ItemGroup>
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-net472\cs\System.CommandLine.resources.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-net472\de\System.CommandLine.resources.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-net472\es\System.CommandLine.resources.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-net472\fr\System.CommandLine.resources.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-net472\it\System.CommandLine.resources.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-net472\ja\System.CommandLine.resources.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-net472\ko\System.CommandLine.resources.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-net472\Microsoft.Build.Locator.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-net472\Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.exe" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-net472\Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.exe.config" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-net472\Microsoft.IO.Redist.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-net472\Newtonsoft.Json.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-net472\pl\System.CommandLine.resources.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-net472\pt-BR\System.CommandLine.resources.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-net472\ru\System.CommandLine.resources.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-net472\System.Buffers.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-net472\System.Collections.Immutable.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-net472\System.CommandLine.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-net472\System.Memory.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-net472\System.Numerics.Vectors.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-net472\System.Runtime.CompilerServices.Unsafe.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-net472\System.Threading.Tasks.Extensions.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-net472\tr\System.CommandLine.resources.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-net472\zh-Hans\System.CommandLine.resources.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-net472\zh-Hant\System.CommandLine.resources.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-netcore\cs\System.CommandLine.resources.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-netcore\de\System.CommandLine.resources.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-netcore\es\System.CommandLine.resources.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-netcore\fr\System.CommandLine.resources.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-netcore\it\System.CommandLine.resources.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-netcore\ja\System.CommandLine.resources.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-netcore\ko\System.CommandLine.resources.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-netcore\Microsoft.Build.Locator.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-netcore\Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.deps.json" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-netcore\Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-netcore\Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.runtimeconfig.json" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-netcore\Newtonsoft.Json.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-netcore\pl\System.CommandLine.resources.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-netcore\pt-BR\System.CommandLine.resources.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-netcore\ru\System.CommandLine.resources.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-netcore\System.Collections.Immutable.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-netcore\System.CommandLine.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-netcore\tr\System.CommandLine.resources.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-netcore\zh-Hans\System.CommandLine.resources.dll" />
	  <Content Remove="C:\Users\Parsa\.nuget\packages\microsoft.codeanalysis.workspaces.msbuild\5.0.0\contentFiles\any\any\BuildHost-netcore\zh-Hant\System.CommandLine.resources.dll" />
	</ItemGroup>

	<ItemGroup>
		<!-- MSBuild.Locator -->
		<PackageReference Include="Microsoft.Build.Locator" Version="1.11.2" />

		<!-- Fix MSBL001: Explicitly exclude all MSBuild-related runtime assets -->
		<PackageReference Include="Microsoft.Build" Version="17.11.4" ExcludeAssets="runtime" PrivateAssets="all" />
		<PackageReference Include="Microsoft.Build.Framework" Version="17.11.31" ExcludeAssets="runtime" PrivateAssets="all" />
		<PackageReference Include="Microsoft.NET.StringTools" Version="17.11.4" ExcludeAssets="runtime" PrivateAssets="all" />
		<PackageReference Include="NuGet.Frameworks" Version="6.11.1" ExcludeAssets="runtime" PrivateAssets="all" />

		<!-- Roslyn packages - ALL version 5.0.0 -->
		<PackageReference Include="Microsoft.CodeAnalysis.Common" Version="5.0.0" />
		<PackageReference Include="Microsoft.CodeAnalysis.CSharp" Version="5.0.0" />
		<PackageReference Include="Microsoft.CodeAnalysis.CSharp.Workspaces" Version="5.0.0" />
		<PackageReference Include="Microsoft.CodeAnalysis.Workspaces.MSBuild" Version="5.0.0" />

		<!-- Other packages -->
		<PackageReference Include="Microsoft.Extensions.FileSystemGlobbing" Version="10.0.3" />
		<PackageReference Include="Microsoft.Extensions.ObjectPool" Version="8.0.0" />
		<PackageReference Include="System.CommandLine" Version="2.0.0-beta4.22272.1" />
		<PackageReference Include="YamlDotNet" Version="16.3.0" />
	</ItemGroup>

	<!-- Only remove Microsoft.Extensions.Logging.Abstractions -->
	<Target Name="RemoveProblematicAssemblies" AfterTargets="Build">
		<ItemGroup>
			<FilesToDelete Include="$(OutDir)Microsoft.Extensions.Logging.Abstractions.*" />
		</ItemGroup>
		<Delete Files="@(FilesToDelete)" ContinueOnError="true" />
	</Target>

</Project>
```

---

### `Styly\Configuration\CallChainOptions.cs`

```csharp
namespace Styly.Configuration;

public class CallChainOptions
{
    public CallChainStyle Style { get; set; } = CallChainStyle.Preserve;
}
```

---

### `Styly\Configuration\CallChainStyle.cs`

```csharp
namespace Styly.Configuration;

public enum CallChainStyle
{
    Preserve,
    SingleLine,
    MultiLine
}
```

---

### `Styly\Configuration\CollectionsOptions.cs`

```csharp
namespace Styly.Configuration;

public class CollectionsOptions
{
    public bool PreferExpression { get; set; }
}
```

---

### `Styly\Configuration\FormatOptions.cs`

```csharp
namespace Styly.Configuration;

public class FormatOptions
{
    public NamespaceFormat Namespace { get; set; } = NamespaceFormat.File;
    public UsingsOptions Usings { get; set; } = new();
    public VariablesOptions? Variables { get; set; }
    public CollectionsOptions? Collections { get; set; }
    public InitializerOptions Initializers { get; set; } = new();
    public SpacingOptions Spacing { get; set; } = new();
    public ModifiersOptions Modifiers { get; set; } = new();
    public OptimizationOptions Optimization { get; set; } = new();
    public TernaryOptions Ternary { get; set; } = new();
    public RawStringsOptions RawStrings { get; set; } = new();
    public LogicalExpressionOptions LogicalExpressions { get; set; } = new();
    public CallChainOptions CallChain { get; set; } = new();
}
```

---

### `Styly\Configuration\InitializerOptions.cs`

```csharp
namespace Styly.Configuration;

public class InitializerOptions
{
    public InitializerStyle AnonymousType { get; set; } = InitializerStyle.Preserve;
    public InitializerStyle Object { get; set; } = InitializerStyle.Preserve;
    public InitializerStyle Collection { get; set; } = InitializerStyle.Preserve;
}
```

---

### `Styly\Configuration\InitializerStyle.cs`

```csharp
namespace Styly.Configuration;

public enum InitializerStyle
{
    Preserve,
    SingleLine,
    MultiLine
}
```

---

### `Styly\Configuration\LogicalExpressionOptions.cs`

```csharp
namespace Styly.Configuration;

public class LogicalExpressionOptions
{
    public LogicalExpressionStyle Style { get; set; } = LogicalExpressionStyle.Preserve;
}
```

---

### `Styly\Configuration\LogicalExpressionStyle.cs`

```csharp
namespace Styly.Configuration;

public enum LogicalExpressionStyle
{
    Preserve,
    SingleLine,
    MultiLine
}
```

---

### `Styly\Configuration\ModifierOptions.cs`

```csharp
namespace Styly.Configuration;

public class ModifiersOptions
{
    public bool MakeStaticWhenPossible { get; set; }
}
```

---

### `Styly\Configuration\NamespaceFormat.cs`

```csharp
namespace Styly.Configuration;

public enum NamespaceFormat
{
    File,
    Block
}
```

---

### `Styly\Configuration\OptimizationOptions.cs`

```csharp
namespace Styly.Configuration;

public class OptimizationOptions
{
    public bool PreferLengthCountOverAny { get; set; } = true;
    public bool PreferNullPatterns { get; set; } = true;
}
```

---

### `Styly\Configuration\RawStringsOptions.cs`

```csharp
namespace Styly.Configuration;

public class RawStringsOptions
{
    public bool PreferRawForMultiline { get; set; } = true;
}
```

---

### `Styly\Configuration\SpacingOptions.cs`

```csharp
namespace Styly.Configuration;

public class SpacingOptions
{
    public bool EmptyLineBeforeControlFlow { get; set; }
    public bool EmptyLineAfterControlFlow { get; set; }
    public bool EmptyLineAroundMultiLineExpression { get; set; } = true;
}
```

---

### `Styly\Configuration\StyleConfig.cs`

```csharp
namespace Styly.Configuration;

public class StylyConfig
{
    public FormatOptions Format { get; set; } = new();
    public List<string> Include { get; set; } = [];
    public List<string> Exclude { get; set; } = [];
    public List<string> References { get; set; } = [];
}
```

---

### `Styly\Configuration\TernaryOptions.cs`

```csharp
namespace Styly.Configuration;

public class TernaryOptions
{
    public TernaryStyle Style { get; set; } = TernaryStyle.Preserve;
}
```

---

### `Styly\Configuration\TernaryStyle.cs`

```csharp
namespace Styly.Configuration;

public enum TernaryStyle
{
    Preserve,
    SingleLine,
    MultiLine
}
```

---

### `Styly\Configuration\UseVarOption.cs`

```csharp
namespace Styly.Configuration;

public enum UseVarOption
{
    WhenApparent,
    Always,
    Never
}
```

---

### `Styly\Configuration\UsingsOptions.cs`

```csharp
namespace Styly.Configuration;

public class UsingsOptions
{
    public UsingSortOrder Sort { get; set; } = UsingSortOrder.None;
    public bool RemoveUnused { get; set; }
}
```

---

### `Styly\Configuration\UsingSortOrder.cs`

```csharp
namespace Styly.Configuration;

public enum UsingSortOrder
{
    None,
    Alphabetical
}
```

---

### `Styly\Configuration\VariablesOptions.cs`

```csharp
namespace Styly.Configuration;

public class VariablesOptions
{
    public UseVarOption? UseVar { get; set; }
}
```

---

### `Styly\Core\CliInstaller.cs`

```csharp
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

            string pathVar = Environment.GetEnvironmentVariable("PATH", scope)
                ?? string.Empty;

            string[] paths = pathVar.Split(Path.PathSeparator, StringSplitOptions.RemoveEmptyEntries);

            if (paths.Contains(exeDir, StringComparer.OrdinalIgnoreCase))
            {
                PrintAlreadyInstalled();
                return;
            }

            Install(exeDir, scope, pathVar);
        }
        catch (Exception ex)
        {
            WriteError($"An unexpected error occurred: {ex.Message}");
        }
    }

    private static void Install(string exeDir, EnvironmentVariableTarget scope, string pathVar)
    {
        string newPath = GetNewPath(exeDir, pathVar);

        Environment.SetEnvironmentVariable("PATH", newPath, scope);

        WriteSuccess("""

                        Successfully added the application directory to your user PATH.
            """);

        WriteSuccess("Please restart your terminal session for the changes to take effect.");
    }

    private static string GetNewPath(string exeDir, string pathVar)
    {
        return string.IsNullOrEmpty(pathVar)
            ? exeDir
            : $"{pathVar}{Path.PathSeparator}{exeDir}";
    }

    private static void PrintAlreadyInstalled()
    {
        ConsoleColor originalColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Yellow;

        Console.WriteLine("The application directory is already in your PATH.");

        Console.ForegroundColor = originalColor;
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
```

---

### `Styly\Core\CodeFormatter.cs`

```csharp
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Styly.Configuration;
using Styly.Rewriters;
using Styly.Rewriters.Initializer;
using Styly.Rewriters.LogicalExpression;

namespace Styly.Core;

public static class CodeFormatter
{
    public static async Task<Document> ReformatAsync(Document document, FormatOptions formatOptions)
    {
        SyntaxNode root = await document.GetSyntaxRootAsync() ?? throw new InvalidOperationException("Could not get syntax root.");
        // Fail-Fast: Verify syntax is valid before formatting
        IEnumerable<Diagnostic> diagnostics = root.GetDiagnostics();

        if (diagnostics.Any(d => d.Severity == DiagnosticSeverity.Error))
        {
            Diagnostic firstError = diagnostics.First(d => d.Severity == DiagnosticSeverity.Error);
            throw new InvalidOperationException($"Syntax error at {firstError.Location.GetLineSpan().StartLinePosition}: {firstError.GetMessage()}");
        }

        // 1. Syntactic Cleaning (Layout)
        root = ApplyBasicCleaning(root);
        document = document.WithSyntaxRoot(root);
        // 2. Semantic Transformations (var, Any, usings)
        document = await ApplySemanticRewritersAsync(document, formatOptions);
        // 3. Final Layout Polish
        root = await document.GetSyntaxRootAsync() ?? throw new InvalidOperationException();
        root = ApplySyntacticRewriters(root, formatOptions);

        return document.WithSyntaxRoot(root);
    }

    public static async Task<string> ReformatScriptAsync(string sourceText, FormatOptions formatOptions)
    {
        using AdhocWorkspace workspace = new();

        IEnumerable<PortableExecutableReference> references = ((string?)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES") ?? "")
            .Split(Path.PathSeparator)
            .Where(path => !string.IsNullOrEmpty(path) && File.Exists(path))
            .Select(path => MetadataReference.CreateFromFile(path));

        Project project = workspace.AddProject("ScriptProject", LanguageNames.CSharp).WithMetadataReferences(references);

        Document document = project.AddDocument("Script.cs", sourceText);
        return await ReformatDocumentInternalAsync(document, formatOptions);
    }

    private static async Task<string> ReformatDocumentInternalAsync(Document document, FormatOptions formatOptions)
    {
        Document formatted = await ReformatAsync(document, formatOptions);
        return (await formatted.GetTextAsync()).ToString();
    }

    // Kept for tests
    public static string Reformat(string sourceText, FormatOptions formatOptions)
    {
        return ReformatScriptAsync(sourceText, formatOptions).GetAwaiter().GetResult();
    }

    private static SyntaxNode ApplyBasicCleaning(SyntaxNode root)
    {
        root = new LayoutAnnotator().Visit(root);

        return root.NormalizeWhitespace(indentation: "    ", eol: """


        """);
    }

    private static async Task<Document> ApplySemanticRewritersAsync(Document document, FormatOptions options)
    {
        if (options.Variables?.UseVar is not null)
        {
            (SemanticModel? model, SyntaxNode? root) = await GetContext(document);
            SyntaxNode newRoot = GetNewRoot(options, model, root);

            document = document.WithSyntaxRoot(newRoot);
        }

        if (options.Collections?.PreferExpression == true)
        {
            (SemanticModel? model, SyntaxNode? root) = await GetContext(document);
            document = document.WithSyntaxRoot(new CollectionExpressionRewriter(model).Visit(root));
        }

        if (options.Modifiers.MakeStaticWhenPossible)
        {
            (SemanticModel? model, SyntaxNode? root) = await GetContext(document);
            document = document.WithSyntaxRoot(new StaticModifierRewriter(model).Visit(root));
        }

        if (options.Optimization.PreferLengthCountOverAny)
        {
            (SemanticModel? model, SyntaxNode? root) = await GetContext(document);
            document = document.WithSyntaxRoot(new EnumerableAnyRewriter(model).Visit(root));
        }

        if (options.Optimization.PreferNullPatterns)
        {
            (SemanticModel _, SyntaxNode? root) = await GetContext(document);
            document = document.WithSyntaxRoot(new NullCheckPatternRewriter().Visit(root));
        }

        if (options.Usings.RemoveUnused)
        {
            return await NameMe(ref document);
        }

        return document;
    }

    private static SyntaxNode GetNewRoot(FormatOptions options, SemanticModel model, SyntaxNode root)
    {
        return options.Variables.UseVar == UseVarOption.Never
            ? new VarToExplicitTypeRewriter(model).Visit(root)
            : new ExplicitTypeToVarRewriter(model, options.Variables.UseVar.Value).Visit(root);
    }

    private static async Task<Document> NameMe(ref Document document)
    {
        (SemanticModel? model, SyntaxNode? root) = await GetContext(document);

        IEnumerable<UsingDirectiveSyntax> unused = model
            .GetDiagnostics()
            .Where(d => d.Id == "CS8019")
            .Select(d => root.FindNode(d.Location.SourceSpan))
            .OfType<UsingDirectiveSyntax>();

        if (!unused.Any())
        {
            return document;
        }

        document = document.WithSyntaxRoot(root.RemoveNodes(unused, SyntaxRemoveOptions.KeepNoTrivia)!);

        return document;
    }

    private static async Task<(SemanticModel, SyntaxNode)> GetContext(Document document)
    {
        SemanticModel semanticModel = await document.GetSemanticModelAsync()
            ?? throw new Exception();

        SyntaxNode syntaxNode = await document.GetSyntaxRootAsync()
            ?? throw new Exception();

        return (semanticModel, syntaxNode);
    }

    private static SyntaxNode? ApplySyntacticRewriters(SyntaxNode root, FormatOptions options)
    {
        root = new NamespaceRewriter(options.Namespace).Visit(root);
        root = new InitializerRewriter(options.Initializers).Visit(root);
        root = new TernaryRewriter(options.Ternary).Visit(root);
        root = new RawStringRewriter(options.RawStrings).Visit(root);
        root = new LogicalExpressionRewriter(options.LogicalExpressions).Visit(root);
        root = new CallChainRewriter(options.CallChain).Visit(root);

        root = new StructuralSpacingRewriter().Visit(root);
        root = new VerticalRhythmRewriter(options.Spacing).Visit(root);

        if (options.Usings.Sort == UsingSortOrder.Alphabetical)
        {
            root = new UsingSorterRewriter().Visit(root);
        }

        root = new TrailingTriviaTrimmer().Visit(root);
        return new TokenSpacingCleanupRewriter().Visit(root)!;
    }
}
```

---

### `Styly\Core\Program.cs`

```csharp
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
        bool flowControl = FindAllMatchingFiles(config, baseDir, out List<string> filePaths);

        if (!flowControl)
        {
            return;
        }

        // 2. Build the Semantic Workspace
        Console.WriteLine($"Found {filePaths.Count} files. Loading semantic context...");
        using AdhocWorkspace workspace = CreateSemanticWorkspace();
        Project project = workspace.CurrentSolution.GetProject(workspace.CurrentSolution.ProjectIds[0])!;
        // 3. Add files to the project
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

        filePaths = [ ..matcher.GetResultsInFullPath(baseDir) ];

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
        return project;
    }

    private static AdhocWorkspace CreateSemanticWorkspace()
    {
        AdhocWorkspace workspace = new();
        // Feed the workspace the same DLLs this tool is running on (.NET 10 Core)
        IEnumerable<PortableExecutableReference> references = ((string? )AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES") ?? "")
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
```

---

### `Styly\Rewriters\CallChainRewriter.cs`

```csharp
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Styly.Configuration;

namespace Styly.Rewriters;

internal class CallChainRewriter : CSharpSyntaxRewriter
{
    private readonly CallChainOptions _options;
    private const int IndentSize = 4;
    public CallChainRewriter(CallChainOptions options)
    {
        _options = options;
    }

    public override SyntaxNode? VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
    {
        if (node.Parent is MemberAccessExpressionSyntax)
        {
            return base.VisitMemberAccessExpression(node);
        }

        if (_options.Style == CallChainStyle.Preserve)
        {
            SyntaxNode syntaxNode = node.HasAnnotations(LayoutAnnotator.MultiLineCallChainAnnotationKind)
                ? FormatMultiLine(node)
                : node;

            return (SyntaxNode? )syntaxNode;
        }

        if (!IsChained(node))
        {
            return base.VisitMemberAccessExpression(node);
        }

        return (SyntaxNode? )GetExpressionSyntax(node);
    }

    private ExpressionSyntax GetExpressionSyntax(MemberAccessExpressionSyntax node)
    {
        return _options.Style == CallChainStyle.MultiLine
            ? FormatMultiLine(node)
            : FormatSingleLine(node);
    }

    private static bool IsChained(MemberAccessExpressionSyntax node)
    {
        int count = 1;

        if (node.Parent is InvocationExpressionSyntax)
        {
            count++;
        }

        ExpressionSyntax? current = node.Expression;

        while (true)
        {
            if (current is MemberAccessExpressionSyntax memberAccess)
            {
                count++;
                current = memberAccess.Expression;
                continue;
            }

            if (current is InvocationExpressionSyntax invocation)
            {
                count++;
                current = invocation.Expression;
                continue;
            }

            break;
        }

        return count >= 2;
    }

    private static ExpressionSyntax FormatMultiLine(MemberAccessExpressionSyntax node)
    {
        SyntaxTriviaList parentIndent = GetParentIndentation(node);
        SyntaxTriviaList itemIndent = parentIndent.Add(SyntaxFactory.Whitespace(new string (' ', IndentSize)));
        SyntaxTrivia newline = SyntaxFactory.CarriageReturnLineFeed;

        ExpressionSyntax wrapped = WrapChainRecursive(node, itemIndent, newline);

        return wrapped.WithLeadingTrivia(node.GetLeadingTrivia()).WithTrailingTrivia(node.GetTrailingTrivia());
    }

    private static ExpressionSyntax WrapChainRecursive(ExpressionSyntax node, SyntaxTriviaList indent, SyntaxTrivia newline)
    {
        if (node is InvocationExpressionSyntax invocation)
        {
            return NameMeToo(indent, newline, invocation);
        }

        if (node is not MemberAccessExpressionSyntax memberAccess)
        {
            return node.WithTrailingTrivia(StripTrailingNewlines(node.GetTrailingTrivia()));
        }

        return NameMeAsap(indent, newline, memberAccess);
    }

    private static ExpressionSyntax NameMeToo(SyntaxTriviaList indent, SyntaxTrivia newline, InvocationExpressionSyntax invocation)
    {
        ExpressionSyntax wrappedExpression = WrapChainRecursive(invocation.Expression, indent, newline);

        return SyntaxFactory
            .InvocationExpression(wrappedExpression, invocation.ArgumentList)
            .WithTrailingTrivia(StripTrailingNewlines(invocation.GetTrailingTrivia()));
    }

    private static MemberAccessExpressionSyntax NameMeAsap(SyntaxTriviaList indent, SyntaxTrivia newline, MemberAccessExpressionSyntax memberAccess)
    {
        ExpressionSyntax wrappedInner = WrapChainRecursive(memberAccess.Expression, indent, newline);
        SyntaxToken dotToken = GetDotToken(indent, newline);

        SimpleNameSyntax name = memberAccess.Name.WithoutLeadingTrivia();
        name = name.WithTrailingTrivia(StripTrailingNewlines(name.GetTrailingTrivia()));

        return SyntaxFactory.MemberAccessExpression(memberAccess.Kind(), wrappedInner, dotToken, name);
    }

    private static SyntaxToken GetDotToken(SyntaxTriviaList indent, SyntaxTrivia newline)
    {
        return SyntaxFactory
            .Token(SyntaxKind.DotToken)
            .WithLeadingTrivia(SyntaxFactory.TriviaList(newline).AddRange(indent));
    }

    private static SyntaxTriviaList StripTrailingNewlines(SyntaxTriviaList trivia)
    {
        return SyntaxFactory.TriviaList(trivia.Where(t => !t.IsKind(SyntaxKind.EndOfLineTrivia)));
    }

    private static ExpressionSyntax FormatSingleLine(MemberAccessExpressionSyntax node)
    {
        return FlattenChain(node)
            .WithLeadingTrivia(node.GetLeadingTrivia())
            .WithTrailingTrivia(node.GetTrailingTrivia());
    }

    private static ExpressionSyntax FlattenChain(ExpressionSyntax node)
    {
        if (node is InvocationExpressionSyntax invocation)
        {
            return SyntaxFactory.InvocationExpression(FlattenChain(invocation.Expression), invocation.ArgumentList);
        }

        if (node is not MemberAccessExpressionSyntax memberAccess)
        {
            return node.WithoutTrailingTrivia();
        }

        ExpressionSyntax expression = FlattenChain(memberAccess.Expression);
        SyntaxToken dotToken = MakeDotToken();

        SimpleNameSyntax name = memberAccess
            .Name
            .WithoutLeadingTrivia()
            .WithoutTrailingTrivia();

        return SyntaxFactory
            .MemberAccessExpression(memberAccess.Kind(), expression, dotToken, name)
            .WithoutLeadingTrivia()
            .WithoutTrailingTrivia();
    }

    private static SyntaxToken MakeDotToken()
    {
        return SyntaxFactory
            .Token(SyntaxKind.DotToken)
            .WithLeadingTrivia(SyntaxFactory.TriviaList())
            .WithTrailingTrivia(SyntaxFactory.TriviaList());
    }

    private static SyntaxTriviaList GetParentIndentation(SyntaxNode node)
    {
        SyntaxNode? container = node.FirstAncestorOrSelf<SyntaxNode>(n =>
        {
            return n is StatementSyntax or MemberDeclarationSyntax;
        });

        if (container is null)
        {
            return SyntaxFactory.TriviaList();
        }

        SyntaxTriviaList leading = container.GetLeadingTrivia();
        SyntaxTrivia lastWhitespace = leading.LastOrDefault(t => t.IsKind(SyntaxKind.WhitespaceTrivia));

        return !lastWhitespace.IsKind(SyntaxKind.None)
            ? SyntaxFactory.TriviaList(lastWhitespace)
            : SyntaxFactory.TriviaList();
    }
}
```

---

### `Styly\Rewriters\CollectionExpressionRewriter.cs`

```csharp
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Styly.Rewriters;

internal class CollectionExpressionRewriter : CSharpSyntaxRewriter
{
    private readonly SemanticModel _semanticModel;
    public CollectionExpressionRewriter(SemanticModel semanticModel)
    {
        _semanticModel = semanticModel;
    }
// FUCK
    private bool IsCollectionLike(ExpressionSyntax node)
    {
        TypeInfo typeInfo = _semanticModel.GetTypeInfo(node);
        ITypeSymbol? type = typeInfo.ConvertedType;

        if (type is null || type.TypeKind == TypeKind.Error)
        {
            return false;
        }

        // Arrays are collection-like
        if (type.TypeKind == TypeKind.Array)
        {
            return true;
        }

        // string is special, but we are visiting creation expressions, so we can ignore it.
        if (type.SpecialType == SpecialType.System_String)
        {
            return false;
        }

        // Check if it's a generic collection.
        return type.AllInterfaces.Any(i => i.IsGenericType && i.OriginalDefinition.SpecialType == SpecialType.System_Collections_Generic_IEnumerable_T);
    }

    private static CollectionExpressionSyntax? CreateCollectionExpression(InitializerExpressionSyntax? initializer, SyntaxNode originalNode)
    {
        CollectionExpressionSyntax newExpression;

        if (initializer is null || initializer.Expressions.Count == 0)
        {
            // Empty collection: []
            newExpression = SyntaxFactory.CollectionExpression();
        }
        else
        {
            // Rebuild the list of elements as an IEnumerable, stripping trailing trivia from each.
            // This is crucial for removing the unwanted space from the last element.
            // We also strip leading trivia to remove excessive indentation added by NormalizeWhitespace.
            IEnumerable<ExpressionElementSyntax> cleanElements = initializer.Expressions.Select(expr => SyntaxFactory.ExpressionElement(expr.WithoutLeadingTrivia().WithoutTrailingTrivia()));
            // Create new comma separators, each with a standard trailing space for correct formatting like `1, 2, 3`.
            IEnumerable<SyntaxToken> separators = Enumerable.Range(0, initializer.Expressions.Count - 1).Select(_ => SyntaxFactory.Token(SyntaxKind.CommaToken).WithTrailingTrivia(SyntaxFactory.Space));
            // Explicitly create a SeparatedSyntaxList of the base type `CollectionElementSyntax`.
            // This works because IEnumerable<T> is covariant, allowing our IEnumerable<ExpressionElementSyntax> to be used.
            SeparatedSyntaxList<CollectionElementSyntax> separatedList = SyntaxFactory.SeparatedList<CollectionElementSyntax>(cleanElements, separators);
            newExpression = SyntaxFactory.CollectionExpression(separatedList);
        }

        // Preserve the original leading and trailing trivia from the entire original expression.
        return newExpression.WithLeadingTrivia(originalNode.GetLeadingTrivia()).WithTrailingTrivia(originalNode.GetTrailingTrivia());
    }

    public override SyntaxNode? VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
    {
        // Must have an initializer `{ ... }` or be parameterless `()`
        if (node.ArgumentList is not null && node.ArgumentList.Arguments.Any())
        {
            return base.VisitObjectCreationExpression(node);
        }

        if (!IsCollectionLike(node))
        {
            return base.VisitObjectCreationExpression(node);
        }

        // This handles both `new List<T>()` and `new List<T> { ... }`
        return CreateCollectionExpression(node.Initializer, node);
    }

    public override SyntaxNode? VisitArrayCreationExpression(ArrayCreationExpressionSyntax node)
    {
        // Handle `new int[0]`.
        if (node.Initializer is not null)
        {
            return CreateCollectionExpression(node.Initializer, node);
        }

        if (NameThisMethodAsap(node))
        {
            // Cannot convert things like `new int[5]` which are not initializers
            return base.VisitArrayCreationExpression(node);
        }

        return GetEmptyCollection(node);
    }

    private static CollectionExpressionSyntax GetEmptyCollection(ArrayCreationExpressionSyntax node)
    {
        return SyntaxFactory
            .CollectionExpression()
            .WithLeadingTrivia(node.GetLeadingTrivia())
            .WithTrailingTrivia(node.GetTrailingTrivia());
    }

    private static bool NameThisMethodAsap(ArrayCreationExpressionSyntax node)
    {
        return node.Type.RankSpecifiers.Count != 1 || node.Type.RankSpecifiers[0].Sizes.Count != 1 || node.Type.RankSpecifiers[0].Sizes[0] is not LiteralExpressionSyntax literal || literal.Token.ValueText != "0";
    }

    public override SyntaxNode? VisitImplicitObjectCreationExpression(ImplicitObjectCreationExpressionSyntax node)
    {
        if (node.ArgumentList.Arguments.Any())
        {
            return base.VisitImplicitObjectCreationExpression(node);
        }

        if (IsCollectionLike(node))
        {
            return CreateCollectionExpression(node.Initializer, node);
        }

        return base.VisitImplicitObjectCreationExpression(node);
    }
}
```

---

### `Styly\Rewriters\EnumeratableAnyRewriter.cs`

```csharp
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Styly.Rewriters;

internal class EnumerableAnyRewriter : CSharpSyntaxRewriter
{
    private readonly SemanticModel _semanticModel;
    public EnumerableAnyRewriter(SemanticModel semanticModel)
    {
        _semanticModel = semanticModel;
    }

    public override SyntaxNode? VisitInvocationExpression(InvocationExpressionSyntax node)
    {
        if (node.Expression is not MemberAccessExpressionSyntax memberAccess || memberAccess.Name.Identifier.ValueText != "Any" || node.ArgumentList.Arguments.Count != 0)
        {
            return base.VisitInvocationExpression(node);
        }

        if (!IsLinqAny(node))
        {
            return base.VisitInvocationExpression(node);
        }

        TypeInfo typeInfo = _semanticModel.GetTypeInfo(memberAccess.Expression);

        if (typeInfo.Type is null)
        {
            return base.VisitInvocationExpression(node);
        }

        return GetLengthOrCountProperty(typeInfo.Type) switch
        {
            null => base.VisitInvocationExpression(node),
            _ => CreateCountComparison(memberAccess.Expression, GetLengthOrCountProperty(typeInfo.Type))
        };
    }

    private bool IsLinqAny(InvocationExpressionSyntax node)
    {
        SymbolInfo symbolInfo = _semanticModel.GetSymbolInfo(node);

        return symbolInfo.Symbol is IMethodSymbol methodSymbol && methodSymbol.Name == "Any" && methodSymbol.ContainingType.ToDisplayString() == "System.Linq.Enumerable";
    }

    private static string? GetLengthOrCountProperty(ITypeSymbol type)
    {
        // Prefer Length for Arrays
        if (type.TypeKind == TypeKind.Array)
        {
            return "Length";
        }

        // Check for explicit Count or Length properties on the type (e.g. List<T>, ICollection<T>)
        foreach (ISymbol member in type.GetMembers())
        {
            if (member is not IPropertySymbol prop || prop.IsStatic || prop.Type.SpecialType != SpecialType.System_Int32)
            {
                continue;
            }

            if (prop.Name is "Count" or "Length")
            {
                return prop.Name;
            }
        }

        return null;
    }

    private static BinaryExpressionSyntax CreateCountComparison(ExpressionSyntax expression, string propertyName)
    {
        // Generates: expression.Count != 0
        // We use NormalizeWhitespace() to ensure correct spacing around the operator (e.g. " != ")
        // instead of manually constructing trivia, which is less robust.
        MemberAccessExpressionSyntax left = SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, expression, SyntaxFactory.IdentifierName(propertyName));

        LiteralExpressionSyntax right = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(0));

        return SyntaxFactory
            .BinaryExpression(SyntaxKind.NotEqualsExpression, left, right)
            .NormalizeWhitespace();
    }
}
```

---

### `Styly\Rewriters\ExplicitTypeToVarRewriter.cs`

```csharp
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Styly.Configuration;

namespace Styly.Rewriters;

internal class ExplicitTypeToVarRewriter : CSharpSyntaxRewriter
{
    private readonly SemanticModel _semanticModel;
    private readonly UseVarOption _option;
    public ExplicitTypeToVarRewriter(SemanticModel semanticModel, UseVarOption option)
    {
        _semanticModel = semanticModel;
        _option = option;
    }

    public override SyntaxNode? VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
    {
        // var is only valid if the declaration is not a constant
        if (node.IsConst)
        {
            return base.VisitLocalDeclarationStatement(node);
        }

        VariableDeclarationSyntax declaration = node.Declaration;
        // If it's already var, do nothing
        if (declaration.Type.IsVar)
        {
            return base.VisitLocalDeclarationStatement(node);
        }

        // var requires an initializer for every variable in the declaration
        // e.g. 'int x = 1, y = 2;' can be 'var', but 'int x;' cannot.
        // Also, 'var' cannot be used if there are multiple variables: 'var x = 1, y = 2' is valid C#,
        // but we simplify by only handling single variable declarations for safety and clarity.
        if (declaration.Variables.Count != 1)
        {
            return base.VisitLocalDeclarationStatement(node);
        }

        VariableDeclaratorSyntax variable = declaration.Variables[0];

        if (variable.Initializer is null)
        {
            return base.VisitLocalDeclarationStatement(node);
        }

        // Check if 'var' is semantically valid here (e.g., not assigning null without cast)
        TypeInfo typeInfo = _semanticModel.GetTypeInfo(declaration.Type);
        ITypeSymbol? typeSymbol = typeInfo.Type;

        TypeInfo initializerTypeInfo = _semanticModel.GetTypeInfo(variable.Initializer.Value);
        ITypeSymbol? initializerType = initializerTypeInfo.Type;
        // If we can't resolve types, we can't safely change to var
        if (typeSymbol is null || typeSymbol.TypeKind == TypeKind.Error)
        {
            return base.VisitLocalDeclarationStatement(node);
        }

        // If assigning null (without cast), var is illegal: var x = null; // Error
        if (initializerType is null && variable.Initializer.Value.IsKind(SyntaxKind.NullLiteralExpression))
        {
            return base.VisitLocalDeclarationStatement(node);
        }

        // Determine if we should replace based on the strategy
        bool shouldReplace = _option == UseVarOption.Always || IsApparent(variable.Initializer.Value);

        if (shouldReplace)
        {
            // Replace the explicit type with 'var'
            // Preserve leading/trailing trivia (indentation/comments)
            TypeSyntax varType = SyntaxFactory.IdentifierName("var").WithLeadingTrivia(declaration.Type.GetLeadingTrivia()).WithTrailingTrivia(declaration.Type.GetTrailingTrivia());

            VariableDeclarationSyntax newDeclaration = declaration.WithType(varType);
            return node.WithDeclaration(newDeclaration);
        }

        return base.VisitLocalDeclarationStatement(node);
    }

    private static bool IsApparent(ExpressionSyntax expression)
    {
        // 1. Object Creation: new Customer()
        if (expression is ObjectCreationExpressionSyntax)
        {
            return true;
        }

        // 2. Array Creation: new int[] { ... }
        if (expression is ArrayCreationExpressionSyntax)
        {
            return true;
        }

        // 3. Cast Expression: (int)x
        if (expression is CastExpressionSyntax)
        {
            return true;
        }

        // 4. Binary Expression using 'as': x as Customer
        if (expression is BinaryExpressionSyntax binary && binary.IsKind(SyntaxKind.AsExpression))
        {
            return true;
        }

        // 5. Literals: 5, "string", true, 10.5
        if (expression is LiteralExpressionSyntax)
        {
            return true;
        }

        // 6. Default expression: default(int) - (But NOT just 'default')
        return expression is DefaultExpressionSyntax;
    }
}
```

---

### `Styly\Rewriters\LayoutAnnotator.cs`

```csharp
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Styly.Rewriters;

internal class LayoutAnnotator : CSharpSyntaxRewriter
{
    public const string SingleLineAnnotationKind = "Styly_SingleLine";
    public const string PreserveBlankLineAnnotationKind = "Styly_PreserveBlankLine";
    public const string MultiLineCallChainAnnotationKind = "Styly_MultiLineCallChain";
    private static bool IsSingleLine(SyntaxNode node)
    {
        FileLinePositionSpan lineSpan = node.GetLocation().GetLineSpan();
        return lineSpan.StartLinePosition.Line == lineSpan.EndLinePosition.Line;
    }

    private static SyntaxList<T> AnnotateBlankLines<T>(SyntaxList<T> items)
        where T : SyntaxNode
    {
        if (items.Count < 2)
        {
            return items;
        }

        List<T> newItems = new(items.Count) { items[0] };

        for (int i = 1; i < items.Count; i++)
        {
            T prev = items[i - 1];
            T curr = items[i];

            if (HasBlankLineBetween(prev, curr))
            {
                curr = curr.WithAdditionalAnnotations(new SyntaxAnnotation(PreserveBlankLineAnnotationKind));
            }

            newItems.Add(curr);
        }

        return SyntaxFactory.List(newItems);
    }

    private static bool HasBlankLineBetween(SyntaxNode prev, SyntaxNode curr)
    {
        int newlines = CountEndingNewlines(prev.GetTrailingTrivia()) + CountStartingNewlines(curr.GetLeadingTrivia());
        return newlines >= 2;
    }

    private static int CountEndingNewlines(SyntaxTriviaList trivia)
    {
        return trivia.Count(t => t.IsKind(SyntaxKind.EndOfLineTrivia));
    }

    private static int CountStartingNewlines(SyntaxTriviaList trivia)
    {
        return trivia.Count(t => t.IsKind(SyntaxKind.EndOfLineTrivia));
    }

    public override SyntaxNode? VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
    {
        // Annotate the root of a call chain if it is currently multi-line.
        // This allows 'Preserve' mode to restore the layout after NormalizeWhitespace flattens it.
        return node.Parent is MemberAccessExpressionSyntax || IsSingleLine(node)
            ? base.VisitMemberAccessExpression(node)
            : base.VisitMemberAccessExpression(node)!.WithAdditionalAnnotations(new SyntaxAnnotation(MultiLineCallChainAnnotationKind));
    }

    public override SyntaxNode? VisitInitializerExpression(InitializerExpressionSyntax node)
    {
        return IsSingleLine(node)
            ? base.VisitInitializerExpression(node)!.WithAdditionalAnnotations(new SyntaxAnnotation(SingleLineAnnotationKind))
            : base.VisitInitializerExpression(node);
    }

    public override SyntaxNode? VisitCollectionExpression(CollectionExpressionSyntax node)
    {
        return IsSingleLine(node)
            ? base.VisitCollectionExpression(node)!.WithAdditionalAnnotations(new SyntaxAnnotation(SingleLineAnnotationKind))
            : base.VisitCollectionExpression(node);
    }

    public override SyntaxNode? VisitAnonymousObjectCreationExpression(AnonymousObjectCreationExpressionSyntax node)
    {
        return IsSingleLine(node)
            ? base.VisitAnonymousObjectCreationExpression(node)!.WithAdditionalAnnotations(new SyntaxAnnotation(SingleLineAnnotationKind))
            : base.VisitAnonymousObjectCreationExpression(node);
    }

    public override SyntaxNode? VisitCompilationUnit(CompilationUnitSyntax node)
    {
        CompilationUnitSyntax visited = (CompilationUnitSyntax)base.VisitCompilationUnit(node)!;
        return visited.WithMembers(AnnotateBlankLines(visited.Members));
    }

    public override SyntaxNode? VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
    {
        NamespaceDeclarationSyntax visited = (NamespaceDeclarationSyntax)base.VisitNamespaceDeclaration(node)!;
        return visited.WithMembers(AnnotateBlankLines(visited.Members));
    }

    public override SyntaxNode? VisitFileScopedNamespaceDeclaration(FileScopedNamespaceDeclarationSyntax node)
    {
        FileScopedNamespaceDeclarationSyntax visited = (FileScopedNamespaceDeclarationSyntax)base.VisitFileScopedNamespaceDeclaration(node)!;
        return visited.WithMembers(AnnotateBlankLines(visited.Members));
    }

    public override SyntaxNode? VisitBlock(BlockSyntax node)
    {
        BlockSyntax visited = (BlockSyntax)base.VisitBlock(node)!;
        return visited.WithStatements(AnnotateBlankLines(visited.Statements));
    }

    public override SyntaxNode? VisitSwitchSection(SwitchSectionSyntax node)
    {
        SwitchSectionSyntax visited = (SwitchSectionSyntax)base.VisitSwitchSection(node)!;
        return visited.WithStatements(AnnotateBlankLines(visited.Statements));
    }

    public override SyntaxNode? VisitClassDeclaration(ClassDeclarationSyntax node)
    {
        ClassDeclarationSyntax visited = (ClassDeclarationSyntax)base.VisitClassDeclaration(node)!;
        return visited.WithMembers(AnnotateBlankLines(visited.Members));
    }

    public override SyntaxNode? VisitStructDeclaration(StructDeclarationSyntax node)
    {
        StructDeclarationSyntax visited = (StructDeclarationSyntax)base.VisitStructDeclaration(node)!;
        return visited.WithMembers(AnnotateBlankLines(visited.Members));
    }

    public override SyntaxNode? VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
    {
        InterfaceDeclarationSyntax visited = (InterfaceDeclarationSyntax)base.VisitInterfaceDeclaration(node)!;
        return visited.WithMembers(AnnotateBlankLines(visited.Members));
    }

    public override SyntaxNode? VisitRecordDeclaration(RecordDeclarationSyntax node)
    {
        RecordDeclarationSyntax visited = (RecordDeclarationSyntax)base.VisitRecordDeclaration(node)!;
        return visited.WithMembers(AnnotateBlankLines(visited.Members));
    }
}
```

---

### `Styly\Rewriters\NamespaceRewriter.cs`

```csharp
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Styly.Configuration;
using Styly.Rewriters.Indentation;

namespace Styly.Rewriters;

internal class NamespaceRewriter : CSharpSyntaxRewriter
{
    private readonly NamespaceFormat _format;
    public NamespaceRewriter(NamespaceFormat format)
    {
        _format = format;
    }

    public override SyntaxNode? VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
    {
        if (_format == NamespaceFormat.File && node.Parent is CompilationUnitSyntax)
        {
            IndentationRemover indentationRemover = new();
            SyntaxList<MemberDeclarationSyntax> unindentedMembers = [];

            foreach (MemberDeclarationSyntax member in node.Members)
            {
                unindentedMembers = unindentedMembers.Add((MemberDeclarationSyntax)indentationRemover.Visit(member));
            }

            FileScopedNamespaceDeclarationSyntax fileScopedNamespace = SyntaxFactory.FileScopedNamespaceDeclaration(node.AttributeLists, node.Modifiers, node.Name.WithoutTrailingTrivia(), node.Externs, node.Usings, unindentedMembers).WithLeadingTrivia(node.GetLeadingTrivia());

            fileScopedNamespace = fileScopedNamespace.WithNamespaceKeyword(node.NamespaceKeyword.WithTrailingTrivia(SyntaxFactory.Space));
            // Add a blank line after the semicolon to separate it from the first member.
            SyntaxToken semicolon = SyntaxFactory.Token(SyntaxKind.SemicolonToken).WithTrailingTrivia(SyntaxFactory.CarriageReturnLineFeed, SyntaxFactory.CarriageReturnLineFeed);

            fileScopedNamespace = fileScopedNamespace.WithSemicolonToken(semicolon);

            SyntaxTriviaList trailingTrivia = node.GetTrailingTrivia();
            int lastNonWhitespaceIndex = -1;

            for (int i = trailingTrivia.Count - 1; i >= 0; i--)
            {
                SyntaxTrivia trivia = trailingTrivia[i];

                if (!trivia.IsKind(SyntaxKind.WhitespaceTrivia) && !trivia.IsKind(SyntaxKind.EndOfLineTrivia))
                {
                    lastNonWhitespaceIndex = i;
                    break;
                }
            }

            SyntaxTriviaList newTrailingTrivia = SyntaxFactory.TriviaList(trailingTrivia.Take(lastNonWhitespaceIndex + 1));
            return fileScopedNamespace.WithTrailingTrivia(newTrailingTrivia);
        }

        return base.VisitNamespaceDeclaration(node);
    }

    public override SyntaxNode? VisitFileScopedNamespaceDeclaration(FileScopedNamespaceDeclarationSyntax node)
    {
        if (_format == NamespaceFormat.Block)
        {
            IndentationAdder indentationAdder = new();
            SyntaxList<UsingDirectiveSyntax> indentedUsings = [];
            SyntaxList<MemberDeclarationSyntax> indentedMembers = [];
            bool isFirstItemProcessed = false;
            // Process Usings
            foreach (UsingDirectiveSyntax u in node.Usings)
            {
                UsingDirectiveSyntax rewritten = (UsingDirectiveSyntax)indentationAdder.Visit(u);

                if (!isFirstItemProcessed)
                {
                    rewritten = FixFirstItemIndentation(rewritten);
                    isFirstItemProcessed = true;
                }
                else
                {
                    rewritten = EnsureIndentation(rewritten);
                }

                indentedUsings = indentedUsings.Add(rewritten);
            }

            // Process Members
            foreach (MemberDeclarationSyntax m in node.Members)
            {
                MemberDeclarationSyntax rewritten = (MemberDeclarationSyntax)indentationAdder.Visit(m);

                if (!isFirstItemProcessed && !indentedUsings.Any())
                {
                    rewritten = FixFirstItemIndentation(rewritten);
                    isFirstItemProcessed = true;
                }
                else
                {
                    rewritten = EnsureIndentation(rewritten);
                }

                indentedMembers = indentedMembers.Add(rewritten);
            }

            NamespaceDeclarationSyntax blockScopedNamespace = SyntaxFactory.NamespaceDeclaration(node.AttributeLists, node.Modifiers, node.Name, node.Externs, indentedUsings, indentedMembers).WithLeadingTrivia(node.GetLeadingTrivia()).WithTrailingTrivia(node.GetTrailingTrivia());

            blockScopedNamespace = blockScopedNamespace.WithNamespaceKeyword(blockScopedNamespace.NamespaceKeyword.WithTrailingTrivia(SyntaxFactory.Space));
            // Ensure braces are properly formatted with newlines
            blockScopedNamespace = blockScopedNamespace.WithOpenBraceToken(SyntaxFactory.Token(SyntaxKind.OpenBraceToken).WithLeadingTrivia(SyntaxFactory.CarriageReturnLineFeed).WithTrailingTrivia(SyntaxFactory.CarriageReturnLineFeed)).WithCloseBraceToken(SyntaxFactory.Token(SyntaxKind.CloseBraceToken).WithLeadingTrivia(SyntaxFactory.CarriageReturnLineFeed));

            return blockScopedNamespace;
        }

        return base.VisitFileScopedNamespaceDeclaration(node);
    }

    private static T FixFirstItemIndentation<T>(T node)
        where T : SyntaxNode
    {
        SyntaxToken firstToken = node.GetFirstToken();
        SyntaxTriviaList leading = firstToken.LeadingTrivia;
        // Strip all initial newlines and whitespace to ensure the item sits flush after the brace's newline.
        IEnumerable<SyntaxTrivia> content = leading.SkipWhile(t => t.IsKind(SyntaxKind.EndOfLineTrivia) || t.IsKind(SyntaxKind.WhitespaceTrivia));
        // Force a single indentation level
        SyntaxTriviaList newTrivia = SyntaxFactory.TriviaList(SyntaxFactory.Whitespace("    "));
        newTrivia = newTrivia.AddRange(content);

        return node.ReplaceToken(firstToken, firstToken.WithLeadingTrivia(newTrivia));
    }

    private static T EnsureIndentation<T>(T node)
        where T : SyntaxNode
    {
        SyntaxToken firstToken = node.GetFirstToken();
        // If the token doesn't start with a newline, IndentationAdder wouldn't have added indentation.
        // We explicitly add it here to ensure alignment within the block.
        if (!firstToken.LeadingTrivia.Any(t => t.IsKind(SyntaxKind.EndOfLineTrivia)))
        {
            SyntaxTriviaList newTrivia = firstToken.LeadingTrivia.Insert(0, SyntaxFactory.Whitespace("    "));
            return node.ReplaceToken(firstToken, firstToken.WithLeadingTrivia(newTrivia));
        }

        return node;
    }
}
```

---

### `Styly\Rewriters\NullCheckPatternRewriter.cs`

```csharp
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Styly.Rewriters;

internal class NullCheckPatternRewriter : CSharpSyntaxRewriter
{
    public override SyntaxNode? VisitBinaryExpression(BinaryExpressionSyntax node)
    {
        bool isEquals = node.IsKind(SyntaxKind.EqualsExpression);
        bool isNotEquals = node.IsKind(SyntaxKind.NotEqualsExpression);

        if (!isEquals && !isNotEquals)
        {
            return base.VisitBinaryExpression(node);
        }

        ExpressionSyntax? expression = null;

        if (node.Left.IsKind(SyntaxKind.NullLiteralExpression))
        {
            expression = node.Right;
        }
        else if (node.Right.IsKind(SyntaxKind.NullLiteralExpression))
        {
            expression = node.Left;
        }

        if (expression is null)
        {
            return base.VisitBinaryExpression(node);
        }

        // x is null
        // Added leading space to prevent 'isnull'
        ConstantPatternSyntax nullPattern = SyntaxFactory.ConstantPattern(SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression));

        if (isEquals)
        {
            return SyntaxFactory.IsPatternExpression(expression.WithoutTrailingTrivia(), SyntaxFactory.Token(SyntaxKind.IsKeyword).WithLeadingTrivia(SyntaxFactory.Space).WithTrailingTrivia(SyntaxFactory.Space), nullPattern).WithLeadingTrivia(node.GetLeadingTrivia()).WithTrailingTrivia(node.GetTrailingTrivia());
        }

        // x is not null
        // The 'not' keyword provides the space after 'is'
        UnaryPatternSyntax notPattern = SyntaxFactory.UnaryPattern(SyntaxFactory.Token(SyntaxKind.NotKeyword).WithTrailingTrivia(SyntaxFactory.Space), nullPattern);

        return SyntaxFactory.IsPatternExpression(expression.WithoutTrailingTrivia(), SyntaxFactory.Token(SyntaxKind.IsKeyword).WithLeadingTrivia(SyntaxFactory.Space).WithTrailingTrivia(SyntaxFactory.Space), notPattern).WithLeadingTrivia(node.GetLeadingTrivia()).WithTrailingTrivia(node.GetTrailingTrivia());
    }
}
```

---

### `Styly\Rewriters\RawStringRewriter.cs`

```csharp
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Styly.Configuration;
using System.Text;
using System.Text.RegularExpressions;

namespace Styly.Rewriters;

internal partial class RawStringRewriter : CSharpSyntaxRewriter
{
    private readonly RawStringsOptions _options;
    private const int IndentSize = 4;

    [GeneratedRegex(@"\r?\n")]
    private static partial Regex MyRegex1();

    public RawStringRewriter(RawStringsOptions options)
    {
        _options = options;
    }

    public override SyntaxNode? VisitLiteralExpression(LiteralExpressionSyntax node)
    {
        if (!_options.PreferRawForMultiline || !node.IsKind(SyntaxKind.StringLiteralExpression))
        {
            return base.VisitLiteralExpression(node);
        }

        // CRITICAL: Do not process existing raw strings.
        // Re-processing raw strings changes their semantic value by baking in current indentation.
        if (IsRawString(node))
        {
            return base.VisitLiteralExpression(node);
        }

        string value = node.Token.ValueText;
        // Only convert if the string contains actual newline characters
        return value.Contains('\n') || value.Contains('\r')
            ? ConvertToRawString(node, value)
            : base.VisitLiteralExpression(node);
    }

    private static bool IsRawString(LiteralExpressionSyntax node)
    {
        return node.Token.IsKind(SyntaxKind.MultiLineRawStringLiteralToken) || node.Token.IsKind(SyntaxKind.SingleLineRawStringLiteralToken);
    }

    private static LiteralExpressionSyntax ConvertToRawString(LiteralExpressionSyntax node, string value)
    {
        // 1. Determine delimiter depth (min 3 quotes)
        string delimiter = GetDelimiter(value);

        // 2. Get statement indentation to align the delimiter
        SyntaxTriviaList parentIndent = GetParentIndentation(node);
        string indentStr = parentIndent.ToString();
        string contentIndent = indentStr + new string(' ', IndentSize);

        // 3. Construct the raw string block
        // The closing delimiter must be aligned with the parent statement.
        // The content lines are indented relative to that delimiter.
        StringBuilder sb = GetRawStringBlock(value, delimiter, indentStr, contentIndent);

        // 4. Parse the generated text into a valid RawStringLiteralToken
        return GetRawStringLiteralToken(node, sb);
    }

    private static LiteralExpressionSyntax GetRawStringLiteralToken(LiteralExpressionSyntax node, StringBuilder sb)
    {
        SyntaxToken rawToken = SyntaxFactory.ParseToken(sb.ToString());

        return SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, rawToken)
            .WithLeadingTrivia(node.GetLeadingTrivia())
            .WithTrailingTrivia(node.GetTrailingTrivia());
    }

    private static StringBuilder GetRawStringBlock(string value, string delimiter, string indentStr, string contentIndent)
    {
        string[] lines = MyRegex1().Split(value);
        StringBuilder sb = new();
        sb.AppendLine(delimiter);

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                sb.AppendLine();
                continue;
            }

            sb.Append(contentIndent);
            sb.AppendLine(line);
        }

        sb.Append(indentStr);
        sb.Append(delimiter);

        return sb;
    }

    private static string GetDelimiter(string value)
    {
        int maxSequentialQuotes = GetMaxSequentialQuotes(value);
        int quotesNeeded = int.Max(3, maxSequentialQuotes + 1);
        string delimiter = new('"', quotesNeeded);

        return delimiter;
    }

    private static int GetMaxSequentialQuotes(string text)
    {
        int max = 0;
        int current = 0;

        foreach (char c in text)
        {
            if (c == '"')
            {
                current++;
                max = Math.Max(max, current);
            }
            else
            {
                current = 0;
            }
        }

        return max;
    }

    private static SyntaxTriviaList GetParentIndentation(SyntaxNode node)
    {
        var container = node.FirstAncestorOrSelf<SyntaxNode>(n => n is StatementSyntax or MemberDeclarationSyntax);

        if (container is null)
        {
            return SyntaxFactory.TriviaList();
        }

        SyntaxTriviaList leading = container.GetLeadingTrivia();
        SyntaxTrivia lastWhitespace = leading.LastOrDefault(t => t.IsKind(SyntaxKind.WhitespaceTrivia));

        if (lastWhitespace.IsKind(SyntaxKind.None))
        {
            return SyntaxFactory.TriviaList();
        }

        return SyntaxFactory.TriviaList(lastWhitespace);
    }
}
```

---

### `Styly\Rewriters\SpacingUtility.cs`

```csharp
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Styly.Rewriters;

internal static class SpacingUtility
{
    public static TNode EnsureBlankLine<TNode>(SyntaxNode prev, TNode curr)
        where TNode : SyntaxNode
    {
        return EnsureBlankLine(prev.GetLastToken(), curr);
    }

    public static TNode EnsureBlankLine<TNode>(SyntaxToken prevToken, TNode curr)
        where TNode : SyntaxNode
    {
        int newlineCount = CountNewlines(prevToken.TrailingTrivia) + CountNewlines(curr.GetLeadingTrivia());
        // A blank line exists if there are at least 2 newline characters between tokens.
        return newlineCount < 2
            ? curr.WithLeadingTrivia(curr.GetLeadingTrivia().Insert(0, SyntaxFactory.CarriageReturnLineFeed))
            : curr;
    }

    private static int CountNewlines(SyntaxTriviaList trivia)
    {
        return trivia.Count(t => t.IsKind(SyntaxKind.EndOfLineTrivia));
    }
}
```

---

### `Styly\Rewriters\StaticModifierRewriter.cs`

```csharp
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Styly.Rewriters;

internal class StaticModifierRewriter : CSharpSyntaxRewriter
{
    private readonly SemanticModel _semanticModel;
    public StaticModifierRewriter(SemanticModel semanticModel)
    {
        _semanticModel = semanticModel;
    }

    public override SyntaxNode? VisitMethodDeclaration(MethodDeclarationSyntax node)
    {
        return node.Body is null && node.ExpressionBody is null
            ? base.VisitMethodDeclaration(node)
            : node.Modifiers.Any(m => m.IsKind(SyntaxKind.StaticKeyword) || m.IsKind(SyntaxKind.AbstractKeyword) || m.IsKind(SyntaxKind.VirtualKeyword) || m.IsKind(SyntaxKind.OverrideKeyword) || m.IsKind(SyntaxKind.NewKeyword) || m.IsKind(SyntaxKind.PartialKeyword)) ? base.VisitMethodDeclaration(node) : node.ExplicitInterfaceSpecifier is not null ? base.VisitMethodDeclaration(node) : AccessesInstanceData(node) || IsInterfaceImplementation(node) ? base.VisitMethodDeclaration(node) : MakeStatic(node);
    }

    private bool IsInterfaceImplementation(MethodDeclarationSyntax node)
    {
        ISymbol? symbol = _semanticModel.GetDeclaredSymbol(node);

        if (symbol is not IMethodSymbol methodSymbol)
        {
            // Safety: If we can't resolve the symbol, assume it might be important.
            return true;
        }

        INamedTypeSymbol type = methodSymbol.ContainingType;

        if (type is null)
        {
            return true;
        }

        // Check if this method implements ANY member of ANY interface the type implements
        foreach (INamedTypeSymbol interfaceType in type.AllInterfaces)
        {
            // Optimization: Only check interface members with the same name
            foreach (ISymbol interfaceMember in interfaceType.GetMembers(methodSymbol.Name))
            {
                if (interfaceMember is IMethodSymbol interfaceMethod)
                {
                    // Ask Roslyn: "Which method in 'type' implements this 'interfaceMethod'?"
                    ISymbol? implementation = type.FindImplementationForInterfaceMember(interfaceMethod);
                    // If the answer is "this method", then we cannot make it static.
                    if (SymbolEqualityComparer.Default.Equals(implementation, methodSymbol))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    private bool AccessesInstanceData(MethodDeclarationSyntax method)
    {
        SyntaxNode body = (SyntaxNode? )method.Body ?? method.ExpressionBody!;
        // 1. Check for 'this' or 'base' keywords
        if (body.DescendantTokens().Any(t => t.IsKind(SyntaxKind.ThisKeyword) || t.IsKind(SyntaxKind.BaseKeyword)))
        {
            return true;
        }

        // 2. Check for implicit instance member access
        IEnumerable<IdentifierNameSyntax> identifiers = body.DescendantNodes().OfType<IdentifierNameSyntax>();
        ISymbol? methodSymbol = _semanticModel.GetDeclaredSymbol(method);
        INamedTypeSymbol? containingType = methodSymbol?.ContainingType;

        if (containingType is null)
        {
            // If we can't determine the containing type, assume it's unsafe to change.
            return true;
        }

        foreach (IdentifierNameSyntax identifier in identifiers)
        {
            // Optimization: Skip 'var' keyword used as type inference
            if (identifier.Identifier.ValueText == "var")
            {
                continue;
            }

            // Optimization: Skip 'nameof(X)' - it relies on metadata, not instance state
            if (IsInNameOf(identifier))
            {
                continue;
            }

            // If identifier is the right side of a member access (e.g. obj.Prop), we check 'obj'.
            // If 'obj' is 'this' (implicit or explicit), then 'Prop' is instance access.
            // If identifier is standalone (e.g. Prop = 1), it's implicit 'this'.
            if (IsMemberAccessOnOtherInstance(identifier))
            {
                continue;
            }

            SymbolInfo symbolInfo = _semanticModel.GetSymbolInfo(identifier);
            ISymbol? symbol = symbolInfo.Symbol;

            if (symbol is null)
            {
                // CRITICAL SAFETY CHECK:
                // If we cannot resolve the symbol, we MUST assume it might be an instance member.
                // Ignoring it (continuing) risks breaking code if dependencies are missing (e.g. base classes in other DLLs).
                // It is better to leave a method as instance (safe) than to incorrectly make it static (compile error).
                return true;
            }

            if (symbol.IsStatic)
            {
                continue;
            }

            // If the symbol is a member of the containing class (or base), and it's not static, 
            // then we are accessing instance data.
            if (IsInstanceMember(symbol, containingType))
            {
                return true;
            }
        }

        return false;
    }

    private static bool IsInNameOf(SyntaxNode node)
    {
        // Traverse up to find if we are inside a nameof(...) expression
        SyntaxNode? current = node.Parent;

        while (current is not null)
        {
            if (current is InvocationExpressionSyntax invocation && invocation.Expression is IdentifierNameSyntax name && name.Identifier.ValueText == "nameof")
            {
                return true;
            }

            // Stop at statement boundaries to avoid deep walks
            if (current is StatementSyntax or MemberDeclarationSyntax)
            {
                break;
            }

            current = current.Parent;
        }

        return false;
    }

    private static bool IsMemberAccessOnOtherInstance(IdentifierNameSyntax identifier)
    {
        // Check if this identifier is the 'Name' part of a MemberAccessExpression (e.g. "other.Name")
        if (identifier.Parent is not MemberAccessExpressionSyntax memberAccess || memberAccess.Name != identifier)
        {
            return false;
        }

        // If the expression on the left (Expression) is NOT 'this' or 'base' (explicitly or implicitly),
        // then it's access on a different object.
        if (memberAccess.Expression is not ThisExpressionSyntax and not BaseExpressionSyntax)
        {
            // It's something like "obj.Prop". 
            // CAUTION: If "obj" itself resolves to an instance field of 'this', checking "obj" in the loop
            // will catch it. So we can safely ignore the "Prop" part here.
            return true;
        }

        return false;
    }

    private static bool IsInstanceMember(ISymbol symbol, INamedTypeSymbol containingType)
    {
        if (symbol.Kind is not (SymbolKind.Field or SymbolKind.Property or SymbolKind.Method or SymbolKind.Event))
        {
            return false;
        }

        // Check if the symbol belongs to the containing type or its base types
        ITypeSymbol? current = containingType;

        while (current is not null)
        {
            // Use OriginalDefinition to handle generic types correctly
            if (SymbolEqualityComparer.Default.Equals(current.OriginalDefinition, symbol.ContainingType?.OriginalDefinition))
            {
                return true;
            }

            current = current.BaseType;
        }

        return false;
    }

    private static MethodDeclarationSyntax MakeStatic(MethodDeclarationSyntax node)
    {
        SyntaxToken staticToken = SyntaxFactory.Token(SyntaxKind.StaticKeyword).WithTrailingTrivia(SyntaxFactory.Space);

        int insertIndex = -1;

        for (int i = 0; i < node.Modifiers.Count; i++)
        {
            SyntaxKind kind = node.Modifiers[i].Kind();

            if (kind is SyntaxKind.PublicKeyword or SyntaxKind.PrivateKeyword or SyntaxKind.ProtectedKeyword or SyntaxKind.InternalKeyword)
            {
                insertIndex = i;
            }
        }

        if (insertIndex != -1)
        {
            // Insert after visibility modifiers (e.g. public static).
            // The previous modifier likely holds the indentation/trivia, which we preserve.
            return node.WithModifiers(node.Modifiers.Insert(insertIndex + 1, staticToken));
        }

        // Insert at the start of the modifiers list.
        if (node.Modifiers.Count > 0)
        {
            // We need to move the leading trivia (indentation/comments) from the first existing modifier to our new 'static' token.
            SyntaxToken firstMod = node.Modifiers[0];
            staticToken = staticToken.WithLeadingTrivia(firstMod.LeadingTrivia);
            // Strip the trivia from the old first modifier so it doesn't duplicate.
            SyntaxTokenList newModifiers = node.Modifiers.Replace(firstMod, firstMod.WithLeadingTrivia(SyntaxFactory.TriviaList()));
            return node.WithModifiers(newModifiers.Insert(0, staticToken));
        }
        else
        {
            // No existing modifiers. The leading trivia is currently attached to the ReturnType.
            // We move it to 'static'.
            staticToken = staticToken.WithLeadingTrivia(node.ReturnType.GetLeadingTrivia());
            // Strip trivia from ReturnType.
            MethodDeclarationSyntax newNode = node.WithReturnType(node.ReturnType.WithLeadingTrivia(SyntaxFactory.TriviaList()));

            return newNode.WithModifiers(SyntaxFactory.TokenList(staticToken));
        }
    }
}
```

---

### `Styly\Rewriters\StructuralSpacingRewriter.cs`

```csharp
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Styly.Rewriters;

internal class StructuralSpacingRewriter : CSharpSyntaxRewriter
{
    public override SyntaxNode? VisitCompilationUnit(CompilationUnitSyntax node)
    {
        node = EnsureUsingSeparator(node, node.Usings, node.Members, (n, m) => n.WithMembers(m));
        return base.VisitCompilationUnit(node);
    }

    public override SyntaxNode? VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
    {
        node = EnsureUsingSeparator(node, node.Usings, node.Members, (n, m) => n.WithMembers(m));
        return base.VisitNamespaceDeclaration(node);
    }

    public override SyntaxNode? VisitFileScopedNamespaceDeclaration(FileScopedNamespaceDeclarationSyntax node)
    {
        node = EnsureUsingSeparator(node, node.Usings, node.Members, (n, m) => n.WithMembers(m));

        if (node.Usings.Any() || !node.Members.Any())
        {
            return base.VisitFileScopedNamespaceDeclaration(node);
        }

        MemberDeclarationSyntax firstMember = node.Members[0];
        MemberDeclarationSyntax newFirstMember = SpacingUtility.EnsureBlankLine(node.SemicolonToken, firstMember);

        if (newFirstMember != firstMember)
        {
            node = node.WithMembers(node.Members.Replace(firstMember, newFirstMember));
        }

        return base.VisitFileScopedNamespaceDeclaration(node);
    }

    private static TNode EnsureUsingSeparator<TNode>(TNode node, SyntaxList<UsingDirectiveSyntax> usings, SyntaxList<MemberDeclarationSyntax> members, Func<TNode, SyntaxList<MemberDeclarationSyntax>, TNode> withMembers)
        where TNode : SyntaxNode
    {
        if (!usings.Any() || !members.Any())
        {
            return node;
        }

        UsingDirectiveSyntax lastUsing = usings.Last();
        MemberDeclarationSyntax firstMember = members.First();
        MemberDeclarationSyntax newFirstMember = SpacingUtility.EnsureBlankLine(lastUsing, firstMember);

        return newFirstMember != firstMember
            ? withMembers(node, members.Replace(firstMember, newFirstMember))
            : node;
    }
}
```

---

### `Styly\Rewriters\TernaryRewriter.cs`

```csharp
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Styly.Configuration;

namespace Styly.Rewriters;

internal class TernaryRewriter : CSharpSyntaxRewriter
{
    private readonly TernaryOptions _options;
    private const int IndentSize = 4;
    public TernaryRewriter(TernaryOptions options)
    {
        _options = options;
    }

    public override SyntaxNode? VisitConditionalExpression(ConditionalExpressionSyntax node)
    {
        return _options.Style == TernaryStyle.Preserve
            ? base.VisitConditionalExpression(node)
            : _options.Style == TernaryStyle.MultiLine ? FormatMultiLine(node) : _options.Style == TernaryStyle.SingleLine ? FormatSingleLine(node) : base.VisitConditionalExpression(node);
    }

    private static ConditionalExpressionSyntax FormatMultiLine(ConditionalExpressionSyntax node)
    {
        SyntaxTriviaList parentIndent = GetParentIndentation(node);
        SyntaxTriviaList itemIndent = parentIndent.Add(SyntaxFactory.Whitespace(new string (' ', IndentSize)));
        SyntaxTrivia newline = SyntaxFactory.CarriageReturnLineFeed;
        // Ensure the condition doesn't have trailing spaces
        ExpressionSyntax condition = node.Condition.WithoutTrailingTrivia();
        // Question mark on new line with indent
        SyntaxToken questionToken = SyntaxFactory.Token(SyntaxKind.QuestionToken).WithLeadingTrivia(SyntaxFactory.TriviaList(newline).AddRange(itemIndent)).WithTrailingTrivia(SyntaxFactory.Space);
        // Result branches cleaned up
        ExpressionSyntax whenTrue = node.WhenTrue.WithoutLeadingTrivia().WithoutTrailingTrivia();

        SyntaxToken colonToken = SyntaxFactory.Token(SyntaxKind.ColonToken).WithLeadingTrivia(SyntaxFactory.TriviaList(newline).AddRange(itemIndent)).WithTrailingTrivia(SyntaxFactory.Space);

        ExpressionSyntax whenFalse = node.WhenFalse.WithoutLeadingTrivia().WithoutTrailingTrivia();

        return node.WithCondition(condition).WithQuestionToken(questionToken).WithWhenTrue(whenTrue).WithColonToken(colonToken).WithWhenFalse(whenFalse);
    }

    private static ConditionalExpressionSyntax FormatSingleLine(ConditionalExpressionSyntax node)
    {
        return node.WithCondition(node.Condition.WithoutTrailingTrivia().WithTrailingTrivia(SyntaxFactory.Space)).WithQuestionToken(SyntaxFactory.Token(SyntaxKind.QuestionToken).WithTrailingTrivia(SyntaxFactory.Space)).WithWhenTrue(node.WhenTrue.WithoutLeadingTrivia().WithoutTrailingTrivia().WithTrailingTrivia(SyntaxFactory.Space)).WithColonToken(SyntaxFactory.Token(SyntaxKind.ColonToken).WithTrailingTrivia(SyntaxFactory.Space)).WithWhenFalse(node.WhenFalse.WithoutLeadingTrivia().WithoutTrailingTrivia());
    }

    private static SyntaxTriviaList GetParentIndentation(SyntaxNode node)
    {
        SyntaxNode? container = node.FirstAncestorOrSelf<SyntaxNode>(n => n is StatementSyntax or MemberDeclarationSyntax);

        if (container is not null)
        {
            SyntaxTriviaList leading = container.GetLeadingTrivia();
            SyntaxTrivia lastWhitespace = leading.LastOrDefault(t => t.IsKind(SyntaxKind.WhitespaceTrivia));

            if (!lastWhitespace.IsKind(SyntaxKind.None))
            {
                return SyntaxFactory.TriviaList(lastWhitespace);
            }
        }

        return SyntaxFactory.TriviaList();
    }
}
```

---

### `Styly\Rewriters\TokenSpacingCleanupRewriter.cs`

```csharp
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Styly.Rewriters;

internal class TokenSpacingCleanupRewriter : CSharpSyntaxRewriter
{
    public override SyntaxNode? Visit(SyntaxNode? node)
    {
        return node is null
            ? null
            : CleanupTokenSpacing(node);
    }

    private static SyntaxNode CleanupTokenSpacing(SyntaxNode root)
    {
        List<SyntaxToken> tokens = [ ..root.DescendantTokens() ];
        Dictionary<SyntaxToken, SyntaxToken> replacements = [];

        for (int i = 1; i < tokens.Count; i++)
        {
            SyntaxToken prev = tokens[i - 1];
            SyntaxToken current = tokens[i];

            if (IsUnwantedSpaceAfterBrace(prev, current))
            {
                replacements[current] = RemoveLeadingWhitespace(current);
            }
        }

        return replacements.Count != 0
            ? root.ReplaceTokens(replacements.Keys, (original, _) => replacements[original])
            : root;
    }

    private static bool IsUnwantedSpaceAfterBrace(SyntaxToken prev, SyntaxToken current)
    {
        return IsBraceFollowedByCloserOrSeparator(prev, current) && !AreSeparatedByNewline(prev, current);
    }

    private static bool IsBraceFollowedByCloserOrSeparator(SyntaxToken prev, SyntaxToken current)
    {
        return prev.IsKind(SyntaxKind.CloseBraceToken) && (current.IsKind(SyntaxKind.CloseParenToken) || current.IsKind(SyntaxKind.CloseBracketToken) || current.IsKind(SyntaxKind.SemicolonToken) || current.IsKind(SyntaxKind.CommaToken));
    }

    private static bool AreSeparatedByNewline(SyntaxToken prev, SyntaxToken current)
    {
        return prev.TrailingTrivia.Any(t => t.IsKind(SyntaxKind.EndOfLineTrivia)) || current.LeadingTrivia.Any(t => t.IsKind(SyntaxKind.EndOfLineTrivia));
    }

    private static SyntaxToken RemoveLeadingWhitespace(SyntaxToken token)
    {
        return token.LeadingTrivia.Any(t => t.IsKind(SyntaxKind.WhitespaceTrivia))
            ? token.WithLeadingTrivia(GetNonWhitespaceTrivia(token.LeadingTrivia))
            : token;
    }

    private static SyntaxTriviaList GetNonWhitespaceTrivia(SyntaxTriviaList trivia)
    {
        return SyntaxFactory.TriviaList(trivia.Where(t => !t.IsKind(SyntaxKind.WhitespaceTrivia)));
    }
}
```

---

### `Styly\Rewriters\TrailingTriviaTrimmer.cs`

```csharp
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Styly.Rewriters;

public class TrailingTriviaTrimmer : CSharpSyntaxRewriter
{
    public override SyntaxNode? VisitCompilationUnit(CompilationUnitSyntax node)
    {
        SyntaxToken lastToken = node.GetLastToken();

        if (lastToken.IsKind(SyntaxKind.None) || !lastToken.TrailingTrivia.Any())
        {
            return node;
        }

        int lastSignificantIndex = GetLastSignificantTriviaIndex(lastToken.TrailingTrivia);

        IEnumerable<SyntaxTrivia> triviaToKeep = lastToken.TrailingTrivia.Take(lastSignificantIndex + 1);
        SyntaxTriviaList newTrailingTrivia = SyntaxFactory.TriviaList(triviaToKeep);

        return newTrailingTrivia.SequenceEqual(lastToken.TrailingTrivia)
            ? node
            : node.ReplaceToken(lastToken, lastToken.WithTrailingTrivia(newTrailingTrivia));
    }

    private static int GetLastSignificantTriviaIndex(SyntaxTriviaList trailingTrivia)
    {
        for (int i = trailingTrivia.Count - 1; i >= 0; i--)
        {
            SyntaxTrivia trivia = trailingTrivia[i];

            if (IsWhitespaceOrNewline(trivia))
            {
                continue;
            }

            return i;
        }

        return -1;
    }

    private static bool IsWhitespaceOrNewline(SyntaxTrivia trivia)
    {
        return trivia.IsKind(SyntaxKind.EndOfLineTrivia) || trivia.IsKind(SyntaxKind.WhitespaceTrivia);
    }
}
```

---

### `Styly\Rewriters\UsingSorterRewriter.cs`

```csharp
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Styly.Rewriters;

internal class UsingSorterRewriter : CSharpSyntaxRewriter
{
    private static int CompareUsings(UsingDirectiveSyntax a, UsingDirectiveSyntax b)
    {
        string nameA = a.Name?.ToString() ?? string.Empty;
        string nameB = b.Name?.ToString() ?? string.Empty;

        bool isSystemA = nameA.StartsWith("System");
        bool isSystemB = nameB.StartsWith("System");

        if (isSystemA == isSystemB)
        {
            return string.Compare(nameA, nameB, StringComparison.Ordinal);
        }

        return isSystemA
            ? -1
            : 1;
    }

    public override SyntaxNode? VisitCompilationUnit(CompilationUnitSyntax node)
    {
        var visited = (CompilationUnitSyntax)base.VisitCompilationUnit(node)!;

        if (!visited.Usings.Any())
        {
            return visited;
        }

        SyntaxList<UsingDirectiveSyntax> sortedUsings = ProcessUsings(visited.Usings);
        return visited.WithUsings(sortedUsings);
    }

    public override SyntaxNode? VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
    {
        var visited = (NamespaceDeclarationSyntax)base.VisitNamespaceDeclaration(node)!;

        if (!visited.Usings.Any())
        {
            return visited;
        }

        SyntaxList<UsingDirectiveSyntax> sortedUsings = ProcessUsings(visited.Usings);
        return visited.WithUsings(sortedUsings);
    }

    public override SyntaxNode? VisitFileScopedNamespaceDeclaration(FileScopedNamespaceDeclarationSyntax node)
    {
        var visited = (FileScopedNamespaceDeclarationSyntax)base.VisitFileScopedNamespaceDeclaration(node)!;

        if (!visited.Usings.Any())
        {
            return visited;
        }

        SyntaxList<UsingDirectiveSyntax> sortedUsings = ProcessUsings(visited.Usings);
        return visited.WithUsings(sortedUsings);
    }

    private static SyntaxList<UsingDirectiveSyntax> ProcessUsings(SyntaxList<UsingDirectiveSyntax> usings)
    {
        // 1. Sort the list
        List<UsingDirectiveSyntax> sortedList = [.. usings];

        if (sortedList.Count > 1)
        {
            sortedList.Sort(CompareUsings);
        }

        // 2. Preserve File Header (Leading trivia of the *original* first item)
        // We attach it to the new first item.
        SyntaxTriviaList originalFirstTrivia = usings
            .First()
            .GetLeadingTrivia();

        sortedList[0] = sortedList[0].WithLeadingTrivia(originalFirstTrivia);
        // 3. Normalize spacing for all items
        for (int i = 0; i < sortedList.Count; i++)
        {
            UsingDirectiveSyntax current = sortedList[i];
            // A. Strip Leading Newlines (for items 1..N)
            // This prevents gaps if the previous item already has a trailing newline.
            // The first item (0) keeps its header (assigned above).
            if (i > 0)
            {
                current = StripLeadingNewlines(current);
            }

            // B. Set Trailing Newlines
            // Last item gets 2 newlines (blank line separator).
            // All other items get 1 newline (standard list).
            int requiredNewlines = (i == sortedList.Count - 1)
                ? 2
                : 1;

            current = SetTrailingNewlines(current, requiredNewlines);

            sortedList[i] = current;
        }

        return SyntaxFactory.List(sortedList);
    }

    private static UsingDirectiveSyntax StripLeadingNewlines(UsingDirectiveSyntax u)
    {
        SyntaxTriviaList leading = u.GetLeadingTrivia();
        IEnumerable<SyntaxTrivia> newLeading = leading.SkipWhile(t => t.IsKind(SyntaxKind.EndOfLineTrivia));

        return u.WithLeadingTrivia(SyntaxFactory.TriviaList(newLeading));
    }

    private static UsingDirectiveSyntax SetTrailingNewlines(UsingDirectiveSyntax node, int count)
    {
        SyntaxTriviaList trailing = node.GetTrailingTrivia();
        // Find the last significant trivia (comment or non-whitespace/newline).
        // We want to keep comments, but strip existing whitespace/newlines at the end.
        int lastIndexToKeep = -1;

        for (int i = trailing.Count - 1; i >= 0; i--)
        {
            if (trailing[i].IsKind(SyntaxKind.WhitespaceTrivia) || trailing[i].IsKind(SyntaxKind.EndOfLineTrivia))
            {
                continue;
            }

            lastIndexToKeep = i;
            break;
        }

        SyntaxTriviaList newTrivia = SyntaxFactory.TriviaList(trailing.Take(lastIndexToKeep + 1));

        for (int i = 0; i < count; i++)
        {
            newTrivia = newTrivia.Add(SyntaxFactory.CarriageReturnLineFeed);
        }

        return node.WithTrailingTrivia(newTrivia);
    }
}
```

---

### `Styly\Rewriters\VarToExplicitTypeRewriter.cs`

```csharp
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Styly.Rewriters;

internal class VarToExplicitTypeRewriter : CSharpSyntaxRewriter
{
    private readonly SemanticModel _semanticModel;
    public VarToExplicitTypeRewriter(SemanticModel semanticModel)
    {
        _semanticModel = semanticModel;
    }

    public override SyntaxNode? VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
    {
        VariableDeclarationSyntax declaration = node.Declaration;

        if (!declaration.Type.IsVar)
        {
            return base.VisitLocalDeclarationStatement(node);
        }

        TypeInfo typeInfo = _semanticModel.GetTypeInfo(declaration.Type);
        ITypeSymbol? typeSymbol = typeInfo.ConvertedType;
        // Skip rewriting if:
        // 1. Type symbol is not resolved (null or Error).
        // 2. Type is void (cannot be a variable type).
        // 3. Type is Anonymous (e.g. new { A = 1 }), as it has no explicit type name.
        if (typeSymbol is null || typeSymbol.TypeKind == TypeKind.Error || typeSymbol.SpecialType == SpecialType.System_Void || typeSymbol.IsAnonymousType)
        {
            return base.VisitLocalDeclarationStatement(node);
        }

        string typeName = typeSymbol.ToMinimalDisplayString(_semanticModel, declaration.Type.SpanStart);
        TypeSyntax explicitTypeSyntax = SyntaxFactory.ParseTypeName(typeName).WithLeadingTrivia(declaration.Type.GetLeadingTrivia()).WithTrailingTrivia(declaration.Type.GetTrailingTrivia());

        VariableDeclarationSyntax newDeclaration = declaration.WithType(explicitTypeSyntax);

        return node.WithDeclaration(newDeclaration);
    }
}
```

---

### `Styly\Rewriters\VerticalRhythmRewriter.cs`

```csharp
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Styly.Configuration;

namespace Styly.Rewriters;

internal class VerticalRhythmRewriter : CSharpSyntaxRewriter
{
    private readonly SpacingOptions _options;
    public VerticalRhythmRewriter(SpacingOptions options)
    {
        _options = options;
    }

    public override SyntaxNode? VisitCompilationUnit(CompilationUnitSyntax node)
    {
        SyntaxList<MemberDeclarationSyntax> newMembers = ProcessList(node.Members, m =>
        {
            return m is GlobalStatementSyntax g
                ? g.Statement
                : null;
        });

        return base.VisitCompilationUnit(node.WithMembers(newMembers));
    }

    public override SyntaxNode? VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
    {
        SyntaxList<MemberDeclarationSyntax> newMembers = ProcessList(node.Members, _ => null);
        return base.VisitNamespaceDeclaration(node.WithMembers(newMembers));
    }

    public override SyntaxNode? VisitFileScopedNamespaceDeclaration(FileScopedNamespaceDeclarationSyntax node)
    {
        SyntaxList<MemberDeclarationSyntax> newMembers = ProcessList(node.Members, _ => null);
        return base.VisitFileScopedNamespaceDeclaration(node.WithMembers(newMembers));
    }

    public override SyntaxNode? VisitBlock(BlockSyntax node)
    {
        SyntaxList<StatementSyntax> newStatements = ProcessList(node.Statements, s => s);
        return base.VisitBlock(node.WithStatements(newStatements));
    }

    public override SyntaxNode? VisitSwitchSection(SwitchSectionSyntax node)
    {
        SyntaxList<StatementSyntax> newStatements = ProcessList(node.Statements, s => s);
        return base.VisitSwitchSection(node.WithStatements(newStatements));
    }

    private SyntaxList<T> ProcessList<T>(SyntaxList<T> items, Func<T, StatementSyntax?> getStatement)
        where T : SyntaxNode
    {
        if (items.Count < 2)
        {
            return items;
        }

        List<T> newItems = [ items[0] ];

        for (int i = 1; i < items.Count; i++)
        {
            T prev = items[i - 1];
            T curr = items[i];

            StatementSyntax? prevStmt = getStatement(prev);
            StatementSyntax? currStmt = getStatement(curr);

            bool shouldGap = ShouldGap(curr, prevStmt, currStmt);

            if (shouldGap)
            {
                curr = SpacingUtility.EnsureBlankLine(prev, curr);
            }

            newItems.Add(curr);
        }

        return SyntaxFactory.List(newItems);
    }

    private bool ShouldGap<T>(T curr, StatementSyntax? prevStmt, StatementSyntax? currStmt)
        where T : SyntaxNode
    {
        return (_options.EmptyLineBeforeControlFlow && IsControlFlow(currStmt)) || (_options.EmptyLineAfterControlFlow && IsControlFlow(prevStmt)) || (_options.EmptyLineAroundMultiLineExpression && (IsHeavyExpression(prevStmt) || IsHeavyExpression(currStmt))) || curr.HasAnnotations(LayoutAnnotator.PreserveBlankLineAnnotationKind);
    }

    private static bool IsControlFlow(StatementSyntax? s)
    {
        return s is IfStatementSyntax or SwitchStatementSyntax or WhileStatementSyntax or DoStatementSyntax or ForStatementSyntax or ForEachStatementSyntax or TryStatementSyntax or LocalFunctionStatementSyntax;
    }

    private static bool IsHeavyExpression(StatementSyntax? s)
    {
        if (s is null)
        {
            return false;
        }

        // A statement contains a "heavy" expression if any internal expression 
        // (like a ternary or raw string) factually spans multiple lines.
        return s.DescendantNodes().OfType<ExpressionSyntax>().Any(e =>
        {
            FileLinePositionSpan lineSpan = e.GetLocation().GetLineSpan();
            return lineSpan.EndLinePosition.Line > lineSpan.StartLinePosition.Line;
        });
    }
}
```

---

### `Styly\Rewriters\Indentation\IndentationAdder.cs`

```csharp
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Styly.Rewriters.Indentation;

internal class IndentationAdder : CSharpSyntaxRewriter
{
    private const string Indent = "    ";
    public override SyntaxToken VisitToken(SyntaxToken token)
    {
        SyntaxToken processedToken = token;

        if (processedToken.HasLeadingTrivia)
        {
            processedToken = processedToken.WithLeadingTrivia(ProcessTriviaList(processedToken.LeadingTrivia));
        }

        if (processedToken.HasTrailingTrivia)
        {
            processedToken = processedToken.WithTrailingTrivia(ProcessTriviaList(processedToken.TrailingTrivia));
        }

        return processedToken;
    }

    private static SyntaxTriviaList ProcessTriviaList(SyntaxTriviaList originalTrivia)
    {
        List<SyntaxTrivia> newTrivia = [];

        for (int i = 0; i < originalTrivia.Count; i++)
        {
            SyntaxTrivia current = originalTrivia[i];
            newTrivia.Add(current);

            if (!current.IsKind(SyntaxKind.EndOfLineTrivia))
            {
                continue;
            }

            // We just added a newline. We need to add indentation now.
            // Check if the NEXT trivia in the original list is whitespace.
            if (i + 1 < originalTrivia.Count && originalTrivia[i + 1].IsKind(SyntaxKind.WhitespaceTrivia))
            {
                // The next trivia is whitespace (existing indentation).
                // We combine our indent with it.
                SyntaxTrivia existingWhitespace = originalTrivia[i + 1];
                string combinedIndent = existingWhitespace.ToString() + Indent;

                newTrivia.Add(SyntaxFactory.Whitespace(combinedIndent));
                // Skip the next trivia since we've merged it.
                i++;
            }
            else
            {
                // The next trivia is NOT whitespace (or we are at the end of the list).
                // Just insert our indent.
                newTrivia.Add(SyntaxFactory.Whitespace(Indent));
            }
        }

        return SyntaxFactory.TriviaList(newTrivia);
    }
}
```

---

### `Styly\Rewriters\Indentation\IndentationRemover.cs`

```csharp
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Styly.Rewriters.Indentation;

internal class IndentationRemover : CSharpSyntaxRewriter
{
    private const int IndentSize = 4;
    public override SyntaxToken VisitToken(SyntaxToken token)
    {
        if (!token.HasLeadingTrivia)
        {
            return base.VisitToken(token);
        }

        SyntaxTriviaList newTrivia = AdjustLeadingTrivia(token.LeadingTrivia);
        return token.WithLeadingTrivia(newTrivia);
    }

    private static SyntaxTriviaList AdjustLeadingTrivia(SyntaxTriviaList trivia)
    {
        int lastNewlineIndex = FindLastNewlineIndex(trivia);

        return lastNewlineIndex == -1
            ? ReduceIndentationAtStart(trivia)
            : ReduceIndentationAfterNewline(trivia, lastNewlineIndex);
    }

    private static int FindLastNewlineIndex(SyntaxTriviaList trivia)
    {
        for (int i = trivia.Count - 1; i >= 0; i--)
        {
            if (trivia[i].IsKind(SyntaxKind.EndOfLineTrivia))
            {
                return i;
            }
        }

        return -1;
    }

    private static SyntaxTriviaList ReduceIndentationAfterNewline(SyntaxTriviaList trivia, int newlineIndex)
    {
        int indentIndex = newlineIndex + 1;

        if (indentIndex >= trivia.Count)
        {
            return trivia;
        }

        SyntaxTrivia candidate = trivia[indentIndex];

        return candidate.IsKind(SyntaxKind.WhitespaceTrivia)
            ? trivia.Replace(candidate, ReduceWhitespace(candidate))
            : trivia;
    }

    private static SyntaxTriviaList ReduceIndentationAtStart(SyntaxTriviaList trivia)
    {
        if (!trivia.Any())
        {
            return trivia;
        }

        SyntaxTrivia firstTrivia = trivia.First();

        return firstTrivia.IsKind(SyntaxKind.WhitespaceTrivia)
            ? trivia.Replace(firstTrivia, ReduceWhitespace(firstTrivia))
            : trivia;
    }

    private static SyntaxTrivia ReduceWhitespace(SyntaxTrivia whitespace)
    {
        string text = whitespace.ToString();

        string newText = text.Length >= IndentSize
            ? text[IndentSize..]
            : string.Empty;

        return SyntaxFactory.Whitespace(newText);
    }
}
```

---

### `Styly\Rewriters\Initializer\InitializerFormatter.cs`

```csharp
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Styly.Rewriters.Initializer;

internal static class InitializerFormatter
{
    private const int IndentSize = 4;
    public static bool HasComments(SyntaxNode node)
    {
        return node.DescendantTrivia().Any(t =>
        {
            return t.IsKind(SyntaxKind.SingleLineCommentTrivia) || t.IsKind(SyntaxKind.MultiLineCommentTrivia);
        });
    }

    public static SyntaxTriviaList GetParentIndentation(SyntaxNode node)
    {
        SyntaxNode? container = node.FirstAncestorOrSelf<SyntaxNode>(n => n is StatementSyntax or MemberDeclarationSyntax);

        if (container is not null)
        {
            SyntaxTriviaList leading = container.GetLeadingTrivia();
            SyntaxTrivia lastWhitespace = leading.LastOrDefault(t => t.IsKind(SyntaxKind.WhitespaceTrivia));

            if (!lastWhitespace.IsKind(SyntaxKind.None))
            {
                return SyntaxFactory.TriviaList(lastWhitespace);
            }
        }

        return SyntaxFactory.TriviaList();
    }

    private static T RemoveWhitespaceOnly<T>(T node)
        where T : SyntaxNode
    {
        // Remove leading whitespace, keep comments
        SyntaxTriviaList leading = node.GetLeadingTrivia();

        IEnumerable<SyntaxTrivia> newLeading = leading.Where(t =>
        {
            return !t.IsKind(SyntaxKind.WhitespaceTrivia) && !t.IsKind(SyntaxKind.EndOfLineTrivia);
        });
        // Remove trailing whitespace, keep comments
        SyntaxTriviaList trailing = node.GetTrailingTrivia();

        IEnumerable<SyntaxTrivia> newTrailing = trailing.Where(t =>
        {
            return !t.IsKind(SyntaxKind.WhitespaceTrivia) && !t.IsKind(SyntaxKind.EndOfLineTrivia);
        });

        return node.WithLeadingTrivia(newLeading).WithTrailingTrivia(newTrailing);
    }

    public static SeparatedSyntaxList<T> FormatListSingleLine<T>(SeparatedSyntaxList<T> items)
        where T : SyntaxNode
    {
        // Use RemoveWhitespaceOnly instead of WithoutLeadingTrivia/WithoutTrailingTrivia to preserve comments.
        IEnumerable<T> nodes = items.Select(i => RemoveWhitespaceOnly(i));

        IEnumerable<SyntaxToken> separators = Enumerable.Repeat(SyntaxFactory
            .Token(SyntaxKind.CommaToken)
            .WithTrailingTrivia(SyntaxFactory.Space), items.Count - 1);

        return SyntaxFactory.SeparatedList(nodes, separators);
    }

    public static SeparatedSyntaxList<T> FormatListMultiLine<T>(SeparatedSyntaxList<T> items, SyntaxTriviaList parentIndent)
        where T : SyntaxNode
    {
        SyntaxTriviaList itemIndent = parentIndent.Add(SyntaxFactory.Whitespace(new string(' ', IndentSize)));
        SyntaxTrivia newline = SyntaxFactory.CarriageReturnLineFeed;

        IEnumerable<T> nodes = items.Select(i =>
        {
            return i
                .WithoutLeadingTrivia()
                .WithoutTrailingTrivia()
                .WithLeadingTrivia(itemIndent);
        });

        IEnumerable<SyntaxToken> separators = Enumerable.Repeat(SyntaxFactory
            .Token(SyntaxKind.CommaToken)
            .WithTrailingTrivia(newline), items.Count - 1);

        return SyntaxFactory.SeparatedList(nodes, separators);
    }

    public static TNode StripPrecedingTrivia<TNode>(TNode node)
        where TNode : SyntaxNode
    {
        return node switch
        {
            ObjectCreationExpressionSyntax oce => NameMe_1<TNode>(oce),
            ImplicitObjectCreationExpressionSyntax ioce => NameMe_2<TNode>(ioce),
            ArrayCreationExpressionSyntax ace => NameMe_3<TNode>(ace),
            ImplicitArrayCreationExpressionSyntax iace => NameMe_4<TNode>(iace),
            _ => node
        };
    }

    private static TNode NameMe_4<TNode>(ImplicitArrayCreationExpressionSyntax iace) where TNode : SyntaxNode
    {
        return (TNode)(object)iace.WithCloseBracketToken(iace.CloseBracketToken.WithTrailingTrivia(SyntaxFactory.TriviaList()));
    }

    private static TNode NameMe_3<TNode>(ArrayCreationExpressionSyntax ace) where TNode : SyntaxNode
    {
        return (TNode)(object)ace.WithType(ace.Type.WithoutTrailingTrivia());
    }

    private static TNode NameMe_2<TNode>(ImplicitObjectCreationExpressionSyntax ioce) where TNode : SyntaxNode
    {
        return (TNode)(object)ioce.WithArgumentList(ioce.ArgumentList.WithoutTrailingTrivia());
    }

    private static TNode NameMe_1<TNode>(ObjectCreationExpressionSyntax oce) where TNode : SyntaxNode
    {
        return (TNode)(object)(oce.ArgumentList is not null
            ? oce.WithArgumentList(oce.ArgumentList.WithoutTrailingTrivia())
            : oce.WithType(oce.Type.WithoutTrailingTrivia()));
    }

    public static SyntaxToken FormatOpenBraceSingleLine(SyntaxToken openBrace)
    {
        // Ensure space around brace, but preserve existing comments.
        // Flatten leading/trailing to remove newlines, but keep non-whitespace.
        IEnumerable<SyntaxTrivia> leading = openBrace.LeadingTrivia.Where(t =>
        {
            return !t.IsKind(SyntaxKind.WhitespaceTrivia) && !t.IsKind(SyntaxKind.EndOfLineTrivia);
        });

        IEnumerable<SyntaxTrivia> trailing = openBrace.TrailingTrivia.Where(t =>
        {
            return !t.IsKind(SyntaxKind.WhitespaceTrivia) && !t.IsKind(SyntaxKind.EndOfLineTrivia);
        });
        // Start with a space, then comments
        SyntaxTriviaList newLeading = SyntaxFactory.TriviaList(SyntaxFactory.Space).AddRange(leading);
        // Ensure space inside the brace.
        // If there are comments (trailing trivia of open brace), ensure space before AND after them.
        SyntaxTriviaList newTrailing = SyntaxFactory.TriviaList(SyntaxFactory.Space).AddRange(trailing);

        if (trailing.Any())
        {
            newTrailing = newTrailing.Add(SyntaxFactory.Space);
        }

        return openBrace.WithLeadingTrivia(newLeading).WithTrailingTrivia(newTrailing);
    }

    public static SyntaxToken FormatCloseBraceSingleLine(SyntaxToken closeBrace)
    {
        IEnumerable<SyntaxTrivia> leading = closeBrace.LeadingTrivia.Where(t =>
        {
            return !t.IsKind(SyntaxKind.WhitespaceTrivia) && !t.IsKind(SyntaxKind.EndOfLineTrivia);
        });
        // Prepend space
        SyntaxTriviaList newLeading = SyntaxFactory.TriviaList(SyntaxFactory.Space).AddRange(leading);
        // If there are comments, ensure space after them as well (before the brace)
        if (leading.Any())
        {
            newLeading = newLeading.Add(SyntaxFactory.Space);
        }

        // Clean trailing trivia too (remove whitespace/newlines) to ensure compact formatting
        IEnumerable<SyntaxTrivia> trailing = closeBrace.TrailingTrivia.Where(t =>
        {
            return !t.IsKind(SyntaxKind.WhitespaceTrivia) & !t.IsKind(SyntaxKind.EndOfLineTrivia);
        });

        SyntaxTriviaList newTrailing = SyntaxFactory.TriviaList(trailing);

        return closeBrace.WithLeadingTrivia(newLeading).WithTrailingTrivia(newTrailing);
    }

    public static SyntaxToken FormatOpenBraceMultiLine(SyntaxToken openBrace, SyntaxTriviaList parentIndent)
    {
        SyntaxTriviaList trivia = SyntaxFactory.TriviaList(SyntaxFactory.CarriageReturnLineFeed).AddRange(parentIndent);

        return openBrace.WithLeadingTrivia(trivia).WithTrailingTrivia(SyntaxFactory.CarriageReturnLineFeed);
    }

    public static SyntaxToken FormatCloseBraceMultiLine(SyntaxToken closeBrace, SyntaxTriviaList parentIndent)
    {
        SyntaxTriviaList trivia = SyntaxFactory.TriviaList(SyntaxFactory.CarriageReturnLineFeed).AddRange(parentIndent);

        return closeBrace.WithLeadingTrivia(trivia);
    }
}
```

---

### `Styly\Rewriters\Initializer\InitializerRewriter.cs`

```csharp
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Styly.Configuration;

namespace Styly.Rewriters.Initializer;

internal class InitializerRewriter : CSharpSyntaxRewriter
{
    private readonly InitializerOptions _options;
    private enum FormattingAction
    {
        None,
        SingleLine,
        MultiLine
    }

    public InitializerRewriter(InitializerOptions options)
    {
        _options = options;
    }

    private static FormattingAction DetermineAction(InitializerStyle style, bool hasComments, bool wasSingleLine, bool hasItems)
    {
        if (!hasItems)
        {
            return FormattingAction.None;
        }

        // If comments exist, we preserve layout to avoid breaking comment placement.
        if (hasComments)
        {
            // If it was originally single-line, we recover it from NormalizeWhitespace.
            return wasSingleLine
                ? FormattingAction.SingleLine
                : FormattingAction.None;
        }

        return style switch
        {
            InitializerStyle.SingleLine => FormattingAction.SingleLine,
            InitializerStyle.MultiLine => FormattingAction.MultiLine,
            // Preserve mode: only force SingleLine if it was already single-line (recovery)
            InitializerStyle.Preserve => wasSingleLine
                ? FormattingAction.SingleLine
                : FormattingAction.None,
            _ => wasSingleLine
                ? FormattingAction.SingleLine
                : FormattingAction.None,
        };
    }

    public override SyntaxNode? VisitAnonymousObjectCreationExpression(AnonymousObjectCreationExpressionSyntax node)
    {
        FormattingAction action = GetFormattingAction(node);

        return action switch
        {
            FormattingAction.SingleLine => NameMe_6(node),
            FormattingAction.MultiLine => NameMe_5(node),
            FormattingAction.None => throw new NotImplementedException(),
            _ => base.VisitAnonymousObjectCreationExpression(node)
        };
    }

    private FormattingAction GetFormattingAction(AnonymousObjectCreationExpressionSyntax node)
    {
        bool hasComments = InitializerFormatter.HasComments(node);
        bool wasSingleLine = node.HasAnnotations(LayoutAnnotator.SingleLineAnnotationKind);
        bool hasItems = node.Initializers.Any();

        FormattingAction action = DetermineAction(_options.AnonymousType, hasComments, wasSingleLine, hasItems);
        return action;
    }

    private static AnonymousObjectCreationExpressionSyntax NameMe_6(AnonymousObjectCreationExpressionSyntax node)
    {
        SeparatedSyntaxList<AnonymousObjectMemberDeclaratorSyntax> newMembers = InitializerFormatter.FormatListSingleLine(node.Initializers);
        AnonymousObjectCreationExpressionSyntax cleanNode = node.WithNewKeyword(node.NewKeyword.WithTrailingTrivia(SyntaxFactory.TriviaList()));

        return GetSingleLineCleanNode(newMembers, cleanNode);
    }

    private static AnonymousObjectCreationExpressionSyntax NameMe_5(AnonymousObjectCreationExpressionSyntax node)
    {
        SyntaxTriviaList parentIndent = InitializerFormatter.GetParentIndentation(node);
        SeparatedSyntaxList<AnonymousObjectMemberDeclaratorSyntax> newMembers = InitializerFormatter.FormatListMultiLine(node.Initializers, parentIndent);
        AnonymousObjectCreationExpressionSyntax cleanNode = node.WithNewKeyword(node.NewKeyword.WithTrailingTrivia(SyntaxFactory.TriviaList()));

        return GetMultiLineCleanNode(parentIndent, newMembers, cleanNode);
    }

    private static AnonymousObjectCreationExpressionSyntax GetMultiLineCleanNode(SyntaxTriviaList parentIndent, SeparatedSyntaxList<AnonymousObjectMemberDeclaratorSyntax> newMembers, AnonymousObjectCreationExpressionSyntax cleanNode)
    {
        return cleanNode
            .WithOpenBraceToken(InitializerFormatter.FormatOpenBraceMultiLine(cleanNode.OpenBraceToken, parentIndent))
            .WithInitializers(newMembers)
            .WithCloseBraceToken(InitializerFormatter.FormatCloseBraceMultiLine(cleanNode.CloseBraceToken, parentIndent));
    }

    private static AnonymousObjectCreationExpressionSyntax GetSingleLineCleanNode(SeparatedSyntaxList<AnonymousObjectMemberDeclaratorSyntax> newMembers, AnonymousObjectCreationExpressionSyntax cleanNode)
    {
        return cleanNode
            .WithOpenBraceToken(InitializerFormatter.FormatOpenBraceSingleLine(cleanNode.OpenBraceToken))
            .WithInitializers(newMembers)
            .WithCloseBraceToken(InitializerFormatter.FormatCloseBraceSingleLine(cleanNode.CloseBraceToken));
    }

    public override SyntaxNode? VisitCollectionExpression(CollectionExpressionSyntax node)
    {
        FormattingAction action = GetFormattingAction(node);

        return action switch
        {
            FormattingAction.SingleLine => NameMe_4(node),
            FormattingAction.MultiLine => NameMe_3(node),
            FormattingAction.None => throw new NotImplementedException(),
            _ => base.VisitCollectionExpression(node)
        };
    }

    private FormattingAction GetFormattingAction(CollectionExpressionSyntax node)
    {
        bool hasComments = InitializerFormatter.HasComments(node);
        bool wasSingleLine = node.HasAnnotations(LayoutAnnotator.SingleLineAnnotationKind);
        bool hasItems = node.Elements.Any();

        return DetermineAction(_options.Collection, hasComments, wasSingleLine, hasItems);
    }

    private static CollectionExpressionSyntax NameMe_4(CollectionExpressionSyntax node)
    {
        SeparatedSyntaxList<CollectionElementSyntax> newElements = InitializerFormatter.FormatListSingleLine(node.Elements);

        SyntaxToken openBracketToken = node.OpenBracketToken
            .WithLeadingTrivia(SyntaxFactory.TriviaList())
            .WithTrailingTrivia(SyntaxFactory.Space);

        return node
            .WithOpenBracketToken(openBracketToken)
            .WithElements(newElements)
            .WithCloseBracketToken(node.CloseBracketToken.WithLeadingTrivia(SyntaxFactory.Space));
    }

    private static CollectionExpressionSyntax NameMe_3(CollectionExpressionSyntax node)
    {
        SyntaxTriviaList parentIndent = InitializerFormatter.GetParentIndentation(node);
        SeparatedSyntaxList<CollectionElementSyntax> newElements = InitializerFormatter.FormatListMultiLine(node.Elements, parentIndent);

        return node
            .WithOpenBracketToken(InitializerFormatter.FormatOpenBraceMultiLine(node.OpenBracketToken, parentIndent))
            .WithElements(newElements)
            .WithCloseBracketToken(InitializerFormatter.FormatCloseBraceMultiLine(node.CloseBracketToken, parentIndent));
    }

    public override SyntaxNode? VisitEqualsValueClause(EqualsValueClauseSyntax node)
    {
        EqualsValueClauseSyntax visited = (EqualsValueClauseSyntax)base.VisitEqualsValueClause(node)!;

        return _options.Collection == InitializerStyle.MultiLine && visited.Value is CollectionExpressionSyntax
            ? visited.WithEqualsToken(visited.EqualsToken.WithTrailingTrivia(SyntaxFactory.TriviaList()))
            : visited;
    }

    public override SyntaxNode? VisitAssignmentExpression(AssignmentExpressionSyntax node)
    {
        AssignmentExpressionSyntax visited = (AssignmentExpressionSyntax)base.VisitAssignmentExpression(node)!;

        return _options.Collection == InitializerStyle.MultiLine && visited.Right is CollectionExpressionSyntax
            ? visited.WithOperatorToken(visited.OperatorToken.WithTrailingTrivia(SyntaxFactory.TriviaList()))
            : visited;
    }

    public override SyntaxNode? VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
    {
        return ProcessObjectCreation(node, node.Initializer, (n, i) => n.WithInitializer(i));
    }

    public override SyntaxNode? VisitImplicitObjectCreationExpression(ImplicitObjectCreationExpressionSyntax node)
    {
        return ProcessObjectCreation(node, node.Initializer, (n, i) => n.WithInitializer(i));
    }

    public override SyntaxNode? VisitArrayCreationExpression(ArrayCreationExpressionSyntax node)
    {
        return ProcessObjectCreation(node, node.Initializer, (n, i) => n.WithInitializer(i));
    }

    public override SyntaxNode? VisitImplicitArrayCreationExpression(ImplicitArrayCreationExpressionSyntax node)
    {
        return ProcessObjectCreation(node, node.Initializer, (n, i) => n.WithInitializer(i));
    }

    private SyntaxNode ProcessObjectCreation<TNode>(TNode node, InitializerExpressionSyntax? initializer, Func<TNode, InitializerExpressionSyntax, TNode> withInitializer)
        where TNode : ExpressionSyntax
    {
        if (initializer is null)
        {
            return VisitBaseExpression(node);
        }

        return GetFormattingAction(node, initializer) switch
        {
            FormattingAction.SingleLine => NameMe_1(node, initializer, withInitializer),
            FormattingAction.MultiLine => NameMe_2(node, initializer, withInitializer),
            FormattingAction.None => throw new NotImplementedException(),
            _ => VisitBaseExpression(node)
        };
    }

    private FormattingAction GetFormattingAction<TNode>(TNode node, InitializerExpressionSyntax initializer)
        where TNode : ExpressionSyntax
    {
        bool hasComments = InitializerFormatter.HasComments(node);
        bool wasSingleLine = initializer.HasAnnotations(LayoutAnnotator.SingleLineAnnotationKind);
        bool hasItems = initializer.Expressions.Any();

        InitializerStyle style = GetInitializerStyle(initializer);
        FormattingAction action = DetermineAction(style, hasComments, wasSingleLine, hasItems);
        return action;
    }

    private static SyntaxNode NameMe_2<TNode>(TNode node, InitializerExpressionSyntax initializer, Func<TNode, InitializerExpressionSyntax, TNode> withInitializer)
        where TNode : ExpressionSyntax
    {
        SyntaxTriviaList parentIndent = InitializerFormatter.GetParentIndentation(node);
        SeparatedSyntaxList<ExpressionSyntax> newExps = InitializerFormatter.FormatListMultiLine(initializer.Expressions, parentIndent);
        TNode cleanNode = InitializerFormatter.StripPrecedingTrivia(node);

        InitializerExpressionSyntax newInit = initializer
            .WithOpenBraceToken(InitializerFormatter.FormatOpenBraceMultiLine(initializer.OpenBraceToken, parentIndent))
            .WithExpressions(newExps)
            .WithCloseBraceToken(InitializerFormatter.FormatCloseBraceMultiLine(initializer.CloseBraceToken, parentIndent));

        return withInitializer(cleanNode, newInit);
    }

    private static SyntaxNode NameMe_1<TNode>(TNode node, InitializerExpressionSyntax initializer, Func<TNode, InitializerExpressionSyntax, TNode> withInitializer)
        where TNode : ExpressionSyntax
    {
        SeparatedSyntaxList<ExpressionSyntax> newExps = InitializerFormatter.FormatListSingleLine(initializer.Expressions);
        TNode cleanNode = InitializerFormatter.StripPrecedingTrivia(node);

        InitializerExpressionSyntax newInit = initializer
            .WithOpenBraceToken(InitializerFormatter.FormatOpenBraceSingleLine(initializer.OpenBraceToken))
            .WithExpressions(newExps)
            .WithCloseBraceToken(InitializerFormatter.FormatCloseBraceSingleLine(initializer.CloseBraceToken));

        return withInitializer(cleanNode, newInit);
    }

    private InitializerStyle GetInitializerStyle(InitializerExpressionSyntax initializer)
    {
        return IsCollectionInitializer(initializer)
            ? _options.Collection
            : _options.Object;
    }

    private static bool IsCollectionInitializer(InitializerExpressionSyntax initializer)
    {
        return initializer.IsKind(SyntaxKind.CollectionInitializerExpression) || initializer.IsKind(SyntaxKind.ArrayInitializerExpression);
    }

    private SyntaxNode VisitBaseExpression(SyntaxNode node)
    {
        return node switch
        {
            ObjectCreationExpressionSyntax oce => base.VisitObjectCreationExpression(oce)!,
            ImplicitObjectCreationExpressionSyntax ioce => base.VisitImplicitObjectCreationExpression(ioce)!,
            ArrayCreationExpressionSyntax ace => base.VisitArrayCreationExpression(ace)!,
            ImplicitArrayCreationExpressionSyntax iace => base.VisitImplicitArrayCreationExpression(iace)!,
            _ => node
        };
    }
}
```

---

### `Styly\Rewriters\LogicalExpression\LogicalExpressionRewriter.cs`

```csharp
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Styly.Configuration;

namespace Styly.Rewriters.LogicalExpression;

internal class LogicalExpressionRewriter : CSharpSyntaxRewriter
{
    private readonly LogicalExpressionOptions _options;
    private const int IndentSize = 4;
    public LogicalExpressionRewriter(LogicalExpressionOptions options)
    {
        _options = options;
    }

    public override SyntaxNode? VisitBinaryExpression(BinaryExpressionSyntax node)
    {
        if (_options.Style != LogicalExpressionStyle.MultiLine || !IsLogical(node.Kind()))
        {
            return base.VisitBinaryExpression(node);
        }

        // Only process from the root of a logical chain to avoid redundant work
        return IsLogical(node.Parent?.Kind() ?? SyntaxKind.None)
            ? base.VisitBinaryExpression(node)
            : FormatChain(node);
    }

    public override SyntaxNode? VisitBinaryPattern(BinaryPatternSyntax node)
    {
        if (_options.Style != LogicalExpressionStyle.MultiLine)
        {
            return base.VisitBinaryPattern(node);
        }

        // Only process from the root of a pattern chain
        return node.Parent is BinaryPatternSyntax
            ? base.VisitBinaryPattern(node)
            : FormatChain(node);
    }

    private static SyntaxNode FormatChain(SyntaxNode root)
    {
        SyntaxTriviaList parentIndent = GetParentIndentation(root);
        SyntaxTriviaList itemIndent = parentIndent.Add(SyntaxFactory.Whitespace(new string (' ', IndentSize)));
        // Delegate the actual token wrapping to the specialized visitor
        return new LogicalTriviaApplier(itemIndent).Visit(root);
    }

    private static bool IsLogical(SyntaxKind kind)
    {
        return kind is SyntaxKind.LogicalAndExpression or SyntaxKind.LogicalOrExpression;
    }

    private static SyntaxTriviaList GetParentIndentation(SyntaxNode node)
    {
        SyntaxNode? container = node.FirstAncestorOrSelf<SyntaxNode>(n =>
        {
            return n is StatementSyntax or MemberDeclarationSyntax;
        });

        if (container is null)
        {
            return SyntaxFactory.TriviaList();
        }

        SyntaxTriviaList leading = container.GetLeadingTrivia();
        SyntaxTrivia lastWhitespace = leading.LastOrDefault(t => t.IsKind(SyntaxKind.WhitespaceTrivia));

        return !lastWhitespace.IsKind(SyntaxKind.None)
            ? SyntaxFactory.TriviaList(lastWhitespace)
            : SyntaxFactory.TriviaList();
    }
}
```

---

### `Styly\Rewriters\LogicalExpression\LogicalTriviaApplier.cs`

```csharp
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Styly.Rewriters.LogicalExpression;

internal class LogicalTriviaApplier : CSharpSyntaxRewriter
{
    private readonly SyntaxTriviaList _indent;
    private readonly SyntaxTrivia _newline = SyntaxFactory.CarriageReturnLineFeed;
    public LogicalTriviaApplier(SyntaxTriviaList indent)
    {
        _indent = indent;
    }

    public override SyntaxToken VisitToken(SyntaxToken token)
    {
        // If it's a logical operator within a binary expression or pattern, wrap it
        if (IsLogicalOperator(token) && (token.Parent is BinaryExpressionSyntax or BinaryPatternSyntax))
        {
            // If it's a logical operator within a binary expression or pattern, wrap it
            return NameMe(token);
        }

        // If it's a logical operator within a binary expression or pattern, wrap it
        return base.VisitToken(token);
    }

    private SyntaxToken NameMe(SyntaxToken token)
    {
        return token
            .WithLeadingTrivia(SyntaxFactory.TriviaList(_newline).AddRange(_indent))
            .WithTrailingTrivia(SyntaxFactory.Space);
    }

    private static bool IsLogicalOperator(SyntaxToken token)
    {
        return token.IsKind(SyntaxKind.AmpersandAmpersandToken) || token.IsKind(SyntaxKind.BarBarToken) || token.IsKind(SyntaxKind.AndKeyword) || token.IsKind(SyntaxKind.OrKeyword);
    }
}
```

---

### `Styly.Tests\BasicFormattingTests.cs`

```csharp
using Styly.Configuration;

namespace Styly.Tests;

public class BasicFormattingTests : FormatterTestBase
{
    [Fact]
    public static void Cleanup_FixesSpacing_ControlFlow()
    {
        string input = "void M(){if(true){}}";

        string expected = """
            void M()
            {
                if (true)
                {
                }
            }
            """;

        AssertFormatting(input, expected, new FormatOptions());
    }

    [Fact]
    public static void Cleanup_FixesIndentation()
    {
        string input = """
            void M()
            {
            Console.WriteLine("test");
                  if (true)
                {
                    return;
                }
            }
            """;

        string expected = """
            void M()
            {
                Console.WriteLine("test");
                if (true)
                {
                    return;
                }
            }
            """;

        AssertFormatting(input, expected, new FormatOptions());
    }

    [Fact]
    public static void Cleanup_RemovesExcessSpaces()
    {
        string input = """
            void M()
            {
                int    x  =   1;
            }
            """;

        string expected = """
            void M()
            {
                int x = 1;
            }
            """;

        AssertFormatting(input, expected, new FormatOptions());
    }
}
```

---

### `Styly.Tests\CollectionTests.cs`

```csharp
using Styly.Configuration;

namespace Styly.Tests;

public class CollectionTests : FormatterTestBase
{
    [Fact]
    public static void Collections_PreferExpression_ListAndArray()
    {
        string input = """
            using System.Collections.Generic;
            void M()
            {
                List<int> l = new List<int> { 1, 2 };
                int[] a = new int[] { 3, 4 };
                int[] b = new int[0];
            }
            """;

        string expected = """
            using System.Collections.Generic;

            void M()
            {
                List<int> l = [1, 2];
                int[] a = [3, 4];
                int[] b = [];
            }
            """;

        FormatOptions options = new()
        {
            Collections = new CollectionsOptions
            {
                PreferExpression = true
            }
        };

        AssertFormatting(input, expected, options);
    }
}
```

---

### `Styly.Tests\EnumerableAnyTests.cs`

```csharp
using Styly.Configuration;

namespace Styly.Tests;

public class EnumerableAnyTests : FormatterTestBase
{
    [Fact]
    public static void Any_To_Count_List()
    {
        string input = """
            using System.Collections.Generic;
            using System.Linq;

            class C
            {
                void M()
                {
                    List<int> list = new List<int>();
                    if (list.Any()) { }
                }
            }
            """;

        string expected = """
            using System.Collections.Generic;
            using System.Linq;

            class C
            {
                void M()
                {
                    List<int> list = new List<int>();
                    if (list.Count != 0)
                    {
                    }
                }
            }
            """;

        AssertFormatting(input, expected, new FormatOptions());
    }

    [Fact]
    public static void Any_To_Length_Array()
    {
        string input = """
            using System.Linq;

            class C
            {
                void M()
                {
                    int[] arr = new int[0];
                    if (arr.Any()) { }
                }
            }
            """;

        string expected = """
            using System.Linq;

            class C
            {
                void M()
                {
                    int[] arr = new int[0];
                    if (arr.Length != 0)
                    {
                    }
                }
            }
            """;

        AssertFormatting(input, expected, new FormatOptions());
    }

    [Fact]
    public static void Any_Ignored_When_No_Count_Or_Length()
    {
        string input = """
            using System.Collections.Generic;
            using System.Linq;

            class C
            {
                void M(IEnumerable<int> seq)
                {
                    if (seq.Any()) { }
                }
            }
            """;

        string expected = """
            using System.Collections.Generic;
            using System.Linq;

            class C
            {
                void M(IEnumerable<int> seq)
                {
                    if (seq.Any())
                    {
                    }
                }
            }
            """;

        AssertFormatting(input, expected, new FormatOptions());
    }

    [Fact]
    public static void Any_Ignored_With_Predicate()
    {
        string input = """
            using System.Collections.Generic;
            using System.Linq;

            class C
            {
                void M()
                {
                    List<int> list = new List<int>();
                    if (list.Any(x => x > 5)) { }
                }
            }
            """;

        string expected = """
            using System.Collections.Generic;
            using System.Linq;

            class C
            {
                void M()
                {
                    List<int> list = new List<int>();
                    if (list.Any(x => x > 5))
                    {
                    }
                }
            }
            """;

        AssertFormatting(input, expected, new FormatOptions());
    }

    [Fact]
    public static void Any_Is_Converted_And_UnusedUsing_Removed()
    {
        string input = """
            using System.Collections.Generic;
            using System.Linq;

            class C
            {
                void M()
                {
                    List<int> list = new List<int>();
                    if (list.Any()) { }
                }
            }
            """;

        string expected = """
            using System.Collections.Generic;

            class C
            {
                void M()
                {
                    List<int> list = new List<int>();
                    if (list.Count != 0)
                    {
                    }
                }
            }
            """;

        FormatOptions options = new()
        {
            Usings = new UsingsOptions
            {
                RemoveUnused = true
            }
        };

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void Any_To_Count_Operator_Spacing()
    {
        string input = """
            using System.Collections.Generic;
            using System.Linq;

            class C
            {
                void M()
                {
                    List<int> list = [];
                    if(list.Any()){}
                }
            }
            """;

        string expected = """
            using System.Collections.Generic;

            class C
            {
                void M()
                {
                    List<int> list = [];
                    if (list.Count != 0)
                    {
                    }
                }
            }
            """;

        FormatOptions options = new()
        {
            Usings = new UsingsOptions
            {
                RemoveUnused = true
            }
        };

        AssertFormatting(input, expected, options);
    }
}
```

---

### `Styly.Tests\FormatterTests.cs`

```csharp
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
        string normalized = text
            .Replace("""


        """, """


        """)
            .Trim();
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

        return string
            .Join("""


        """, cleanedLines)
            .Trim();
    }
}
```

---

### `Styly.Tests\NamespaceTests.cs`

```csharp
using Styly.Configuration;

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
```

---

### `Styly.Tests\NullCheckPatternTests.cs`

```csharp
using Styly.Configuration;

namespace Styly.Tests;

public class NullCheckPatternTests : FormatterTestBase
{
    [Fact]
    public static void NullCheck_EqualsNull_IsConvertedTo_IsNull()
    {
        string input = """
            void M(object x)
            {
                if (x == null) { }
            }
            """;

        string expected = """
            void M(object x)
            {
                if (x is null)
                {
                }
            }
            """;

        FormatOptions options = new();
        options.Optimization.PreferNullPatterns = true;

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void NullCheck_NotEqualsNull_IsConvertedTo_IsNotNull()
    {
        string input = """
            void M(object x)
            {
                if (x != null) { }
            }
            """;

        string expected = """
            void M(object x)
            {
                if (x is not null)
                {
                }
            }
            """;

        FormatOptions options = new();
        options.Optimization.PreferNullPatterns = true;

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void NullCheck_ReverseOrder_IsConverted()
    {
        string input = """
            void M(object x)
            {
                if (null == x) { }
            }
            """;

        string expected = """
            void M(object x)
            {
                if (x is null)
                {
                }
            }
            """;

        FormatOptions options = new();
        options.Optimization.PreferNullPatterns = true;

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void NullCheck_ComplexExpression_IsConverted()
    {
        string input = """
            void M(string s)
            {
                if (s.Length.ToString() == null) { }
            }
            """;

        string expected = """
            void M(string s)
            {
                if (s.Length.ToString() is null)
                {
                }
            }
            """;

        FormatOptions options = new();
        options.Optimization.PreferNullPatterns = true;

        AssertFormatting(input, expected, options);
    }
}
```

---

### `Styly.Tests\RawStringIsolationTests.cs`

```csharp
using Styly.Configuration;

namespace Styly.Tests;

public class RawStringIsolationTests : FormatterTestBase
{
    [Fact]
    public static void Isolation_MultiLineVerbatim_ConvertsAndIsolates()
    {
        string input = """
            void M()
            {
                var a = 1;
                var s = @"line 1
            line 2";
                var b = 2;
            }
            """;
        // Fixed: The closing delimiter of the inner raw string 
        // is now aligned with 'var s', matching the rewriter's logic.
        string expected = """"
            void M()
            {
                var a = 1;

                var s = """
                    line 1
                    line 2
                """;

                var b = 2;
            }
            """";

        FormatOptions options = new();
        options.RawStrings.PreferRawForMultiline = true;
        options.Spacing.EmptyLineAroundMultiLineExpression = true;

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void Isolation_SingleLineStandard_DoesNotIsolate()
    {
        string input = """
            void M()
            {
                var a = 1;
                var s = "just a single line";
                var b = 2;
            }
            """;

        string expected = """
            void M()
            {
                var a = 1;
                var s = "just a single line";
                var b = 2;
            }
            """;

        FormatOptions options = new();
        options.RawStrings.PreferRawForMultiline = true;
        options.Spacing.EmptyLineAroundMultiLineExpression = true;

        AssertFormatting(input, expected, options);
    }
}
```

---

### `Styly.Tests\SafetyTests.cs`

```csharp
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
```

---

### `Styly.Tests\Styly.Tests.csproj`

```xml
<Project Sdk="Microsoft.NET.Sdk">

	<PropertyGroup>
		<TargetFramework>net10.0</TargetFramework>
		<ImplicitUsings>enable</ImplicitUsings>
		<Nullable>enable</Nullable>
		<IsPackable>false</IsPackable>
		<!-- Disable the strict MSBuildLocator copy check in tests -->
		<DisableMSBuildAssemblyCopyCheck>true</DisableMSBuildAssemblyCopyCheck>
	</PropertyGroup>

	<ItemGroup>
		<PackageReference Include="coverlet.collector" Version="6.0.4" />
		<PackageReference Include="FluentAssertions" Version="8.8.0" />
		<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.14.1" />
		<PackageReference Include="NSubstitute" Version="5.3.0" />
		<PackageReference Include="xunit" Version="2.9.3" />
		<PackageReference Include="xunit.runner.visualstudio" Version="3.1.4" />

		<!-- Fix MSBL001: Ensure MSBuild assemblies are not copied to output -->
		<PackageReference Include="Microsoft.Build" Version="17.11.4" ExcludeAssets="runtime" PrivateAssets="all" />
		<PackageReference Include="Microsoft.Build.Framework" Version="17.11.31" ExcludeAssets="runtime" PrivateAssets="all" />
		<PackageReference Include="Microsoft.NET.StringTools" Version="17.11.4" ExcludeAssets="runtime" PrivateAssets="all" />
		<PackageReference Include="NuGet.Frameworks" Version="6.11.1" ExcludeAssets="runtime" PrivateAssets="all" />
	</ItemGroup>

	<ItemGroup>
		<ProjectReference Include="..\Styly\Styly.csproj" />
	</ItemGroup>

	<ItemGroup>
		<Using Include="Xunit" />
	</ItemGroup>

</Project>
```

---

### `Styly.Tests\UsingsTests.cs`

```csharp
using Styly.Configuration;

namespace Styly.Tests;

public class UsingsTests : FormatterTestBase
{
    [Fact]
    public static void Usings_SortAlphabetical_SystemFirst()
    {
        string input = """
            using Styly;
            using System.Linq;
            using System;
            using Xunit;
            """;

        string expected = """
            using System;
            using System.Linq;
            using Styly;
            using Xunit;
            """;

        FormatOptions options = new()
        {
            Usings = new UsingsOptions
            {
                Sort = UsingSortOrder.Alphabetical
            }
        };

        AssertFormatting(input, expected, options);
    }
}
```

---

### `Styly.Tests\VariablesTests.cs`

```csharp
using Styly.Configuration;

namespace Styly.Tests;

public class VariablesTests : FormatterTestBase
{
    [Fact]
    public static void Variables_VarToExplicit_BuiltInTypes()
    {
        string input = """
            void M()
            {
                var x = 10;
                var s = "hello";
            }
            """;

        string expected = """
            void M()
            {
                int x = 10;
                string s = "hello";
            }
            """;

        FormatOptions options = new()
        {
            Variables = new VariablesOptions
            {
                UseVar = UseVarOption.Never
            }
        };

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void Variables_VarToExplicit_PreservesAnonymousTypes()
    {
        string input = """
            void M()
            {
                var anon = new { Name = "Test" };
            }
            """;

        string expected = """
            void M()
            {
                var anon = new { Name = "Test" };
            }
            """;

        FormatOptions options = new()
        {
            Variables = new VariablesOptions
            {
                UseVar = UseVarOption.Never
            }
        };

        AssertFormatting(input, expected, options);
    }
}
```

---

### `Styly.Tests\CallChain\CallChainMultiLineTests.cs`

```csharp
using Styly.Configuration;

namespace Styly.Tests.CallChain;

public class CallChainMultiLineTests : FormatterTestBase
{
    [Fact]
    public static void CallChain_MultiLine_ExpandsChain()
    {
        string input = "var result = obj.Method1().Method2().Method3();";

        string expected = """
            var result = obj
                .Method1()
                .Method2()
                .Method3();
            """;

        FormatOptions options = new()
        {
            CallChain = new CallChainOptions
            {
                Style = CallChainStyle.MultiLine
            }
        };

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void CallChain_MultiLine_PreservesAlreadyMultiLine()
    {
        string input = """
            var result = obj
                .Method1()
                .Method2();
            """;

        string expected = """
            var result = obj
                .Method1()
                .Method2();
            """;

        FormatOptions options = new()
        {
            CallChain = new CallChainOptions
            {
                Style = CallChainStyle.MultiLine
            }
        };

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void CallChain_MultiLine_IgnoresSingleMemberAccess()
    {
        string input = "var x = obj.Property;";
        string expected = "var x = obj.Property;";

        FormatOptions options = new()
        {
            CallChain = new CallChainOptions
            {
                Style = CallChainStyle.MultiLine
            }
        };

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void CallChain_MultiLine_HandlesNestedChains()
    {
        string input = "var result = outer.GetInner().Items.Select(x => x.Value).ToList();";

        string expected = """
            var result = outer
                .GetInner()
                .Items
                .Select(x => x.Value)
                .ToList();
            """;

        FormatOptions options = new()
        {
            CallChain = new CallChainOptions
            {
                Style = CallChainStyle.MultiLine
            }
        };

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void CallChain_MultiLine_PreservesIndentationContext()
    {
        string input = """
            void M()
            {
                if (true)
                {
                    var x = obj.Method1().Method2();
                }
            }
            """;

        string expected = """
            void M()
            {
                if (true)
                {
                    var x = obj
                        .Method1()
                        .Method2();
                }
            }
            """;

        FormatOptions options = new()
        {
            CallChain = new CallChainOptions
            {
                Style = CallChainStyle.MultiLine
            }
        };

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void CallChain_MultiLine_WithInvocationInChain()
    {
        string input = "var r = factory.Create().Configure().Build().Execute();";

        string expected = """
            var r = factory
                .Create()
                .Configure()
                .Build()
                .Execute();
            """;

        FormatOptions options = new()
        {
            CallChain = new CallChainOptions
            {
                Style = CallChainStyle.MultiLine
            }
        };

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void CallChain_MultiLine_TwoPartChainIsExpanded()
    {
        string input = "var x = obj.Method();";

        string expected = """
            var x = obj
                .Method();
            """;

        FormatOptions options = new()
        {
            CallChain = new CallChainOptions
            {
                Style = CallChainStyle.MultiLine
            }
        };

        AssertFormatting(input, expected, options);
    }
}
```

---

### `Styly.Tests\CallChain\CallChainPreserveTests.cs`

```csharp
using Styly.Configuration;

namespace Styly.Tests.CallChain;

public class CallChainPreserveTests : FormatterTestBase
{
    [Fact]
    public static void CallChain_Preserve_DoesNotModify()
    {
        string input = """
            void M()
            {
                var a = obj.Method1().Method2();
                var b = obj
                    .Method1()
                    .Method2();
            }
            """;
        // Note: var b correctly receives a blank line because it is identified
        // as a 'Heavy' (multi-line) expression by the VerticalRhythmRewriter.
        string expected = """
            void M()
            {
                var a = obj.Method1().Method2();

                var b = obj
                    .Method1()
                    .Method2();
            }
            """;

        FormatOptions options = new()
        {
            CallChain = new CallChainOptions
            {
                Style = CallChainStyle.Preserve
            }
        };

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void CallChain_Preserve_DoesNotBreakComments()
    {
        string input = """
            var result = obj
                .Method1() // first step
                .Method2(); // second step
            """;

        string expected = """
            var result = obj
                .Method1() // first step
                .Method2(); // second step
            """;

        FormatOptions options = new()
        {
            CallChain = new CallChainOptions
            {
                Style = CallChainStyle.Preserve
            }
        };

        AssertFormatting(input, expected, options);
    }
}
```

---

### `Styly.Tests\CallChain\CallChainSingleLineTests.cs`

```csharp
using Styly.Configuration;

namespace Styly.Tests.CallChain;

public class CallChainSingleLineTests : FormatterTestBase
{
    [Fact]
    public static void CallChain_SingleLine_FlattensMultiLineChain()
    {
        string input = """
            void M()
            {
                var result = obj
                    .Method1()
                    .Method2()
                    .Method3();
            }
            """;

        string expected = """
            void M()
            {
                var result = obj.Method1().Method2().Method3();
            }
            """;

        FormatOptions options = new()
        {
            CallChain = new CallChainOptions
            {
                Style = CallChainStyle.SingleLine
            }
        };

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void CallChain_SingleLine_PreservesAlreadySingleLine()
    {
        string input = "var result = obj.Method1().Method2();";
        string expected = "var result = obj.Method1().Method2();";

        FormatOptions options = new()
        {
            CallChain = new CallChainOptions
            {
                Style = CallChainStyle.SingleLine
            }
        };

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void CallChain_SingleLine_IgnoresSingleMemberAccess()
    {
        string input = "var x = obj.Property;";
        string expected = "var x = obj.Property;";

        FormatOptions options = new()
        {
            CallChain = new CallChainOptions
            {
                Style = CallChainStyle.SingleLine
            }
        };

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void CallChain_SingleLine_HandlesNestedChains()
    {
        string input = """
            void M()
            {
                var result = outer
                    .GetInner()
                    .Items
                    .Select(x => x.Value)
                    .Where(x => x > 0)
                    .ToList();
            }
            """;

        string expected = """
            void M()
            {
                var result = outer.GetInner().Items.Select(x => x.Value).Where(x => x > 0).ToList();
            }
            """;

        FormatOptions options = new()
        {
            CallChain = new CallChainOptions
            {
                Style = CallChainStyle.SingleLine
            }
        };

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void CallChain_SingleLine_RemovesExtraWhitespace()
    {
        string input = """
            var x = obj
                .Method1 ( )
                .Method2( arg );
            """;

        string expected = "var x = obj.Method1().Method2(arg);";

        FormatOptions options = new()
        {
            CallChain = new CallChainOptions
            {
                Style = CallChainStyle.SingleLine
            }
        };

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void CallChain_SingleLine_TwoPartChainIsFlattened()
    {
        string input = """
            var x = obj
                .Method();
        """;

        string expected = "var x = obj.Method();";

        FormatOptions options = new()
        {
            CallChain = new CallChainOptions
            {
                Style = CallChainStyle.SingleLine
            }
        };

        AssertFormatting(input, expected, options);
    }
}
```

---

### `Styly.Tests\Initializer\InitializerAnonymousTests.cs`

```csharp
using Styly.Configuration;

namespace Styly.Tests.Initializer;

public class InitializerAnonymousTests : FormatterTestBase
{
    [Fact]
    public static void Initializers_Anonymous_MultiLine()
    {
        string input = "var a = new { X = 1, Y = 2 };";

        string expected = """
            var a = new
            {
                X = 1,
                Y = 2
            };
            """;

        FormatOptions options = new();
        options.Initializers.AnonymousType = InitializerStyle.MultiLine;
        AssertFormatting(input, expected, options);
    }
}
```

---

### `Styly.Tests\Initializer\InitializerCollectionTests.cs`

```csharp
using Styly.Configuration;

namespace Styly.Tests.Initializer;

public class InitializerCollectionTests : FormatterTestBase
{
    [Fact]
    public static void Initializers_Collection_MultiLine()
    {
        string input = "var l = new List<int> { 1, 2, 3 };";

        string expected = """
            var l = new List<int>
            {
                1,
                2,
                3
            };
            """;

        FormatOptions options = new();
        options.Initializers.Collection = InitializerStyle.MultiLine;
        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void Initializers_CollectionExpression_MultiLine()
    {
        string input = "var x = [1, 2];";

        string expected = """
            var x =
            [
                1,
                2
            ];
            """;

        FormatOptions options = new();
        options.Initializers.Collection = InitializerStyle.MultiLine;
        AssertFormatting(input, expected, options);
    }
}
```

---

### `Styly.Tests\Initializer\InitializerObjectTests.cs`

```csharp
using Styly.Configuration;

namespace Styly.Tests.Initializer;

public class InitializerObjectTests : FormatterTestBase
{
    [Fact]
    public static void Initializers_Object_MultiLine()
    {
        string input = "var o = new Obj { A = 1, B = 2 };";

        string expected = """
            var o = new Obj
            {
                A = 1,
                B = 2
            };
            """;

        FormatOptions options = new();
        options.Initializers.Object = InitializerStyle.MultiLine;

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void Initializers_Object_SingleLine()
    {
        string input = """
            var o = new Obj
            {
                A = 1,
                B = 2
            };
            """;

        string expected = "var o = new Obj { A = 1, B = 2 };";

        FormatOptions options = new();
        options.Initializers.Object = InitializerStyle.SingleLine;

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void Initializers_Object_Preserve_Comments()
    {
        string input = "var o = new Obj { /* comment */ A = 1 };";
        string expected = "var o = new Obj { /* comment */ A = 1 };";

        FormatOptions options = new();
        options.Initializers.Object = InitializerStyle.MultiLine;

        AssertFormatting(input, expected, options);
    }
}
```

---

### `Styly.Tests\Modifier\ModifierStaticSafetyTetsts.cs`

```csharp
using Styly.Configuration;

namespace Styly.Tests.Modifier;

public class ModifierStaticSafetyTests : FormatterTestBase
{
    private readonly FormatOptions _options = new()
    {
        Modifiers = new ModifiersOptions
        {
            MakeStaticWhenPossible = true
        }
    };
    [Fact]
    public void Modifiers_KeepInstance_AccessingThisField()
    {
        string input = "class C { int _f; void M() => _f = 1; }";

        string expected = """
            class C
            {
                int _f;
                void M() => _f = 1;
            }
            """;

        AssertFormatting(input, expected, _options);
    }

    [Fact]
    public void Modifiers_MakeStatic_AccessingOtherInstance()
    {
        string input = "class C { public int Val; void Update(C other) => other.Val = 5; }";

        string expected = """
            class C
            {
                public int Val;
                static void Update(C other) => other.Val = 5;
            }
            """;

        AssertFormatting(input, expected, _options);
    }

    [Fact]
    public void Modifiers_KeepInstance_InterfaceImplementation()
    {
        string input = """
            interface I { void M(); }
            class C : I { void I.M() { } }
            """;

        string expected = """
            interface I
            {
                void M();
            }

            class C : I
            {
                void I.M()
                {
                }
            }
            """;

        AssertFormatting(input, expected, _options);
    }

    [Fact]
    public void Modifiers_KeepInstance_VirtualMethods()
    {
        string input = "class C { public virtual void M() { } }";

        string expected = """
            class C
            {
                public virtual void M()
                {
                }
            }
            """;

        AssertFormatting(input, expected, _options);
    }
}
```

---

### `Styly.Tests\Modifier\ModifierStaticTests.cs`

```csharp
using Styly.Configuration;

namespace Styly.Tests.Modifier;

public class ModifierStaticTests : FormatterTestBase
{
    private readonly FormatOptions _options = new()
    {
        Modifiers = new ModifiersOptions
        {
            MakeStaticWhenPossible = true
        }
    };
    [Fact]
    public void Modifiers_MakeStatic_SimpleMethod()
    {
        string input = "class C { void M() { int x = 1; } }";

        string expected = """
            class C
            {
                static void M()
                {
                    int x = 1;
                }
            }
            """;

        AssertFormatting(input, expected, _options);
    }

    [Fact]
    public void Modifiers_MakeStatic_WithParameters()
    {
        string input = "class C { int Add(int a, int b) => a + b; }";

        string expected = """
            class C
            {
                static int Add(int a, int b) => a + b;
            }
            """;

        AssertFormatting(input, expected, _options);
    }

    [Fact]
    public void Modifiers_InsertsStaticCorrectly_WithVisibility()
    {
        string input = "class C { public void M() { } }";

        string expected = """
            class C
            {
                public static void M()
                {
                }
            }
            """;

        AssertFormatting(input, expected, _options);
    }
}
```

---

### `Styly.Tests\VerticalRhythm\VerticalRhythmControlTests.cs`

```csharp
using Styly.Configuration;

namespace Styly.Tests.VerticalRhythm;

public class VerticalRhythmControlFlowTests : FormatterTestBase
{
    [Fact]
    public static void Spacing_EmptyLineBeforeControlFlow_If()
    {
        string input = """
            void M()
            {
                Console.WriteLine();
                if (true) { }
            }
            """;

        string expected = """
            void M()
            {
                Console.WriteLine();

                if (true)
                {
                }
            }
            """;

        FormatOptions options = new();
        options.Spacing.EmptyLineBeforeControlFlow = true;
        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void Spacing_BeforeAndAfter_ControlFlow()
    {
        string input = "void M() { Start(); if (true) { } End(); }";

        string expected = """
            void M()
            {
                Start();

                if (true)
                {
                }

                End();
            }
            """;

        FormatOptions options = new();
        options.Spacing.EmptyLineBeforeControlFlow = true;
        options.Spacing.EmptyLineAfterControlFlow = true;
        AssertFormatting(input, expected, options);
    }
}
```

---

### `Styly.Tests\VerticalRhythm\VerticalRhythmExpressionTests.cs`

```csharp
using Styly.Configuration;

namespace Styly.Tests.VerticalRhythm;

public class VerticalRhythmExpressionTests : FormatterTestBase
{
    [Fact]
    public static void Isolation_MultiLineTernary_AddsBlankLines()
    {
        string input = """
            void M()
            {
                var a = 1;
                var b = true ? "long_branch_1" : "long_branch_2";
                var c = 2;
            }
            """;

        string expected = """
            void M()
            {
                var a = 1;

                var b = true
                    ? "long_branch_1"
                    : "long_branch_2";

                var c = 2;
            }
            """;

        FormatOptions options = new();
        options.Ternary.Style = TernaryStyle.MultiLine;
        options.Spacing.EmptyLineAroundMultiLineExpression = true;

        AssertFormatting(input, expected, options);
    }

    [Fact]
    public static void Isolation_MultiLineObjectInitializer_AddsBlankLine()
    {
        string input = """
            void M()
            {
                var x = 10;
                var o = new Obj { A = 1, B = 2 };
                var y = 20;
            }
            """;

        string expected = """
            void M()
            {
                var x = 10;

                var o = new Obj
                {
                    A = 1,
                    B = 2
                };

                var y = 20;
            }
            """;

        FormatOptions options = new();
        options.Initializers.Object = InitializerStyle.MultiLine;
        options.Spacing.EmptyLineAroundMultiLineExpression = true;

        AssertFormatting(input, expected, options);
    }
}
```

---

