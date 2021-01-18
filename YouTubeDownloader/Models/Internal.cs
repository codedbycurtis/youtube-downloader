using System.Collections.Generic;

namespace YouTubeDownloader
{
    internal static class Internal
    {
        /// <summary>
        /// Relative path to the library folder.
        /// </summary>
        internal const string LIBRARY_FOLDER_PATH = @"Library";

        /// <summary>
        /// Relative path to the media store folder.
        /// </summary>
        internal const string MEDIA_STORE_PATH = @"Media";

        /// <summary>
        /// Relative path to the thumbnail cache folder.
        /// </summary>
        internal const string THUMBNAIL_CACHE_PATH = @"Thumbnails";

        /// <summary>
        /// The normalized, relative path to the <see cref="Internal.Library"/>.
        /// </summary>
        internal static readonly string LIBRARY_PATH = $"{LIBRARY_FOLDER_PATH}\\library.json";

        /// <summary>
        /// The user's media library.
        /// </summary>
        internal static List<MediaFile> Library;
    }
}
