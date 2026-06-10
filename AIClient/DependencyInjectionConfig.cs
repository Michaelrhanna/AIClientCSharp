using AIClient.Model;
using AIClient.Model.Interface;
using AIClient.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Whisper.net;

namespace AIClient
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddCoreServices (this IServiceCollection services)
        {
            // Register core services here (e.g., APIKeyManager, IChatService, etc.)
            services.AddSingleton<APIKeyManager>();
            services.AddSingleton<IPromptFormatter, Phi3Formatter>();
            services.AddSingleton<IChatService, ChatService>();
            return services;
        }

        public static IServiceCollection AddForms (this IServiceCollection services)
        {
            // Register your forms here
            services.AddTransient<FormMain>();
            services.AddTransient<FormSetup>();
            services.AddSingleton<Func<FormSetup>>(sp => () => sp.GetRequiredService<FormSetup>());
            return services;
        }

        public static IServiceCollection AddWhisperServices(this IServiceCollection services, string modelPath)
        {
            // 1. Register the factory as a Singleton so the model is only loaded ONCE
            services.AddSingleton<WhisperFactory>(sp =>
            {
                // Ensure the path to your ggml model file is correct (e.g., "ggml-base.bin")
                if (!File.Exists(modelPath))
                {
                    throw new FileNotFoundException($"Whisper model file not found at: {modelPath}");
                }
                return WhisperFactory.FromPath(modelPath);
            });

            // 2. Register your live streaming service
            services.AddTransient<WhisperListener>();

            return services;
        }
    }
}
