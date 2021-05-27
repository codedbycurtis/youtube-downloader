using System.Threading.Tasks;
using System.Linq;
using System.IO;
using YoutubeExplode.Converter;
using YoutubeExplode.Videos.Streams;
using YoutubeExplode.Search;
using YouTubeDownloader.Utils;

namespace YouTubeDownloader
{
    /// <summary>
    /// Provides methods of downloading YouTube videos and thumbnails.
    /// </summary>
    public sealed class DownloadService
    {
        /// <summary>
        /// Downloads a video's highest-quality video and audio streams - multiplexing them together.
        /// </summary>
        /// <param name="video">The video to download.</param>
        public async Task DownloadVideoAsync(VideoSearchResult video)
        {
            // Ensure the video has not been downloaded previously or is currently downloading
            if (!Global.Library.Any(o => o.Id == video.Id.Value))
            {
                // Get stream manifest
                var streamManifest = await Youtube.Client.Videos.Streams.GetManifestAsync(video.Id);

                /* Get highest-quality mp4 encoded audio and video streams
                * 
                * Only mp4-encoded video streams are used because playback of webm and other popular codecs is unsupported on
                * MediaElement controls unless the codec is installed on the user's system
                * Playback of these videos would require packaging the necessary codecs with the application, which is
                * unfeasible given how many there are
                */
                // Attempts to get audio-only streams with the same encoding as the video
                var audioStreamInfo = streamManifest.GetAudioStreams().GetWithHighestBitrate();
                var videoStreamInfo = streamManifest.GetVideoStreams().GetWithHighestVideoQuality();
                var streamInfos = new IStreamInfo[] { audioStreamInfo, videoStreamInfo };

                // Use converter extensions to download and mux both streams, and output an mp4 file
                var conversion = new ConversionRequestBuilder($@"{Global.VideoFolderPath}\{video.Id.Value}.mp4")
                    .SetFormat("mp4")
                    .SetPreset(ConversionPreset.Medium)
                    .Build();

                await Youtube.Client.Videos.DownloadAsync(streamInfos, conversion);
            }
        }

        /// <summary>
        /// Downloads a video's thumbnail.
        /// </summary>
        /// <param name="video">The video with the desired thumbnail.</param>
        public async Task DownloadThumbnailAsync(VideoSearchResult video)
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
