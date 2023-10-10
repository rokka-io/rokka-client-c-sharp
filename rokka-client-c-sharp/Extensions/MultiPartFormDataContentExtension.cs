using Newtonsoft.Json;
using rokka_client_c_sharp.Models.MetaData;

namespace rokka_client_c_sharp.Extensions;

public static class MultiPartFormDataContentExtension
{
    public static void AddRokkaMetadata<T>(this MultipartFormDataContent content, MetaData<T>? metadata)
    {
        if (metadata is null) return;
        
        foreach (var keyValuePair in metadata)
        {
            string stringContent;
            if (keyValuePair.Value is string)
            {
                stringContent = keyValuePair.Value.ToString() ?? string.Empty;
            }
            else
            {
                stringContent = JsonConvert.SerializeObject(keyValuePair.Value, StringExtension.JsonSerializerSettings);
            }
            content.Add(new StringContent(stringContent), keyValuePair.Key);
        }
    }
}