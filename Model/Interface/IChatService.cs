namespace AIClient.Model.Interface
{
    public interface IChatService
    {
        Task<string> SendAsync(string userInput);
        void ClearHistory();
        IReadOnlyList<ChatMessage> GetHistory();
        void AddSystemSetup(string systemSetup);
        bool HasChatHistory();
    }
}
