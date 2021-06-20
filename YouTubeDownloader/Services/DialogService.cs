using YouTubeDownloader.ViewModels.Framework;
using YouTubeDownloader.Views.Dialogs;

namespace YouTubeDownloader.Services
{
    /// <summary>
    /// An intermediary service for instantiating dialogs.
    /// </summary>
    public class DialogService
    {
        /// <summary>
        /// Instantiates a new <see cref="DialogWindow"/> and assigns the specified <paramref name="viewModel"/> as it's DataContext.
        /// </summary>
        /// <param name="viewModel">The <see cref="DialogWindow"/>'s DataContext.</param>
        public void OpenDialog(DialogViewModelBase viewModel)
        {
            IDialogWindow dialog = new DialogWindow() { DataContext = viewModel };
            _ = dialog.ShowDialog();
        }
    }
}
