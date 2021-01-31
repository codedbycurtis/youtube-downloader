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

        #endregion

        #region Public Properties

        /// <summary>
        /// Is the Search tab currently selected.
        /// </summary>
        public bool IsSearchHighlighted
        {
            get => _isSearchHighlighted;
            set => SetProperty(ref _isSearchHighlighted, value);
        }

        /// <summary>
        /// Is the Library tab currently selected.
        /// </summary>
        public bool IsLibraryHighlighted
        {
            get => _isLibraryHighlighted;
            set => SetProperty(ref _isLibraryHighlighted, value);
        }

        /// <summary>
        /// Is the Player tab currently selected.
        /// </summary>
        public bool IsVideoPlayerHighlighted
        {
            get => _isVideoPlayerHighlighted;
            set => SetProperty(ref _isVideoPlayerHighlighted, value);
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
        public SearchViewModel SearchViewModel { get; set; } = new SearchViewModel();

        /// <summary>
        /// An instance of the <see cref="YouTubeDownloader.LibraryViewModel"/>.
        /// </summary>
        public LibraryViewModel LibraryViewModel { get; set; } = new LibraryViewModel();

        #endregion

        #region Commands

        public ICommand SearchTabButton { get; set; }        
        public ICommand LibraryTabButton { get; set; }
        public ICommand PlayerTabButton { get; set; }

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
        }

        #endregion

        #region Helper Methods

        private void SearchButtonClicked()
        {
            IsSearchHighlighted = true;
            IsLibraryHighlighted = false;
            IsVideoPlayerHighlighted = false;
            NotifyTabChanged();
        }

        private void LibraryButtonClicked()
        {
            IsLibraryHighlighted = true;
            IsSearchHighlighted = false;
            IsVideoPlayerHighlighted = false;
            NotifyTabChanged();
        }

        private void PlayerButtonClicked()
        {
            IsVideoPlayerHighlighted = true;
            IsLibraryHighlighted = false;
            IsSearchHighlighted = false;
            NotifyTabChanged();
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
        }

        #endregion
    }
}
