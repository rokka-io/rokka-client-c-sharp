using System.Net;

namespace rokka_client_c_sharp.Models;

public class SourceImagesListResponse: RokkaResponse
{
    public SourceImagesListResponseBody Body { get; private set; }
    public SourceImagesListResponse(HttpStatusCode statusCode, string statusMessage, SourceImagesListResponseBody body) : base(statusCode, statusMessage)
    {
        Body = body;
    }

    public override string DetailedMessage => "OK";
}