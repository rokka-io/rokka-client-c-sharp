using System.Net;

namespace rokka_client_c_sharp.Models;

public class SourceImagesResponse: RokkaResponse
{
    public SourceImage? Body { get; set; }
    
    public SourceImagesResponse(HttpStatusCode statusCode, string statusMessage) : base(statusCode, statusMessage)
    {
    }

    public override string DetailedMessage => "OK";
}