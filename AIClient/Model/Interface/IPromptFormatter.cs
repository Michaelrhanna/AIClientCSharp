namespace AIClient.Model.Interface
{
    public interface IPromptFormatter
    {
        Task<string> FormatAsync(IList<ChatMessage> messages);
        Task<List<string>> GetStopTokensAsync();
    }
}
