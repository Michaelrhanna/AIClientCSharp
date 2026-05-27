using AIClient.Model.Interface;
using static AIClient.Utils.Constants;

namespace AIClient.Model
{
    public class ChatService : IChatService
    {
        private readonly IProvider _Provider;
        private readonly List<ChatMessage> _messages = new();

        public ChatService(IProvider provider)
        {
            _Provider = provider;
        }

        public async Task<string> SendAsync(string userInput)
        {
            _messages.Add(new ChatMessage(MessageRole.User, userInput));

            var response = await _Provider.SendAsync(_messages);

            _messages.Add(new ChatMessage(MessageRole.Assistant, response));
            return response;
        }

        public void ClearHistory() => _messages.RemoveAll(m => m.messageRole != MessageRole.System);
        public IReadOnlyList<ChatMessage> GetHistory() => _messages.AsReadOnly();

        public void AddSystemSetup(string systemSetup)
        {
            _messages.Clear();
            _messages.Insert(0, new ChatMessage(MessageRole.System, systemSetup));
        }

        public bool HasChatHistory() => _messages.Any(m => m.messageRole != MessageRole.System);

        public string GetSystemPrompt() => _messages.FirstOrDefault(m => m.messageRole == MessageRole.System)?.messageContent ?? string.Empty;
    }
}
