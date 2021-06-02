using System.Threading.Tasks;
using System.Linq;
using System.IO;
using YoutubeExplode.Converter;
using YoutubeExplode.Videos.Streams;
using YoutubeExplode.Videos;
using YouTubeDownloader.Utils;

namespace YouTubeDownloader
{
    /// <summary>
    /// Provides methods of downloading YouTube videos and thumbnails.
    /// </summary>
    public sealed class DownloadService
    {
        /// <summary>
        /// Downloads an already multiplexed or audio/video-only stream.
        /// </summary>
        /// <param name="video">The video to download.</param>
        /// <param name="streamInfo">Multiplexed or audio/video-only stream information.</param>
        public async Task DownloadSingleStreamAsync(IVideo video, IStreamInfo streamInfo)
        {
            if (!Global.Library.Any(o => o.Id == video.Id.Value))
            {
                await Youtube.Client.Videos.Streams.DownloadAsync(
                    streamInfo,
                    $@"{Global.VideoFolderPath}\{video.Id.Value}.{streamInfo.Container.Name}");
            }
        }

        /// <summary>
        /// Downloads separate audio and video streams, and multiplexes them together.
        /// </summary>
        /// <param name="video">The video to download.</param>
        /// <param name="audioStreamInfo">Audio stream information.</param>
        /// <param name="videoStreamInfo">Video stream information.</param>
        /// <returns></returns>
        public async Task DownloadMultipleStreamAsync(IVideo video, IStreamInfo audioStreamInfo, IStreamInfo videoStreamInfo)
        {
            if (!Global.Library.Any(o => o.Id == video.Id.Value))
            {
                var streamInfos = new IStreamInfo[] { audioStreamInfo, videoStreamInfo };

                var conversionRequest = new ConversionRequestBuilder(
                    $@"{Global.VideoFolderPath}\{video.Id.Value}.{videoStreamInfo.Container.Name}")
                    .SetFormat(videoStreamInfo.Container.Name)
                    .SetPreset(ConversionPreset.Medium)
                    .Build();

                await Youtube.Client.Videos.DownloadAsync(streamInfos, conversionRequest);
            }
        }

        /// <summary>
        /// Downloads a video's thumbnail.
        /// </summary>
        /// <param name="video">The video with thumbnail to download.</param>
        public async Task DownloadThumbnailAsync(IVideo video)
        {
            using (var response = await Http.Client.GetAsync(video.Thumbnails
                .OrderByDescending(o => o.Resolution.Area)
                .FirstOrDefault().Url))
            {
                using (var stream = new FileStream($@"{Global.ThumbnailFolderPath}\{video.Id.Value}.jpg", FileMode.Create))
                {
                    await response.Content.CopyToAsync(stream);
                }
            }
        }
    }
}
