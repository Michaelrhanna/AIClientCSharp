using System.Text.Json.Serialization;

namespace AIClient.Model
{
    public class ShimmyResponse
    {
        [JsonPropertyName("response")]
        public string Response { get; set; } = string.Empty;
    }
}
