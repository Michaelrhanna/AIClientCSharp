namespace AIClient.Model.Interface
{
    public interface IProvider
    {
        Task<string> SendAsync(IList<ChatMessage> messages);
    }
}
