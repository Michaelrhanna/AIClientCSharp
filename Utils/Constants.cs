using System;
using System.Collections.Generic;
using System.Text;

namespace AIClient.Utils
{
    internal class Constants
    {
        enum Provider
        {
            OpenAI,
            AzureOpenAI,
            Shimmy
        }
        enum Model
        {
            GPT35Turbo,
            GPT4,
            PHI3
        }
        enum MessageRole
        {
            System,
            User,
            Assistant
        }
    }
}
