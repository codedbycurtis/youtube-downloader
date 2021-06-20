using System.IO;
using System.Windows;
using System.Reflection;
using YouTubeDownloader.Utils;
using YouTubeDownloader.Models;
using YouTubeDownloader.ViewModels.Dialogs;
using System.Linq;
using YouTubeDownloader.ViewModels;
using YouTubeDownloader.ViewModels.Framework;
using YouTubeDownloader.ViewModels.Components;

namespace YouTubeDownloader
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Relative path to the library folder.
        /// </summary>
        private const string UserdataFolderPath = "Userdata";

        /// <summary>
        /// Relative path to the video folder.
        /// </summary>
        public const string VideoFolderPath = "Videos";

        /// <summary>
        /// Relative path to the thumbnail folder.
        /// </summary>
        public const string ThumbnailFolderPath = "Thumbnails";
        
        /// <summary>
        /// The relative path to the user library file.
        /// </summary>
        public static string LibraryFilePath => $"{UserdataFolderPath}/library.json";

        /// <summary>
        /// The string representation of the current application's <see cref="System.Version"/>.
        /// </summary>
        public static string AssemblyVersionString => typeof(App).Assembly.GetName().Version.ToString(3);

        /// <summary>
        /// The string representation of the YoutubeExplode API's <see cref="System.Version"/>.
        /// </summary>
        public static string YoutubeExplodeVersionString => AssemblyName.GetAssemblyName("YoutubeExplode.dll").Version.ToString(3);

        /// <summary>
        /// The user's video library.
        /// </summary>
        public static Library VideoLibrary { get; private set; } = new Library();

        /// <summary>
        /// Custom startup procedures that enable dependency injection and more.
        /// </summary>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Displays a custom dialog in the event of an exception
            Application.Current.DispatcherUnhandledException += (_, args) =>
            {
                Dialog.Service.OpenDialog(new ExceptionViewModel("Something went wrong", args.Exception.Message));
                args.Handled = true;
            };

            // Checks that required directories exist
            EnsureRequiredDirectoriesExist();

            // Attempts to load the user's library from the specified path
            if (File.Exists(LibraryFilePath)) { VideoLibrary = Library.Load(); }
            else { VideoLibrary.Save(); }

            // Initialize dependencies for injection
            ISessionContext sessionContext = new SessionContext();
            IViewModelFactory viewModelFactory = new ViewModelFactory(sessionContext);
            var mainViewModel = new MainViewModel(viewModelFactory);
            MainWindow = new MainView() { DataContext = mainViewModel };

            // Show the MainWindow
            MainWindow.Show();
        }

        /// <summary>
        /// Deletes any unused files before shutting down the application.
        /// </summary>
        protected override void OnExit(ExitEventArgs e)
        {
            // Clean directories of temporary files
            DeleteUnusedFiles();
            base.OnExit(e);
        }

        /// <summary>
        /// Checks if required directories exist, and if not, creates them.
        /// </summary>
        private static void EnsureRequiredDirectoriesExist()
        {
            if (!Directory.Exists(UserdataFolderPath)) { _ = Directory.CreateDirectory(UserdataFolderPath); }
            if (!Directory.Exists(VideoFolderPath)) { _ = Directory.CreateDirectory(VideoFolderPath); }
            if (!Directory.Exists(ThumbnailFolderPath)) { _ = Directory.CreateDirectory(ThumbnailFolderPath); }
        }

        /// <summary>
        /// Iterates through all video and image files and deletes any leftovers.
        /// </summary>
        private static void DeleteUnusedFiles()
        {
            var videoFiles = Directory.GetFiles(VideoFolderPath);
            for (int i = 0; i < videoFiles.Length; ++i)
            {
                // Delete video files ending in '.tmp'
                if (videoFiles[i].Substring(videoFiles[i].LastIndexOf('.') + 1) == "tmp") { File.Delete(videoFiles[i]); }

                // Normalize file path
                videoFiles[i] = videoFiles[i].Replace('\\', '/');

                // Get file name
                var id = videoFiles[i].Substring(
                    videoFiles[i].LastIndexOf('/') + 1,
                    videoFiles[i].LastIndexOf('.') - 1 - videoFiles[i].LastIndexOf('/'));

                // Delete the file if it is not in the user's library
                if (!VideoLibrary.Any(o => o.Id == id)) { File.Delete(videoFiles[i]); }
            }

            var imageFiles = Directory.GetFiles(ThumbnailFolderPath);
            for (int i = 0; i < imageFiles.Length; ++i)
            {
                // Normalize file path
                imageFiles[i] = imageFiles[i].Replace('\\', '/');

                // Get file name
                var id = imageFiles[i].Substring(
                    imageFiles[i].LastIndexOf('/') + 1,
                    imageFiles[i].LastIndexOf('.') - 1 - imageFiles[i].LastIndexOf('/'));

                // Delete the file if it is not in the user's library
                if (!VideoLibrary.Any(o => o.Id == id)) { File.Delete(imageFiles[i]); }
            }
        }
    }
}
