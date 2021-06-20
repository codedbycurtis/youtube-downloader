using System;
using Newtonsoft.Json;

namespace YouTubeDownloader.Models
{
    /// <summary>
    /// Encapsulates metadata relevant to video playback.
    /// </summary>
    public class LibraryVideo
    {
        /// <summary>
        /// The name of the video's container.
        /// </summary>
        /// <remarks>E.g. mp4, webm, etc.</remarks>
        [JsonProperty("container")]
        public string Container { get; }

        /// <summary>
        /// Video identifier. This will be used to determine the video and thumbnail filenames.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; }

        /// <summary>
        /// The title of the video.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; }

        /// <summary>
        /// The name of the channel that uploaded the video.
        /// </summary>
        [JsonProperty("author")]
        public string Author { get; }

        /// <summary>
        /// The duration of the video.
        /// </summary>
        [JsonProperty("duration")]
        public TimeSpan Duration { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="LibraryVideo"/> with the specified parameters.
        /// </summary>
        /// <param name="id">Video identifier. This will be used to determine the video and thumbnail filenames.</param>
        /// <param name="title">The title of the video.</param>
        /// <param name="author">The name of the channel that uploaded the video.</param>
        /// <param name="duration">The duration of the video.</param>
        [JsonConstructor()]
        public LibraryVideo(
            string container,
            string id,
            string title,
            string author,
            TimeSpan duration)
        {
            this.Container = container;
            this.Id = id;
            this.Title = title;
            this.Author = author;
            this.Duration = duration;
        }

        /// <inheritdoc />
        public override string ToString() => $"Video: {this.Title}";
    }
}
