using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using YoutubeExplode.Playlists;

namespace YouTubeDownloader
{
    public class SearchViewModel : BaseViewModel
    {
        #region Private Members

        private bool _isBusy;
        private string _searchQuery;
        private readonly QueryService _queryService = new QueryService();
        private readonly DownloadService _downloadService;
        private IReadOnlyList<PlaylistVideo> _requestedVideos;

        #endregion

        #region Public Properties

        /// <summary>
        /// Is the application currently busy (e.g. getting videos).
        /// </summary>
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
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
        /// Initialise a new instance of the <see cref="SearchViewModel"/> with the specified <see cref="DownloadService"/>.
        /// </summary>
        public SearchViewModel(DownloadService downloadService)
        {
            _downloadService = downloadService;

            SearchCommand = new RelayCommand(async () =>
            {
                IsBusy = true;
                try { RequestedVideos = await _queryService.SearchAsync(SearchQuery); }
                catch (Exception ex) { MessageBox.Show(ex.ToString(), "Unexpected exception thrown", MessageBoxButton.OK); }
                finally { IsBusy = false; }
            });

            DownloadCommand = new RelayCommand<PlaylistVideo>(async (video) =>
            {
                IsBusy = true;
                try { await _downloadService.DownloadAsync(video); }
                catch (Exception ex) { MessageBox.Show(ex.ToString(), "Unexpected exception thrown", MessageBoxButton.OK); }
                finally { IsBusy = false; }
            });
        }

        #endregion

        #region Helper Methods

        #endregion
    }
}
