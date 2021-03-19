using System;
using System.ComponentModel;
using System.Timers;
using System.Windows.Input;

namespace YouTubeDownloader
{
    public class VideoPlayerViewModel : BaseViewModel
    {
        #region Private Members

        private bool _isPlaying;
        private string _videoPath;
        private TimeSpan _videoDuration;
        private TimeSpan _timeElapsed;
        private Timer _timer;
        private SharedViewModel _sharedViewModel;
        private IMediaService _mediaService;

        #endregion

        #region Public Properties

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

        public ICommand LoadedCommand { get; set; }
        public ICommand PlayPauseCommand { get; set; }
        public ICommand NextVideoCommand { get; set; }
        public ICommand PreviousVideoCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public VideoPlayerViewModel(SharedViewModel sharedViewModel)
        {
            // Initialise properties

            VideoPath = "";
            TimeElapsed = new TimeSpan(0, 0, 0, 0, 0);

            _sharedViewModel = sharedViewModel;
            _sharedViewModel.PropertyChanged += OnSharedModelPropertyChanged; // Bind PropertyChanged event handler

            // Initialise timer
            _timer = new Timer();
            _timer.BeginInit();
            _timer.Interval = 100;
            _timer.Elapsed += OnTimerElapsed;
            _timer.EndInit();

            // Initialise commands

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
        /// Handles PropertyChanged events from the <see cref="_sharedViewModel"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSharedModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Video")
            {
                VideoPath = $"{Globals.VideoFolderPath}\\{_sharedViewModel.Video.Id}.mp4";
                VideoDuration = _sharedViewModel.Video.Duration;
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
        /// Skips to the next video in the <see cref="Globals.Library"/>.
        /// </summary>
        private void NextVideo()
        {
            var currentIndex = Globals.Library.IndexOf(_sharedViewModel.Video);

            if (currentIndex == (Globals.Library.Count - 1))
                _sharedViewModel.Video = Globals.Library[0];

            else
                _sharedViewModel.Video = Globals.Library[currentIndex + 1];

            TimeElapsed = TimeSpan.Zero;
            _mediaService.Reset();
        }

        /// <summary>
        /// Skips to the previous video in the <see cref="Globals.Library"/>.
        /// </summary>
        private void PreviousVideo()
        {
            var currentIndex = Globals.Library.IndexOf(_sharedViewModel.Video);
            var maxIndex = Globals.Library.Count - 1;

            if (currentIndex == 0)
                _sharedViewModel.Video = Globals.Library[maxIndex];

            else
                _sharedViewModel.Video = Globals.Library[currentIndex - 1];

            TimeElapsed = TimeSpan.Zero;
            _mediaService.Reset();
        }

        #endregion
    }
}
