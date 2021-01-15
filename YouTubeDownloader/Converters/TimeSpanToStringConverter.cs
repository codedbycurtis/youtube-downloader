using System;
using System.Globalization;
using System.Windows.Data;

namespace YouTubeDownloader
{
    [ValueConversion(typeof(TimeSpan), typeof(string))]
    public sealed class TimeSpanToStringConverter : IValueConverter
    {
        public static TimeSpanToStringConverter Instance = new TimeSpanToStringConverter();
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string adjustedTimeSpan;
            TimeSpan timeSpan = (TimeSpan)value;

            adjustedTimeSpan = timeSpan.ToString(@"mm\:ss");

            return adjustedTimeSpan;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
