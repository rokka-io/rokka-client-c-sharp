using System.Net;

namespace rokka_client_c_sharp.Models;

public class RokkaErrorResponse: RokkaResponse
{
    public Error Error { get; }
    public RokkaErrorResponse(HttpStatusCode statusCode, string statusMessage, Error error) : base(statusCode, statusMessage)
    {
        Error = error;
    }

    public override string DetailedMessage => Error.Message ?? string.Empty;
}