using Newtonsoft.Json;

namespace YouTubeDownloader
{
    /// <summary>
    /// Encapsulates a downloaded YouTube video's relevant metadata.
    /// </summary>
    internal struct MediaFile
    {
        /// <summary>
        /// Relative path to the downloaded video file.
        /// </summary>
        [JsonProperty("mediaPath")]
        internal string MediaPath { get; set; }

        /// <summary>
        /// Relative path to the downloaded video file's thumbnail.
        /// </summary>
        [JsonProperty("thumbnailPath")]
        internal string ThumbnailPath { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="mediaPath"></param>
        /// <param name="thumbnailPath"></param>
        [JsonConstructor()]
        internal MediaFile(string mediaPath, string thumbnailPath)
        {
            MediaPath = mediaPath;
            ThumbnailPath = thumbnailPath;
        }

        ///<inheritdoc/>
        public override string ToString() { return $"Media Path: {this.MediaPath}\nThumbnail Path: {this.ThumbnailPath}"; }
    }
}
