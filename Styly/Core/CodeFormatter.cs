using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Styly.Configuration;
using Styly.Rewriters;
using Styly.Rewriters.Initializer;

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

        Project project = workspace.AddProject("ScriptProject", LanguageNames.CSharp)
            .WithMetadataReferences(references);

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
        return root.NormalizeWhitespace(indentation: "    ", eol: "\r\n");
    }

    private static async Task<Document> ApplySemanticRewritersAsync(Document document, FormatOptions options)
    {
        async Task<(SemanticModel, SyntaxNode)> GetCtx()
        {
            return (await document.GetSemanticModelAsync() ?? throw new Exception(),
             await document.GetSyntaxRootAsync() ?? throw new Exception());
        }

        if (options.Variables?.UseVar != null)
        {
            (SemanticModel? model, SyntaxNode? root) = await GetCtx();
            SyntaxNode newRoot = options.Variables.UseVar == UseVarOption.Never
                ? new VarToExplicitTypeRewriter(model).Visit(root)
                : new ExplicitTypeToVarRewriter(model, options.Variables.UseVar.Value).Visit(root);
            document = document.WithSyntaxRoot(newRoot);
        }

        if (options.Collections?.PreferExpression == true)
        {
            (SemanticModel? model, SyntaxNode? root) = await GetCtx();
            document = document.WithSyntaxRoot(new CollectionExpressionRewriter(model).Visit(root));
        }

        if (options.Modifiers.MakeStaticWhenPossible)
        {
            (SemanticModel? model, SyntaxNode? root) = await GetCtx();
            document = document.WithSyntaxRoot(new StaticModifierRewriter(model).Visit(root));
        }

        if (options.Optimization.PreferLengthCountOverAny)
        {
            (SemanticModel? model, SyntaxNode? root) = await GetCtx();
            document = document.WithSyntaxRoot(new EnumerableAnyRewriter(model).Visit(root));
        }

        if (options.Optimization.PreferNullPatterns)
        {
            (SemanticModel _, SyntaxNode? root) = await GetCtx();
            document = document.WithSyntaxRoot(new NullCheckPatternRewriter().Visit(root));
        }

        if (options.Usings.RemoveUnused)
        {
            (SemanticModel? model, SyntaxNode? root) = await GetCtx();
            IEnumerable<UsingDirectiveSyntax> unused = model.GetDiagnostics()
                .Where(d => d.Id == "CS8019")
                .Select(d => root.FindNode(d.Location.SourceSpan))
                .OfType<Microsoft.CodeAnalysis.CSharp.Syntax.UsingDirectiveSyntax>();

            if (unused.Any())
            {
                document = document.WithSyntaxRoot(root.RemoveNodes(unused, SyntaxRemoveOptions.KeepNoTrivia)!);
            }
        }

        return document;
    }

    private static SyntaxNode ApplySyntacticRewriters(SyntaxNode root, FormatOptions options)
    {
        root = new NamespaceRewriter(options.Namespace).Visit(root);
        root = new InitializerRewriter(options.Initializers).Visit(root);
        root = new TernaryRewriter(options.Ternary).Visit(root);
        root = new RawStringRewriter(options.RawStrings).Visit(root);
        root = new LogicalExpressionRewriter(options.LogicalExpressions).Visit(root);

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