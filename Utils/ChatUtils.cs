using DSharpPlus.SlashCommands;
using DSharpPlus.Entities;

namespace VoidBot.Utils
{
    public static class ChatUtils
    {
        public static async Task SendError(InteractionContext ctx, string error, bool edit=false)
        {
            if (!edit)
                await ctx.CreateResponseAsync(
                    new DiscordEmbedBuilder().WithTitle(Localization.Current?.Error!).WithDescription(error).WithColor(DiscordColor.Rose)
                );
            else
                await ctx.EditResponseAsync( new DiscordWebhookBuilder().AddEmbed(
                    new DiscordEmbedBuilder().WithTitle(Localization.Current?.Error!).WithDescription(error).WithColor(DiscordColor.Rose)
                ));
        }

        public static async Task SendMessage(InteractionContext ctx, string message)
        {
            await ctx.CreateResponseAsync(
                new DiscordEmbedBuilder().WithDescription(message).WithColor(DiscordColor.Gold)
            );
        }
    }
}