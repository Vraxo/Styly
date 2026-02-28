namespace Styly.Configuration;

public class UsingsOptions
{
    public UsingSortOrder Sort { get; set; } = UsingSortOrder.None;
    public bool RemoveUnused { get; set; }
}