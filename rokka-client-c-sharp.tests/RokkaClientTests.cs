using rokka_client_c_sharp.Configuration;
using rokka_client_c_sharp.Exceptions;

namespace rokka_client_c_sharp.tests;

public class RokkaClientTests : RokkaClientTestsBase
{
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
    
    [Fact]
    public void GivenAValidConfiguration_WhenCreateClient_NoExceptionIsThrown()
    {
        var configuration = new RokkaConfiguration
            { Key = Key, Organization = Organization, RenderStack = RenderStack };
        
        var client = new RokkaClient(configuration);
        
        Assert.NotNull(client);
    }
    
}