using System;
using System.Globalization;
using System.Windows.Data;

namespace YouTubeDownloader.Converters
{
    /// <summary>
    /// Formats the hours, minutes, and seconds of a <see cref="TimeSpan"/> to a <see cref="string"/>.
    /// </summary>
    [ValueConversion(typeof(TimeSpan), typeof(string))]
    public sealed class TimeSpanToStringConverter : IValueConverter
    {
        public static TimeSpanToStringConverter Instance => new TimeSpanToStringConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var timeSpan = (TimeSpan?)value;

            if (timeSpan.HasValue)
            {
                var duration = timeSpan.Value;
                return duration.Hours > 0 ? duration.ToString(@"hh\:mm\:ss") : duration.ToString(@"mm\:ss");
            }

            return "LIVE NOW"; // If the TimeSpan does not have a value, then it must be a livestream, not a video.
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
