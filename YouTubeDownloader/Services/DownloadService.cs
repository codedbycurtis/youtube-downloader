using System.Threading.Tasks;
using System.Linq;
using System.IO;
using YoutubeExplode.Converter;
using YoutubeExplode.Videos.Streams;
using YoutubeExplode.Videos;
using YouTubeDownloader.Utils;
using System.Threading;

namespace YouTubeDownloader.Services
{
    /// <summary>
    /// Provides methods of downloading YouTube videos and thumbnails.
    /// </summary>
    public static class DownloadService
    {
        /// <summary>
        /// Downloads an already multiplexed or audio/video-only stream.
        /// </summary>
        /// <param name="video">The video to download.</param>
        /// <param name="streamInfo">Multiplexed or audio/video-only stream information.</param>
        /// <param name="cancellationToken">Used to cancel asynchronous operations.</param>
        public static async Task DownloadSingleStreamAsync(IVideo video, IStreamInfo streamInfo, CancellationToken cancellationToken)
        {
            await Youtube.Client.Videos.Streams.DownloadAsync(
                streamInfo,
                $@"{App.VideoFolderPath}/{video.Id.Value}.{streamInfo.Container.Name}",
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Downloads separate audio and video streams, and multiplexes them together.
        /// </summary>
        /// <param name="video">The video to download.</param>
        /// <param name="audioStreamInfo">Audio stream information.</param>
        /// <param name="videoStreamInfo">Video stream information.</param>
        /// <param name="cancellationToken">Used to cancel asynchronous operations.</param>
        public static async Task DownloadMultipleStreamsAsync(IVideo video, IStreamInfo audioStreamInfo, IStreamInfo videoStreamInfo, CancellationToken cancellationToken)
        {
            var streamInfos = new IStreamInfo[] { audioStreamInfo, videoStreamInfo };

            var conversionRequest = new ConversionRequestBuilder(
                $@"{App.VideoFolderPath}/{video.Id.Value}.{videoStreamInfo.Container.Name}")
                .SetFormat(videoStreamInfo.Container.Name)
                .SetPreset(ConversionPreset.Medium)
                .Build();

            await Youtube.Client.Videos.DownloadAsync(
                streamInfos,
                conversionRequest,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Downloads a video's thumbnail.
        /// </summary>
        /// <param name="video">The video with thumbnail to download.</param>
        /// <param name="cancellationToken">Used to cancel asynchronous operations.</param>
        public static async Task DownloadThumbnailAsync(IVideo video, CancellationToken cancellationToken)
        {
            using (var response = await Http.Client.GetAsync(
                video.Thumbnails.OrderByDescending(o => o.Resolution.Area).FirstOrDefault().Url,
                cancellationToken))
            {
                using (var stream = new FileStream($@"{App.ThumbnailFolderPath}/{video.Id.Value}.jpg", FileMode.Create))
                {
                    await response.Content.CopyToAsync(stream);
                }
            }
        }
    }
}
