namespace Styly.Configuration;

public class CollectionsOptions
{
    // YamlDotNet maps 'preferExpression' from the config to this property via the project's CamelCaseNamingConvention.
    public bool PreferExpression { get; set; }
}