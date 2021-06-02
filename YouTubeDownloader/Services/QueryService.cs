using System.Collections.Generic;
using System.Threading.Tasks;
using YoutubeExplode.Channels;
using YoutubeExplode.Playlists;
using YoutubeExplode.Videos;
using YoutubeExplode.Common;
using YouTubeDownloader.Utils;

namespace YouTubeDownloader
{
    /// <summary>
    /// Provides methods of querying YouTube for specific videos.
    /// </summary>
    public sealed class QueryService
    {
        /// <summary>
        /// Asynchronously searches YouTube for the specified <paramref name="searchQuery"/>.
        /// </summary>
        /// <param name="searchQuery">The video, playlist, or channel to search for.</param>
        /// <returns>An <see cref="IReadOnlyList{T}"/> of <see cref="IVideo"/>'s matching the user's specified <paramref name="searchQuery"/>.</returns>
        public async ValueTask<IReadOnlyList<IVideo>> SearchAsync(string searchQuery)
        {
            if (PlaylistId.TryParse(searchQuery).HasValue)
            {
                return await Youtube.Client.Playlists.GetVideosAsync(PlaylistId.Parse(searchQuery)).CollectAsync(200);
            }
            else if (ChannelId.TryParse(searchQuery).HasValue)
            {
                return await Youtube.Client.Channels.GetUploadsAsync(ChannelId.Parse(searchQuery)).CollectAsync(200);
            }

            return await Youtube.Client.Search.GetVideosAsync(searchQuery).CollectAsync(200);
        }
    }
}
