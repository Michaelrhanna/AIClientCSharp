using AIClient.Model;
using System;
using System.Collections.Generic;
using System.Text;
using static AIClient.Utils.Constants;

namespace AIClientTests
{
    public class Phi3FormatterTests
    {
        private readonly Phi3Formatter _formatter;

        public Phi3FormatterTests()
        {
            _formatter = new Phi3Formatter();
        }

        [Fact]
        public async Task FormatAsync_WrapsSystemMessage_WithCorrectTokens()
        {
            var messages = new List<ChatMessage>
            {
                new ChatMessage(MessageRole.System, "you are a helpful assistant")
            };

            var result = await _formatter.FormatAsync(messages);

            Assert.Contains("<|system|>", result);
            Assert.Contains("you are a helpful assistant", result);
            Assert.Contains("<|end|>", result);
        }

        [Fact]
        public async Task FormatAsync_WrapsUserMessage_WithCorrectTokens()
        {
            var messages = new List<ChatMessage>
            {
                new ChatMessage(MessageRole.User, "hello")
            };

            var result = await _formatter.FormatAsync(messages);

            Assert.Contains("<|user|>", result);
            Assert.Contains("hello", result);
            Assert.Contains("<|end|>", result);
        }

        [Fact]
        public async Task FormatAsync_WrapsAssistantMessage_WithCorrectTokens()
        {
            var messages = new List<ChatMessage>
            {
                new ChatMessage(MessageRole.Assistant, "hi there")
            };

            var result = await _formatter.FormatAsync(messages);

            Assert.Contains("<|assistant|>", result);
            Assert.Contains("hi there", result);
            Assert.Contains("<|end|>", result);
        }

        [Fact]
        public async Task FormatAsync_AppendsAssistantToken_AtEnd()
        {
            var messages = new List<ChatMessage>
            {
                new ChatMessage(MessageRole.User, "hello")
            };

            var result = await _formatter.FormatAsync(messages);

            Assert.EndsWith("<|assistant|>" + Environment.NewLine, result);
        }

        [Fact]
        public async Task FormatAsync_FormatsFullConversation_InCorrectOrder()
        {
            var messages = new List<ChatMessage>
            {
                new ChatMessage(MessageRole.System, "you are a helpful assistant"),
                new ChatMessage(MessageRole.User, "hello"),
                new ChatMessage(MessageRole.Assistant, "hi there"),
                new ChatMessage(MessageRole.User, "how are you?")
            };

            var result = await _formatter.FormatAsync(messages);

            var systemIndex = result.IndexOf("<|system|>");
            var userIndex = result.IndexOf("<|user|>");
            var assistantIndex = result.IndexOf("<|assistant|>");
            var secondUser = result.IndexOf("<|user|>", userIndex + 1);

            Assert.True(systemIndex < userIndex);
            Assert.True(userIndex < assistantIndex);
            Assert.True(assistantIndex < secondUser);
        }

        [Fact]
        public async Task FormatAsync_DoesNotIncludeSystemToken_WhenNoSystemMessage()
        {
            var messages = new List<ChatMessage>
            {
                new ChatMessage(MessageRole.User, "hello")
            };

            var result = await _formatter.FormatAsync(messages);

            Assert.DoesNotContain("<|system|>", result);
        }

        [Fact]
        public async Task GetStopTokensAsync_ReturnsUserAndEndTokens()
        {
            var stopTokens = await _formatter.GetStopTokensAsync();

            Assert.Contains("<|user|>", stopTokens);
            Assert.Contains("<|end|>", stopTokens);
        }

        [Fact]
        public async Task GetStopTokensAsync_ReturnsTwoTokens()
        {
            var stopTokens = await _formatter.GetStopTokensAsync();

            Assert.Equal(2, stopTokens.Count);
        }

        [Theory]
        [InlineData("<|system|>")]
        [InlineData("<|user|>")]
        [InlineData("<|end|>")]
        [InlineData("<|assistant|>")]
        public async Task FormatAsync_ContainsExpectedToken_ForFullConversation(string token)
        {
            var messages = new List<ChatMessage>
            {
                new ChatMessage(MessageRole.System, "you are a helpful assistant"),
                new ChatMessage(MessageRole.User, "hello"),
                new ChatMessage(MessageRole.Assistant, "hi there")
            };

            var result = await _formatter.FormatAsync(messages);

            Assert.Contains(token, result);
        }
    }
}
