namespace Styly.Configuration;

public class InitializerOptions
{
    public InitializerStyle AnonymousType { get; set; } = InitializerStyle.Preserve;
    public InitializerStyle Object { get; set; } = InitializerStyle.Preserve;
    public InitializerStyle Collection { get; set; } = InitializerStyle.Preserve;
}