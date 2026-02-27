namespace Styly.Configuration;

public class StylyConfig
{
    public FormatOptions Format { get; set; } = new();
    public List<string> Include { get; set; } = [];
    public List<string> Exclude { get; set; } = [];
    public List<string> References { get; set; } = [];
}