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
        /// Video identifier. This will be used to determine the video and thumbnail filenames.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; private set; }

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
        /// Initialises a new instance of <see cref="LibraryVideo"/> with the specified parameters.
        /// </summary>
        /// <param name="id">Video identifier. This will be used to determine the video and thumbnail filenames.</param>
        /// <param name="title">The title of the video.</param>
        /// <param name="author">The name of the channel that uploaded the video.</param>
        /// <param name="duration">The duration of the video.</param>
        [JsonConstructor()]
        public LibraryVideo(string id, string title, string author, TimeSpan duration)
        {
            Id = id;
            Title = title;
            Author = author;
            Duration = duration;
        }

        public override string ToString()
        {
            return $"Video Id: {this.Id} | Title: {this.Title} | Author: {this.Author} | Duration: {this.Duration}";
        }
    }
}
