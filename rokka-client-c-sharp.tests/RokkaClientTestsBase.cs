using System.Net;
using Moq;
using Moq.Protected;
using rokka_client_c_sharp.Configuration;

namespace rokka_client_c_sharp.tests;

public abstract class RokkaClientTestsBase
{
    protected Mock<HttpMessageHandler>? _msgHandler;
    protected const string Organization = "ROKKA";
    protected const string Key = "ROKKA_KEY";
    protected const string RenderStack = "ROKKA_STACK";
    protected RokkaConfiguration Configuration = new RokkaConfiguration
        { Key = Key, Organization = Organization, RenderStack = RenderStack }; 

    protected void MockHttpHandler(Func<HttpRequestMessage, bool>? assertRequest = null,
        HttpResponseMessage? response = null)
    {
        _msgHandler = new Mock<HttpMessageHandler>();
        var protectedMock = _msgHandler.Protected();
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
        if (_msgHandler == null) MockHttpHandler();

        return new RokkaClient(Configuration, _msgHandler!.Object);
    }
}