using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using YoutubeExplode.Search;
using YoutubeExplode.Videos;
using YouTubeDownloader.ViewModels.Framework;

namespace YouTubeDownloader
{
    public class SearchViewModel : BaseViewModel
    {
        #region Members

        private bool _isBusy;
        private string _searchQuery;
        private readonly QueryService _queryService = new QueryService();
        private readonly DownloadService _downloadService = new DownloadService();
        private IReadOnlyList<IVideo> _requestedVideos;

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
                        RequestedVideos = await _queryService.SearchAsync(SearchQuery);
                    }
                    catch (Exception ex)
                    {
                        _ = MessageBox.Show(ex.ToString(), "Unexpected exception thrown", MessageBoxButton.OK);
                    }
                    finally
                    {
                        IsBusy = false;
                    }
                }
            });

            ShowDownloadDialogCommand = new RelayCommand(() =>
            {

            });

            //DownloadCommand = new RelayCommand<VideoSearchResult>(async (video) =>
            //{
            //    IsBusy = true;
            //    try
            //    {
            //        await _downloadService.DownloadVideoAsync(video);
            //        await _downloadService.DownloadThumbnailAsync(video);

            //        Global.Library.Add(
            //            new LibraryVideo(
            //                video.Id.Value,
            //                video.Title,
            //                video.Author.Title,
            //                video.Duration.Value));

            //        Json.Save(Global.Library, Global.LibraryFilePath);
            //    }
            //    catch (Exception ex)
            //    {
            //        _ = MessageBox.Show(ex.ToString(), "Unexpected exception thrown", MessageBoxButton.OK);
            //    }
            //    finally
            //    {
            //        IsBusy = false;
            //    }
            //});
        }

        #endregion
    }
}
