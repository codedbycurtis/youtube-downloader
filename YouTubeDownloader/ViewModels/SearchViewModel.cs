using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;
using YoutubeExplode.Exceptions;
using YoutubeExplode.Playlists;

namespace YouTubeDownloader
{
    public class SearchViewModel : BaseViewModel
    {
        #region Private Members

        private bool _isBusy;
        private string _searchQuery;
        private readonly HttpClient _httpClient;
        private readonly YoutubeClient _youtubeClient;
        private IReadOnlyList<PlaylistVideo> _requestedVideos;

        #endregion

        #region Public Properties

        /// <summary>
        /// Is the application currently busy (e.g. getting videos).
        /// </summary>
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                SetProperty(ref _isBusy, value);
                NotifyPropertyChanged(nameof(RequestedVideosVisibility));
                NotifyPropertyChanged(nameof(LoadingScreenVisibility));
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
        /// The visibility of the <see cref="RequestedVideos"/>, depending on whether or not the application is currently busy.
        /// </summary>
        public Visibility RequestedVideosVisibility
        {
            get
            {
                if (IsBusy) { return Visibility.Hidden; }
                else { return Visibility.Visible; }
            }
        }

        /// <summary>
        /// The visibility of the loading screen, depending on whether or not the application is currently busy.
        /// </summary>
        public Visibility LoadingScreenVisibility
        {
            get
            {
                if (IsBusy) { return Visibility.Visible; }
                else { return Visibility.Hidden; }
            }
        }

        /// <summary>
        /// A list of videos matching the user's specified <see cref="SearchQuery"/>.
        /// </summary>
        public IReadOnlyList<PlaylistVideo> RequestedVideos
        {
            get => _requestedVideos;
            set => SetProperty(ref _requestedVideos, value);
        }

        #endregion

        #region Public Commands

        public ICommand SearchCommand { get; set; }
        public ICommand DownloadCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public SearchViewModel()
        {
            _httpClient = new HttpClient();
            _youtubeClient = new YoutubeClient(_httpClient);
            SearchCommand = new RelayCommand(async () =>
            {
                try { await VideoSearchButtonClicked(); }
                catch (TransientFailureException)
                {
                    IsBusy = false;
                }
            });
            DownloadCommand = new RelayCommand<PlaylistVideo>(async (video) => await VideoDownloadButtonClicked(video));
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Searches for videos based on the specified <see cref="SearchQuery"/>, and downloads them to <see cref="RequestedVideos"/>.
        /// </summary>
        private async Task VideoSearchButtonClicked()
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

        /// <summary>
        /// Downloads the specified <see cref="PlaylistVideo"/> and saves it to the user's <see cref="Globals.Library"/>.
        /// </summary>
        /// <param name="video"></param>
        private async Task VideoDownloadButtonClicked(PlaylistVideo video)
        {
            // Ensure the video has not been downloaded previously
            if (Globals.Library.Any(videoMetadata => videoMetadata.VideoId == video.Id)) { return; }

            // Set the application to the busy state
            IsBusy = true;

            // Download the highest-quality muxed video stream
            var streamManifest = await _youtubeClient.Videos.Streams.GetManifestAsync(video.Id);
            var muxedStreamInfo = streamManifest.GetMuxed();
            var videoStreamInfo = muxedStreamInfo.WithHighestVideoQuality();
            await _youtubeClient.Videos.Streams.DownloadAsync(videoStreamInfo, $"{Globals.VideoFolderPath}\\{video.Id.Value}.mp4");

            // Download the thumbnail of the requested video
            var response = await _httpClient.GetAsync(video.Thumbnails.MediumResUrl);
            using (var stream = new FileStream($"{Globals.ThumbnailFolderPath}\\{video.Id.Value}.jpg", FileMode.Create))
                await response.Content.CopyToAsync(stream);

            // Add the video to the user's library and save it
            Globals.Library.Add(new VideoMetadata(video.Title, video.Author, video.Id.Value, video.Duration));
            Json.Write(Globals.Library, Globals.LibraryFilePath);

            // Set the application to the idle state
            IsBusy = false;
        }

        #endregion
    }
}
