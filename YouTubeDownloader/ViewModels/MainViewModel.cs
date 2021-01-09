using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.Generic;
using YoutubeExplode;

namespace YouTubeDownloader
{
    public class MainViewModel : BaseViewModel
    {
        #region Private Properties

        private bool _isSearchHighlighted;
        private bool _isLibraryHighlighted;
        private YoutubeClient _youtubeClient;
        private string _searchQuery;

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
        /// The search query input by the user.
        /// </summary>
        public string SearchQuery
        {
            get => _searchQuery;
            set => SetProperty(ref _searchQuery, value);
        }

        #endregion

        #region Commands

        public ICommand SearchTabButton { get; set; }        
        public ICommand LibraryTabButton { get; set; }
        public ICommand VideoSearchButton { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainViewModel()
        {
            // Initialize application
            Initialize();

            // Initialize commands
            SearchTabButton = new RelayCommand(() => SearchButtonClicked());
            LibraryTabButton = new RelayCommand(() => LibraryButtonClicked());
            VideoSearchButton = new RelayCommand(() => VideoSearchButtonClicked());
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Performs initialization checks to ensure application continuity.
        /// </summary>
        private void Initialize()
        {
            if (!Directory.Exists(Internal.LIBRARY_CACHE_PATH)) { Directory.CreateDirectory(Internal.LIBRARY_CACHE_PATH); }
            if (!Directory.Exists(Internal.THUMBNAIL_CACHE_PATH)) { Directory.CreateDirectory(Internal.THUMBNAIL_CACHE_PATH); }
            if (!Directory.Exists(Internal.MEDIA_CACHE_PATH)) { Directory.CreateDirectory(Internal.MEDIA_CACHE_PATH); }
            if (!File.Exists(Internal.LIBRARY_PATH)) { Internal.Library = new List<MediaFile>(); }
            else { Serialization.JsonDeserialize(); }
            _youtubeClient = new YoutubeClient();
        }

        /// <summary>
        /// If the Search button is clicked, this method is called by <see cref="SearchTabButton"/>.
        /// </summary>
        private void SearchButtonClicked()
        {
            IsSearchHighlighted = true;
            IsLibraryHighlighted = false;
            NotifyButtonClicked();
        }

        /// <summary>
        /// If the Library button is clicked, this method is called by <see cref="LibraryTabButton"/>.
        /// </summary>
        private void LibraryButtonClicked()
        {
            IsSearchHighlighted = false;
            IsLibraryHighlighted = true;
            NotifyButtonClicked();
        }

        /// <summary>
        /// Notifies dependent elements that an <see cref="ICommand"/> has been invoked.
        /// </summary>
        private void NotifyButtonClicked()
        {
            NotifyPropertyChanged(nameof(SearchButtonContentColour));
            NotifyPropertyChanged(nameof(LibraryButtonContentColour));
            NotifyPropertyChanged(nameof(SearchButtonContentDropShadowOpacity));
            NotifyPropertyChanged(nameof(LibraryButtonContentDropShadowOpacity));
        }

        /// <summary>
        /// If the magnifying glass icon in the video search box is clicked, this method is called by
        /// <see cref="VideoSearchButton"/>.
        /// </summary>
        private void VideoSearchButtonClicked()
        {
            if (string.IsNullOrEmpty(SearchQuery)) { MessageBox.Show("This field cannot be left blank.", "Search Query", MessageBoxButton.OK); }
        }

        #endregion
    }
}
