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
            string adjustedTimeSpan;
            TimeSpan timeSpan = (TimeSpan)value;
            if (timeSpan.Hours > 0) { adjustedTimeSpan = timeSpan.ToString(@"hh\:mm\:ss"); }
            else { adjustedTimeSpan = timeSpan.ToString(@"mm\:ss"); }
            return adjustedTimeSpan;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
