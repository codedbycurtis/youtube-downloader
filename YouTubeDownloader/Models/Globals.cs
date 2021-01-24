namespace YouTubeDownloader
{
    /// <summary>
    /// Global properties
    /// </summary>
    public static class Globals
    {
        /// <summary>
        /// Relative path to the library folder.
        /// </summary>
        public const string LibraryFolderPath = @"Library";

        /// <summary>
        /// Relative path to the media store folder.
        /// </summary>
        public const string MediaFolderPath = @"Media";

        /// <summary>
        /// Relative path to the thumbnail cache folder.
        /// </summary>
        public const string ThumbnailFolderPath = @"Thumbnails";

        /// <summary>
        /// The normalized, relative path to the user library file.
        /// </summary>
        public static readonly string LibraryFilePath = $"{LibraryFolderPath}\\library.json";
    }
}
