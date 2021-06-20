using System;
using System.ComponentModel;
using System.Timers;
using System.Windows.Input;
using YouTubeDownloader.ViewModels.Components;
using YouTubeDownloader.ViewModels.Framework;

namespace YouTubeDownloader.ViewModels
{
    public class VideoPlayerViewModel : BaseViewModel
    {
        #region Fields

        private bool _isPlaying;
        private string _videoPath;
        private TimeSpan _videoDuration;
        private TimeSpan _timeElapsed;
        private IMediaService _mediaService;
        private readonly Timer _timer;
        private readonly ISessionContext _sessionContext;

        #endregion

        #region Properties

        /// <summary>
        /// Is there a video currently playing.
        /// </summary>
        public bool IsPlaying
        {
            get => _isPlaying;
            set => SetProperty(ref _isPlaying, value);
        }

        /// <summary>
        /// The duration of the current video.
        /// </summary>
        public TimeSpan VideoDuration
        {
            get => _videoDuration;
            set => SetProperty(ref _videoDuration, value);
        }

        /// <summary>
        /// The elapsed time of the current video.
        /// </summary>
        public TimeSpan TimeElapsed
        {
            get => _timeElapsed;
            set => SetProperty(ref _timeElapsed, value);
        }

        /// <summary>
        /// The relative, normalised path to the current video on disk.
        /// </summary>
        public string VideoPath
        {
            get => _videoPath;
            set => SetProperty(ref _videoPath, value);
        }

        #endregion

        #region Commands

        public ICommand LoadedCommand { get; }

        public ICommand PlayPauseCommand { get; }

        public ICommand NextVideoCommand { get; }

        public ICommand PreviousVideoCommand { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initialises a new instance of <see cref="VideoPlayerViewModel"/> with the specified <see cref="SessionContext"/>.
        /// </summary>
        public VideoPlayerViewModel(ISessionContext sessionContext)
        {
            // Initialize properties
            VideoPath = "";
            TimeElapsed = new TimeSpan(0, 0, 0, 0, 0);

            _sessionContext = sessionContext;
            _sessionContext.PropertyChanged += OnSessionContextPropertyChanged; // Bind PropertyChanged event handler

            // Initialize timer
            _timer = new Timer();
            _timer.BeginInit();
            _timer.Interval = 100;
            _timer.Elapsed += OnTimerElapsed;
            _timer.EndInit();

            // Initialize commands
            LoadedCommand = new RelayCommand<IMediaService>((mediaService) =>
            {
                _mediaService = mediaService;
                _mediaService.PropertyChanged += OnMediaServicePropertyChanged;
            });

            PlayPauseCommand = new RelayCommand(() =>
            {
                if (IsPlaying)
                {
                    _mediaService.Pause();
                    _timer.Stop();
                }

                else
                {
                    _mediaService.Play();
                    _timer.Start();
                }

                IsPlaying = !IsPlaying;
            });

            NextVideoCommand = new RelayCommand(() => NextVideo());

            PreviousVideoCommand = new RelayCommand(() => PreviousVideo());
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Handles PropertyChanged events from the <see cref="_sessionContext"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSessionContextPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_sessionContext.CurrentlyPlaying))
            {
                VideoPath =
                    $"{App.VideoFolderPath}/{_sessionContext.CurrentlyPlaying.Id}.{_sessionContext.CurrentlyPlaying.Container}";
                VideoDuration = _sessionContext.CurrentlyPlaying.Duration;
                IsPlaying = true;
                _mediaService.Play();
                TimeElapsed = TimeSpan.Zero;
                _timer.Start();
            }
        }

        /// <summary>
        /// Handles PropertyChanged events from the <see cref="_mediaService"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMediaServicePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "TimeElapsed")
                TimeElapsed = _mediaService.TimeElapsed;
        }

        /// <summary>
        /// Handles Elapsed events from the <see cref="_timer"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (TimeElapsed >= VideoDuration)
                NextVideo();

            TimeElapsed += TimeSpan.FromMilliseconds(100);

            if (!_mediaService.IsSliderBeingManipulated)
                _mediaService.UpdateSlider();
        }

        /// <summary>
        /// Skips to the next video in the <see cref="Global.Library"/>.
        /// </summary>
        private void NextVideo()
        {
            var currentIndex = App.VideoLibrary.IndexOf(_sessionContext.CurrentlyPlaying);
            var maxIndex = App.VideoLibrary.Count - 1;

            if (currentIndex == -1)
                return;

            else
                _sessionContext.CurrentlyPlaying = currentIndex == maxIndex ? App.VideoLibrary[0] : App.VideoLibrary[currentIndex + 1];

            TimeElapsed = TimeSpan.Zero;
            _mediaService.Reset();
        }

        /// <summary>
        /// Skips to the previous video in the <see cref="Global.Library"/>.
        /// </summary>
        private void PreviousVideo()
        {
            var currentIndex = App.VideoLibrary.IndexOf(_sessionContext.CurrentlyPlaying);
            var maxIndex = App.VideoLibrary.Count - 1;

            if (currentIndex == -1)
                return;

            else
                _sessionContext.CurrentlyPlaying = currentIndex == 0 ? App.VideoLibrary[maxIndex] : App.VideoLibrary[currentIndex - 1];

            TimeElapsed = TimeSpan.Zero;
            _mediaService.Reset();
        }

        #endregion
    }
}
