using System;
using YoutubeExplode;

namespace YouTubeDownloader.Utils
{
    internal static class Youtube
    {
        private static readonly Lazy<YoutubeClient> LazyYoutubeClient = new Lazy<YoutubeClient>(() =>
        {
            return new YoutubeClient(Http.Client);
        });

        internal static YoutubeClient Client => LazyYoutubeClient.Value;
    }
}
