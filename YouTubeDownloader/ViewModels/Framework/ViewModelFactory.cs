using YouTubeDownloader.ViewModels.Components;

namespace YouTubeDownloader.ViewModels.Framework
{
    /// <summary>
    /// Implementation used to retrieve ViewModels that support dependency injection.
    /// </summary>
    public class ViewModelFactory : IViewModelFactory
    {
        #region Fields

        private readonly ISessionContext _sessionContext;

        #endregion

        #region Properties

        public ISessionContext GetSessionContext() => _sessionContext;

        public SearchViewModel CreateSearchViewModel() => new SearchViewModel();

        public LibraryViewModel CreateLibraryViewModel() => new LibraryViewModel(this._sessionContext);

        public VideoPlayerViewModel CreateVideoPlayerViewModel() => new VideoPlayerViewModel(this._sessionContext);

        public AboutViewModel CreateAboutViewModel() => new AboutViewModel();

        public SettingsViewModel CreateSettingsViewModel() => new SettingsViewModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelFactory"/> class with the specified
        /// <paramref name="sessionContext"/>.
        /// </summary>
        /// <param name="sessionContext">A context that is injected to enable ViewModel-messaging.</param>
        public ViewModelFactory(ISessionContext sessionContext)
            => this._sessionContext = sessionContext;

        #endregion
    }
}
