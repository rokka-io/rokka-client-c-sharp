using System.Text;
using rokka_client_c_sharp.Models.MetaData;

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
    
    [Fact]
    public async void GivenAContent_WhenCreate_CorrectFormContentNameIsSent()
    {
        MockHttpHandler(AssertStringContent("name", "filedata"));
    
        var client = CreateRokkaClient();
    
        await client.SourceImages.Create(FileName, _bytes);
    
        MessageHandler!.VerifyAll();
    }
    
    [Fact]
    public async void GivenAContentAndOptions_WhenCreate_CorrectFormContentIsSent()
    {
        var createOptions = new CreateOptions() { OptimizeSource = true };
        MockHttpHandler(AssertStringContent("optimize_source", "true"));
    
        var client = CreateRokkaClient();
    
        await client.SourceImages.Create(FileName, _bytes, options: createOptions);
    
        MessageHandler!.VerifyAll();
    }
    
    [Fact]
    public async void GivenCreateMetadataWithUserMetadata_WhenCreate_CorrectFormContentIsSent()
    {
        var metadata = new CreateMetadata { MetaUser = new MetaDataUser { { "id", "unit_test"} } };
        MockHttpHandler(AssertStringContent("meta_user[0]", "{\"id\":\"unit_test\"}"));
    
        var client = CreateRokkaClient();
    
        await client.SourceImages.Create(FileName, _bytes, metadata: metadata);
    
        MessageHandler!.VerifyAll();
    }
    
    [Fact]
    public async void GivenCreateMetadataWithDynamicMetadataCropArea_WhenCreate_CorrectFormContentIsSent()
    {
        var metadataDynamic = new MetaDataDynamic() { { "id", "unit_test" } };
        metadataDynamic.CropArea = new Area { X = 2, Y = 4 };
        var metadata = new CreateMetadata { MetaDynamic = metadataDynamic };
        MockHttpHandler(AssertStringContent("meta_dynamic[0]", "{\"id\":\"unit_test\",\"crop_area\":{\"x\":2,\"y\":4}}"));
    
        var client = CreateRokkaClient();
    
        await client.SourceImages.Create(FileName, _bytes, metadata: metadata);
    
        MessageHandler!.VerifyAll();
    }
    
    [Fact]
    public async void GivenCreateMetadataWithDynamicMetadataSubjectArea_WhenCreate_CorrectFormContentIsSent()
    {
        var metadataDynamic = new MetaDataDynamic() { { "id", "unit_test" } };
        metadataDynamic.SubjectArea = new Area { X = 2, Y = 4, Width = 5, Height = 6};
        var metadata = new CreateMetadata { MetaDynamic = metadataDynamic };
        MockHttpHandler(AssertStringContent("meta_dynamic[0]", "{\"id\":\"unit_test\",\"subject_area\":{\"x\":2,\"y\":4,\"width\":5,\"height\":6}}"));
    
        var client = CreateRokkaClient();
    
        await client.SourceImages.Create(FileName, _bytes, metadata: metadata);
    
        MessageHandler!.VerifyAll();
    }
    
    [Fact]
    public async void GivenCreateMetadataWithDynamicMetadataVersion_WhenCreate_CorrectFormContentIsSent()
    {
        var metadataDynamic = new MetaDataDynamic() { { "id", "unit_test" } };
        metadataDynamic.Version = new Models.MetaData.Version { Text = "sample_text" };
        var metadata = new CreateMetadata { MetaDynamic = metadataDynamic };
        MockHttpHandler(AssertStringContent("meta_dynamic[0]", "{\"id\":\"unit_test\",\"version\":{\"text\":\"sample_text\"}}"));
    
        var client = CreateRokkaClient();
    
        await client.SourceImages.Create(FileName, _bytes, metadata: metadata);
    
        MessageHandler!.VerifyAll();
    }
    
    [Fact]
    public async void GivenCreateMetadataWithOptionsMetadataBinaryHash_WhenCreate_CorrectFormContentIsSent()
    {
        var options = new MetaDataOptions() { { "id", "unit_test" } };
        options.VisualBinaryhash = true;
        var metadata = new CreateMetadata { Options = options };
        MockHttpHandler(AssertStringContent("options[0]", "{\"id\":\"unit_test\",\"visual_binaryhash\":true}"));
    
        var client = CreateRokkaClient();
    
        await client.SourceImages.Create(FileName, _bytes, metadata: metadata);
    
        MessageHandler!.VerifyAll();
    }
    
    [Fact]
    public async void GivenCreateMetadataWithOptionsMetadataProtected_WhenCreate_CorrectFormContentIsSent()
    {
        var options = new MetaDataOptions() { { "id", "unit_test" } };
        options.Protected = false;
        var metadata = new CreateMetadata { Options = options };
        MockHttpHandler(AssertStringContent("options[0]", "{\"id\":\"unit_test\",\"protected\":false}"));
    
        var client = CreateRokkaClient();
    
        await client.SourceImages.Create(FileName, _bytes, metadata: metadata);
    
        MessageHandler!.VerifyAll();
    }
    
    [Fact]
    public async void GivenCreateMetadataCustomStringMetadata_WhenCreate_CorrectFormContentIsSent()
    {
        var metadata = new CreateMetadata();
        metadata.Add("StringMetadata", "Value");
        MockHttpHandler(AssertStringContent("string_metadata[0]", "Value"));
    
        var client = CreateRokkaClient();
    
        await client.SourceImages.Create(FileName, _bytes, metadata: metadata);
    
        MessageHandler!.VerifyAll();
    }
}