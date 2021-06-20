using System;
using YouTubeDownloader.ViewModels.Framework;

namespace YouTubeDownloader.ViewModels.Components
{
    /// <summary>
    /// Represents an item found in the application's Download Manager.
    /// </summary>
    public class DownloadingItemViewModel : BaseViewModel
    {
        #region Properties

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        
        private string _author;
        public string Author
        {
            get => _author;
            set => SetProperty(ref _author, value);
        }

        private IProgress<double> _progress;
        public IProgress<double> Progress
        {
            get => _progress;
            set => SetProperty(ref _progress, value);
        }
        
        #endregion
        
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadingItemViewModel"/> class.
        /// </summary>
        public DownloadingItemViewModel(
            string title,
            string author,
            IProgress<double> progress)
        {
            this.Title = title;
            this.Author = author;
            this.Progress = progress;
        }
        
        #endregion
    }
}