using Moq.Protected;
using Moq;
using System.Net;

namespace FrontEndWebAPI.UnitTests.Helpers;

public class HttpClientMockHelper
{
    public HttpClient GetHttpClientWithHttpMessageHandlerSequenceResponseMock(List<Tuple<HttpStatusCode, HttpContent>> returns)
    {

        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        var handlerPart = handlerMock
           .Protected()
           // Setup the PROTECTED method to mock
           .SetupSequence<Task<HttpResponseMessage>>(
              "SendAsync",
              ItExpr.IsAny<HttpRequestMessage>(),
              ItExpr.IsAny<CancellationToken>()
           );

        foreach (var item in returns)
        {
            handlerPart = AddReturnPart(handlerPart, item.Item1, item.Item2);
        }
        handlerMock.Verify();
        // use real http client with mocked handler here
        var httpClient = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com/"),
        };
        return httpClient;
    }

    private Moq.Language.ISetupSequentialResult<Task<HttpResponseMessage>> AddReturnPart(Moq.Language.ISetupSequentialResult<Task<HttpResponseMessage>> handlerPart,
        HttpStatusCode statusCode, HttpContent content)
    {
        return handlerPart

           // prepare the expected response of the mocked http call
           .ReturnsAsync(new HttpResponseMessage()
           {
               StatusCode = statusCode, 
               Content = content 
           });
    }
}
