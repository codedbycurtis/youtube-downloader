using Newtonsoft.Json;
using System;

namespace YouTubeDownloader
{
    /// <summary>
    /// Encapsulates a downloaded <see cref="YoutubeExplode.Videos.Video"/>'s relevant metadata.
    /// </summary>
    public class MediaFile
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
        [JsonProperty("uploader")]
        public string Uploader { get; private set; }

        /// <summary>
        /// Relative path to the downloaded video file.
        /// </summary>
        [JsonProperty("mediaPath")]
        public string MediaPath { get; private set; }

        /// <summary>
        /// Relative path to the downloaded video file's thumbnail.
        /// </summary>
        [JsonProperty("thumbnailPath")]
        public string ThumbnailPath { get; private set; }

        /// <summary>
        /// The duration of the downloaded video.
        /// </summary>
        [JsonProperty("duration")]
        public TimeSpan Duration { get; private set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="title"></param>
        /// <param name="uploader"></param>
        /// <param name="videoId"></param>
        /// <param name="duration"></param>
        [JsonConstructor()]
        public MediaFile(string title, string uploader, string videoId, TimeSpan duration)
        {
            VideoId = videoId;
            Title = title;
            Uploader = uploader;
            MediaPath = $"{Global.MEDIA_STORE_PATH}\\{VideoId}.mp4";
            ThumbnailPath = $"{Global.THUMBNAIL_CACHE_PATH}\\{VideoId}.jpg";
            Duration = duration;
        }

        /// <summary>
        /// Adds this <see cref="MediaFile"/> to the user's <see cref="Global.Library"/>.
        /// </summary>
        public void AddToLibrary()
        {
            Global.Library.Add(this);
            Json.Write(Global.Library, Global.LIBRARY_PATH);
        }

        ///<inheritdoc/>
        public override string ToString() { return $"Video Id: {this.VideoId}\nTitle: {this.Title}\nUploader:{this.Uploader}\n" +
                $"Media Path: {this.MediaPath}\nThumbnail Path: {this.ThumbnailPath}\nDuration: {this.Duration}"; }
    }
}
