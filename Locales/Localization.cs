using Newtonsoft.Json;

namespace VoidBot
{
    public class Localization
    {
        public struct AnimeInfo
        {
            public string? Score { get; set; }
            public string? Status { get; set; }
            public string? Genres { get; set; }
        }

        public string? SearchResults { get; set; }
        public AnimeInfo? AnimeInfoLocales { get; set; }
        public string? Error { get; set; }
        public string? Wait { get; set; }
        public string? NoResults { get; set; }

        public static Localization? Current { get; private set; }
        public static string? CurrentLocalization { get; private set; }

        public static async Task Initialize()
        {
            CurrentLocalization = Config.Current!.Localization;
            Current = JsonConvert.DeserializeObject<Localization>(await File.ReadAllTextAsync($"Locales\\{Config.Current?.Localization}.json"));

            var watcher = new FileSystemWatcher("Locales");
            watcher.Filter = $"{Config.Current?.Localization}";
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;
            watcher.Changed += async (sender, e) =>
            {
                Current = JsonConvert.DeserializeObject<Localization>(await File.ReadAllTextAsync($"Locales\\{Config.Current?.Localization}.json"));
            };

            Config.OnConfigChanged += async (sender, config) =>
            {
                CurrentLocalization = config.Localization;
                Current = JsonConvert.DeserializeObject<Localization>(await File.ReadAllTextAsync($"Locales\\{config.Localization}.json"));
            };
        }
    }
}