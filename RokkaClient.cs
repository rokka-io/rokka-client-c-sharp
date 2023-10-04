using rokka_client_c_sharp.Configuration;
using rokka_client_c_sharp.Factories;
using rokka_client_c_sharp.Interfaces;
using rokka_client_c_sharp.Models;

namespace rokka_client_c_sharp;

public class RokkaClient : IRokkaClient
{
    private readonly RokkaConfiguration _configuration;
    private readonly HttpClient _apiHttpClient = new() { BaseAddress = new Uri("https://api.rokka.io")};
    private readonly HttpClient _renderHttpClient;
    private readonly RokkaResponseFactory _responseFactory;

    
    public RokkaClient(RokkaConfiguration configuration)
    {
        _configuration = configuration;
        _renderHttpClient = new() { BaseAddress = new Uri($"https://{_configuration.Organization}.rokka.io")};
        _responseFactory = new();
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