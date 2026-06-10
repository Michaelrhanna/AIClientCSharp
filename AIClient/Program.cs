using AIClient.Model;
using AIClient.Model.Interface;
using AIClient.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace AIClient
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            ApplicationConfiguration.Initialize();

            var services = new ServiceCollection();
            ConfigureServices(services);

            using var serviceProvider = services.BuildServiceProvider();

            var mainForm = serviceProvider.GetRequiredService<FormMain>();
            Application.Run(mainForm);
        }

        static void ConfigureServices(IServiceCollection services)
        {
            // Config
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
            
            var appSettings = config.Get<AppSettings>()!;
            services.AddSingleton(appSettings);
            services.AddSingleton<SettingsService>();

            // Core services
            services.AddCoreServices();

            // HttpClient for providers
            services.AddHttpClient<IProvider, ShimmyProvider>((client) => { client.Timeout = TimeSpan.FromSeconds(appSettings.HttpTimeoutSeconds); });   

            // Forms
            services.AddForms();

            // TTSSpeaker
            services.AddSingleton<TTSSpeaker>();

            string modelPath = Path.Combine(AppContext.BaseDirectory, "WhisperModels\\base\\", "ggml-base.en.bin");
            services.AddWhisperServices(modelPath);
        }
    }
}