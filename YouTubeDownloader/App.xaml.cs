using System.IO;
using System.Windows;
using System.Collections.ObjectModel;

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
            if (!Directory.Exists(Globals.VideoFolderPath)) { Directory.CreateDirectory(Globals.VideoFolderPath); }
            if (!Directory.Exists(Globals.ThumbnailFolderPath)) { Directory.CreateDirectory(Globals.ThumbnailFolderPath); }
            if (!Directory.Exists(Globals.TempFolderPath)) { Directory.CreateDirectory(Globals.TempFolderPath); }

            // Attempts to load the user library from the specified path. . .
            try { Globals.Library = (ObservableCollection<VideoMetadata>)Json.Load<ObservableCollection<VideoMetadata>>(Globals.LibraryFilePath); }

            catch (FileNotFoundException) { } /* . . .and handles any FileNotFoundExceptions.
                                                       * In this instance, a FileNotFoundException simply means that the user does
                                                       * not have a saved video library. We can ignore this.
                                                       */

            finally { base.OnStartup(e); } // Perform default initialization proceduress
        }
    }
}
