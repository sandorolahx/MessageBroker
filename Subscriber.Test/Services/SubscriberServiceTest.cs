using Moq;
using Moq.Protected;
using Subscriber.Services;
using System.Net;

namespace Subscriber.Test.Services
{
    public class SubscriberServiceTest
    {
        [Fact]
        public async void Should_Get_Topic_By_Id()
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{ ""id"": 123, ""name"": ""Test Topic""}"),
            };

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var httpClient = new HttpClient(handlerMock.Object);

            var subscriberService = new SubscriberService(httpClient);
            var testTopic = await subscriberService.GetTopicById(1);

            Assert.NotNull(testTopic);
            Assert.Equal(123, testTopic.Id);
            Assert.Equal("Test Topic", testTopic.Name);
        }
    }
}
