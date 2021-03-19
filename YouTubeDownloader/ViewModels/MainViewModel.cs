using System.ComponentModel;
using System.Windows.Input;

namespace YouTubeDownloader
{
    public class MainViewModel : BaseViewModel
    {
        #region Private Members

        private bool _isSearchViewVisible;
        private bool _isLibraryViewVisible;
        private bool _isVideoPlayerViewVisible;
        private bool _isAboutViewVisible;
        private bool _isSettingsViewVisible;
        private static SharedViewModel _sharedViewModel = new SharedViewModel();
        private static DownloadService _downloadService = new DownloadService();

        #endregion

        #region Public Properties

        /// <summary>
        /// Is the SearchView visible?
        /// </summary>
        public bool IsSearchViewVisible
        {
            get => _isSearchViewVisible;
            set => SetProperty(ref _isSearchViewVisible, value);
        }

        /// <summary>
        /// Is the LibraryView visible?
        /// </summary>
        public bool IsLibraryViewVisible
        {
            get => _isLibraryViewVisible;
            set => SetProperty(ref _isLibraryViewVisible, value);
        }

        /// <summary>
        /// Is the VideoPlayerView visible?
        /// </summary>
        public bool IsVideoPlayerViewVisible
        {
            get => _isVideoPlayerViewVisible;
            set => SetProperty(ref _isVideoPlayerViewVisible, value);
        }

        /// <summary>
        /// Is the AboutView visible?
        /// </summary>
        public bool IsAboutViewVisible
        {
            get => _isAboutViewVisible;
            set => SetProperty(ref _isAboutViewVisible, value);
        }

        /// <summary>
        /// Is the SettingsView visible?
        /// </summary>
        public bool IsSettingsViewVisible
        {
            get => _isSettingsViewVisible;
            set => SetProperty(ref _isSettingsViewVisible, value);
        }

        /// <summary>
        /// An instance of the <see cref="YouTubeDownloader.SearchViewModel"/>.
        /// </summary>
        public SearchViewModel SearchViewModel { get; } = new SearchViewModel(_downloadService);

        /// <summary>
        /// An instance of the <see cref="YouTubeDownloader.LibraryViewModel"/>.
        /// </summary>
        public LibraryViewModel LibraryViewModel { get; } = new LibraryViewModel(_sharedViewModel);

        /// <summary>
        /// An instance of the <see cref="YouTubeDownloader.VideoPlayerViewModel"/>.
        /// </summary>
        public VideoPlayerViewModel VideoPlayerViewModel { get; } = new VideoPlayerViewModel(_sharedViewModel);

        /// <summary>
        /// An instance of the <see cref="YouTubeDownloader.AboutViewModel"/>.
        /// </summary>
        public AboutViewModel AboutViewModel { get; } = new AboutViewModel();

        /// <summary>
        /// An instance of the <see cref="YouTubeDownloader.SettingsViewModel"/>.
        /// </summary>
        public SettingsViewModel SettingsViewModel { get; } = new SettingsViewModel();

        #endregion

        #region Commands

        public ICommand SearchTabButton { get; }        
        public ICommand LibraryTabButton { get; }
        public ICommand PlayerTabButton { get; }
        public ICommand AboutButton { get; }
        public ICommand SettingsButton { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainViewModel()
        {
            // Initialise commands

            SearchTabButton = new RelayCommand(() =>
            {
                IsSearchViewVisible = true;
                IsLibraryViewVisible = IsVideoPlayerViewVisible = IsAboutViewVisible = IsSettingsViewVisible = false;
            });

            LibraryTabButton = new RelayCommand(() =>
            {
                IsLibraryViewVisible = true;
                IsSearchViewVisible = IsVideoPlayerViewVisible = IsAboutViewVisible = IsSettingsViewVisible = false;
            });

            PlayerTabButton = new RelayCommand(() =>
            {
                IsVideoPlayerViewVisible = true;
                IsSearchViewVisible = IsLibraryViewVisible = IsAboutViewVisible = IsSettingsViewVisible = false;
            });


            AboutButton = new RelayCommand(() =>
            {
                IsAboutViewVisible = true;
                IsSearchViewVisible = IsLibraryViewVisible = IsVideoPlayerViewVisible = IsSettingsViewVisible = false;
            });

            SettingsButton = new RelayCommand(() =>
            {
                IsSettingsViewVisible = true;
                IsSearchViewVisible = IsLibraryViewVisible = IsVideoPlayerViewVisible = IsAboutViewVisible = false;
            });

            // Bind event handlers
            _sharedViewModel.PropertyChanged += OnSharedViewModelPropertyChanged;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Handles PropertyChanged events invoked by the <see cref="_sharedViewModel"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSharedViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Video")
            {
                IsVideoPlayerViewVisible = true;
                IsSearchViewVisible = IsLibraryViewVisible = IsAboutViewVisible = IsSettingsViewVisible = false;
            }
        }

        #endregion
    }
}
