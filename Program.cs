using DSharpPlus.CommandsNext;
using DSharpPlus.VoiceNext;

async Task Main()
{
    await VoidBot.Config.Initialize();
    await VoidBot.Localization.Initialize();

    var token = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows)
        ? Environment.GetEnvironmentVariable(VoidBot.Config.Current!.TokenEnvermomentName!, EnvironmentVariableTarget.User) 
        : Environment.GetEnvironmentVariable(VoidBot.Config.Current!.TokenEnvermomentName!);

    if (token == null)
    {
        Console.WriteLine($"{VoidBot.Config.Current!.TokenEnvermomentName!} environment variable is not set");
        return;
    }

    var client = new DSharpPlus.DiscordClient(new DSharpPlus.DiscordConfiguration
    {
        Token = token,
        TokenType = DSharpPlus.TokenType.Bot,
        AutoReconnect = true
    });

    client.UseVoiceNext();

    var commands = client.UseCommandsNext(new CommandsNextConfiguration
    {
        StringPrefixes = VoidBot.Config.Current!.Prefixes!
    });

    commands.RegisterCommands<VoidBot.Modules.AnimeModule>();
    commands.RegisterCommands<VoidBot.Modules.MusicModule>();

    await client.ConnectAsync();

    await Task.Delay(Timeout.Infinite);
}

Main().GetAwaiter().GetResult();