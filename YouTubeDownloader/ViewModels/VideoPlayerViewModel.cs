namespace YouTubeDownloader
{
    public class VideoPlayerViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The relative, normalised path to the currently selected video file.
        /// </summary>
        public string VideoPath { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public VideoPlayerViewModel()
        {

        }

        #endregion
    }
}
