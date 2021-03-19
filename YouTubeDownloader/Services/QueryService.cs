using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System;
using YoutubeExplode;
using YoutubeExplode.Playlists;

namespace YouTubeDownloader
{
    public sealed class QueryService
    {
        private readonly YoutubeClient _youtubeClient = new YoutubeClient();

        // TODO: Implement different query types for: e.g. playlists, channels, videos, etc.
        public async Task<IReadOnlyList<PlaylistVideo>> SearchAsync(string searchQuery)
        {
             return await _youtubeClient.Search.GetVideosAsync(searchQuery).BufferAsync(100);
        }
    }
}
