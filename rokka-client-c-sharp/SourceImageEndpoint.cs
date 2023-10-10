using rokka_client_c_sharp.Configuration;

namespace rokka_client_c_sharp;

public class SourceImageEndpoint : EndpointBase
{
    internal SourceImageEndpoint(RokkaConfiguration configuration, RequestExecutor requestExecutor) : base("sourceimages", requestExecutor, configuration)
    {
    }

    public async Task<RokkaResponse> Create(string fileName, byte[] bytes)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, Endpoint);
        using var content = new MultipartFormDataContent();
        content.Add(new ByteArrayContent(bytes), "filedata", fileName);
        content.Add(new StringContent(fileName), "fileName");
        request.Content = content;
        return await RequestExecutor.PerformRequest(request);
    }
}