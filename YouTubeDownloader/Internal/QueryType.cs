namespace YouTubeDownloader
{
    /// <summary>
    /// The type of search query specified by the user.
    /// </summary>
    internal enum QueryType
    {
        /// <summary>
        /// General search query.
        /// </summary>
        General,

        /// <summary>
        /// Specific video search via id or url.
        /// </summary>
        Video,

        /// <summary>
        /// Playlist search via id or url.
        /// </summary>
        Playlist,

        /// <summary>
        /// Channel search via id or url.
        /// </summary>
        Channel
    }
}
