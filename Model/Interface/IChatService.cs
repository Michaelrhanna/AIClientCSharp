using System;
using System.Collections.Generic;
using System.Text;

namespace AIClient.Model.Interface
{
    internal interface IChatService
    {
        SendAsync(IList<ChatMessage> messages);
    }
}
