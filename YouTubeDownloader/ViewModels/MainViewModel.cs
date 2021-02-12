using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace YouTubeDownloader
{
    public class MainViewModel : BaseViewModel
    {
        #region Private Members

        private bool _isSearchHighlighted;
        private bool _isLibraryHighlighted;
        private bool _isVideoPlayerHighlighted;
        private bool _isAboutHighlighted;
        private bool _isSettingsHighlighted;

        #endregion

        #region Public Properties

        /// <summary>
        /// Is the Search tab currently selected.
        /// </summary>
        public bool IsSearchHighlighted
        {
            get => _isSearchHighlighted;
            set
            {
                SetProperty(ref _isSearchHighlighted, value);
                NotifyTabChanged();
            }
        }

        /// <summary>
        /// Is the Library tab currently selected.
        /// </summary>
        public bool IsLibraryHighlighted
        {
            get => _isLibraryHighlighted;
            set
            {
                SetProperty(ref _isLibraryHighlighted, value);
                NotifyTabChanged();
            }
        }

        /// <summary>
        /// Is the Player tab currently selected.
        /// </summary>
        public bool IsVideoPlayerHighlighted
        {
            get => _isVideoPlayerHighlighted;
            set
            {
                SetProperty(ref _isVideoPlayerHighlighted, value);
                NotifyTabChanged();
            }
        }

        /// <summary>
        /// Is the About screen currently selected.
        /// </summary>
        public bool IsAboutHighlighted
        {
            get => _isAboutHighlighted;
            set
            {
                SetProperty(ref _isAboutHighlighted, value);
                NotifyTabChanged();
            }
        }

        /// <summary>
        /// Is the Settings screen currently selected.
        /// </summary>
        public bool IsSettingsHighlighted
        {
            get => _isSettingsHighlighted;
            set
            {
                SetProperty(ref _isSettingsHighlighted, value);
                NotifyTabChanged();
            }
        }

        /// <summary>
        /// The visibility of the Search tab, depending on whether or not it is currently selected.
        /// </summary>
        public Visibility SearchVisibility
        {
            get
            {
                if (IsSearchHighlighted) { return Visibility.Visible; }
                return Visibility.Hidden;
            }
        }

        /// <summary>
        /// The visibility of the Library tab, depending on whether or not it is currently selected.
        /// </summary>
        public Visibility LibraryVisibility
        {
            get
            {
                if (IsLibraryHighlighted) { return Visibility.Visible; }
                return Visibility.Hidden;
            }
        }

        /// <summary>
        /// The visibility of the Player tab, depending on whether or not it is currently selected.
        /// </summary>
        public Visibility VideoPlayerVisibility
        {
            get
            {
                if (IsVideoPlayerHighlighted) { return Visibility.Visible; }
                return Visibility.Hidden;
            }
        }

        /// <summary>
        /// The visibility of the About screen, depending on whether or not it was selected from the drop-down menu.
        /// </summary>
        public Visibility AboutVisibility
        {
            get
            {
                if (IsAboutHighlighted) { return Visibility.Visible; }
                return Visibility.Hidden;
            }
        }

        /// <summary>
        /// The visibility of the Settings screen, depending on whether or not it was selected from the drop-down menu.
        /// </summary>
        public Visibility SettingsVisibility
        {
            get
            {
                if (IsSettingsHighlighted) { return Visibility.Visible; }
                return Visibility.Hidden;
            }
        }

        /// <summary>
        /// The colour of the Search button, depending on whether or not it is currently selected.
        /// </summary>
        public SolidColorBrush SearchButtonColour
        {
            get
            {
                if (IsSearchHighlighted) { return new SolidColorBrush(Color.FromRgb(51, 51, 51)); }
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
                if (IsLibraryHighlighted) { return new SolidColorBrush(Color.FromRgb(51, 51, 51)); }
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
                if (IsVideoPlayerHighlighted) { return new SolidColorBrush(Color.FromRgb(51, 51, 51)); }
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
                if (IsSearchHighlighted) { return new SolidColorBrush(Color.FromRgb(51, 51, 51)); }
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
                if (IsLibraryHighlighted) { return new SolidColorBrush(Color.FromRgb(51, 51, 51)); }
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
                if (IsVideoPlayerHighlighted) { return new SolidColorBrush(Color.FromRgb(51, 51, 51)); }
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
        public LibraryViewModel LibraryViewModel { get; } = new LibraryViewModel();

        /// <summary>
        /// An instance of the <see cref="YouTubeDownloader.VideoPlayerViewModel"/>.
        /// </summary>
        public VideoPlayerViewModel VideoPlayerViewModel { get; } = new VideoPlayerViewModel();

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

        public ICommand SearchTabButton { get; set; }        
        public ICommand LibraryTabButton { get; set; }
        public ICommand PlayerTabButton { get; set; }
        public ICommand AboutButton { get; set; }
        public ICommand SettingsButton { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainViewModel()
        {
            // Initialize commands
            SearchTabButton = new RelayCommand(() => SearchButtonClicked());
            LibraryTabButton = new RelayCommand(() => LibraryButtonClicked());
            PlayerTabButton = new RelayCommand(() => PlayerButtonClicked());
            AboutButton = new RelayCommand(() => AboutButtonClicked());
            SettingsButton = new RelayCommand(() => SettingsButtonClicked());
        }

        #endregion

        #region Helper Methods

        private void SearchButtonClicked()
        {
            IsSearchHighlighted = true;
            IsLibraryHighlighted = IsVideoPlayerHighlighted = IsAboutHighlighted = IsSettingsHighlighted = false;
        }

        private void LibraryButtonClicked()
        {
            IsLibraryHighlighted = true;
            IsSearchHighlighted = IsVideoPlayerHighlighted = IsAboutHighlighted = IsSettingsHighlighted = false;
        }

        private void PlayerButtonClicked()
        {
            IsVideoPlayerHighlighted = true;
            IsLibraryHighlighted = IsSearchHighlighted = IsAboutHighlighted = IsSettingsHighlighted = false;
        }

        private void AboutButtonClicked()
        {
            IsAboutHighlighted = true;
            IsLibraryHighlighted = IsSearchHighlighted = IsVideoPlayerHighlighted = IsSettingsHighlighted = false;
        }

        private void SettingsButtonClicked()
        {
            IsSettingsHighlighted = true;
            IsLibraryHighlighted = IsSearchHighlighted = IsVideoPlayerHighlighted = IsAboutHighlighted = false;
        }

        /// <summary>
        /// Notifies dependent elements that the selected tab has been changed.
        /// </summary>
        private void NotifyTabChanged()
        {
            NotifyPropertyChanged(nameof(SearchButtonColour));
            NotifyPropertyChanged(nameof(LibraryButtonColour));
            NotifyPropertyChanged(nameof(PlayerButtonColour));
            NotifyPropertyChanged(nameof(SearchButtonBorderBrush));
            NotifyPropertyChanged(nameof(LibraryButtonBorderBrush));
            NotifyPropertyChanged(nameof(PlayerButtonBorderBrush));
            NotifyPropertyChanged(nameof(SearchVisibility));
            NotifyPropertyChanged(nameof(LibraryVisibility));
            NotifyPropertyChanged(nameof(VideoPlayerVisibility));
            NotifyPropertyChanged(nameof(AboutVisibility));
            NotifyPropertyChanged(nameof(SettingsVisibility));
        }

        #endregion
    }
}
