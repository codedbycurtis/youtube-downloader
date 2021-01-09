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

        public bool IsSearchHighlighted
        {
            get => _isSearchHighlighted;
            set => SetProperty(ref _isSearchHighlighted, value);
        }

        public bool IsLibraryHighlighted
        {
            get => _isLibraryHighlighted;
            set => SetProperty(ref _isLibraryHighlighted, value);
        }

        public SolidColorBrush SearchButtonContentColour
        {
            get
            {
                if (IsSearchHighlighted) { return new SolidColorBrush(Colors.White); }
                else { return new SolidColorBrush(Colors.DarkGray); }
            }
        }

        public SolidColorBrush LibraryButtonContentColour
        {
            get
            {
                if (IsLibraryHighlighted) { return new SolidColorBrush(Colors.White); }
                else { return new SolidColorBrush(Colors.DarkGray); }
            }
        }

        public double SearchButtonContentDropShadowOpacity 
        {
            get
            {
                if (IsSearchHighlighted) { return 0.6; }
                else { return 0; }
            }
        }

        public double LibraryButtonContentDropShadowOpacity
        {
            get
            {
                if (IsLibraryHighlighted) { return 0.6; }
                else { return 0; }
            }
        }

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

        private void SearchButtonClicked()
        {
            IsSearchHighlighted = true;
            IsLibraryHighlighted = false;
            NotifyButtonClicked();
        }

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

        private void VideoSearchButtonClicked()
        {
            if (string.IsNullOrEmpty(SearchQuery)) { MessageBox.Show("This field cannot be left blank.", "Search Query", MessageBoxButton.OK); }
        }

        #endregion
    }
}
