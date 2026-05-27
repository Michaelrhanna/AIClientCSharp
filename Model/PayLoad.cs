using System.Text.Json.Serialization;

namespace AIClient.Model
{
    internal class PayLoad
    {
        [JsonPropertyName("model")]
        public string Model { get; set; } = string.Empty;
        [JsonPropertyName("prompt")]
        public string Prompt { get; set; } = string.Empty;
        [JsonPropertyName("max_tokens")]
        public int MaxTokens { get; set; }
        [JsonPropertyName("stream")]
        public bool Stream { get; set; }
        [JsonPropertyName("stop")]
        public List<string> StopTokens { get; set; } = new List<string>();
    }
}
