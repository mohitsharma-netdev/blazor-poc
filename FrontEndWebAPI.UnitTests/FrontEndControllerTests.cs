using FrontEndWebAPI.Controllers;
using FrontEndWebAPI.UnitTests.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;

namespace FrontEndWebAPI.UnitTests;

public class FrontEndControllerTests
{
    HttpClientMockHelper httpClientMockHelper;
    public FrontEndControllerTests()
    {
        httpClientMockHelper = new HttpClientMockHelper();
    }

    
    [Fact]
    public async Task InvokeApis_GET_ReturnsOkResult()
    {
        // Arrange
        var mockApiResponse = new List<Tuple<HttpStatusCode, HttpContent>>
            {
                new (HttpStatusCode.OK, new StringContent(@"Test Response 1", Encoding.UTF8, "application/json")),
                new (HttpStatusCode.Created, new StringContent(@"Test Response 2", Encoding.UTF8, "application/json"))
            };

        var httpClient = httpClientMockHelper.GetHttpClientWithHttpMessageHandlerSequenceResponseMock(mockApiResponse);

        var frontEndApiController = new FrontEndApiController(httpClient);

        // Act
        var result = await frontEndApiController.GetProducts("iPhone");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
        Assert.Equal(200, okResult.StatusCode);
  
    }

    [Fact]
    public async Task InvokeApis_POST_ReturnsOkResult()
    {
        // Arrange
        var mockApiResponse = new List<Tuple<HttpStatusCode, HttpContent>>
            {
                new (HttpStatusCode.OK, new StringContent(@"Test Response 1", Encoding.UTF8, "application/json")),
                new (HttpStatusCode.Created, new StringContent(@"Test Response 2", Encoding.UTF8, "application/json"))
            };

        var httpClient = httpClientMockHelper.GetHttpClientWithHttpMessageHandlerSequenceResponseMock(mockApiResponse);

        var frontEndApiController = new FrontEndApiController(httpClient);

        // Act
        var request = new { SearchText ="iPad"};
        var result = await frontEndApiController.GetServices(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
        Assert.Equal(200, okResult.StatusCode);

    }

}
