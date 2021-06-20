using YouTubeDownloader.ViewModels.Framework;

namespace YouTubeDownloader.ViewModels.Dialogs
{
    public class ExceptionViewModel : DialogViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ExceptionViewModel"/>.
        /// </summary>
        public ExceptionViewModel(string title, string message) : base(title, message)
        {
            Title = title;
            Message = message;
        }
    }
}
