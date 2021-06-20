using System.IO;
using System.Windows.Input;
using YouTubeDownloader.ViewModels.Framework;
using YouTubeDownloader.Models;
using YouTubeDownloader.ViewModels.Components;

namespace YouTubeDownloader.ViewModels
{
    public class LibraryViewModel : BaseViewModel
    {
        #region Fields

        /// <summary>
        /// Context that allows for ViewModel-messaging.
        /// </summary>
        private readonly ISessionContext _sessionContext;

        #endregion

        #region Commands

        /// <summary>
        /// Plays the specified video.
        /// </summary>
        public ICommand PlayCommand { get; }

        /// <summary>
        /// Deletes the specified video.
        /// </summary>
        public ICommand DeleteCommand { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryViewModel"/> class with the specified <paramref name="sharedViewModel"/>.
        /// </summary>
        public LibraryViewModel(ISessionContext sessionContext)
        {
            _sessionContext = sessionContext;
            PlayCommand = new RelayCommand<LibraryVideo>((video) => _sessionContext.CurrentlyPlaying = video);
            DeleteCommand = new RelayCommand<LibraryVideo>((video) =>
            {
                _ = App.VideoLibrary.Remove(video);
                App.VideoLibrary.Save();
                File.Delete($@"{App.VideoFolderPath}/{video.Id}.mp4");
                File.Delete($@"{App.VideoFolderPath}/{video.Id}.webm");
                File.Delete($@"{App.ThumbnailFolderPath}/{video.Id}.jpg");
            });
        }

        #endregion
    }
}
