using System.Diagnostics;
using System.Reflection;
using System.Windows.Documents;
using System.Windows.Input;

namespace YouTubeDownloader
{
    public class AboutViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The string representation of the current application's <see cref="AssemblyName.Version"/>.
        /// </summary>
        public string CurrentAssemblyVersion
        {
            get => Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        #endregion

        #region Public Commands

        /// <summary>
        /// Opens the specified <see cref="Hyperlink.NavigateUri"/>.
        /// </summary>
        public ICommand OpenWebUrl { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public AboutViewModel()
        {
            // Initialise commands
            OpenWebUrl = new RelayCommand<Hyperlink>((hyperlink) => { Process.Start(hyperlink.NavigateUri.ToString()); });
        }

        #endregion
    }
}
