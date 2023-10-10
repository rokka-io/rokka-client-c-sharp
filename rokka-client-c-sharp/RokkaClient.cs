using rokka_client_c_sharp.Configuration;
using rokka_client_c_sharp.Interfaces;

namespace rokka_client_c_sharp;

public class RokkaClient : IRokkaClient
{
    public static readonly Uri ApiUri = new("https://api.rokka.io");
    public SourceImageEndpoint SourceImages { get; }

    private RokkaClient(RokkaConfiguration configuration, HttpClient httpClient)
    {
        if (configuration is null || !configuration.IsValid) throw new RokkaClientException("Rokka Client configuration is invalid");
        var requestExecutor = new RequestExecutor(configuration, httpClient);
        SourceImages = new SourceImageEndpoint(configuration, requestExecutor);
    }
    public RokkaClient(RokkaConfiguration configuration): this(configuration, new HttpClient {BaseAddress = ApiUri})
    {
    }
    
    internal RokkaClient(RokkaConfiguration configuration, HttpMessageHandler httpMessageHandler): this(configuration, new HttpClient(httpMessageHandler) {BaseAddress = ApiUri})
    {
    }
}