namespace YouTubeDownloader.Services
{
    /// <summary>
    /// Abstracts regular windows from dialog windows.
    /// </summary>
    public interface IDialogWindow
    {
        object DataContext { get; set; }
        bool? DialogResult { get; set; }
        bool? ShowDialog();
    }
}
