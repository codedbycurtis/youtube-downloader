using System.ComponentModel;
using YouTubeDownloader.Models;

namespace YouTubeDownloader.ViewModels.Components
{
    /// <summary>
    /// Abstract interface used for ViewModel-messaging.
    /// </summary>
    public interface ISessionContext : INotifyPropertyChanged
    {
        LibraryVideo CurrentlyPlaying { get; set; }
    }
}
