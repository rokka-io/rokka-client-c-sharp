namespace rokka_client_c_sharp.tests;

public class RequestExecutorTests: RokkaClientTestsBase
{
    private RequestExecutor CreateRequestExecutor()
    {
        if (MessageHandler == null) MockHttpHandler();

        return new RequestExecutor(Configuration, new HttpClient(MessageHandler!.Object) {BaseAddress = RokkaClient.ApiUri});
    }
    
    [Fact]
    public async void GivenAConfiguration_WhenCreateImageSource_ThenCorrectApiKeyIsInTheHeader()
    {
        MockHttpHandler(AssertHeader("Api-Key", Key));
        var requestExecutor = CreateRequestExecutor();
        var request = new HttpRequestMessage(HttpMethod.Options, "/");

        await requestExecutor.PerformRequest(request);

        MessageHandler!.VerifyAll();
    }

    [Fact]
    public async void GivenAConfiguration_WhenCreateImageSource_ThenCorrectApiVersionIsInTheHeader()
    {
        MockHttpHandler(AssertHeader("Api-Version","1"));
        var requestExecutor = CreateRequestExecutor();
        var request = new HttpRequestMessage(HttpMethod.Options, "/");

        await requestExecutor.PerformRequest(request);

        MessageHandler!.VerifyAll();
    }
}