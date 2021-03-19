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
        public static string DataFolderPath { get; } = "Data";

        /// <summary>
        /// Relative path to the video folder.
        /// </summary>
        public static string VideoFolderPath { get; } = "Videos";

        /// <summary>
        /// Relative path to the thumbnail folder.
        /// </summary>
        public static string ThumbnailFolderPath { get; } = "Thumbnails";

        /// <summary>
        /// The normalized, relative path to the user library file.
        /// </summary>
        public static string LibraryFilePath { get; } = $"{DataFolderPath}\\library.json";

        /// <summary>
        /// The user's video library.
        /// </summary>
        public static ObservableCollection<LibraryVideo> Library { get; set; } = new ObservableCollection<LibraryVideo>();
    }
}
