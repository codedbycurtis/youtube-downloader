using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace YouTubeDownloader
{
    public class LibraryViewModel : BaseViewModel
    {
        #region Private Members

        private bool _isLibraryPopulated;

        #endregion

        #region Public Properties

        /// <summary>
        /// Is the count of the <see cref="Globals.Library"/> greater than 0.
        /// </summary>
        public bool IsLibraryPopulated
        {
            get => _isLibraryPopulated;
            set
            {
                SetProperty(ref _isLibraryPopulated, value);
                NotifyPropertyChanged(nameof(LibraryContentVisibility));
            }
        }

        /// <summary>
        /// The visibility of the <see cref="Globals.Library"/> content panel, depending on whether or not it is populated.
        /// </summary>
        public Visibility LibraryContentVisibility
        {
            get
            {
                if (IsLibraryPopulated) { return Visibility.Visible; }
                return Visibility.Hidden;
            }
        }

        #endregion

        #region Public Commands

        public ICommand PlayVideoButton { get; set; }
        public ICommand DeleteVideoButton { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public LibraryViewModel()
        {
            PlayVideoButton = new RelayCommand<VideoMetadata>((videoMetadata) => PlayVideoButtonClicked(videoMetadata));
            DeleteVideoButton = new RelayCommand<VideoMetadata>((videoMetadata) => DeleteVideoButtonClicked(videoMetadata));
            UpdateLibraryPopulation();
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Opens the specified video in the default video player.
        /// </summary>
        /// <param name="mediaFile"></param>
        private void PlayVideoButtonClicked(VideoMetadata mediaFile) { Process.Start($"{Globals.VideoFolderPath}\\{mediaFile.VideoId}.mp4"); }

        /// <summary>
        /// Deletes the specified video from the <see cref="Globals.Library"/>, and associated files.\
        /// </summary>
        /// <param name="videoMetadata">The video to delete.</param>
        private void DeleteVideoButtonClicked(VideoMetadata videoMetadata)
        {
            Globals.Library.Remove(videoMetadata);
            Json.Write(Globals.Library, Globals.LibraryFilePath);
            UpdateLibraryPopulation();
            File.Delete($"{Globals.VideoFolderPath}\\{videoMetadata.VideoId}.mp4");
            File.Delete($"{Globals.ThumbnailFolderPath}\\{videoMetadata.VideoId}.jpg");
        }

        /// <summary>
        /// Updates <see cref="Globals.Library"/> population dependent properties.
        /// </summary>
        private void UpdateLibraryPopulation()
        {
            if (Globals.Library.Count > 0) { IsLibraryPopulated = true; }
            else { IsLibraryPopulated = false; }
        }

        #endregion
    }
}
