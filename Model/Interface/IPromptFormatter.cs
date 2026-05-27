using System;
using System.Collections.Generic;
using System.Text;

namespace AIClient.Model.Interface
{
    internal interface IPromptFormatter
    {
        Task<string> FormatAsync(IList<ChatMessage> messages);
    }
}
