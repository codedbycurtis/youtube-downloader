using System;
using System.Globalization;
using System.Windows.Data;

namespace YouTubeDownloader
{
    /// <summary>
    /// Converts a <see cref="TimeSpan"/> to a <see cref="double"/>. This class cannot be inherited.
    /// </summary>
    [ValueConversion(typeof(TimeSpan), typeof(double))]
    public sealed class TimeSpanToDoubleConverter : IValueConverter
    {
        public static TimeSpanToDoubleConverter Instance { get; } = new TimeSpanToDoubleConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var duration = (TimeSpan)value;
            return (double)duration.TotalSeconds;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
