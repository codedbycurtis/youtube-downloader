using System;
using System.ComponentModel;

namespace YouTubeDownloader
{
    /// <summary>
    /// Common controls for a <see cref="System.Windows.Controls.MediaElement"/> and <see cref="System.Windows.Controls.Slider"/>.
    /// </summary>
    public interface IMediaService
    {
        event PropertyChangedEventHandler PropertyChanged;
        bool IsSliderBeingManipulated { get; set; }
        TimeSpan TimeElapsed { get; set; }
        void Play();
        void Pause();
        void Reset();
        void UpdateSlider();
    }
}
