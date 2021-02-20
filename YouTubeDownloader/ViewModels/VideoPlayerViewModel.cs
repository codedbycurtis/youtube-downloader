using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace YouTubeDownloader
{
    public class VideoPlayerViewModel : BaseViewModel
    {
        #region Private Members

        private bool _isPlaying;
        private string _videoPath;
        private SharedViewModel _sharedModel;

        #endregion

        #region Public Properties

        /// <summary>
        /// Is there a video currently playing.
        /// </summary>
        public bool IsPlaying
        {
            get => _isPlaying;
            set
            {
                SetProperty(ref _isPlaying, value);
            }
        }

        /// <summary>
        /// The relative, normalised path to the currently selected video file.
        /// </summary>
        public string VideoPath
        {
            get => _videoPath;
            set => SetProperty(ref _videoPath, value);
        }

        /// <summary>
        /// Allows a <see cref="MediaElement"/> to be controlled from within a ViewModel.
        /// </summary>
        public IMediaService MediaService { get; set; }

        #endregion

        #region Commands

        public ICommand LoadedCommand { get; set; }
        public ICommand PlayPauseCommand { get; set; }
        public ICommand FastForwardCommand { get; set; }
        public ICommand RewindCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public VideoPlayerViewModel(SharedViewModel sharedModel)
        {
            VideoPath = ""; // Removes null type conversion binding failures
            _sharedModel = sharedModel;
            _sharedModel.PropertyChanged += OnSharedModelPropertyChanged;

            LoadedCommand = new RelayCommand<IMediaService>((mediaService) => { this.MediaService = mediaService; });

            PlayPauseCommand = new RelayCommand(() =>
            {
                if (IsPlaying)
                    MediaService.Pause();

                else
                    MediaService.Play();

                IsPlaying = !IsPlaying;
            });

            FastForwardCommand = new RelayCommand(() =>
            {

            });

            RewindCommand = new RelayCommand(() =>
            {

            });
        }

        #endregion

        #region Helpers

        private void OnSharedModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "VideoPath")
            {
                this.VideoPath = _sharedModel.VideoPath;
                this.IsPlaying = true;
                this.MediaService.Play();
            }
        }

        #endregion
    }
}
