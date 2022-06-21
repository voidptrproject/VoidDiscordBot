
using System.Diagnostics;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using Xabe.FFmpeg;
using Xabe.FFmpeg.Streams;

namespace VoidBot.Utils
{
    public static class Audio
    {
        public static Stream GetStream(string Url)
        {
            var args = $"/C youtube-dl -o - {Url}" + " | " +
                        $"ffmpeg -err_detect ignore_err -i pipe:0 -ac 2 -f s16le -ar 48000 pipe:1";
            var result = Process.Start(new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = args,
                RedirectStandardOutput = true,
                RedirectStandardError = false,
                UseShellExecute = false
            });

            return result.StandardOutput.BaseStream ?? throw new Exception("Failed to get audio stream"); 
        }

        public static async Task<string> GetSongTitleAsync(string Url)
        {
            var youtube = new YoutubeClient();
            var video = await youtube.Videos.GetAsync(Url);
            return video.Title;
        }
    }
}