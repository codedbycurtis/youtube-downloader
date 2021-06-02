using System.Windows.Input;
using System.Collections.Generic;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;
using YouTubeDownloader.ViewModels.Framework;
using YouTubeDownloader.Services;
using YouTubeDownloader.Models;
using YouTubeDownloader.Utils;

namespace YouTubeDownloader.ViewModels.Dialogs
{
    public class VideoDownloadOptionsViewModel : BaseDialogViewModel
    {
        #region Members

        private readonly DownloadService _downloadService = new DownloadService();

        #endregion

        #region Commands

        public ICommand LoadedCommand { get; }
        public ICommand DownloadCommand { get; }
        public ICommand CancelCommand { get; }

        #endregion

        #region Properties

        private bool _muxedOnly;
        public bool MuxedOnly
        {
            get => _muxedOnly;
            set => SetProperty(ref _muxedOnly, value);
        }

        private int _selectedSingleStream;
        public int SelectedSingleStream
        {
            get => _selectedSingleStream;
            set => SetProperty(ref _selectedSingleStream, value);
        }

        private int _selectedAudioStream;
        public int SelectedAudioStream
        {
            get => _selectedVideoStream;
            set => SetProperty(ref _selectedAudioStream, value);
        }

        private int _selectedVideoStream;
        public int SelectedVideoStream
        {
            get => _selectedVideoStream;
            set => SetProperty(ref _selectedVideoStream, value);
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
        /// Initializes a new instance of <see cref="VideoDownloadOptionsViewModel"/> with the specified <paramref name="title"/> and <paramref name="video"/>.
        /// </summary>
        /// <param name="title">The dialog window's title.</param>
        /// <param name="video">The displayed video.</param>
        public VideoDownloadOptionsViewModel(string title, IVideo video) : base(title)
        {
            Video = video;

            LoadedCommand = new RelayCommand(async () =>
            {
                var streamManifest = await Youtube.Client.Videos.Streams.GetManifestAsync(Video.Id);
                SingleStreamInfos = streamManifest.Streams;
            });

            DownloadCommand = new RelayCommand(async () =>
            {
                if (MuxedOnly)
                {
                    await _downloadService.DownloadSingleStreamAsync(Video, SingleStreamInfos[SelectedVideoStream]);
                }
                else
                {
                    await _downloadService.DownloadMultipleStreamAsync(
                        Video,
                        AudioStreamInfos[SelectedAudioStream],
                        VideoStreamInfos[SelectedVideoStream]);
                }

                await _downloadService.DownloadThumbnailAsync(Video);

                Global.Library.Add(
                    new LibraryVideo(
                        Video.Id.Value,
                        Video.Title,
                        Video.Author.Title,
                        Video.Duration.Value));

                Json.Save(Global.Library, Global.LibraryFilePath);
            });

            CancelCommand = new RelayCommand<IDialogWindow>((dialog) =>
            {
                CloseDialog(dialog);
            });
        }

        #endregion
    }
}
