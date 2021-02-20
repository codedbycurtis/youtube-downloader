using System.Windows.Controls;

namespace YouTubeDownloader
{
    /// <summary>
    /// Interaction logic for VideoPlayerUserControl.xaml
    /// </summary>
    public partial class VideoPlayerUserControl : UserControl, IMediaService
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public VideoPlayerUserControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Fast-forward a video.
        /// </summary>
        public void FastForward()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Pause a video.
        /// </summary>
        public void Pause()
        {
            this.MediaElement.Pause();
        }

        /// <summary>
        /// Play a video.
        /// </summary>
        public void Play()
        {
            this.MediaElement.Play();
        }

        /// <summary>
        /// Rewind a video.
        /// </summary>
        public void Rewind()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
