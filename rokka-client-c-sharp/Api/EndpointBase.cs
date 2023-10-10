using rokka_client_c_sharp.Configuration;

namespace rokka_client_c_sharp.Api;

public abstract class EndpointBase
{
    protected readonly RequestExecutor RequestExecutor;
    private readonly RokkaConfiguration _configuration;
    protected readonly string Endpoint;

    protected EndpointBase(string endpoint, RequestExecutor requestExecutor, RokkaConfiguration configuration)
    {
        RequestExecutor = requestExecutor;
        _configuration = configuration;
        Endpoint = $"{endpoint}/{_configuration.Organization}";
    }
}