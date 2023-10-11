using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace rokka_client_c_sharp.Extensions;

public static class StringExtension
{
    public static readonly SnakeCaseNamingStrategy NamingStrategy = new(true, false);
    
    public static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = NamingStrategy
        },
        NullValueHandling = NullValueHandling.Ignore,
        MissingMemberHandling = MissingMemberHandling.Error
    };
    
    public static string SnakeCase(this string s)
    {
        return NamingStrategy.GetPropertyName(s, false);
    }
}