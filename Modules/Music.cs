using System.Diagnostics.CodeAnalysis;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus;
using DSharpPlus.VoiceNext;

// namespace VoidBot.Modules
// {
//     [Description("Music Module"), Group("music")]
//     public class MusicModule : BaseCommandModule
//     {
//         [Command("play"), Description("Play music")]
//         public async Task Play(CommandContext ctx, [Description("Music url")] Uri url)
//         {
//             if (Music.Queue.Instance == null)
//                 Music.Queue.Instance = new Music.Queue(ctx.Client);

//             if (ctx.Member == null) 
//             {
//                 await Utils.ChatUtils.SendError(ctx, "This command can only be used by members");
//                 return;
//             }

//             if (ctx.Member?.VoiceState == null)
//             {
//                 await Utils.ChatUtils.SendError(ctx, "You must be in a voice channel to use this command");
//                 return;
//             }

//             Music.Queue.Instance.Add(ctx.Member?.VoiceState?.Channel!, new Music.Song 
//             { 
//                 Title = await Utils.Audio.GetSongTitleAsync(url.AbsoluteUri),
//                 Url = url.ToString()
//             });

//             await Utils.ChatUtils.SendMessage(ctx, $"{await Utils.Audio.GetSongTitleAsync(url.AbsoluteUri)} added to queue!");
//         }
//     }
// }