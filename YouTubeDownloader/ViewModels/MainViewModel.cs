﻿using System;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;
using System.Net.Http;
using System.IO;
using System.Collections.ObjectModel;
using System.Linq;
using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace YouTubeDownloader
{
    public class MainViewModel : BaseViewModel
    {
        #region Private Members

        private bool _isSearchHighlighted;
        private bool _isLibraryHighlighted;
        private bool _isBusy;
        private string _searchQuery;
        private readonly YoutubeClient _youtubeClient;
        private IReadOnlyList<Video> _requestedVideos;
        private readonly HttpClient _httpClient;

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
        /// A list of videos matching the user's specified <see cref="SearchQuery"/>.
        /// </summary>
        public IReadOnlyList<Video> RequestedVideos
        {
            get => _requestedVideos;
            set => SetProperty(ref _requestedVideos, value);
        }

        /// <summary>
        /// The user's <see cref="MediaFile"/> library.
        /// </summary>
        public ObservableCollection<MediaFile> Library { get; set; } = new ObservableCollection<MediaFile>();

        #endregion

        #region Commands

        public ICommand SearchTabButton { get; set; }        
        public ICommand LibraryTabButton { get; set; }
        public ICommand VideoSearchButton { get; set; }
        public ICommand VideoDownloadButton { get; set; }
        public ICommand PlayVideoButton { get; set; }

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
            PlayVideoButton = new RelayCommand<string>((path) => PlayVideoButtonClicked(path));

            // Initialize members
            _httpClient = new HttpClient();
            _youtubeClient = new YoutubeClient(_httpClient);

            // Initialize properties
            IsBusy = false;

            // Attempts to load the user library from the specified path. . .
            try { this.Library = (ObservableCollection<MediaFile>)Json.Load<ObservableCollection<MediaFile>>(Globals.LIBRARY_PATH); }

            catch (FileNotFoundException) { return; } /* . . .and handles any FileNotFoundExceptions.
                                                       * In this instance, a FileNotFoundException simply means that the user does
                                                       * not have a saved media library. We can ignore this.
                                                       */
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
            NotifyPropertyChanged(nameof(IsSearchContentVisible));
            NotifyPropertyChanged(nameof(IsLibraryContentVisible));
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

        /// <summary>
        /// Downloads the specified <see cref="Video"/> and saves it to the user's <see cref="Globals.Library"/>.
        /// </summary>
        /// <param name="video"></param>
        private async void VideoDownloadButtonClicked(Video video)
        {
            if (Library.Any(mediaFile => mediaFile.VideoId == video.Id)) { return; }
            IsBusy = true;
            string title = video.Title;
            string uploader = video.Author;
            string videoId = video.Id.Value;
            TimeSpan duration = video.Duration;
            StreamManifest streamManifest = await _youtubeClient.Videos.Streams.GetManifestAsync(video.Id);
            IEnumerable<MuxedStreamInfo>  muxedStreamInfo = streamManifest.GetMuxed();
            IVideoStreamInfo videoStreamInfo = muxedStreamInfo.WithHighestVideoQuality();
            await _youtubeClient.Videos.Streams.DownloadAsync(videoStreamInfo, $"{Globals.MEDIA_STORE_PATH}\\{video.Id.Value}.mp4");
            // http client downloads the video's thumbnail
            var response = await _httpClient.GetAsync(video.Thumbnails.MediumResUrl);
            using (FileStream fileStream = new FileStream($"{Globals.THUMBNAIL_CACHE_PATH}\\{video.Id.Value}.jpg", FileMode.Create)) 
            { await response.Content.CopyToAsync(fileStream); }
            this.AddToLibrary(new MediaFile(title, uploader, videoId, duration));
            IsBusy = false;
            
        }

        private void PlayVideoButtonClicked(string path)
        {

        }

        /// <summary>
        /// Adds a <see cref="MediaFile"/> to the <see cref="Library"/> and saves it.
        /// </summary>
        /// <param name="toAdd"></param>
        private void AddToLibrary(MediaFile toAdd)
        {
            this.Library.Add(toAdd);
            Json.Write(this.Library, Globals.LIBRARY_PATH);
        }

        #endregion
    }
}
