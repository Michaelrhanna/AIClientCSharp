using AIClient.Model;
using AIClient.Model.Interface;
using Moq;
using static AIClient.Utils.Constants;

namespace AIClientTests
{
    public class ChatServiceTests
    {
        private readonly Mock<IProvider> _mockProvider;
        private readonly ChatService _chatService;

        public ChatServiceTests()
        {
            _mockProvider = new Mock<IProvider>();
            _chatService = new ChatService(_mockProvider.Object);
        }

        [Fact]
        public async Task SendAsync_AddsUserMessageToHistory()
        {
            _mockProvider.Setup(p => p.SendAsync(It.IsAny<IList<ChatMessage>>()))
                         .ReturnsAsync("test reply");

            await _chatService.SendAsync("hello");

            var history = _chatService.GetHistory();
            Assert.Contains(history, m => m.messageRole == MessageRole.User && m.messageContent == "hello");
        }

        [Fact]
        public async Task SendAsync_AddsAssistantMessageToHistory()
        {
            _mockProvider.Setup(p => p.SendAsync(It.IsAny<IList<ChatMessage>>()))
                         .ReturnsAsync("test reply");

            await _chatService.SendAsync("hello");

            var history = _chatService.GetHistory();
            Assert.Contains(history, m => m.messageRole == MessageRole.Assistant && m.messageContent == "test reply");
        }

        [Fact]
        public async Task SendAsync_ReturnsProviderResponse()
        {
            _mockProvider.Setup(p => p.SendAsync(It.IsAny<IList<ChatMessage>>()))
                         .ReturnsAsync("this is the reply");

            var result = await _chatService.SendAsync("hello");

            Assert.Equal("this is the reply", result);
        }

        [Fact]
        public async Task SendAsync_ThrowsWhenProviderThrows()
        {
            _mockProvider.Setup(p => p.SendAsync(It.IsAny<IList<ChatMessage>>()))
                         .ThrowsAsync(new Exception("provider error"));

            await Assert.ThrowsAsync<Exception>(() => _chatService.SendAsync("hello"));
        }

        [Fact]
        public void ClearHistory_RemovesAllMessages()
        {
            _chatService.AddSystemSetup("you are a helpful assistant");
            _chatService.ClearHistory();

            var history = _chatService.GetHistory();
            Assert.Single(history);
            Assert.Equal(MessageRole.System, history[0].messageRole);
        }

        [Fact]
        public async Task ClearHistory_PreservesSystemMessage_WhenConversationExists()
        {
            _mockProvider.Setup(p => p.SendAsync(It.IsAny<IList<ChatMessage>>()))
                         .ReturnsAsync("reply");

            _chatService.AddSystemSetup("you are a helpful assistant");
            await _chatService.SendAsync("hello");
            _chatService.ClearHistory();

            var history = _chatService.GetHistory();
            Assert.Single(history);
            Assert.Equal(MessageRole.System, history[0].messageRole);
        }

        [Fact]
        public void AddSystemSetup_InsertsSystemMessageAtIndexZero()
        {
            _chatService.AddSystemSetup("you are a helpful assistant");

            var history = _chatService.GetHistory();
            Assert.Equal(MessageRole.System, history[0].messageRole);
            Assert.Equal("you are a helpful assistant", history[0].messageContent);
        }

        [Fact]
        public void AddSystemSetup_ClearsExistingHistory()
        {
            _mockProvider.Setup(p => p.SendAsync(It.IsAny<IList<ChatMessage>>()))
                         .ReturnsAsync("reply");

            _chatService.AddSystemSetup("first setup");
            _chatService.AddSystemSetup("second setup");

            var history = _chatService.GetHistory();
            Assert.Single(history);
            Assert.Equal("second setup", history[0].messageContent);
        }

        [Fact]
        public void HasChatHistory_ReturnsFalse_WhenEmpty()
        {
            Assert.False(_chatService.HasChatHistory());
        }

        [Fact]
        public void HasChatHistory_ReturnsFalse_WhenOnlySystemMessage()
        {
            _chatService.AddSystemSetup("you are a helpful assistant");

            Assert.False(_chatService.HasChatHistory());
        }

        [Fact]
        public async Task HasChatHistory_ReturnsTrue_WhenUserMessageExists()
        {
            _mockProvider.Setup(p => p.SendAsync(It.IsAny<IList<ChatMessage>>()))
                         .ReturnsAsync("reply");

            await _chatService.SendAsync("hello");

            Assert.True(_chatService.HasChatHistory());
        }

        [Fact]
        public void GetSystemPrompt_ReturnsEmpty_WhenNoSystemMessage()
        {
            Assert.Equal(string.Empty, _chatService.GetSystemPrompt());
        }

        [Fact]
        public void GetSystemPrompt_ReturnsSystemMessage()
        {
            _chatService.AddSystemSetup("you are a helpful assistant");

            Assert.Equal("you are a helpful assistant", _chatService.GetSystemPrompt());
        }

        
    }
}
