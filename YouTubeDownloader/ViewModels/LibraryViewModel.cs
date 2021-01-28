using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;

namespace YouTubeDownloader
{
    public class LibraryViewModel : BaseViewModel
    {
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
            PlayVideoButton = new RelayCommand<MediaFile>((mediaFile) => PlayVideoButtonClicked(mediaFile));
            DeleteVideoButton = new RelayCommand<MediaFile>((mediaFile) => DeleteVideoButtonClicked(mediaFile));
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Opens the specified video in the default video player.
        /// </summary>
        /// <param name="mediaFile"></param>
        private void PlayVideoButtonClicked(MediaFile mediaFile) { Process.Start($"{Globals.MediaFolderPath}\\{mediaFile.VideoId}.mp4"); }

        /// <summary>
        /// Deletes the specified video from the <see cref="Globals.Library"/>, and associated files.
        /// </summary>
        /// <param name="mediaFile">The video to delete.</param>
        private void DeleteVideoButtonClicked(MediaFile mediaFile)
        {
            Globals.Library.Remove(mediaFile);
            Json.Write(Globals.Library, Globals.LibraryFilePath);
            File.Delete($"{Globals.MediaFolderPath}\\{mediaFile.VideoId}.mp4");
            File.Delete($"{Globals.ThumbnailFolderPath}\\{mediaFile.VideoId}.jpg");
        }

        #endregion
    }
}
