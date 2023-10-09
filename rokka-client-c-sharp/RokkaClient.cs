using System.Runtime.CompilerServices;
using rokka_client_c_sharp.Configuration;
using rokka_client_c_sharp.Factories;
using rokka_client_c_sharp.Interfaces;

namespace rokka_client_c_sharp;

public class RokkaClient : IRokkaClient
{
    private readonly RokkaConfiguration _configuration;
    private static readonly Uri ApiUri = new("https://api.rokka.io");
    private readonly HttpClient _apiHttpClient;
    private readonly RokkaResponseFactory _responseFactory;

    private RokkaClient(RokkaConfiguration configuration, HttpClient httpClient)
    {
        _configuration = configuration;
        _apiHttpClient = httpClient;
        _responseFactory = new();
    }
    public RokkaClient(RokkaConfiguration configuration): this(configuration, new HttpClient() {BaseAddress = ApiUri})
    {
        _configuration = configuration;
    }
    
    internal RokkaClient(RokkaConfiguration configuration, HttpMessageHandler httpMessageHandler): this(configuration, new HttpClient(httpMessageHandler) {BaseAddress = ApiUri})
    {
    }

    private async Task<RokkaResponse> PerformRequest(HttpRequestMessage request)
    {
        request.Headers.Add("Api-Version", "1");
        request.Headers.Add("Api-Key", _configuration.Key);
        var response = await _apiHttpClient.SendAsync(request);
        return await _responseFactory.BuildRokkaResponse(response);
    }

    public async Task<RokkaResponse> CreateSourceImage(string fileName, byte[] bytes)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, $"sourceimages/{_configuration.Organization}");
        using var content = new MultipartFormDataContent();
        content.Add(new ByteArrayContent(bytes), "filedata", fileName);
        content.Add(new StringContent(fileName), "fileName");
        request.Content = content;
        return await PerformRequest(request);
    }
}