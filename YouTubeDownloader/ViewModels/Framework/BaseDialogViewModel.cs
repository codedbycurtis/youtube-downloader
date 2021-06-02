using YouTubeDownloader.Services;

namespace YouTubeDownloader.ViewModels.Framework
{
    public abstract class BaseDialogViewModel : BaseViewModel
    {
        public string Title { get; set; }
        public string Message { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="BaseDialogViewModel"/> with the specified <paramref name="title"/> and <paramref name="message"/>.
        /// </summary>
        public BaseDialogViewModel(string title, string message)
        {
            Title = title;
            Message = message;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="BaseDialogViewModel"/> with the specified <paramref name="title"/>.
        /// </summary>
        public BaseDialogViewModel(string title) : this(title, "") { }

        /// <summary>
        /// Initializes a new instance of <see cref="BaseDialogViewModel"/>.
        /// </summary>
        public BaseDialogViewModel() : this("", "") { }

        /// <summary>
        /// Closes the specified <paramref name="dialog"/>.
        /// </summary>
        public void CloseDialog(IDialogWindow dialog)
        {
            dialog.DialogResult = true;
        }
    }
}
