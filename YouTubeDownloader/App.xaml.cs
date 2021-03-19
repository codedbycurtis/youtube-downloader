using System.IO;
using System.Windows;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace YouTubeDownloader
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// The string representation of the current application's <see cref="System.Version"/>.
        /// </summary>
        public static string AssemblyVersionString { get; } = typeof(App).Assembly.GetName().Version.ToString(3);

        protected override void OnStartup(StartupEventArgs e)
        {
            // Check that required directories exist
            if (!Directory.Exists(Globals.DataFolderPath)) { Directory.CreateDirectory(Globals.DataFolderPath); }
            if (!Directory.Exists(Globals.VideoFolderPath)) { Directory.CreateDirectory(Globals.VideoFolderPath); }
            if (!Directory.Exists(Globals.ThumbnailFolderPath)) { Directory.CreateDirectory(Globals.ThumbnailFolderPath); }

            // Attempts to load the user library from the specified path. . .
            try { Globals.Library = Json.Load<ObservableCollection<LibraryVideo>>(Globals.LibraryFilePath); }

            catch (FileNotFoundException) { } /* . . .and handles any FileNotFoundExceptions.
                                                       * In this instance, a FileNotFoundException simply means that the user does
                                                       * not have a saved video library. We can ignore this.
                                                       */

            // If the library json file cannot be read, it is purged along with all downloaded videos and thumbnails.
            catch (JsonException)
            {
                var result = MessageBox.Show("Library data could not be read - likely due to corruption.\nThe library must be purged.", "Json deserialization exception thrown", MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    File.Delete(Globals.LibraryFilePath);
                    foreach (var video in Directory.GetFiles(Globals.VideoFolderPath)) { File.Delete(video); }
                    foreach (var thumbnail in Directory.GetFiles(Globals.ThumbnailFolderPath)) { File.Delete(thumbnail); }
                }

                else { Application.Current.Shutdown(); }
            }

            finally { base.OnStartup(e); } // Perform default initialisation procedures
        }
    }
}
