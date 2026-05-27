using System;
using System.Collections.Generic;
using System.Text;

namespace AIClient.Utils
{
    public static class Constants
    {
        public enum Provider
        {
            OpenAI,
            AzureOpenAI,
            Shimmy
        }
        public enum Model
        {
            GPT35Turbo,
            GPT4,
            PHI3
        }
        public enum MessageRole
        {
            System,
            User,
            Assistant
        }
    }
}
