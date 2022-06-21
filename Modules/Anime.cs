using System.Diagnostics.CodeAnalysis;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus;

namespace VoidBot.Modules
{
    [Description("Anime Module"), Group("anime")]
    public class AnimeModule : DSharpPlus.CommandsNext.BaseCommandModule
    {
        [Description("Get info about anime"), Command("info"), Cooldown(1, 5, CooldownBucketType.User)]
        async Task AnimeInfo(CommandContext ctx, [Description("Anime name"), RemainingText, NotNull] string anime)
        {
            var wait = await Utils.ChatUtils.SendWait(ctx);

            try {
                var animes = await Utils.AnimeInfo.FindAnimesByName(anime, 1);
                if (animes.Count == 0)
                    await Utils.ChatUtils.SendError(ctx, Localization.Current?.NoResults!);
                else
                    await wait.ModifyAsync(await Utils.AnimeInfo.AnimeToEmbed(animes[0]!));
            } catch (Exception e) {
                await Utils.ChatUtils.SendError(ctx, wait, e.StackTrace?.ToString() ?? e.Message);
            }
        }

        [Description("Get info about anime using id"), Command("info_id"), Cooldown(1, 5, CooldownBucketType.User)]
        async Task AnimeInfoById(CommandContext ctx, [Description("Anime id")] int animeId)
        {
            var wait = await Utils.ChatUtils.SendWait(ctx);

            try {
                var anime = await Utils.AnimeInfo.GetAnime(animeId);
                if (anime == null)
                    await Utils.ChatUtils.SendError(ctx, Localization.Current?.NoResults!);
                else
                    await wait.ModifyAsync(await Utils.AnimeInfo.AnimeToEmbed(anime));
            } catch (Exception e) {
                await Utils.ChatUtils.SendError(ctx, wait, e.StackTrace?.ToString() ?? e.Message);
            }
        }

        [Description("Search for anime"), Command("search"), Cooldown(1, 5, CooldownBucketType.User)]
        async Task AnimeSearch(CommandContext ctx, 
            [Description("Anime count to output")] int num,
            [RemainingText, Description("Anime name")] string? animeName)
        {
            var wait = await Utils.ChatUtils.SendWait(ctx);

            try {
                var animes = await Utils.AnimeInfo.FindAnimesByName(animeName!, num);
                if (animes.Count == 0)
                    await Utils.ChatUtils.SendError(ctx, Localization.Current?.NoResults!);
                else
                    await wait.ModifyAsync(await Utils.AnimeInfo.AnimeListToEmbed(animes!));
            } catch (Exception e) {
                await Utils.ChatUtils.SendError(ctx, wait, e.StackTrace?.ToString() ?? e.Message);
            }
        }
    }
}