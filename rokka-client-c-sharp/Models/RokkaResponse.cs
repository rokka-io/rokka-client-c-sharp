using System.Net;

namespace rokka_client_c_sharp.Models;

public abstract class RokkaResponse
{
    protected RokkaResponse(HttpStatusCode statusCode, string statusMessage)
    {
        StatusCode = statusCode;
        StatusMessage = statusMessage;
    }
    public HttpStatusCode StatusCode { get; set; }
    public string StatusMessage { get; set; }
    public bool IsError => this is RokkaErrorResponse;
    public abstract string DetailedMessage { get; }

}