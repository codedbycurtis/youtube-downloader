using YoutubeExplode.Playlists;

namespace YouTubeDownloader
{
    public static class YoutubeExplodeExtensions
    {
        public static VideoDownload ToVideoDownload(this PlaylistVideo video)
        {
            return new VideoDownload(video.Id.Value, video.Thumbnails.MediumResUrl, video.Title);
        }
    }
}
