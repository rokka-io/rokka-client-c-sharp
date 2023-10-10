using Newtonsoft.Json.Serialization;

namespace rokka_client_c_sharp.Extensions;

public static class StringExtension
{
    public static readonly SnakeCaseNamingStrategy NamingStrategy = new();
    
    public static string SnakeCase(this string s)
    {
        return NamingStrategy.GetPropertyName(s, false);
    }
}