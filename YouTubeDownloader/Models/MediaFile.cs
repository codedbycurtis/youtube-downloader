using Newtonsoft.Json;
using System;
using YoutubeExplode.Videos;

namespace YouTubeDownloader
{
    /// <summary>
    /// Encapsulates a downloaded <see cref="Video"/>'s relevant metadata.
    /// </summary>
    public class MediaFile
    {
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
        [JsonProperty("filePath")]
        public string FilePath { get; private set; }

        /// <summary>
        /// Relative path to the downloaded video file's thumbnail.
        /// </summary>
        [JsonProperty("thumbnailPath")]
        public string ThumbnailPath { get; private set; }

        /// <summary>
        /// The length of the downloaded video.
        /// </summary>
        [JsonProperty("length")]
        public TimeSpan Length { get; private set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="video"></param>
        [JsonConstructor()]
        public MediaFile(string title, string uploader, string videoId, TimeSpan length)
        {
            Title = title;
            Uploader = uploader;
            FilePath = $"{Global.MEDIA_STORE_PATH}\\{videoId}.mp4";
            ThumbnailPath = $"{Global.THUMBNAIL_CACHE_PATH}\\{videoId}.jpg";
            Length = length;
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
        public override string ToString() { return $"File Path: {this.FilePath}\nThumbnail Path: {this.ThumbnailPath}"; }
    }
}
