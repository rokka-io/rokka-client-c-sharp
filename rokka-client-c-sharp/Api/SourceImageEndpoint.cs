using rokka_client_c_sharp.Configuration;
using rokka_client_c_sharp.Extensions;
using rokka_client_c_sharp.Models.MetaData;

namespace rokka_client_c_sharp.Api;

public class SourceImageEndpoint : EndpointBase
{
    internal SourceImageEndpoint(RokkaConfiguration configuration, RequestExecutor requestExecutor) : base("sourceimages", requestExecutor, configuration)
    {
    }

    public async Task<RokkaResponse> Create(string fileName, byte[] bytes, CreateMetadata? metadata = null, CreateOptions? options = null)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, Endpoint);
        using var content = new MultipartFormDataContent();
        content.Add(new ByteArrayContent(bytes), "filedata", fileName);
        content.Add(new StringContent(fileName), "fileName");
        content.Add(new StringContent("filedata"), "name");
        content.AddRokkaMetadata(options);
        content.AddRokkaMetadata(metadata);
        request.Content = content;
        return await RequestExecutor.PerformRequest(request);
    }
}