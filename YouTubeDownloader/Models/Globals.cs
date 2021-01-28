using System.Collections.ObjectModel;

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
        public const string LibraryFolderPath = @"library_data";

        /// <summary>
        /// Relative path to the media store folder.
        /// </summary>
        public const string MediaFolderPath = @"videos";

        /// <summary>
        /// Relative path to the thumbnail cache folder.
        /// </summary>
        public const string ThumbnailFolderPath = @"thumbnails";

        /// <summary>
        /// The normalized, relative path to the user library file.
        /// </summary>
        public static readonly string LibraryFilePath = $"{LibraryFolderPath}\\library.json";

        /// <summary>
        /// The user's <see cref="MediaFile"/> library.
        /// </summary>
        public static ObservableCollection<MediaFile> Library { get; set; } = new ObservableCollection<MediaFile>();
    }
}
