namespace AIClient.Model
{
    public class AppSettings
    {
        public string ShimmyApiKey { get; set; } = string.Empty;
        public string ShimmyBaseUrl { get; set; } = string.Empty;
        public string ModelName { get; set; } = string.Empty;
        public int MaxTokens { get; set; }
        public int HttpTimeoutSeconds { get; set; } = 100;
    }
}
