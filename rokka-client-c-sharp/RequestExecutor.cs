using rokka_client_c_sharp.Configuration;
using rokka_client_c_sharp.Factories;

namespace rokka_client_c_sharp;

internal class RequestExecutor
{
    private readonly RokkaConfiguration _configuration;
    private readonly HttpClient _apiHttpClient;
    private readonly RokkaResponseFactory _responseFactory;

    public RequestExecutor(RokkaConfiguration configuration, HttpClient apiHttpClient)
    {
        _configuration = configuration;
        _apiHttpClient = apiHttpClient;
        _responseFactory = new();
    }

    internal async Task<RokkaResponse> PerformRequest(HttpRequestMessage request)
    {
        request.Headers.Add("Api-Version", "1");
        request.Headers.Add("Api-Key", _configuration.Key);
        var response = await _apiHttpClient.SendAsync(request);
        return await _responseFactory.BuildRokkaResponse(response);
    }
}