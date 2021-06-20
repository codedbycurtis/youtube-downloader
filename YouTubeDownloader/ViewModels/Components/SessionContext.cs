using System.Collections.Generic;
using System.ComponentModel;
using YouTubeDownloader.Models;

namespace YouTubeDownloader.ViewModels.Components
{
    /// <summary>
    /// Context that supports ViewModel-messaging.
    /// </summary>
    public class SessionContext : ISessionContext
    {
        #region Fields

        private LibraryVideo _currentlyPlaying;

        #endregion

        #region Properties

        public LibraryVideo CurrentlyPlaying
        {
            get => this._currentlyPlaying;
            set
            {
                if (EqualityComparer<LibraryVideo>.Default.Equals(value)) return;
                this._currentlyPlaying = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.CurrentlyPlaying)));
            }
        }

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
