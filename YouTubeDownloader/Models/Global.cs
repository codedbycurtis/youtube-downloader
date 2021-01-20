using System.Collections.Generic;

namespace YouTubeDownloader
{
    public static class Global
    {
        /// <summary>
        /// Relative path to the library folder.
        /// </summary>
        public const string LIBRARY_FOLDER_PATH = @"Library";

        /// <summary>
        /// Relative path to the media store folder.
        /// </summary>
        public const string MEDIA_STORE_PATH = @"Media";

        /// <summary>
        /// Relative path to the thumbnail cache folder.
        /// </summary>
        public const string THUMBNAIL_CACHE_PATH = @"Thumbnails";

        /// <summary>
        /// The normalized, relative path to the <see cref="Global.Library"/>.
        /// </summary>
        public static readonly string LIBRARY_PATH = $"{LIBRARY_FOLDER_PATH}\\library.json";

        /// <summary>
        /// The user's media library.
        /// </summary>
        public static List<MediaFile> Library;
    }
}
