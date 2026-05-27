using System;
using System.Collections.Generic;
using System.Text;

namespace AIClient.Model.Interface
{
    public interface IPromptFormatter
    {
        Task<string> FormatAsync(IList<ChatMessage> messages);
        Task<List<string>> GetStopTokensAsync();
    }
}
