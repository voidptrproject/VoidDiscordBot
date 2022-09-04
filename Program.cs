using DSharpPlus.CommandsNext;
using DSharpPlus.VoiceNext;
using DSharpPlus.SlashCommands;

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

    var commands = client.UseSlashCommands();

    commands.RegisterCommands<VoidBot.Modules.AnimeModule>();

    // TODO: FIX THIS
    //commands.RegisterCommands<VoidBot.Modules.MusicModule>();

    commands.SlashCommandErrored += async (cmd, e) =>
    {
        if (cmd == null)
            await VoidBot.Utils.ChatUtils.SendError(e.Context, e.Exception.Message);
        else
            await VoidBot.Utils.ChatUtils.SendError(e.Context, $"{e.Context.CommandName} - {e.Exception.Message}\n{e.Exception.StackTrace}");
    };

    await client.ConnectAsync();

    await Task.Delay(Timeout.Infinite);
}

Main().GetAwaiter().GetResult();