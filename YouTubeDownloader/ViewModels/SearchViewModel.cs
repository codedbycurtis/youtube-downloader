using System;
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

namespace YouTubeDownloader
{
    public class SearchViewModel : BaseViewModel
    {
        #region Private Members

        private bool _isBusy;
        private string _searchQuery;
        private readonly HttpClient _httpClient;
        private readonly YoutubeClient _youtubeClient;
        private IReadOnlyList<Video> _requestedVideos;

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
        /// An instance of the <see cref="YouTubeDownloader.LoadingViewModel"/>.
        /// </summary>
        public LoadingViewModel LoadingViewModel { get; set; } = new LoadingViewModel();

        /// <summary>
        /// A list of videos matching the user's specified <see cref="SearchQuery"/>.
        /// </summary>
        public IReadOnlyList<Video> RequestedVideos
        {
            get => _requestedVideos;
            set => SetProperty(ref _requestedVideos, value);
        }

        #endregion

        #region Public Commands

        public ICommand VideoSearchButton { get; set; }
        public ICommand VideoDownloadButton { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public SearchViewModel()
        {
            _httpClient = new HttpClient();
            _youtubeClient = new YoutubeClient(_httpClient);
            VideoSearchButton = new RelayCommand(async () => await VideoSearchButtonClicked());
            VideoDownloadButton = new RelayCommand<Video>(async (video) => await VideoDownloadButtonClicked(video));
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// If the right-arrow in the search box is clicked, this method is called by <see cref="VideoSearchButton"/>.
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
        /// Downloads the specified <see cref="Video"/> and saves it to the user's <see cref="Globals.Library"/>.
        /// </summary>
        /// <param name="video"></param>
        private async Task VideoDownloadButtonClicked(Video video)
        {
            // Ensure the video has not been downloaded previously
            if (Globals.Library.Any(mediaFile => mediaFile.VideoId == video.Id)) { return; }

            // Set the application to the busy state
            IsBusy = true;

            // Download the highest-quality muxed video stream
            StreamManifest streamManifest = await _youtubeClient.Videos.Streams.GetManifestAsync(video.Id);
            IEnumerable<MuxedStreamInfo> muxedStreamInfo = streamManifest.GetMuxed();
            IVideoStreamInfo videoStreamInfo = muxedStreamInfo.WithHighestVideoQuality();
            await _youtubeClient.Videos.Streams.DownloadAsync(videoStreamInfo, $"{Globals.MediaFolderPath}\\{video.Id.Value}.mp4");

            // Download the thumbnail of the requested video
            var response = await _httpClient.GetAsync(video.Thumbnails.MediumResUrl);
            using (FileStream fileStream = new FileStream($"{Globals.ThumbnailFolderPath}\\{video.Id.Value}.jpg", FileMode.Create))
                await response.Content.CopyToAsync(fileStream);

            // Add the video to the user's library
            this.AddToLibrary(new MediaFile(video.Title, video.Author, video.Id.Value, video.Duration));

            IsBusy = false;
        }

        /// <summary>
        /// Adds a <see cref="MediaFile"/> to the <see cref="Globals.Library"/> and saves it.
        /// </summary>
        /// <param name="toAdd"></param>
        private void AddToLibrary(MediaFile toAdd)
        {
            Globals.Library.Add(toAdd);
            Json.Write(Globals.Library, Globals.LibraryFilePath);
        }

        #endregion
    }
}
