using System.IO;
using System.Windows.Input;
using YouTubeDownloader.ViewModels.Framework;

namespace YouTubeDownloader
{
    public class LibraryViewModel : BaseViewModel
    {
        #region Private Members

        private readonly SharedViewModel _sharedViewModel;

        #endregion

        #region Public Commands

        public ICommand PlayCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initialises a new instance of <see cref="LibraryViewModel"/> with the specified <see cref="SharedViewModel"/>.
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
