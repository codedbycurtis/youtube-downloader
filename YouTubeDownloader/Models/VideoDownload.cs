namespace YouTubeDownloader
{
    /// <summary>
    /// Metadata regarding an in-progress video download.
    /// </summary>
    public class VideoDownload
    {
        public string VideoId { get; }

        public string ThumbnailUrl { get; }

        public string Title { get; }

        public string Status { get; set; }

        public VideoDownload(string videoId, string thumbnailUrl, string title)
        {
            VideoId = videoId;
            ThumbnailUrl = thumbnailUrl;
            Title = title;
        }
    }
}
