using System.Net;
using rokka_client_c_sharp.Exceptions;
using rokka_client_c_sharp.Factories;
using rokka_client_c_sharp.Models;

namespace rokka_client_c_sharp.tests.Factories;

public class RokkaResponseFactoryTests
{
    private const string SuccessReasonPhrase = "OK";
    private const string ErrorReasonPhrase = "Forbidden";
    private const string ErrorDetailedMessage = "API credentials are not supplied or not a valid format.";

    private static HttpResponseMessage CreateSuccessResponseMessage()
    {
        const string responseString = "{\"total\":1,\"items\":[{\"hash\":\"07c68ab9e6718b8f23f5e797b3517f22def4219c\",\"short_hash\":\"07c68a\",\"binary_hash\":\"b1c5e43ddb26379491722e928f5bb50d6cd121eb\",\"created\":\"2023-10-03T15:02:50+00:00\",\"name\":\"38b3d4d5bc2c48a59649b26cf25df3c9a8a952c418e84e7b815402b5795b9f9c.jpg\",\"mimetype\":\"image/jpeg\",\"format\":\"jpg\",\"size\":2483460,\"width\":5092,\"height\":3819,\"organization\":\"rokka\",\"link\":\"/sourceimages/rokka/07c68ab9e6718b8f23f5e797b3517f22def4219c\",\"protected\":false,\"locked\":false,\"entropy\":9.01,\"opaque\":true}]}";
        
        return new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            ReasonPhrase = SuccessReasonPhrase,
            Content = new StringContent(responseString)
        };
    }
    
    private static HttpResponseMessage CreateErrorResponseMessage()
    {
        const string responseString = "{\n  \"code\": 403,\n  \"message\": \"API credentials are not supplied or not a valid format.\",\n  \"invalid_authentication\": true\n}";
        
        return new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.Forbidden,
            ReasonPhrase = ErrorReasonPhrase,
            Content = new StringContent(responseString)
        };
    }
    
    [Fact]
    public async void GivenNullResponse_WhenBuildRokkaResponse_ThrowRokkaException()
    {
        HttpResponseMessage? responseMessage = null;
        
       await Assert.ThrowsAsync<RokkaClientException>( () => new RokkaResponseFactory().BuildRokkaResponse(responseMessage));
    }
    
    [Fact]
    public async void GivenASuccessResponse_WhenBuildRokkaResponse_BasicFieldsAreCorrect()
    {
        var responseMessage = CreateSuccessResponseMessage();
        
        var response = await new RokkaResponseFactory().BuildRokkaResponse(responseMessage);
        
        Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
        Assert.Equal(SuccessReasonPhrase, response.StatusMessage);
        Assert.Equal("OK", response.DetailedMessage);
    }
    
    [Fact]
    public async void GivenASuccessResponse_WhenBuildRokkaResponse_ResponseIsCreated()
    {
        var responseMessage = CreateSuccessResponseMessage();
        
        var response = await new RokkaResponseFactory().BuildRokkaResponse(responseMessage);
        
        Assert.NotNull(response);
    }
    
    [Fact]
    public async void GivenASuccessResponse_WhenBuildRokkaResponse_ResponseIsCorrectType()
    {
        var responseMessage = CreateSuccessResponseMessage();
        
        var response = await new RokkaResponseFactory().BuildRokkaResponse(responseMessage);

        Assert.IsType<RokkaSuccessResponse>(response);
    }
    
    [Fact]
    public async void GivenASuccessResponse_WhenBuildRokkaResponse_ResponseIsNotError()
    {
        var responseMessage = CreateSuccessResponseMessage();
        
        var response = await new RokkaResponseFactory().BuildRokkaResponse(responseMessage) as RokkaSuccessResponse;

        Assert.NotNull(response);
        Assert.False(response.IsError);
    }
    
    [Fact]
    public async void GivenASuccessListResponse_WhenBuildRokkaResponse_BodyIsCorrect()
    {
        var responseMessage = CreateSuccessResponseMessage();
        var expectedItems = new List<ImageInfos>
        {
            new()
            {
                Hash = "07c68ab9e6718b8f23f5e797b3517f22def4219c",
                ShortHash = "07c68a",
                BinaryHash = "b1c5e43ddb26379491722e928f5bb50d6cd121eb",
                Created = DateTime.Parse("2023-10-03T15:02:50+00:00"),
                Name = "38b3d4d5bc2c48a59649b26cf25df3c9a8a952c418e84e7b815402b5795b9f9c.jpg",
                Mimetype = "image/jpeg", 
                Format = "jpg",
                Size = 2483460,
                Width = 5092,
                Height = 3819,
                Organization = "rokka",
                Link = "/sourceimages/rokka/07c68ab9e6718b8f23f5e797b3517f22def4219c",
                Protected = false,
                Locked = false,
                Entropy = 9.01d,
                Opaque = true
            }
        };
        
        var response = await new RokkaResponseFactory().BuildRokkaResponse(responseMessage) as RokkaSuccessResponse;

        Assert.NotNull(response);
        Assert.NotNull(response.Body);
        
        var body = response.Body;
        Assert.Equal(1, body.Total);
        Assert.NotNull(body.Items);
        Assert.Equivalent(expectedItems, body.Items);
    }
    
    [Fact]
    public async void GivenAnErrorResponse_WhenBuildRokkaResponse_BasicFieldsAreCorrect()
    {
        var responseMessage = CreateErrorResponseMessage();
        
        var response = await new RokkaResponseFactory().BuildRokkaResponse(responseMessage);
        
        Assert.Equal(HttpStatusCode.Forbidden, responseMessage.StatusCode);
        Assert.Equal(ErrorReasonPhrase, response.StatusMessage);
        Assert.Equal(ErrorDetailedMessage, response.DetailedMessage);
    }
    
    [Fact]
    public async void GivenAnErrorResponse_WhenBuildRokkaResponse_ResponseIsCreated()
    {
        var responseMessage = CreateErrorResponseMessage();
        
        var response = await new RokkaResponseFactory().BuildRokkaResponse(responseMessage);
        
        Assert.NotNull(response);
    }
    
    [Fact]
    public async void GivenAnErrorResponse_WhenBuildRokkaResponse_ResponseIsCorrectType()
    {
        var responseMessage = CreateErrorResponseMessage();
        
        var response = await new RokkaResponseFactory().BuildRokkaResponse(responseMessage);

        Assert.IsType<RokkaErrorResponse>(response);
    }
    
    [Fact]
    public async void GivenAnErrorResponse_WhenBuildRokkaResponse_ResponseIsError()
    {
        var responseMessage = CreateErrorResponseMessage();
        
        var response = await new RokkaResponseFactory().BuildRokkaResponse(responseMessage) as RokkaErrorResponse;

        Assert.NotNull(response);
        Assert.True(response.IsError);
    }
    
    [Fact]
    public async void GivenAnErrorListResponse_WhenBuildRokkaResponse_ErrorIsCorrect()
    {
        var responseMessage = CreateErrorResponseMessage();
        var expectedError = new Error
        {
            Code = 403,
            Message = "API credentials are not supplied or not a valid format.",
            InvalidAuthentication = true
        };
        
        var response = await new RokkaResponseFactory().BuildRokkaResponse(responseMessage) as RokkaErrorResponse;

        Assert.NotNull(response);
        Assert.NotNull(response.Error);
        
        var error = response.Error;
        Assert.Equivalent(expectedError, error);
    }
}