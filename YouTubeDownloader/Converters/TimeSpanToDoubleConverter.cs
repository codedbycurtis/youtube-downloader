using System;
using System.Globalization;
using System.Windows.Data;

namespace YouTubeDownloader.Converters
{
    /// <summary>
    /// Returns the <see cref="TimeSpan.TotalSeconds"/> property.
    /// </summary>
    [ValueConversion(typeof(TimeSpan), typeof(double))]
    public sealed class TimeSpanToDoubleConverter : IValueConverter
    {
        public static TimeSpanToDoubleConverter Instance { get; } = new TimeSpanToDoubleConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var duration = (TimeSpan)value;
            return duration.TotalSeconds;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
