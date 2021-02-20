using System.Collections.ObjectModel;

namespace YouTubeDownloader
{
    /// <summary>
    /// Global properties
    /// </summary>
    public class Globals
    {
        /// <summary>
        /// Relative path to the library folder.
        /// </summary>
        public static string LibraryFolderPath { get; } = "library";

        /// <summary>
        /// Relative path to the video folder.
        /// </summary>
        public static string VideoFolderPath { get; } = "videos";

        /// <summary>
        /// Relative path to the thumbnail folder.
        /// </summary>
        public static string ThumbnailFolderPath { get; } = "thumbnails";

        /// <summary>
        /// Relative path to the temp folder.
        /// </summary>
        public static string TempFolderPath { get; } = "temp";

        /// <summary>
        /// The normalized, relative path to the user library file.
        /// </summary>
        public static string LibraryFilePath { get; } = $"{LibraryFolderPath}\\library.json";

        /// <summary>
        /// The user's <see cref="VideoMetadata"/> library.
        /// </summary>
        public static ObservableCollection<VideoMetadata> Library { get; set; } = new ObservableCollection<VideoMetadata>();
    }
}
