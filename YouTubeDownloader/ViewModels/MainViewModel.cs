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
        /// The opacity (visibility) of the Search button's drop shadow, depending on whether or not
        /// it is currently selected.
        /// </summary>
        public double SearchButtonContentDropShadowOpacity
        {
            get
            {
                if (IsSearchHighlighted) { return 0.6; }
                else { return 0; }
            }
        }

        /// <summary>
        /// The opacity (visibility) of the Library button's drop shadow, depending on whether or not
        /// it is currently selected.
        /// </summary>
        public double LibraryButtonContentDropShadowOpacity
        {
            get
            {
                if (IsLibraryHighlighted) { return 0.6; }
                else { return 0; }
            }
        }


        /// <summary>
        /// The visibility of the search tab, depending on whether or not it is currently selected.
        /// </summary>
        public Visibility IsSearchContentVisible
        {
            get
            {
                if (IsSearchHighlighted) { return Visibility.Visible; }
                else { return Visibility.Hidden; }
            }
        }

        /// <summary>
        /// The visibility of the library tab, depending on whether or not it is currently selected.
        /// </summary>
        public Visibility IsLibraryContentVisible
        {
            get
            {
                if (IsLibraryHighlighted) { return Visibility.Visible; }
                else { return Visibility.Hidden; }
            }
        }

        /// <summary>
        /// The colour of the Search button, depending on whether or not it is currently selected.
        /// </summary>
        public SolidColorBrush SearchButtonContentColour
        {
            get
            {
                if (IsSearchHighlighted) { return new SolidColorBrush(Colors.White); }
                else { return new SolidColorBrush(Colors.DarkGray); }
            }
        }

        /// <summary>
        /// The colour of the Library button, depending on whether or not it is currently selected.
        /// </summary>
        public SolidColorBrush LibraryButtonContentColour
        {
            get
            {
                if (IsLibraryHighlighted) { return new SolidColorBrush(Colors.White); }
                else { return new SolidColorBrush(Colors.DarkGray); }
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
        }

        #endregion

        #region Helper Methods

        private void SearchButtonClicked()
        {
            IsSearchHighlighted = true;
            IsLibraryHighlighted = false;
            NotifyTabChanged();
        }

        private void LibraryButtonClicked()
        {
            IsLibraryHighlighted = true;
            IsSearchHighlighted = false;
            NotifyTabChanged();
        }

        /// <summary>
        /// Notifies dependent elements that the selected tab has been changed.
        /// </summary>
        private void NotifyTabChanged()
        {
            NotifyPropertyChanged(nameof(SearchButtonContentColour));
            NotifyPropertyChanged(nameof(LibraryButtonContentColour));
            NotifyPropertyChanged(nameof(SearchButtonContentDropShadowOpacity));
            NotifyPropertyChanged(nameof(LibraryButtonContentDropShadowOpacity));
            NotifyPropertyChanged(nameof(IsSearchContentVisible));
            NotifyPropertyChanged(nameof(IsLibraryContentVisible));
        }

        #endregion
    }
}
