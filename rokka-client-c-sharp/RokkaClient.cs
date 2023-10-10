using rokka_client_c_sharp.Configuration;
using rokka_client_c_sharp.Interfaces;

namespace rokka_client_c_sharp;

public class RokkaClient : IRokkaClient
{
    public static readonly Uri ApiUri = new("https://api.rokka.io");
    private readonly RequestExecutor _requestExecutor;
    private readonly RokkaConfiguration _configuration;

    private RokkaClient(RokkaConfiguration configuration, HttpClient httpClient)
    {
        if (configuration is null || !configuration.IsValid) throw new RokkaClientException("Rokka Client configuration is invalid");
        _configuration = configuration;
        _requestExecutor = new RequestExecutor(configuration, httpClient);
    }
    public RokkaClient(RokkaConfiguration configuration): this(configuration, new HttpClient {BaseAddress = ApiUri})
    {
    }
    
    internal RokkaClient(RokkaConfiguration configuration, HttpMessageHandler httpMessageHandler): this(configuration, new HttpClient(httpMessageHandler) {BaseAddress = ApiUri})
    {
    }

    public async Task<RokkaResponse> CreateSourceImage(string fileName, byte[] bytes)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, $"sourceimages/{_configuration.Organization}");
        using var content = new MultipartFormDataContent();
        content.Add(new ByteArrayContent(bytes), "filedata", fileName);
        content.Add(new StringContent(fileName), "fileName");
        request.Content = content;
        return await _requestExecutor.PerformRequest(request);
    }
}