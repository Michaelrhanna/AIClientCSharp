using System;
using System.Collections.Generic;
using System.Text;

namespace AIClient.Model.Interface
{
    public interface IChatService
    {
        Task<string> SendAsync(string userInput);
        void ClearHistory();
        IReadOnlyList<ChatMessage> GetHistory();
    }
}
