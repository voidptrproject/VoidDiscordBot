using System.Diagnostics.CodeAnalysis;
using DSharpPlus.SlashCommands;

namespace VoidBot.Modules
{
    public class AnimeModule : ApplicationCommandModule
    {
        [SlashCommand("info", "Get info about anime")]
        async Task AnimeInfo(InteractionContext ctx, [Option("animename", "Name of anime")] string anime)
        {
            try {
                var animes = await Utils.AnimeInfo.FindAnimesByName(anime, 1);
                if (animes.Count == 0)
                    await Utils.ChatUtils.SendError(ctx, Localization.Current?.NoResults!);
                else
                    await ctx.CreateResponseAsync(await Utils.AnimeInfo.AnimeToEmbed(animes[0]!));
            } catch (Exception e) {
                await Utils.ChatUtils.SendError(ctx, e.StackTrace?.ToString() ?? e.Message);
            }
        }

        [SlashCommand("info_id", "Get info about anime using id")]
        async Task AnimeInfoById(InteractionContext ctx, [Option("animeid", "Id of anime")] long animeId)
        {
            try {
                var anime = await Utils.AnimeInfo.GetAnime(animeId);
                if (anime == null)
                    await Utils.ChatUtils.SendError(ctx, Localization.Current?.NoResults!);
                else
                    await ctx.CreateResponseAsync(await Utils.AnimeInfo.AnimeToEmbed(anime));
            } catch (Exception e) {
                await Utils.ChatUtils.SendError(ctx, e.StackTrace?.ToString() ?? e.Message);
            }
        }

        [SlashCommand("search", "Search for anime")]
        async Task AnimeSearch(InteractionContext ctx, 
            [Option("count", "Number of anime to output")] long num,
            [Option("animename", "Name of anime")] string animeName)
        {
            await ctx.CreateResponseAsync(DSharpPlus.InteractionResponseType.DeferredChannelMessageWithSource);

            try {
                var animes = await Utils.AnimeInfo.FindAnimesByName(animeName!, num);
                if (animes.Count == 0)
                    await Utils.ChatUtils.SendError(ctx, Localization.Current?.NoResults!, true);
                else
                    await ctx.EditResponseAsync(
                        new DSharpPlus.Entities.DiscordWebhookBuilder().AddEmbed(await Utils.AnimeInfo.AnimeListToEmbed(animes!))
                    );
            } catch (Exception e) {
                await Utils.ChatUtils.SendError(ctx, e.StackTrace?.ToString() ?? e.Message, true);
            }
        }
    }
}