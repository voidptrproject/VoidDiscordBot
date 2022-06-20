using System.Diagnostics.CodeAnalysis;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus;
using DSharpPlus.VoiceNext;

namespace VoidBot.Modules
{
    [Description("Music Module"), Group("music")]
    public class MusicModule : BaseCommandModule
    {
        [Command("play"), Description("Play music")]
        public async Task Play(CommandContext ctx, [Description("Music url")] Uri url)
        {
            var vnext = ctx.Client.GetVoiceNext();
            var connection = vnext.GetConnection(ctx.Guild);
            if (connection == null)
                connection = await vnext.ConnectAsync(ctx.Member?.VoiceState?.Channel);

            var transmit = connection.GetTransmitSink();

            var pcm = Utils.Audio.ConvertAudioToPcm(url.AbsoluteUri);
            await pcm.CopyToAsync(transmit);
            await pcm.DisposeAsync();
        }
    }
}