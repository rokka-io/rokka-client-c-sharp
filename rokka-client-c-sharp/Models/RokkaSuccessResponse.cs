using System.Net;

namespace rokka_client_c_sharp.Models;

public class RokkaSuccessResponse: RokkaResponse
{
    public RokkaListResponseBody Body { get; private set; }
    public RokkaSuccessResponse(HttpStatusCode statusCode, string statusMessage, RokkaListResponseBody body) : base(statusCode, statusMessage)
    {
        Body = body;
    }

    public override string DetailedMessage => "OK";
}