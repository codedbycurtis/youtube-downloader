using System.ComponentModel;

namespace YouTubeDownloader
{
    /// <summary>
    /// Allows properties to be shared across multiple ViewModels.
    /// </summary>
    public class SharedViewModel : INotifyPropertyChanged
    {
        #region Private Members

        private string _videoPath;

        #endregion

        #region Public Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public Properties

        public string VideoPath
        {
            get => _videoPath;
            set
            {
                if (_videoPath != value)
                {
                    _videoPath = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VideoPath)));
                }
            }
        }

        #endregion
    }
}
