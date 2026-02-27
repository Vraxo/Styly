namespace Styly.Configuration;

public enum UsingSortOrder
{
    None,
    Alphabetical
}

public class UsingsOptions
{
    public UsingSortOrder Sort { get; set; } = UsingSortOrder.None;
    public bool RemoveUnused { get; set; }
}