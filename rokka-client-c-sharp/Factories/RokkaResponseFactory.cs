using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using rokka_client_c_sharp.Models;

namespace rokka_client_c_sharp.Factories;

public class RokkaResponseFactory
{
    private readonly JsonSerializerSettings _jsonSerializerSettings = new()
    {
        ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new SnakeCaseNamingStrategy()
        }
    };
    
    public async Task<RokkaResponse> BuildRokkaResponse(HttpResponseMessage httpResponseMessage)
    {
        if (httpResponseMessage is null) return null;

        return !httpResponseMessage.IsSuccessStatusCode ? await BuildErrorResponse(httpResponseMessage) : await BuildSuccessResponse(httpResponseMessage);
    }

    private async Task<T> DeserializeBody<T>(HttpResponseMessage httpResponseMessage) 
    {
        var bodyString = await httpResponseMessage.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(bodyString, _jsonSerializerSettings);
    }

    private async Task<RokkaResponse> BuildSuccessResponse(HttpResponseMessage httpResponseMessage)
    {
        var body = await DeserializeBody<RokkaListResponseBody>(httpResponseMessage);
        return new RokkaSuccessResponse(httpResponseMessage.StatusCode, httpResponseMessage.ReasonPhrase, body);
    }

    private async Task<RokkaResponse> BuildErrorResponse(HttpResponseMessage httpResponseMessage)
    {
        var error = await DeserializeBody<Error>(httpResponseMessage);
        return new RokkaErrorResponse(httpResponseMessage.StatusCode, httpResponseMessage.ReasonPhrase, error);
    }
}