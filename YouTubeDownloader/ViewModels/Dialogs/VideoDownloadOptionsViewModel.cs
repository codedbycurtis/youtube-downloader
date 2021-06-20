using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;
using YouTubeDownloader.ViewModels.Framework;
using YouTubeDownloader.Services;
using YouTubeDownloader.Models;
using YouTubeDownloader.Utils;

namespace YouTubeDownloader.ViewModels.Dialogs
{
    public class VideoDownloadOptionsViewModel : DialogViewModelBase
    {
        #region Fields

        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        #endregion

        #region Commands

        /// <summary>
        /// Downloads the specified <see cref="Video"/>.
        /// </summary>
        public ICommand DownloadCommand { get; }

        /// <summary>
        /// Cancels the download (if there is one) and closes the window.
        /// </summary>
        public ICommand CancelCommand { get; }

        #endregion

        #region Properties

        private bool _separateStreams;
        public bool SeparateStreams
        {
            get => _separateStreams;
            set => SetProperty(ref _separateStreams, value, UpdateFileSize);
        }

        private int _selectedSingleStream;
        public int SelectedSingleStream
        {
            get => _selectedSingleStream;
            set => SetProperty(ref _selectedSingleStream, value, UpdateFileSize);
        }

        private int _selectedAudioStream;
        public int SelectedAudioStream
        {
            get => _selectedAudioStream;
            set => SetProperty(ref _selectedAudioStream, value, UpdateFileSize);
        }

        private int _selectedVideoStream;
        public int SelectedVideoStream
        {
            get => _selectedVideoStream;
            set => SetProperty(ref _selectedVideoStream, value, UpdateFileSize);
        }

        private string _estimatedFileSize;
        public string EstimatedFileSize
        {
            get => _estimatedFileSize;
            set => SetProperty(ref _estimatedFileSize, value);
        }

        private IVideo _video;
        public IVideo Video
        {
            get => _video;
            set => SetProperty(ref _video, value);
        }

        private IReadOnlyList<IStreamInfo> _singleStreamInfos;
        public IReadOnlyList<IStreamInfo> SingleStreamInfos
        {
            get => _singleStreamInfos;
            set => SetProperty(ref _singleStreamInfos, value);
        }

        private IReadOnlyList<IStreamInfo> _audioStreamInfos;
        public IReadOnlyList<IStreamInfo> AudioStreamInfos
        {
            get => _audioStreamInfos;
            set => SetProperty(ref _audioStreamInfos, value);
        }

        private IReadOnlyList<IStreamInfo> _videoStreamInfos;
        public IReadOnlyList<IStreamInfo> VideoStreamInfos
        {
            get => _videoStreamInfos;
            set => SetProperty(ref _videoStreamInfos, value);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of <see cref="VideoDownloadOptionsViewModel"/> class with the specified <paramref name="title"/> and <paramref name="video"/>.
        /// </summary>
        /// <param name="title">The dialog window's title.</param>
        /// <param name="video">The displayed video.</param>
        private VideoDownloadOptionsViewModel(string title, IVideo video) : base(title)
        {
            Video = video;

            DownloadCommand = new RelayCommand<IDialogWindow>(async (dialog) =>
            {
                // Ensure the video has not already been downloaded
                if (!App.VideoLibrary.Any(o => o.Id == Video.Id.Value))
                {
                    string container;
                    Task videoTask;
                    if (SeparateStreams)
                    {
                        container = VideoStreamInfos[SelectedVideoStream].Container.Name;
                        videoTask = DownloadService.DownloadMultipleStreamsAsync(
                            Video,
                            AudioStreamInfos[SelectedAudioStream],
                            VideoStreamInfos[SelectedVideoStream],
                            _cancellationTokenSource.Token);
                    }
                    else
                    {
                        container = SingleStreamInfos[SelectedSingleStream].Container.Name;
                        videoTask = DownloadService.DownloadSingleStreamAsync(
                            Video,
                            SingleStreamInfos[SelectedSingleStream],
                            _cancellationTokenSource.Token);
                    }

                    var thumbnailTask = DownloadService.DownloadThumbnailAsync(
                        Video,
                        _cancellationTokenSource.Token);

                    try
                    {
                        await Task.WhenAll(videoTask, thumbnailTask);
                        App.VideoLibrary.Add(
                            new LibraryVideo(
                                container,
                                Video.Id.Value,
                                Video.Title,
                                Video.Author.Title,
                                Video.Duration.Value));
                        App.VideoLibrary.Save();
                        CloseDialog(dialog);
                    }
                    catch (TaskCanceledException) { } // Task was cancelled,
                                                      // no actions need to be taken here
                }
            });

            CancelCommand = new RelayCommand<IDialogWindow>((dialog) =>
            {
                _cancellationTokenSource.Cancel();
                CloseDialog(dialog);
            });
        }

        /// <summary>
        /// Asynchronously constructs a new <see cref="VideoDownloadOptionsViewModel"/>.
        /// </summary>
        /// <param name="title">The dialog window's title.</param>
        /// <param name="video">The displayed video.</param>
        /// <returns>A new instance of the <see cref="VideoDownloadOptionsViewModel"/> class.</returns>
        public static async Task<VideoDownloadOptionsViewModel> Create(string title, IVideo video)
        {            
            var streamManifest = await Youtube.Client.Videos.Streams.GetManifestAsync(video.Id);
            var muxedStreamInfos = streamManifest.Streams.Where(o => o.GetType() == typeof(MuxedStreamInfo)).ToList();
            var audioOnlyStreamInfos = streamManifest.Streams.Where(o => o.GetType() == typeof(AudioOnlyStreamInfo));
            var videoOnlyStreamInfos = streamManifest.Streams.Where(o => o.GetType() == typeof(VideoOnlyStreamInfo));
            var videoDownloadOptionsViewModel = new VideoDownloadOptionsViewModel(title, video)
            {
                SingleStreamInfos = streamManifest.Streams,
                AudioStreamInfos = muxedStreamInfos.Concat(audioOnlyStreamInfos).ToList(),
                VideoStreamInfos = muxedStreamInfos.Concat(videoOnlyStreamInfos).ToList()
            };
            videoDownloadOptionsViewModel.UpdateFileSize();
            return videoDownloadOptionsViewModel;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Updates the <see cref="EstimatedFileSize"/>.
        /// </summary>
        private void UpdateFileSize()
        {
            EstimatedFileSize = SeparateStreams ? $"{Math.Round(VideoStreamInfos[SelectedVideoStream].Size.MegaBytes + AudioStreamInfos[SelectedAudioStream].Size.MegaBytes)}"
                : $"{Math.Round(SingleStreamInfos[SelectedSingleStream].Size.MegaBytes)}";
            if (EstimatedFileSize == "0") { EstimatedFileSize = "<1"; }
        }

        #endregion
    }
}
