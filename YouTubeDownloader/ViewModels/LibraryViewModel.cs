using System.IO;
using System.Windows.Input;
using YouTubeDownloader.ViewModels.Framework;
using YouTubeDownloader.Utils;
using YouTubeDownloader.Models;

namespace YouTubeDownloader
{
    public class LibraryViewModel : BaseViewModel
    {
        #region Members

        private readonly SharedViewModel _sharedViewModel;

        #endregion

        #region Commands

        public ICommand PlayCommand { get; }
        public ICommand DeleteCommand { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of <see cref="LibraryViewModel"/> with the specified <paramref name="sharedViewModel"/>.
        /// </summary>
        public LibraryViewModel(SharedViewModel sharedViewModel)
        {
            _sharedViewModel = sharedViewModel;

            PlayCommand = new RelayCommand<LibraryVideo>((video) => _sharedViewModel.Video = video);

            DeleteCommand = new RelayCommand<LibraryVideo>((video) =>
            {
                Global.Library.Remove(video);
                Json.Save(Global.Library, Global.LibraryFilePath);
                File.Delete($"{Global.VideoFolderPath}\\{video.Id}.mp4");
                File.Delete($"{Global.ThumbnailFolderPath}\\{video.Id}.jpg");
            });
        }

        #endregion
    }
}
