using System.Diagnostics;
using System.Windows.Documents;
using System.Windows.Input;
using YouTubeDownloader.ViewModels.Framework;

namespace YouTubeDownloader
{
    public class AboutViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The string representation of the current application's <see cref="System.Version"/>.
        /// </summary>
        public string AssemblyVersionString { get => App.AssemblyVersionString; }

        /// <summary>
        /// The string representation of the YoutubeExplode API's <see cref="System.Version"/>.
        /// </summary>
        public string YoutubeExplodeVersionString { get => App.YoutubeExplodeVersionString; }

        #endregion

        #region Public Commands

        /// <summary>
        /// Opens the specified <see cref="Hyperlink.NavigateUri"/>.
        /// </summary>
        public ICommand OpenWebUrlCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public AboutViewModel()
        {
            // Initialise commands
            OpenWebUrlCommand = new RelayCommand<string>((url) => { Process.Start(url); });
        }

        #endregion
    }
}
