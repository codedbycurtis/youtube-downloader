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
            if (!Directory.Exists(Globals.MediaFolderPath)) { Directory.CreateDirectory(Globals.MediaFolderPath); }
            if (!Directory.Exists(Globals.ThumbnailFolderPath)) { Directory.CreateDirectory(Globals.ThumbnailFolderPath); }

            // Attempts to load the user library from the specified path. . .
            try { Globals.Library = (ObservableCollection<MediaFile>)Json.Load<ObservableCollection<MediaFile>>(Globals.LibraryFilePath); }

            catch (FileNotFoundException) { } /* . . .and handles any FileNotFoundExceptions.
                                                       * In this instance, a FileNotFoundException simply means that the user does
                                                       * not have a saved media library. We can ignore this.
                                                       */

            finally { base.OnStartup(e); } // Perform default initialization proceduress
        }
    }
}
