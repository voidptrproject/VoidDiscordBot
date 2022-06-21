using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus;
using DSharpPlus.Entities;

namespace VoidBot.Utils
{
    public static class ChatUtils
    {
        public static async Task SendError(CommandContext ctx, string error)
        {
            await ctx.RespondAsync(new DiscordEmbedBuilder().WithTitle(Localization.Current?.Error!).WithDescription(error).WithColor(DiscordColor.Rose));
        }

        public static async Task SendError(CommandContext ctx, DiscordMessage msg, string error)
        {
            await msg.ModifyAsync(new DiscordEmbedBuilder().WithTitle(Localization.Current?.Error!).WithDescription(error)
                .WithColor(DiscordColor.Rose).Build());
        }

        public static async Task<DiscordMessage> SendWait(CommandContext ctx)
        {
            return await ctx.RespondAsync(new DiscordEmbedBuilder().WithTitle(Localization.Current?.Wait!).WithColor(DiscordColor.Azure));
        }

        public static async Task SendMessage(CommandContext ctx, string message)
        {
            await ctx.RespondAsync(new DiscordEmbedBuilder().WithDescription(message).WithColor(DiscordColor.Gold));
        }
    }
}