using System.IO;
using System.Windows;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Windows.Controls;
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

        /// <summary>
        /// The string representation of the YoutubeExplode API's <see cref="System.Version"/>.
        /// </summary>
        public static string YoutubeExplodeVersionString { get; } = AssemblyName.GetAssemblyName("YoutubeExplode.dll").Version.ToString(3);

        protected override void OnStartup(StartupEventArgs e)
        {
            // Check that required directories exist
            EnsureRequiredDirectoriesExist();

            // Attempts to load the user library from the specified path. . .
            try { Global.Library = Json.Load<ObservableCollection<LibraryVideo>>(Global.LibraryFilePath); }

            catch (FileNotFoundException) { } /* . . .and handles any FileNotFoundExceptions.
                                                       * In this instance, a FileNotFoundException simply means that the user does
                                                       * not have a saved video library. We can ignore this.
                                                       */

            // If the library json file cannot be read, it is purged along with all downloaded videos and thumbnails.
            catch (JsonException)
            {
                var result = MessageBox.Show("Library data could not be read.\nContinuing will result in the deletion of all downloaded videos.", "Json deserialization exception thrown", MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    Directory.Delete(Global.DataFolderPath, true);
                    Directory.Delete(Global.VideoFolderPath, true);
                    Directory.Delete(Global.ThumbnailFolderPath, true);
                    EnsureRequiredDirectoriesExist();
                }

                else { Application.Current.Shutdown(); }
            }

            finally // Perform default initialisation procedures
            {
                // Ensures that ToolTip controls don't close automatically after 5 seconds
                ToolTipService.ShowDurationProperty.OverrideMetadata(typeof(DependencyObject), new FrameworkPropertyMetadata(int.MaxValue));
                base.OnStartup(e);
            }
        }

        /// <summary>
        /// Checks if required directories exist, and if not, creates them.
        /// </summary>
        private void EnsureRequiredDirectoriesExist()
        {
            if (!Directory.Exists(Global.DataFolderPath)) { _ = Directory.CreateDirectory(Global.DataFolderPath); }
            if (!Directory.Exists(Global.VideoFolderPath)) { _ = Directory.CreateDirectory(Global.VideoFolderPath); }
            if (!Directory.Exists(Global.ThumbnailFolderPath)) { _ = Directory.CreateDirectory(Global.ThumbnailFolderPath); }
        }
    }
}
