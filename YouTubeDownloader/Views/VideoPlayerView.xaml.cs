using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace YouTubeDownloader
{
    /// <summary>
    /// Interaction logic for VideoPlayerView.xaml
    /// </summary>
    public partial class VideoPlayerView : UserControl, IMediaService
    {
        #region Private Members

        private bool _isSliderBeingManipulated;
        private TimeSpan _timeElapsed;

        #endregion

        #region Public Properties

        /// <summary>
        /// Is the <see cref="slider"/> control currently being manipulated?
        /// <para>E.g. being dragged.</para>
        /// </summary>
        public bool IsSliderBeingManipulated
        {
            get => _isSliderBeingManipulated;
            set
            {
                if (_isSliderBeingManipulated != value)
                {
                    _isSliderBeingManipulated = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSliderBeingManipulated)));
                }
            }
        }

        /// <summary>
        /// The elapsed time of the current video.
        /// </summary>
        public TimeSpan TimeElapsed
        {
            get => _timeElapsed;
            set
            {
                if (_timeElapsed != value)
                {
                    _timeElapsed = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TimeElapsed)));
                }
            }
        }

        #endregion

        #region Public Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public VideoPlayerView()
        {
            InitializeComponent();
            TimeElapsed = new TimeSpan(0, 0, 0, 0, 0);
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Pause a video.
        /// </summary>
        public void Pause() => Dispatcher.Invoke(() => this.mediaElement.Pause());

        /// <summary>
        /// Play a video.
        /// </summary>
        public void Play() => Dispatcher.Invoke(() => this.mediaElement.Play());

        /// <summary>
        /// Resets controls and properties to zero.
        /// </summary>
        public void Reset()
        {
            Dispatcher.Invoke(() =>
            {
                this.slider.Value = 0;
                this.mediaElement.Position = TimeSpan.Zero;
                TimeElapsed = TimeSpan.Zero;
            });
        }

        /// <summary>
        /// Updates the <see cref="slider"/>'s position based on the currently elapsed time if it is not being manipulated.
        /// </summary>
        public void UpdateSlider() => Dispatcher.Invoke(() =>
        {
            this.slider.ValueChanged -= slider_ValueChanged;
            this.slider.Value = this.mediaElement.Position.TotalSeconds;
            this.slider.ValueChanged += slider_ValueChanged;
        });

        /// <summary>
        /// Deassigns the <see cref="slider"/>'s ValueChanged event handler while being dragged.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void slider_DragStarted(object sender, DragStartedEventArgs e)
        {
            IsSliderBeingManipulated = true;
            this.slider.ValueChanged -= slider_ValueChanged;
        }

        /// <summary>
        /// Sets the new <see cref="MediaElement.Position"/> upon drag completion, and reassigns
        /// the <see cref="Slider"/>'s ValueChanged event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void slider_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            var newPos = new TimeSpan(0, 0, 0, (int)this.slider.Value);
            this.mediaElement.Position = newPos;
            TimeElapsed = newPos;
            this.slider.ValueChanged += slider_ValueChanged;
            IsSliderBeingManipulated = false;
        }

        /// <summary>
        /// Sets the <see cref="mediaElement"/>'s new position if the <see cref="slider"/>'s value has
        /// been changed in a method other than dragging.
        /// <para>E.g. clicking a position on the <see cref="slider"/>.</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            IsSliderBeingManipulated = true;

            if (this.slider.Value >= this.slider.Maximum)
                Reset();

            var newPos = new TimeSpan(0, 0, 0, (int)this.slider.Value);
            this.mediaElement.Position = newPos;
            TimeElapsed = newPos;
            IsSliderBeingManipulated = false;
        }

        #endregion
    }
}
