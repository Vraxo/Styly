using Styly.Core;

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
}