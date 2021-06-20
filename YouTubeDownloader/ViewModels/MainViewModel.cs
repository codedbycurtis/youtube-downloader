using System.ComponentModel;
using System.Windows.Input;
using YouTubeDownloader.ViewModels.Components;
using YouTubeDownloader.ViewModels.Framework;

namespace YouTubeDownloader.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Fields

        private bool _isSearchViewVisible;
        private bool _isLibraryViewVisible;
        private bool _isVideoPlayerViewVisible;
        private bool _isAboutViewVisible;
        private bool _isSettingsViewVisible;
        private readonly ISessionContext _sessionContext;
        private readonly IViewModelFactory _viewModelFactory;

        #endregion

        #region Properties

        /// <summary>
        /// Is the <see cref="SearchView"/> visible?
        /// </summary>
        public bool IsSearchViewVisible
        {
            get => _isSearchViewVisible;
            set => SetProperty(ref _isSearchViewVisible, value);
        }

        /// <summary>
        /// Is the <see cref="LibraryView"/> visible?
        /// </summary>
        public bool IsLibraryViewVisible
        {
            get => _isLibraryViewVisible;
            set => SetProperty(ref _isLibraryViewVisible, value);
        }

        /// <summary>
        /// Is the <see cref="VideoPlayerView"/> visible?
        /// </summary>
        public bool IsVideoPlayerViewVisible
        {
            get => _isVideoPlayerViewVisible;
            set => SetProperty(ref _isVideoPlayerViewVisible, value);
        }

        /// <summary>
        /// Is the <see cref="AboutView"/> visible?
        /// </summary>
        public bool IsAboutViewVisible
        {
            get => _isAboutViewVisible;
            set => SetProperty(ref _isAboutViewVisible, value);
        }

        /// <summary>
        /// Is the <see cref="SettingsView"/> visible?
        /// </summary>
        public bool IsSettingsViewVisible
        {
            get => _isSettingsViewVisible;
            set => SetProperty(ref _isSettingsViewVisible, value);
        }

        /// <summary>
        /// Title of the application with the current version appended.
        /// </summary>
        public string Title { get => $"YouTube Downloader v{App.AssemblyVersionString}"; }

        /// <summary>
        /// An instance of the <see cref="ViewModels.SearchViewModel"/>.
        /// </summary>
        public SearchViewModel SearchViewModel { get; }

        /// <summary>
        /// An instance of the <see cref="ViewModels.LibraryViewModel"/>.
        /// </summary>
        public LibraryViewModel LibraryViewModel { get; }

        /// <summary>
        /// An instance of the <see cref="ViewModels.VideoPlayerViewModel"/>.
        /// </summary>
        public VideoPlayerViewModel VideoPlayerViewModel { get; }

        /// <summary>
        /// An instance of the <see cref="ViewModels.AboutViewModel"/>.
        /// </summary>
        public AboutViewModel AboutViewModel { get; }

        /// <summary>
        /// An instance of the <see cref="ViewModels.SettingsViewModel"/>.
        /// </summary>
        public SettingsViewModel SettingsViewModel { get; }

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
        /// Initializes a new instance of the <see cref="MainViewModel"/> class with the specified
        /// <paramref name="viewModelFactory"/>.
        /// </summary>
        /// <param name="viewModelFactory">The factory implementation that is used to instantiate new
        /// ViewModels.</param>
        public MainViewModel(IViewModelFactory viewModelFactory)
        {
            // Instaniate ViewModel factory
            _viewModelFactory = viewModelFactory;

            // Instantiate ViewModels
            _sessionContext = _viewModelFactory.GetSessionContext();
            SearchViewModel = _viewModelFactory.CreateSearchViewModel();
            LibraryViewModel = _viewModelFactory.CreateLibraryViewModel();
            VideoPlayerViewModel = _viewModelFactory.CreateVideoPlayerViewModel();
            AboutViewModel = _viewModelFactory.CreateAboutViewModel();
            SettingsViewModel = _viewModelFactory.CreateSettingsViewModel();

            // Initialize commands
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
            _sessionContext.PropertyChanged += OnSessionContextPropertyChanged; // Bind event handler
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles PropertyChanged events invoked by the <see cref="_sessionContext"/>.
        /// </summary>
        private void OnSessionContextPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_sessionContext.CurrentlyPlaying))
            {
                IsVideoPlayerViewVisible = true;
                IsSearchViewVisible = IsLibraryViewVisible = IsAboutViewVisible = IsSettingsViewVisible = false;
            }
        }

        #endregion
    }
}
