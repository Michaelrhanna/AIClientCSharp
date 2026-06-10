namespace AIClient.Model
{
    public class AppSettings
    {
        public string ShimmyApiKey { get; set; } = string.Empty;
        public string ShimmyBaseUrl { get; set; } = string.Empty;
        public string ModelName { get; set; } = string.Empty;
        public int MaxTokens { get; set; }
        public int HttpTimeoutSeconds { get; set; } = 100;
        public string TTSVoice { get; set; } = "Microsoft Zira";
        public string SystemMessage { get; set; } = string.Empty;
        public bool UseTTS { get; set; } = true;
    }
}
