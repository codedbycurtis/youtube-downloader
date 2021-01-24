using System.IO;
using System.Windows;

namespace YouTubeDownloader
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        { 
            // Check that required directories exist
            if (!Directory.Exists(Globals.LibraryFolderPath)) { Directory.CreateDirectory(Globals.LibraryFolderPath); }
            if (!Directory.Exists(Globals.MediaFolderPath)) { Directory.CreateDirectory(Globals.MediaFolderPath); }
            if (!Directory.Exists(Globals.ThumbnailFolderPath)) { Directory.CreateDirectory(Globals.ThumbnailFolderPath); }

            base.OnStartup(e); // Perform default initialization procedures
        }
    }
}
