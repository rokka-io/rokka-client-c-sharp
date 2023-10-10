namespace rokka_client_c_sharp.tests;

public class RequestExecutorTests: RokkaClientTestsBase
{
    private RequestExecutor CreateRequestExecutor()
    {
        if (_msgHandler == null) MockHttpHandler();

        return new RequestExecutor(Configuration, new HttpClient(_msgHandler!.Object) {BaseAddress = RokkaClient.ApiUri});
    }
    
    [Fact]
    public async void GivenAConfiguration_WhenCreateImageSource_ThenCorrectApiKeyIsInTheHeader()
    {
        MockHttpHandler(request => request.Headers.GetValues("Api-Key").FirstOrDefault() == Key);
        var requestExecutor = CreateRequestExecutor();
        var request = new HttpRequestMessage(HttpMethod.Options, "/");

        await requestExecutor.PerformRequest(request);

        _msgHandler!.VerifyAll();
    }

    [Fact]
    public async void GivenAConfiguration_WhenCreateImageSource_ThenCorrectApiVersionIsInTheHeader()
    {
        MockHttpHandler(request => request.Headers.GetValues("Api-Version").FirstOrDefault() == "1");
        var requestExecutor = CreateRequestExecutor();
        var request = new HttpRequestMessage(HttpMethod.Options, "/");

        await requestExecutor.PerformRequest(request);

        _msgHandler!.VerifyAll();
    }
}