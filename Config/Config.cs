using Newtonsoft.Json;

namespace VoidBot
{
    public class Config
    {
        public string? TokenEnvermomentName { get; set; }
        public string? Localization { get; set; }
        public string[]? Prefixes { get; set; }

        public static Config? Current { get; private set; }
        public static EventHandler<Config>? OnConfigChanged;

        public static async Task Initialize()
        {
            Current = JsonConvert.DeserializeObject<Config>(await File.ReadAllTextAsync("Config\\config.json"));

            var watcher = new FileSystemWatcher("Config");
            watcher.Filter = "config.json";
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;
            watcher.Changed += async (sender, e) =>
            {
                Current = JsonConvert.DeserializeObject<Config>(await File.ReadAllTextAsync("Config\\config.json"));
                OnConfigChanged?.Invoke(null, Current!);
            };
        }
    }
}