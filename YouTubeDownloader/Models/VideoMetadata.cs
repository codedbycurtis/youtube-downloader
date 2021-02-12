using System;
using Newtonsoft.Json;

namespace YouTubeDownloader
{
    /// <summary>
    /// Encapsulates a downloaded <see cref="YoutubeExplode.Videos.Video"/>'s relevant metadata.
    /// </summary>
    public class VideoMetadata
    {
        /// <summary>
        /// String representation of the downloaded YouTube video's <see cref="YoutubeExplode.Videos.VideoId"/>.
        /// </summary>
        [JsonProperty("videoId")]
        public string VideoId { get; private set; }

        /// <summary>
        /// The title of the download video file.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; private set; }

        /// <summary>
        /// The name of the channel that uploaded the downloaded video file.
        /// </summary>
        [JsonProperty("author")]
        public string Author { get; private set; }

        /// <summary>
        /// The duration of the downloaded video.
        /// </summary>
        [JsonProperty("duration")]
        public TimeSpan Duration { get; private set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="title"></param>
        /// <param name="author"></param>
        /// <param name="videoId"></param>
        /// <param name="duration"></param>
        [JsonConstructor()]
        public VideoMetadata(string title, string author, string videoId, TimeSpan duration)
        {
            VideoId = videoId;
            Title = title;
            Author = author;
            Duration = duration;
        }

        /// <inheritdoc/>
        public override string ToString() { return $"Video Id: {this.VideoId}\nTitle: {this.Title}\nAuthor:{this.Author}\nDuration: {this.Duration}"; }
    }
}
