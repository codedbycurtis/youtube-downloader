namespace YouTubeDownloader
{
    /// <summary>
    /// Common controls for MediaElements.
    /// </summary>
    public interface IMediaService
    {
        void Play();
        void Pause();
        void FastForward();
        void Rewind();
    }
}
