using System;
using Newtonsoft.Json;

namespace YouTubeDownloader
{
    /// <summary>
    /// Encapsulates the relevant metadata of a <see cref="YoutubeExplode.Videos.Video"/>.
    /// </summary>
    public class VideoMetadata
    {
        /// <summary>
        /// String representation of a <see cref="YoutubeExplode.Videos.VideoId"/>.
        /// </summary>
        [JsonProperty("videoId")]
        public string VideoId { get; private set; }

        /// <summary>
        /// The title of the video.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; private set; }

        /// <summary>
        /// The name of the channel that uploaded the video.
        /// </summary>
        [JsonProperty("author")]
        public string Author { get; private set; }

        /// <summary>
        /// The duration of the video.
        /// </summary>
        [JsonProperty("duration")]
        public TimeSpan Duration { get; private set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="videoId">String representation of a <see cref="YoutubeExplode.Videos.VideoId"/>.</param>
        /// <param name="title">The title of the video.</param>
        /// <param name="author">The name of the channel that uploaded the video.</param>
        /// <param name="duration">The duration of the video.</param>
        [JsonConstructor()]
        public VideoMetadata(string videoId, string title, string author, TimeSpan duration)
        {
            VideoId = videoId;
            Title = title;
            Author = author;
            Duration = duration;
        }

        public override string ToString() { return $"Video Id: {this.VideoId} | Title: {this.Title} | Author: {this.Author} | Duration: {this.Duration}"; }
    }
}
