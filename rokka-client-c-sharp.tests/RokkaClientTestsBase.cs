using System.Net;
using Moq;
using Moq.Protected;
using rokka_client_c_sharp.Configuration;

namespace rokka_client_c_sharp.tests;

public abstract class RokkaClientTestsBase
{
    protected Mock<HttpMessageHandler>? MessageHandler;
    protected const string Organization = "ROKKA";
    protected const string Key = "ROKKA_KEY";
    protected const string RenderStack = "ROKKA_STACK";
    protected readonly RokkaConfiguration Configuration = new RokkaConfiguration
        { Key = Key, Organization = Organization, RenderStack = RenderStack }; 

    protected void MockHttpHandler(Func<HttpRequestMessage, bool>? assertRequest = null,
        HttpResponseMessage? response = null)
    {
        MessageHandler = new Mock<HttpMessageHandler>();
        var protectedMock = MessageHandler.Protected();
        var setupApiRequest = protectedMock.Setup<Task<HttpResponseMessage>>(
            "SendAsync",
            ItExpr.Is<HttpRequestMessage>(m => assertRequest == null || assertRequest(m)),
            ItExpr.IsAny<CancellationToken>()
        );

        var httpResponseMessage = response ?? new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent("{}")
        };
        var apiMockedResponse = setupApiRequest.ReturnsAsync(httpResponseMessage);

        apiMockedResponse.Verifiable();
    }

    protected RokkaClient CreateRokkaClient()
    {
        if (MessageHandler == null) MockHttpHandler();

        return new RokkaClient(Configuration, MessageHandler!.Object);
    }

    protected static Func<HttpRequestMessage, bool> AssertHeader(string name, string value)
    {
        return request => request.Headers.GetValues(name).FirstOrDefault() == value;
    }
    
    protected static Func<HttpRequestMessage, bool> AssertStringContent(string name, string value)
    {
        return request =>
        {
            if (request.Content is not MultipartFormDataContent content) return false;
            
            return content.Any(c => c is StringContent stringContent
                                    && stringContent.Headers.ContentDisposition?.Name == name
                                    && stringContent.ReadAsStringAsync().Result == value);
            
        };
    }
    
    protected static Func<HttpRequestMessage, bool> AssertBytesContent(string name, IEnumerable<byte> bytes)
    {
        return request =>
        {
            if (request.Content is not MultipartFormDataContent content) return false;
            
            return content.Any(c => c is ByteArrayContent bytesContent
                                    && bytesContent.Headers.ContentDisposition?.Name == name
                                    && bytesContent.ReadAsByteArrayAsync().Result.SequenceEqual(bytes));
            
        };
    }
    
    protected static Func<HttpRequestMessage, bool> AssertEndpoint(string endpoint)
    {
        return request => request.RequestUri?.AbsolutePath == endpoint;
    }
    
    protected static Func<HttpRequestMessage, bool> AssertHttpMethod(HttpMethod method)
    {
        return request => request.Method == method;
    }
}