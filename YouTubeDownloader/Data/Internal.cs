using System.Collections.Generic;

namespace YouTubeDownloader
{
    internal static class Internal
    {
        /// <summary>
        /// Relative path to the library cache folder.
        /// </summary>
        internal const string LIBRARY_CACHE_PATH = @"libraryCache";

        /// <summary>
        /// Relative path to the media cache folder.
        /// </summary>
        internal const string MEDIA_CACHE_PATH = @"mediaCache";

        /// <summary>
        /// Relative path to the thumbnail cache folder.
        /// </summary>
        internal const string THUMBNAIL_CACHE_PATH = @"thumbnailCache";

        /// <summary>
        /// The normalized, relative path to the <see cref="Internal.Library"/>.
        /// </summary>
        internal static readonly string LIBRARY_PATH = $"{LIBRARY_CACHE_PATH}\\library.json";

        /// <summary>
        /// The user's media library.
        /// </summary>
        internal static List<MediaFile> Library;
    }
}
