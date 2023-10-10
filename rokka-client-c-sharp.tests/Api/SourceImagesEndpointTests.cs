using System.Text;

namespace rokka_client_c_sharp.tests.Api;

public class SourceImagesEndpointTests: RokkaClientTestsBase
{
    private const string FileName = "file.jpg";
    private readonly byte[] _bytes = Encoding.Unicode.GetBytes("test_data");

    [Fact]
    public async void GivenAFileName_WhenCreate_ThenRequestIsCorrect()
    {
        MockHttpHandler(AssertStringContent("fileName", FileName));
        
        var client = CreateRokkaClient();

        await client.SourceImages.Create(FileName, Array.Empty<byte>());

        MessageHandler!.VerifyAll();
    }

    [Fact]
    public async void GivenAFileContent_WhenCreate_ThenRequestIsCorrect()
    {
        MockHttpHandler(AssertBytesContent("filedata", _bytes));
    
        var client = CreateRokkaClient();
    
        await client.SourceImages.Create(FileName, _bytes);
    
        MessageHandler!.VerifyAll();
    }
    
    [Fact]
    public async void GivenAContent_WhenCreate_CorrectEndpointIsCalled()
    {
        MockHttpHandler(AssertEndpoint($"/sourceimages/{Configuration.Organization}"));
    
        var client = CreateRokkaClient();
    
        await client.SourceImages.Create(FileName, _bytes);
    
        MessageHandler!.VerifyAll();
    }
    
    [Fact]
    public async void GivenAContent_WhenCreate_CorrectHttpMethodIsUsed()
    {
        MockHttpHandler(AssertHttpMethod(HttpMethod.Post));
    
        var client = CreateRokkaClient();
    
        await client.SourceImages.Create(FileName, _bytes);
    
        MessageHandler!.VerifyAll();
    }
}