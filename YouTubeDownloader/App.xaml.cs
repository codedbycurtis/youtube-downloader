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
            if (!Directory.Exists(Globals.LIBRARY_FOLDER_PATH)) { Directory.CreateDirectory(Globals.LIBRARY_FOLDER_PATH); }
            if (!Directory.Exists(Globals.MEDIA_STORE_PATH)) { Directory.CreateDirectory(Globals.MEDIA_STORE_PATH); }
            if (!Directory.Exists(Globals.THUMBNAIL_CACHE_PATH)) { Directory.CreateDirectory(Globals.THUMBNAIL_CACHE_PATH); }

            base.OnStartup(e); // Perform default initialization procedures
        }
    }
}
