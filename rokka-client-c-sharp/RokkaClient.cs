using rokka_client_c_sharp.Configuration;
using rokka_client_c_sharp.Interfaces;

namespace rokka_client_c_sharp;

public class RokkaClient : IRokkaClient
{
    public static readonly Uri ApiUri = new("https://api.rokka.io");
    private readonly Lazy<SourceImageEndpoint> _sourceImages;
    public SourceImageEndpoint SourceImages => _sourceImages.Value;

    private RokkaClient(RokkaConfiguration configuration, HttpClient httpClient)
    {
        if (configuration is null || !configuration.IsValid) throw new RokkaClientException("Rokka Client configuration is invalid");
        var requestExecutor = new RequestExecutor(configuration, httpClient);
        _sourceImages = new Lazy<SourceImageEndpoint>(() => new SourceImageEndpoint(configuration, requestExecutor));
    }
    public RokkaClient(RokkaConfiguration configuration): this(configuration, new HttpClient {BaseAddress = ApiUri})
    {
    }
    
    internal RokkaClient(RokkaConfiguration configuration, HttpMessageHandler httpMessageHandler): this(configuration, new HttpClient(httpMessageHandler) {BaseAddress = ApiUri})
    {
    }


}