using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration.Internal;
using System.Text;
using static AIClient.Utils.Constants;

namespace AIClient.Model
{
    internal class APIKeyManager
    {
        private AppSettings _appSettings;

        APIKeyManager(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        private async Task<string> GetKeyAsync(Provider provider)
        {
            // In a real implementation, you might want to fetch the API key from a secure vault or environment variable.
            // In multiple provider scenarios, you could have a switch statement or a dictionary to manage different keys for different providers.
            /*
            switch (provider)
            {
                case Provider.OpenAI:
                    return GetOpenAIKey();
                case Provider.AzureOpenAI:
                    return GetAzureOpenAIKey();
                case Provider.Shimmy:
                    return GetShimmyKey();
                default:
                    throw new ArgumentException("Unsupported provider");
            }
            */
            //for now we just return the Shimmy key since that's the only provider we have
            return _appSettings.ShimmyApiKey.Trim();
        }

        public async Task InjectHeaderAsync(HttpClient client, Provider provider)  // adds Authorization header
        {             
            var apiKey = await GetKeyAsync(provider);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
        }

    }
}
