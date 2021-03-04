using System.IO;
using System.Windows;
using System.Windows.Input;

namespace YouTubeDownloader
{
    public class LibraryViewModel : BaseViewModel
    {
        #region Private Members

        private SharedViewModel _sharedViewModel;

        #endregion

        #region Public Properties

        #endregion

        #region Public Commands

        public ICommand PlayCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public LibraryViewModel(SharedViewModel sharedViewModel)
        {
            _sharedViewModel = sharedViewModel;
            PlayCommand = new RelayCommand<VideoMetadata>((videoMetadata) => PlayVideoButtonClicked(videoMetadata));
            DeleteCommand = new RelayCommand<VideoMetadata>((videoMetadata) => DeleteVideoButtonClicked(videoMetadata));
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Opens the specified video in the built-in video player.
        /// </summary>
        /// <param name="video">The video to play.</param>
        private void PlayVideoButtonClicked(VideoMetadata video) => _sharedViewModel.Video = video;

        /// <summary>
        /// Deletes the specified video from the <see cref="Globals.Library"/>, and all associated files.
        /// </summary>
        /// <param name="videoMetadata">The video to delete.</param>
        private void DeleteVideoButtonClicked(VideoMetadata videoMetadata)
        {
            Globals.Library.Remove(videoMetadata);
            Json.Write(Globals.Library, Globals.LibraryFilePath);
            File.Delete($"{Globals.VideoFolderPath}\\{videoMetadata.VideoId}.mp4");
            File.Delete($"{Globals.ThumbnailFolderPath}\\{videoMetadata.VideoId}.jpg");
        }

        #endregion
    }
}
