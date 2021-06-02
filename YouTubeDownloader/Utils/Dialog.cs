using System;
using YouTubeDownloader.Services;

namespace YouTubeDownloader.Utils
{
    internal static class Dialog
    {
        private static Lazy<DialogService> LazyDialogService = new Lazy<DialogService>();

        internal static DialogService Service => LazyDialogService.Value;
    }
}
