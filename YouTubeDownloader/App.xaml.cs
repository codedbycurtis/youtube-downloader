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
            if (!Directory.Exists(Global.LIBRARY_FOLDER_PATH)) { Directory.CreateDirectory(Global.LIBRARY_FOLDER_PATH); }
            if (!Directory.Exists(Global.MEDIA_STORE_PATH)) { Directory.CreateDirectory(Global.MEDIA_STORE_PATH); }
            if (!Directory.Exists(Global.THUMBNAIL_CACHE_PATH)) { Directory.CreateDirectory(Global.THUMBNAIL_CACHE_PATH); }

            // Instantiates the user library to prevent NullReferenceExceptions
            Global.Library = new List<MediaFile>();

            // Attempts to load the user library from the specified path. . .
            try { Global.Library = (List<MediaFile>)Json.Load<List<MediaFile>>(Global.LIBRARY_PATH); }

            catch (FileNotFoundException) { return; } /* . . .and handles any FileNotFoundExceptions.
                                                       * In this instance, a FileNotFoundException simply means that the user does
                                                       * not have a saved media library. We can ignore this.
                                                       */

            finally { base.OnStartup(e); } // Performs default initialization procedures
        }
    }
}
