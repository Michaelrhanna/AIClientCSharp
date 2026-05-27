using AIClient.Model.Interface;
using System.Text;
using System.Text.Json;
using static AIClient.Utils.Constants;

namespace AIClient.Model
{
    public class ShimmyProvider : IProvider
    {
        private readonly IPromptFormatter _promptFormatter;
        private readonly AppSettings _appSettings;
        private readonly HttpClient _httpClient;
        private readonly APIKeyManager _apiKeyManager;

        public ShimmyProvider(IPromptFormatter promptFormatter, AppSettings appSettings, HttpClient httpClient, APIKeyManager apiKeyManager)
        {
            _promptFormatter = promptFormatter;
            _appSettings = appSettings;
            _httpClient = httpClient;
            _apiKeyManager = apiKeyManager;
            _apiKeyManager.InjectHeader(_httpClient, Provider.Shimmy);
        }


        public async Task<string> SendAsync(IList<ChatMessage> messages)
        {
            // Format the prompt using the provided formatter
            string formattedPrompt = await _promptFormatter.FormatAsync(messages);
            PayLoad payLoad = new PayLoad
            {
                Model = _appSettings.ModelName,
                Prompt = formattedPrompt,
                MaxTokens = _appSettings.MaxTokens,
                Stream = false,
                StopTokens = await _promptFormatter.GetStopTokensAsync()
            };

            var response = await _httpClient.PostAsync(_appSettings.ShimmyBaseUrl, new StringContent(JsonSerializer.Serialize(payLoad), Encoding.UTF8, "application/json"));
            (bool result, string resultMessage) =  await CheckResponse(response);

            if(result == true)
                return resultMessage;

            throw new Exception($"Error from Shimmy API: {resultMessage}");
        }

        private async Task<(bool, string)> CheckResponse(HttpResponseMessage response)
        {
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<ShimmyResponse>(body);
                return (true, result?.Response.Trim() ?? string.Empty);
            }
            else
                return (false, body);
        }
    }
}
