using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using YoutubeExplode;
using YoutubeExplode.Playlists;
using YoutubeExplode.Converter;
using YoutubeExplode.Videos.Streams;

namespace YouTubeDownloader
{
    public sealed class DownloadService
    {
        private readonly YoutubeClient _youtubeClient;
        private readonly HttpClient _httpClient = new HttpClient();

        /// <summary>
        /// Downloads currently in progress.
        /// </summary>
        public ObservableCollection<VideoDownload> InProgress { get; } = new ObservableCollection<VideoDownload>();

        /// <summary>
        /// Default constructor
        /// </summary>
        public DownloadService()
        {
            _youtubeClient = new YoutubeClient(_httpClient);
        }

        /// <summary>
        /// Downloads a video's thumbnail, and highest-quality video and audio streams - multiplexing them together.
        /// </summary>
        /// <param name="video"></param>
        /// <returns></returns>
        public async Task DownloadAsync(PlaylistVideo video)
        {
            // Ensure the video has not been downloaded previously or is currently downloading
            if (!Globals.Library.Any(x => x.Id == video.Id.Value) &&
                !InProgress.Any(x => x.VideoId == video.Id.Value))
            {
                var videoDownload = video.ToVideoDownload();

                try
                {
                    // Ensures the video is marked as in progress
                    InProgress.Add(videoDownload);

                    // Get stream manifest
                    var streamManifest = await _youtubeClient.Videos.Streams.GetManifestAsync(video.Id);

                    /* Get highest-quality mp4 encoded audio and video streams
                     * 
                     * Only mp4-encoded video streams are used because playback of webm and other popular codecs is unsupported on
                     * MediaElement controls unless the codec is installed on the user's system
                     * Playback of these videos would require packaging the necessary codecs with the application, which is
                     * unfeasible given how many there are
                     */
                    // Attempts to get audio-only streams with the same encoding as the video
                    var audioStreamInfos = streamManifest.GetAudioOnly().Where(o => o.Container.Name == "mp4") ?? streamManifest.GetAudioOnly();
                    var audioStreamInfo = audioStreamInfos.WithHighestBitrate();
                    var videoStreamInfo = streamManifest.GetVideoOnly().Where(o => o.Container.Name == "mp4").WithHighestVideoQuality();
                    var streamInfos = new IStreamInfo[] { audioStreamInfo, videoStreamInfo };

                    // Use converter extensions to download and mux both streams, and output an mp4 file
                    var conversion = new ConversionRequestBuilder($"{Globals.VideoFolderPath}\\{video.Id.Value}.mp4")
                        .SetFormat("mp4")
                        .SetPreset(ConversionPreset.Medium)
                        .Build();
                    await _youtubeClient.Videos.DownloadAsync(streamInfos, conversion);

                    // Download the thumbnail of the requested video
                    var response = await _httpClient.GetAsync(video.Thumbnails.MediumResUrl);
                    using (var stream = new FileStream($"{Globals.ThumbnailFolderPath}\\{video.Id.Value}.jpg", FileMode.Create))
                        await response.Content.CopyToAsync(stream);

                    // Add the video to the user's library and save it
                    Globals.Library.Add(new LibraryVideo(video.Id.Value, video.Title, video.Author, video.Duration));
                    Json.Save(Globals.Library, Globals.LibraryFilePath);
                }
                
                finally { InProgress.Remove(videoDownload); }
            }
        }
    }
}
