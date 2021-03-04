using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;

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
        /// The colour of the Search button, depending on whether or not it is currently selected.
        /// </summary>
        public SolidColorBrush SearchButtonColour
        {
            get
            {
                if (IsSearchViewVisible) { return new SolidColorBrush(Color.FromRgb(51, 51, 51)); }
                return new SolidColorBrush(Colors.Gray);
            }
        }

        /// <summary>
        /// The colour of the Library button, depending on whether or not it is currently selected.
        /// </summary>
        public SolidColorBrush LibraryButtonColour
        {
            get
            {
                if (IsLibraryViewVisible) { return new SolidColorBrush(Color.FromRgb(51, 51, 51)); }
                return new SolidColorBrush(Colors.Gray);
            }
        }

        /// <summary>
        /// The colour of the Player button, depending on whether or not it is currently selected.
        /// </summary>
        public SolidColorBrush PlayerButtonColour
        {
            get
            {
                if (IsVideoPlayerViewVisible) { return new SolidColorBrush(Color.FromRgb(51, 51, 51)); }
                return new SolidColorBrush(Colors.Gray);
            }
        }

        /// <summary>
        /// The colour of the Search button's border, depending on whether or not it is currently selected.
        /// </summary>
        public SolidColorBrush SearchButtonBorderBrush
        {
            get
            {
                if (IsSearchViewVisible) { return new SolidColorBrush(Color.FromRgb(51, 51, 51)); }
                return new SolidColorBrush(Colors.Transparent);
            }
        }

        /// <summary>
        /// The colour of the Library button's border, depending on whether or not it is currently selected.
        /// </summary>
        public SolidColorBrush LibraryButtonBorderBrush
        {
            get
            {
                if (IsLibraryViewVisible) { return new SolidColorBrush(Color.FromRgb(51, 51, 51)); }
                return new SolidColorBrush(Colors.Transparent);
            }
        }

        /// <summary>
        /// The colour of the Player button's border, depending on whether or not it is currently selected.
        /// </summary>
        public SolidColorBrush PlayerButtonBorderBrush
        {
            get
            {
                if (IsVideoPlayerViewVisible) { return new SolidColorBrush(Color.FromRgb(51, 51, 51)); }
                return new SolidColorBrush(Colors.Transparent);
            }
        }

        /// <summary>
        /// An instance of the <see cref="YouTubeDownloader.SearchViewModel"/>.
        /// </summary>
        public SearchViewModel SearchViewModel { get; } = new SearchViewModel();

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
            IsSearchViewVisible = false;

            // Initialize commands
            SearchTabButton = new RelayCommand(() =>
            {
                IsSearchViewVisible = true;
                IsLibraryViewVisible = IsVideoPlayerViewVisible = IsAboutViewVisible = IsSettingsViewVisible = false;
                NotifyViewChanged();
            });

            LibraryTabButton = new RelayCommand(() =>
            {
                IsLibraryViewVisible = true;
                IsSearchViewVisible = IsVideoPlayerViewVisible = IsAboutViewVisible = IsSettingsViewVisible = false;
                NotifyViewChanged();
            });

            PlayerTabButton = new RelayCommand(() =>
            {
                IsVideoPlayerViewVisible = true;
                IsLibraryViewVisible = IsSearchViewVisible = IsAboutViewVisible = IsSettingsViewVisible = false;
                NotifyViewChanged();
            });


            AboutButton = new RelayCommand(() =>
            {
                IsAboutViewVisible = true;
                IsLibraryViewVisible = IsSearchViewVisible = IsVideoPlayerViewVisible = IsSettingsViewVisible = false;
                NotifyViewChanged();
            });

            SettingsButton = new RelayCommand(() =>
            {
                IsSettingsViewVisible = true;
                IsLibraryViewVisible = IsSearchViewVisible = IsVideoPlayerViewVisible = IsAboutViewVisible = false;
                NotifyViewChanged();
            });

            // Initialise event handlers
            _sharedViewModel.PropertyChanged += OnSharedModelPropertyChanged;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Notifies dependent elements that the selected view has been changed.
        /// </summary>
        private void NotifyViewChanged()
        {
            NotifyPropertyChanged(nameof(SearchButtonColour));
            NotifyPropertyChanged(nameof(LibraryButtonColour));
            NotifyPropertyChanged(nameof(PlayerButtonColour));
            NotifyPropertyChanged(nameof(SearchButtonBorderBrush));
            NotifyPropertyChanged(nameof(LibraryButtonBorderBrush));
            NotifyPropertyChanged(nameof(PlayerButtonBorderBrush));
        }

        private void OnSharedModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Video")
            {
                IsVideoPlayerViewVisible = true;
                IsLibraryViewVisible = IsSearchViewVisible = IsAboutViewVisible = IsSettingsViewVisible = false;
                NotifyViewChanged();
            }
        }

        #endregion
    }
}
