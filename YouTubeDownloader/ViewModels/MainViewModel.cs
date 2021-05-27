using System.ComponentModel;
using System.Windows.Input;
using YouTubeDownloader.ViewModels.Framework;

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
        private readonly SharedViewModel _sharedViewModel = new SharedViewModel();

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
        /// Title of the application with the current version appended.
        /// </summary>
        public string Title
        {
            get => $"YouTube Downloader v{App.AssemblyVersionString}";
        }

        /// <summary>
        /// An instance of the <see cref="YouTubeDownloader.SearchViewModel"/>.
        /// </summary>
        public SearchViewModel SearchViewModel { get; }

        /// <summary>
        /// An instance of the <see cref="YouTubeDownloader.LibraryViewModel"/>.
        /// </summary>
        public LibraryViewModel LibraryViewModel { get; }

        /// <summary>
        /// An instance of the <see cref="YouTubeDownloader.VideoPlayerViewModel"/>.
        /// </summary>
        public VideoPlayerViewModel VideoPlayerViewModel { get; }

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

        public ICommand ShowSearchCommand { get; }
        public ICommand ShowLibraryCommand { get; }
        public ICommand ShowPlayerCommand { get; }
        public ICommand ShowAboutCommand { get; }
        public ICommand ShowSettingsCommand { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of <see cref="MainViewModel"/>.
        /// </summary>
        public MainViewModel()
        {
            // Initialise services and ViewModels
            SearchViewModel = new SearchViewModel();
            LibraryViewModel = new LibraryViewModel(_sharedViewModel);
            VideoPlayerViewModel = new VideoPlayerViewModel(_sharedViewModel);

            // Initialise commands
            ShowSearchCommand = new RelayCommand(() =>
            {
                IsSearchViewVisible = true;
                IsLibraryViewVisible = IsVideoPlayerViewVisible = IsAboutViewVisible = IsSettingsViewVisible = false;
            });

            ShowLibraryCommand = new RelayCommand(() =>
            {
                IsLibraryViewVisible = true;
                IsSearchViewVisible = IsVideoPlayerViewVisible = IsAboutViewVisible = IsSettingsViewVisible = false;
            });

            ShowPlayerCommand = new RelayCommand(() =>
            {
                IsVideoPlayerViewVisible = true;
                IsSearchViewVisible = IsLibraryViewVisible = IsAboutViewVisible = IsSettingsViewVisible = false;
            });

            ShowAboutCommand = new RelayCommand(() =>
            {
                IsAboutViewVisible = true;
                IsSearchViewVisible = IsLibraryViewVisible = IsVideoPlayerViewVisible = IsSettingsViewVisible = false;
            });

            ShowSettingsCommand = new RelayCommand(() =>
            {
                IsSettingsViewVisible = true;
                IsSearchViewVisible = IsLibraryViewVisible = IsVideoPlayerViewVisible = IsAboutViewVisible = false;
            });

            _sharedViewModel.PropertyChanged += OnSharedViewModelPropertyChanged; // Bind event handler
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Handles PropertyChanged events invoked by the <see cref="_sharedViewModel"/>.
        /// </summary>
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
