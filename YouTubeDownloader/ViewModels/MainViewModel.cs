using System.Windows;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;
using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;
using System.Windows.Controls;

namespace YouTubeDownloader
{
    public class MainViewModel : BaseViewModel
    {
        #region Private Members

        private bool _isSearchHighlighted;
        private bool _isLibraryHighlighted;
        private bool _isBusy;
        private string _searchQuery;
        private YoutubeClient _youtubeClient;
        private IReadOnlyList<Video> _requestedVideos;

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
        /// Is the application currently busy (e.g. getting videos).
        /// </summary>
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
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
        /// A list of videos matching the user's specified <see cref="SearchQuery"/>.
        /// </summary>
        public IReadOnlyList<Video> RequestedVideos
        {
            get => _requestedVideos;
            set => SetProperty(ref _requestedVideos, value);
        }

        #endregion

        #region Commands

        public ICommand SearchTabButton { get; set; }        
        public ICommand LibraryTabButton { get; set; }
        public ICommand VideoSearchButton { get; set; }
        public ICommand VideoDownloadButton { get; set; }

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
            VideoSearchButton = new RelayCommand(() => VideoSearchButtonClicked());
            VideoDownloadButton = new RelayCommand<Video>((video) => VideoDownloadButtonClicked(video));

            // Initialize members
            _youtubeClient = new YoutubeClient();
        }

        #endregion

        #region Helper Methods

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
        /// If the right-arrow in the search box is clicked, this method is called by <see cref="VideoSearchButton"/>.
        /// </summary>
        private async void VideoSearchButtonClicked()
        {
            if (string.IsNullOrEmpty(SearchQuery)) 
            {
                MessageBox.Show("This field cannot be left blank.", "Search Query", MessageBoxButton.OK);
                return;
            }
            IsBusy = true;
            RequestedVideos = await _youtubeClient.Search.GetVideosAsync(SearchQuery);
            IsBusy = false;
        }

        private async void VideoDownloadButtonClicked(Video video)
        {
            StreamManifest streamManifest;
            IEnumerable<MuxedStreamInfo> muxedStreamInfo;
            IVideoStreamInfo videoStreamInfo;
            streamManifest = await _youtubeClient.Videos.Streams.GetManifestAsync(video.Id);
            muxedStreamInfo = streamManifest.GetMuxed();
            videoStreamInfo = muxedStreamInfo.WithHighestVideoQuality();
            await _youtubeClient.Videos.Streams.DownloadAsync(videoStreamInfo, $"{Internal.MEDIA_STORE_PATH}\\{video.Title}.mp4");
        }

        #endregion
    }
}
