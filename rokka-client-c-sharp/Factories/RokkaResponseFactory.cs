using System.Net;
using Newtonsoft.Json;
using rokka_client_c_sharp.Extensions;

namespace rokka_client_c_sharp.Factories;

public class RokkaResponseFactory
{
    private const string UnknownReason = "Unspecified";
    
    public async Task<RokkaResponse> BuildRokkaResponse(HttpResponseMessage httpResponseMessage)
    {
        if (httpResponseMessage is null) throw new RokkaClientException("HTTPResponseMessage cannot be null");

        return !httpResponseMessage.IsSuccessStatusCode ? await BuildErrorResponse(httpResponseMessage) : await BuildSuccessResponse(httpResponseMessage);
    }

    private async Task<T> DeserializeBody<T>(HttpResponseMessage httpResponseMessage)
    {
        var bodyString = await httpResponseMessage.Content.ReadAsStringAsync();
        try
        {
            var deserializeObject = JsonConvert.DeserializeObject<T>(bodyString, StringExtension.JsonSerializerSettings);
            return deserializeObject!;
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
        var body = await DeserializeBody<SourceImagesListResponseBody>(httpResponseMessage);
        return new SourceImagesListResponse(httpResponseMessage.StatusCode, httpResponseMessage.ReasonPhrase ?? UnknownReason, body);
    }

    private async Task<RokkaResponse> BuildErrorResponse(HttpResponseMessage httpResponseMessage)
    {
        RokkaResponse response;
        try
        {
            var error = await DeserializeBody<Error>(httpResponseMessage);
            return new RokkaErrorResponse(httpResponseMessage.StatusCode, httpResponseMessage.ReasonPhrase ?? UnknownReason, error);
        }
        catch (RokkaClientException)
        {
            var errorResponse = await DeserializeBody<RokkaErrorResponse>(httpResponseMessage);
            errorResponse.StatusCode = (HttpStatusCode)errorResponse.Error.Code;
            errorResponse.StatusMessage = errorResponse.Error.Message;
            return errorResponse;
        }
    }
}