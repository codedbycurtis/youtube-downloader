using System.Collections.Generic;
using System.Windows.Input;
using YoutubeExplode.Videos;
using YouTubeDownloader.ViewModels.Framework;
using YouTubeDownloader.ViewModels.Dialogs;
using YouTubeDownloader.Utils;

namespace YouTubeDownloader
{
    public class SearchViewModel : BaseViewModel
    {
        #region Members

        private bool _isBusy;
        private string _searchQuery;
        private IReadOnlyList<IVideo> _requestedVideos;
        private readonly QueryService _queryService = new QueryService();

        #endregion

        #region Properties

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
        public IReadOnlyList<IVideo> RequestedVideos
        {
            get => _requestedVideos;
            set => SetProperty(ref _requestedVideos, value);
        }

        #endregion

        #region Commands

        public ICommand SearchCommand { get; }
        public ICommand ShowDownloadDialogCommand { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of <see cref="SearchViewModel"/>.
        /// </summary>
        public SearchViewModel()
        {
            SearchCommand = new RelayCommand(async () =>
            {
                if (!string.IsNullOrEmpty(SearchQuery))
                {
                    IsBusy = true;
                    try
                    {
                        if (VideoId.TryParse(SearchQuery).HasValue)
                        {
                            var video = await Youtube.Client.Videos.GetAsync(VideoId.Parse(SearchQuery));
                            IsBusy = false;
                            Dialog.Service.OpenDialog(new VideoDownloadOptionsViewModel(video.Title, video));
                        }
                        else
                        {
                            RequestedVideos = await _queryService.SearchAsync(SearchQuery);
                        }
                    }
                    finally
                    {
                        IsBusy = false;
                    }
                }
            });

            ShowDownloadDialogCommand = new RelayCommand<IVideo>((video) =>
            {
                Dialog.Service.OpenDialog(new VideoDownloadOptionsViewModel(video.Title, video));
            });
        }

        #endregion
    }
}
