using System.Net;
using Moq;
using Moq.Protected;
using rokka_client_c_sharp.Configuration;
using rokka_client_c_sharp.Exceptions;

namespace rokka_client_c_sharp.tests;

public class RokkaClientTests
{
    private const string Organization = "ROKKA";
    private const string Key = "ROKKA_KEY";
    private const string RenderStack = "ROKKA_STACK";
    private Mock<HttpMessageHandler>? _msgHandler;

    private void MockHttpHandler(Func<HttpRequestMessage, bool>? assertRequest = null,
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

    private RokkaClient CreateRokkaClient()
    {
        if (_msgHandler == null) MockHttpHandler();

        return new RokkaClient(new RokkaConfiguration
        {
            Organization = Organization, Key = Key, RenderStack = RenderStack
        }, _msgHandler!.Object);
    }

    [Fact]
    public async void GivenAConfiguration_WhenCreateImageSource_ThenCorrectApiKeyIsInTheHeader()
    {
        MockHttpHandler(request => request.Headers.GetValues("Api-Key").FirstOrDefault() == Key);
        
        var client = CreateRokkaClient();

        await client.CreateSourceImage("file.jpg", Array.Empty<byte>());

        _msgHandler!.VerifyAll();
    }

    [Fact]
    public async void GivenAConfiguration_WhenCreateImageSource_ThenCorrectApiVersionIsInTheHeader()
    {
        MockHttpHandler(request => request.Headers.GetValues("Api-Version").FirstOrDefault() == "1");

        var client = CreateRokkaClient();

        await client.CreateSourceImage("file.jpg", Array.Empty<byte>());

        _msgHandler!.VerifyAll();
    }
    
    [Fact]
    public void GivenAnConfigurationWithoutKey_WhenCreateClient_ThenThrowsException()
    {
        var configuration = new RokkaConfiguration { Organization = Organization, RenderStack = RenderStack };
        Assert.Throws<RokkaClientException>(() => new RokkaClient(configuration));
    }
    
    [Fact]
    public void GivenAnConfigurationWithoutOrganization_WhenCreateClient_ThenThrowsException()
    {
        var configuration = new RokkaConfiguration { Key = Key, RenderStack = RenderStack };
        Assert.Throws<RokkaClientException>(() => new RokkaClient(configuration));
    }
    
    [Fact]
    public void GivenAnConfigurationWithoutRenderStack_WhenCreateClient_ThenThrowsException()
    {
        var configuration = new RokkaConfiguration { Key = Key, Organization = Organization };
        Assert.Throws<RokkaClientException>(() => new RokkaClient(configuration));
    }
    
    [Fact]
    public void GivenAnNullConfiguration_WhenCreateClient_ThenThrowsException()
    {
        Assert.Throws<RokkaClientException>(() => new RokkaClient(null!));
    }
}