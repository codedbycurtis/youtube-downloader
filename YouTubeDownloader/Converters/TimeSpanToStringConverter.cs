using System;
using System.Globalization;
using System.Windows.Data;

namespace YouTubeDownloader
{
    /// <summary>
    ///  Converts a <see cref="TimeSpan"/> to a <see cref="string"/>. This class cannot be inherited.
    /// </summary>
    [ValueConversion(typeof(TimeSpan), typeof(string))]
    public sealed class TimeSpanToStringConverter : IValueConverter
    {
        public static TimeSpanToStringConverter Instance = new TimeSpanToStringConverter();
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan timeSpan = (TimeSpan)value;
            string adjustedTimeSpan = timeSpan.ToString(@"mm\:ss");
            return adjustedTimeSpan;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
