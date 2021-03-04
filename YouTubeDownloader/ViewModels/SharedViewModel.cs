using System.ComponentModel;

namespace YouTubeDownloader
{
    /// <summary>
    /// Allows properties to be shared across multiple ViewModels.
    /// </summary>
    public class SharedViewModel : INotifyPropertyChanged
    {
        #region Private Members

        private VideoMetadata _video;

        #endregion

        #region Public Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public Properties

        public VideoMetadata Video
        {
            get => _video;
            set
            {
                if (_video != value)
                {
                    _video = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Video)));
                }
            }
        }

        #endregion
    }
}
