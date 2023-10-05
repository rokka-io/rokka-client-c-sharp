using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using rokka_client_c_sharp.Models;

namespace rokka_client_c_sharp.Factories;

public class RokkaResponseFactory
{
    private const string UnknownReason = "Unspecified";
    
    private readonly JsonSerializerSettings _jsonSerializerSettings = new()
    {
        ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new SnakeCaseNamingStrategy()
        },
        MissingMemberHandling = MissingMemberHandling.Error
    };
    
    public async Task<RokkaResponse> BuildRokkaResponse(HttpResponseMessage httpResponseMessage)
    {
        if (httpResponseMessage is null) throw new RokkaClientException("HTTPResponseMessage cannot be null");

        return !httpResponseMessage.IsSuccessStatusCode ? await BuildErrorResponse(httpResponseMessage) : await BuildSuccessResponse(httpResponseMessage);
    }

    private async Task<T> DeserializeBody<T>(HttpResponseMessage httpResponseMessage) where T : new()
    {
        var bodyString = await httpResponseMessage.Content.ReadAsStringAsync();
        try
        {
            var deserializeObject = JsonConvert.DeserializeObject<T>(bodyString, _jsonSerializerSettings);
            return deserializeObject ?? new T();
        }
        catch (JsonReaderException e)
        {
            throw new RokkaClientException($"Response from Rokka is not JSON. Reason: {e.Message}");
        }
        catch (JsonSerializationException e)
        {
            throw new RokkaClientException($"Unknown JSON response from Rokka. Reason: {e.Message}");
        }
    }

    private async Task<RokkaResponse> BuildSuccessResponse(HttpResponseMessage httpResponseMessage)
    {
        var body = await DeserializeBody<RokkaListResponseBody>(httpResponseMessage);
        return new RokkaSuccessResponse(httpResponseMessage.StatusCode, httpResponseMessage.ReasonPhrase ?? UnknownReason, body);
    }

    private async Task<RokkaResponse> BuildErrorResponse(HttpResponseMessage httpResponseMessage)
    {
        var error = await DeserializeBody<Error>(httpResponseMessage);
        return new RokkaErrorResponse(httpResponseMessage.StatusCode, httpResponseMessage.ReasonPhrase ?? UnknownReason, error);
    }
}