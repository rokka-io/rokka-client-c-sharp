using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using rokka_client_c_sharp.Models.MetaData;

namespace rokka_client_c_sharp.Extensions;

public static class MultiPartFormDataContentExtension
{
    private static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = StringExtension.NamingStrategy
        },
        NullValueHandling = NullValueHandling.Ignore,
        MissingMemberHandling = MissingMemberHandling.Error
    };
    
    public static void AddRokkaMetadata(this MultipartFormDataContent content, CreateOptions? options)
    {
        if (options is { OptimizeSource: not null })
        {
            var optionsOptimizeSourceName = nameof(options.OptimizeSource).SnakeCase();
            content.Add(new StringContent(options.OptimizeSource.Value.ToString().ToLower()), optionsOptimizeSourceName);
        }
    }
    
    public static void AddRokkaMetadata(this MultipartFormDataContent content, CreateMetadata? metadata)
    {
        
        if (metadata is not null)
        {
            var serializedObject = JsonConvert.SerializeObject(metadata.MetaUser, JsonSerializerSettings);
            content.Add(new StringContent(serializedObject), nameof(metadata.MetaUser).SnakeCase());
            serializedObject = JsonConvert.SerializeObject(metadata.Options, JsonSerializerSettings);
            content.Add(new StringContent(serializedObject), nameof(metadata.Options).SnakeCase());
            serializedObject = JsonConvert.SerializeObject(metadata.MetaDynamic, JsonSerializerSettings);
            content.Add(new StringContent(serializedObject),nameof(metadata.MetaDynamic).SnakeCase());
        }
    }
}