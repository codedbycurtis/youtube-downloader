using System.Collections.Generic;
using System.Collections.ObjectModel;
using YoutubeExplode.Videos;

namespace YouTubeDownloader
{
    public class DownloadService
    {
        public ObservableCollection<Video> InProgress { get; } = new ObservableCollection<Video>();

        public IReadOnlyList<Video> Search(string query)
        {
            return null;
        }

        public void Download(Video video)
        {
            InProgress.Add(video);
        }
    }
}
