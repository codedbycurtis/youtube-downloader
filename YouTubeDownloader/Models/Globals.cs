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
        public static string LibraryFolderPath { get; } = "Library";

        /// <summary>
        /// Relative path to the video folder.
        /// </summary>
        public static string VideoFolderPath { get; } = "Videos";

        /// <summary>
        /// Relative path to the thumbnail folder.
        /// </summary>
        public static string ThumbnailFolderPath { get; } = "Thumbnails";

        /// <summary>
        /// Relative path to the temp folder.
        /// </summary>
        public static string TempFolderPath { get; } = "Temp";

        /// <summary>
        /// The normalized, relative path to the user library file.
        /// </summary>
        public static string LibraryFilePath { get; } = $"{LibraryFolderPath}\\Library.json";

        /// <summary>
        /// The user's <see cref="VideoMetadata"/> library.
        /// </summary>
        public static ObservableCollection<VideoMetadata> Library { get; set; } = new ObservableCollection<VideoMetadata>();
    }
}
