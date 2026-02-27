namespace Styly.Configuration;

public enum UseVarOption
{
    /// <summary>
    /// Only use var when the type is apparent from the right-hand side.
    /// </summary>
    WhenApparent,
    /// <summary>
    /// Always prefer using var where possible.
    /// </summary>
    Always,
    /// <summary>
    /// Never use var; always use explicit types.
    /// </summary>
    Never
}

public class VariablesOptions
{
    public UseVarOption? UseVar { get; set; }
}