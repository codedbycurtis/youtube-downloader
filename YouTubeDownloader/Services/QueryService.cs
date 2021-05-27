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
            var queryType = GetQueryType(searchQuery);

            switch (queryType)
            {
                case QueryType.Video:
                case QueryType.General:
                default:
                    return await Youtube.Client.Search.GetVideosAsync(searchQuery).CollectAsync(100);

                case QueryType.Playlist:
                    return await Youtube.Client.Playlists.GetVideosAsync(PlaylistId.Parse(searchQuery)).CollectAsync(100);

                case QueryType.Channel:
                    return await Youtube.Client.Channels.GetUploadsAsync(ChannelId.Parse(searchQuery)).CollectAsync(100);
            }
        }

        /// <summary>
        /// Gets the <see cref="QueryType"/> of the user's search query.
        /// <para>E.g. general search query, specific video search, playlist search, channel search, etc.</para>
        /// </summary>
        private QueryType GetQueryType(string idOrUrl)
        {
            if (VideoId.TryParse(idOrUrl).HasValue)
            {
                return QueryType.Video;
            }
            else if (PlaylistId.TryParse(idOrUrl).HasValue)
            {
                return QueryType.Playlist;
            }
            else if (ChannelId.TryParse(idOrUrl).HasValue)
            {
                return QueryType.Channel;
            }

            return QueryType.General;
        }
    }
}
