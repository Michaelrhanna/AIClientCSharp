    
using static AIClient.Utils.Constants;

namespace AIClient.Model
{
    public class ChatMessage
    {
        public MessageRole messageRole { get; set; }
        public string messageContent { get; set; } = string.Empty;

        public ChatMessage(MessageRole role, string content)
        {
            messageRole = role;
            messageContent = content;
        }
    }
}