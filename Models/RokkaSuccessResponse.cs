using System.Net;

namespace rokka_client_c_sharp.Models;

public class RokkaSuccessResponse: RokkaResponse
{
    public RokkaResponseBody Body { get; private set; }
    public RokkaSuccessResponse(HttpStatusCode statusCode, string statusMessage, RokkaResponseBody body) : base(statusCode, statusMessage)
    {
        Body = body;
    }

    public override string DetailedMessage => "OK";
}