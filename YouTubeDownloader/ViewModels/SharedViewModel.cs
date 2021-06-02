using YouTubeDownloader.ViewModels.Framework;
using YouTubeDownloader.Models;

namespace YouTubeDownloader
{
    /// <summary>
    /// Allows properties to be shared across multiple ViewModels.
    /// </summary>
    /// <remarks>An alternative to dependency injection.</remarks>
    public class SharedViewModel : BaseViewModel
    {
        #region Members

        private LibraryVideo _video;

        #endregion

        #region Properties

        public LibraryVideo Video
        {
            get => _video;
            set => SetProperty(ref _video, value);
        }

        #endregion
    }
}
