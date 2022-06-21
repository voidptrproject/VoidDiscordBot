using System.Collections.Generic;
using DSharpPlus.VoiceNext;

namespace VoidBot.Modules.Music
{
    class Queue
    {
        private struct QueueItem
        {
            public DSharpPlus.Entities.DiscordChannel Channel { get; set; }
            public Song Song { get; set; }
        }

        public static Queue? Instance { get; set; } = null;

        private Queue<QueueItem> _queue = new Queue<QueueItem>();
        private DSharpPlus.DiscordClient? _client = null;

        public Queue(DSharpPlus.DiscordClient client)
        {
            _client = client;
            Task.Run(async () =>
            {
                while (true)
                {
                    if (_queue.TryDequeue(out var item))
                        await Play(_client, item.Channel, item.Song);
                    await Task.Delay(1000);
                }
            });
        }

        public void Add(DSharpPlus.Entities.DiscordChannel channel, Song song)
        {
            _queue.Enqueue(new QueueItem
            {
                Channel = channel,
                Song = song
            });
        }

        private static async Task Play(DSharpPlus.DiscordClient client, DSharpPlus.Entities.DiscordChannel channel, Song song)
        {
            try
            {
                var vnext = client.GetVoiceNext();
                var connection = vnext.GetConnection(channel.Guild);
                if (connection == null)
                    connection = await vnext.ConnectAsync(channel);
                var transmit = connection.GetTransmitSink();
                var stream = Utils.Audio.GetStream(song.Url!);
                await stream.CopyToAsync(transmit);
                await stream.DisposeAsync();
            } 
            catch(Exception e)
            {
                Console.WriteLine($"Error playing song: {e.Message}\n{e.StackTrace}");
            }
        }
    }
}