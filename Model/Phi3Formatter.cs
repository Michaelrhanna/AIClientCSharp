using AIClient.Model.Interface;
using static AIClient.Utils.Constants;

namespace AIClient.Model
{
    internal class Phi3Formatter : IPromptFormatter
    {
        private readonly string System_Start_Token = "<|system|>";
        private readonly string User_Start_Token = "<|user|>";
        private readonly string Assistant_Start_Token = "<|assistant|>";
        private readonly string End_Token = "<|end|>";

        public async Task<IList<string>> GetStopTokens()
        {
            IList<string> stopTokens = new List<string>
            {
                User_Start_Token,
                End_Token
            };
            return stopTokens;
        }

        public async Task<string> FormatAsync(IList<ChatMessage> messages)
        {
            string prompt = BuildPrompt(messages).Aggregate((current, next) => current + next);
            prompt += Assistant_Start_Token + Environment.NewLine;
            return prompt;
        }

        private IEnumerable<string> BuildPrompt(IList<ChatMessage> messages)
        {
            foreach (var message in messages)
            {
                switch (message.messageRole)
                {
                    case MessageRole.System:
                        yield return System_Start_Token + Environment.NewLine;
                        yield return message.messageContent + Environment.NewLine;
                        yield return End_Token + Environment.NewLine;
                        break;
                    case MessageRole.User:
                        yield return User_Start_Token + Environment.NewLine;
                        yield return message.messageContent + Environment.NewLine;
                        yield return End_Token + Environment.NewLine;
                        break;
                    case MessageRole.Assistant:
                        yield return Assistant_Start_Token + Environment.NewLine;
                        yield return message.messageContent + Environment.NewLine;
                        yield return End_Token + Environment.NewLine;
                        break;
                }
            }
        }
    }
}
