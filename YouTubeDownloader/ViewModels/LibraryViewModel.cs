using System.IO;
using System.Windows;
using System.Windows.Input;

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
        /// Default constructor
        /// </summary>
        public LibraryViewModel(SharedViewModel sharedViewModel)
        {
            _sharedViewModel = sharedViewModel;
            PlayCommand = new RelayCommand<LibraryVideo>((video) => _sharedViewModel.Video = video);
            DeleteCommand = new RelayCommand<LibraryVideo>((video) =>
            {
                Globals.Library.Remove(video);
                Json.Save(Globals.Library, Globals.LibraryFilePath);
                File.Delete($"{Globals.VideoFolderPath}\\{video.Id}.mp4");
                File.Delete($"{Globals.ThumbnailFolderPath}\\{video.Id}.jpg");
            });
        }

        #endregion
    }
}
