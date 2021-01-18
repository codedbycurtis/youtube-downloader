using System;
using System.Globalization;
using System.Windows.Data;

namespace YouTubeDownloader
{
    /// <summary>
    /// Converts a <see cref="long"/> to a <see cref="string"/>. This class cannot be inherited.
    /// </summary>
    [ValueConversion(typeof(long), typeof(string))]
    public sealed class LongToStringConverter : IValueConverter
    {
        public static LongToStringConverter Instance = new LongToStringConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            long viewCount = (long)value;
            string abbreviatedViewCount, viewCountString = viewCount.ToString();

            if (viewCountString.Length == 10) { abbreviatedViewCount = $"{viewCountString[0]}B views"; }
            else if (viewCountString.Length == 9) { abbreviatedViewCount = $"{viewCountString[0]}{viewCountString[1]}{viewCountString[2]}M views"; }
            else if (viewCountString.Length == 8) { abbreviatedViewCount = $"{viewCountString[0]}{viewCountString[1]}M views"; }
            else if (viewCountString.Length == 7) { abbreviatedViewCount = $"{viewCountString[0]}M views"; }
            else if (viewCountString.Length == 6) { abbreviatedViewCount = $"{viewCountString[0]}{viewCountString[1]}{viewCountString[2]}K views"; }
            else if (viewCountString.Length == 5) { abbreviatedViewCount = $"{viewCountString[0]}{viewCountString[1]}K views"; }
            else if (viewCountString.Length == 4) { abbreviatedViewCount = $"{viewCountString[0]}K views"; }
            else { abbreviatedViewCount = $"{viewCountString} views"; }

            return abbreviatedViewCount;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
