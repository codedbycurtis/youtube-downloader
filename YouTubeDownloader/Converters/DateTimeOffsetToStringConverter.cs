using System;
using System.Globalization;
using System.Windows.Data;

namespace YouTubeDownloader
{
    /// <summary>
    /// Converts a <see cref="DateTimeOffset"/> to a <see cref="string"/>. This class cannot be inherited.
    /// </summary>
    [ValueConversion(typeof(DateTimeOffset), typeof(string))]
    public sealed class DateTimeOffsetToStringConverter : IValueConverter
    {
        public static DateTimeOffsetToStringConverter Instance = new DateTimeOffsetToStringConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTimeOffset dateTimeOffset = (DateTimeOffset)value;
            DateTime uploadDate = dateTimeOffset.Date;
            string normalizedDate = uploadDate.ToString("MMM dd, yyyy");
            return normalizedDate;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
