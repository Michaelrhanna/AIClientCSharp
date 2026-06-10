using System.Text.Json;

namespace AIClient.Model
{
    public class SettingsService
    {
        private readonly string _settingsPath;
        private readonly AppSettings _appSettings;

        public SettingsService(AppSettings appSettings)
        {
            _appSettings = appSettings;
            _settingsPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
        }

        public void SaveSettings()
        {
            var json = JsonSerializer.Serialize(_appSettings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_settingsPath, json);
        }
    }
}
