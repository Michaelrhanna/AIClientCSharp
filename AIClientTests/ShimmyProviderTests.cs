using AIClient.Model;
using AIClient.Model.Interface;
using Moq;
using System.Net;
using System.Text.Json;
using static AIClient.Utils.Constants;

namespace AIClientTests
{
    // Helper to intercept HttpClient calls
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage _response;

        public MockHttpMessageHandler(HttpResponseMessage response)
        {
            _response = response;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_response);
        }
    }

    public class ShimmyProviderTests
    {

        private readonly Mock<IPromptFormatter> _mockFormatter;
        private readonly AppSettings _appSettings;

        public ShimmyProviderTests()
        {
            _mockFormatter = new Mock<IPromptFormatter>();
            _mockFormatter.Setup(f => f.FormatAsync(It.IsAny<IList<ChatMessage>>()))
                          .ReturnsAsync("formatted prompt");
            _mockFormatter.Setup(f => f.GetStopTokensAsync())
                          .ReturnsAsync(new List<string> { "<|user|>", "<|end|>" });

            _appSettings = new AppSettings
            {
                ShimmyApiKey = "test-key",
                ShimmyBaseUrl = "http://localhost:8000/api/generate",
                ModelName = "test-model",
                MaxTokens = 512
            };
        }

        private ShimmyProvider BuildProvider(HttpResponseMessage httpResponse)
        {
            var httpClient = new HttpClient(new MockHttpMessageHandler(httpResponse))
            {
                BaseAddress = new Uri("http://localhost:8000")
            };

            var apiKeyManager = new APIKeyManager(_appSettings);

            return new ShimmyProvider(_mockFormatter.Object, _appSettings, httpClient, apiKeyManager);
        }

        [Fact]
        public async Task SendAsync_ReturnsResponse_WhenSuccessful()
        {
            var shimMyResponse = new ShimmyResponse { Response = "  hello from shimmy  " };
            var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(shimMyResponse))
            };

            var provider = BuildProvider(httpResponse);
            var messages = new List<ChatMessage>
            {
                new ChatMessage(MessageRole.User, "hello")
            };

            var result = await provider.SendAsync(messages);

            Assert.Equal("hello from shimmy", result); 
        }

        [Fact]
        public async Task SendAsync_ThrowsException_WhenResponseIsNotSuccess()
        {
            var httpResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("something went wrong")
            };

            var provider = BuildProvider(httpResponse);
            var messages = new List<ChatMessage>
            {
                new ChatMessage(MessageRole.User, "hello")
            };

            var ex = await Assert.ThrowsAsync<Exception>(() => provider.SendAsync(messages));
            Assert.Contains("something went wrong", ex.Message);
        }

        [Fact]
        public async Task SendAsync_ThrowsException_WhenResponseIsUnauthorized()
        {
            var httpResponse = new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                Content = new StringContent("unauthorized")
            };

            var provider = BuildProvider(httpResponse);
            var messages = new List<ChatMessage>
            {
                new ChatMessage(MessageRole.User, "hello")
            };

            await Assert.ThrowsAsync<Exception>(() => provider.SendAsync(messages));
        }

        [Fact]
        public async Task SendAsync_CallsFormatterWithMessages()
        {
            var shimMyResponse = new ShimmyResponse { Response = "reply" };
            var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(shimMyResponse))
            };

            var provider = BuildProvider(httpResponse);
            var messages = new List<ChatMessage>
            {
                new ChatMessage(MessageRole.User, "hello")
            };

            await provider.SendAsync(messages);

            _mockFormatter.Verify(f => f.FormatAsync(messages), Times.Once);
        }

        [Fact]
        public async Task SendAsync_ReturnsEmpty_WhenResponseFieldIsEmpty()
        {
            var shimMyResponse = new ShimmyResponse { Response = "   " };
            var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(shimMyResponse))
            };

            var provider = BuildProvider(httpResponse);
            var messages = new List<ChatMessage>
            {
                new ChatMessage(MessageRole.User, "hello")
            };

            var result = await provider.SendAsync(messages);

            Assert.Equal(string.Empty, result);
        }

    }
}
