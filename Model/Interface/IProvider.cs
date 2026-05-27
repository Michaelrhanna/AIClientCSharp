using System;
using System.Collections.Generic;
using System.Text;

namespace AIClient.Model.Interface
{
    public interface IProvider
    {
        Task<string> SendAsync(IList<ChatMessage> messages);
    }
}
