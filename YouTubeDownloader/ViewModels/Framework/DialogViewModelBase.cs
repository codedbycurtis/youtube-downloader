using YouTubeDownloader.Services;

namespace YouTubeDownloader.ViewModels.Framework
{
    public abstract class DialogViewModelBase : BaseViewModel
    {
        public string Title { get; set; }
        public string Message { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="DialogViewModelBase"/> with the specified <paramref name="title"/> and <paramref name="message"/>.
        /// </summary>
        public DialogViewModelBase(string title, string message)
        {
            Title = title;
            Message = message;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="DialogViewModelBase"/> with the specified <paramref name="title"/>.
        /// </summary>
        public DialogViewModelBase(string title) : this(title, "") { }

        /// <summary>
        /// Initializes a new instance of <see cref="DialogViewModelBase"/>.
        /// </summary>
        public DialogViewModelBase() : this("", "") { }

        /// <summary>
        /// Closes the specified <paramref name="dialog"/>.
        /// </summary>
        public void CloseDialog(IDialogWindow dialog)
        {
            dialog.DialogResult = true;
        }
    }
}
