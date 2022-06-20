
using System.Diagnostics;

namespace VoidBot.Utils
{
    public static class Audio
    {
        public static Stream ConvertAudioToPcm(string Url)
        {
            var args = $"/C youtube-dl --ignore-errors -o - {Url}" + " | " +
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
    }
}