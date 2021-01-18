using System.IO;
using System.Collections.Generic;
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
            if (!Directory.Exists(Internal.LIBRARY_FOLDER_PATH)) { Directory.CreateDirectory(Internal.LIBRARY_FOLDER_PATH); }
            if (!Directory.Exists(Internal.MEDIA_STORE_PATH)) { Directory.CreateDirectory(Internal.MEDIA_STORE_PATH); }
            if (!Directory.Exists(Internal.THUMBNAIL_CACHE_PATH)) { Directory.CreateDirectory(Internal.THUMBNAIL_CACHE_PATH); }

            // Instantiates the user library to prevent NullReferenceExceptions
            Internal.Library = new List<MediaFile>();

            // Attempts to load the user library from the specified path. . .
            try { Internal.Library = (List<MediaFile>)Json.Load(Internal.LIBRARY_PATH); }

            catch (FileNotFoundException) { return; } /* . . .and handles any FileNotFoundExceptions.
                                                       * In this instance, a FileNotFoundException simply means that the user does
                                                       * not have a saved media library. We can ignore this.
                                                       */

            finally { base.OnStartup(e); } // Performs default initialization procedures
        }
    }
}
