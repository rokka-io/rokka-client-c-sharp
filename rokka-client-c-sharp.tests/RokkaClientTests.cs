using System.Net;
using Moq;
using Moq.Protected;
using rokka_client_c_sharp.Configuration;

namespace rokka_client_c_sharp.tests;

public class RokkaClientTests
{
    private const string Organization = "ROKKA";
    private const string Key = "ROKKA_KEY";
    private const string RenderStack = "ROKKA_STACK";
    private Mock<HttpMessageHandler>? _msgHandler;

    private Mock<HttpMessageHandler> GetMockedHandler(Func<HttpRequestMessage, bool>? assertRequest = null, HttpResponseMessage? response = null)
    {
        var messageHandler = new Mock<HttpMessageHandler>();
        var protectedMock = messageHandler.Protected();
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
        return messageHandler;
    }

    [Fact]
    public async void GivenAConfiguration_WhenCreateImageSource_ThenCorrectApiKeyIsInTheHeader()
    {
        _msgHandler = GetMockedHandler(request => request.Headers.GetValues("Api-Key").FirstOrDefault() == Key);
        
        var client = new RokkaClient(new RokkaConfiguration()
        {
            Organization = Organization, Key = Key, RenderStack = RenderStack
        }, _msgHandler.Object );

       await client.CreateSourceImage("file.jpg", Array.Empty<byte>());
       
       _msgHandler.VerifyAll();
    }
    
    [Fact]
    public async void GivenAConfiguration_WhenCreateImageSource_ThenCorrectApiVersionIsInTheHeader()
    {
        _msgHandler = _msgHandler = GetMockedHandler(request => request.Headers.GetValues("Api-Version").FirstOrDefault() == "1");
        
        var client = new RokkaClient(new RokkaConfiguration()
        {
            Organization = Organization, Key = Key, RenderStack = RenderStack
        }, _msgHandler.Object );

        await client.CreateSourceImage("file.jpg", Array.Empty<byte>());
       
        _msgHandler.VerifyAll();
    }
}