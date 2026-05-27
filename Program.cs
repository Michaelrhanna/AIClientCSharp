using AIClient.Model;
using AIClient.Model.Interface;
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

            // Core services
            services.AddSingleton<APIKeyManager>();
            services.AddSingleton<IPromptFormatter, Phi3Formatter>();       
            services.AddSingleton<IChatService, ChatService>();

            // HttpClient for providers
            services.AddHttpClient<IProvider, ShimmyProvider> ();   

            // Forms
            services.AddTransient<FormMain>();
            services.AddTransient<FormSetup>();
            services.AddSingleton<Func<FormSetup>>(sp => () => sp.GetRequiredService<FormSetup>());

        }
    }
}