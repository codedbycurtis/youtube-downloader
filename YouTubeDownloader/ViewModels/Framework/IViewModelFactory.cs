using YouTubeDownloader.ViewModels.Components;

namespace YouTubeDownloader.ViewModels.Framework
{
    /// <summary>
    /// Abstract factory used to retrieve ViewModels that support dependency injection.
    /// </summary>
    public interface IViewModelFactory
    {
        ISessionContext GetSessionContext();

        SearchViewModel CreateSearchViewModel();

        LibraryViewModel CreateLibraryViewModel();

        VideoPlayerViewModel CreateVideoPlayerViewModel();

        AboutViewModel CreateAboutViewModel();

        SettingsViewModel CreateSettingsViewModel();
    }
}
