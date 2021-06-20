using System;
using System.Globalization;
using System.Windows.Data;

namespace YouTubeDownloader.Converters
{
    /// <summary>
    /// Formats a video's view count as a <see cref="string"/>.
    /// </summary>
    [ValueConversion(typeof(long), typeof(string))]
    public sealed class ViewCountConverter : IValueConverter
    {
        public static ViewCountConverter Instance => new ViewCountConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var viewCount = (long)value;
            var viewCountString = viewCount.ToString();

            switch (viewCountString.Length)
            {
                case 10:
                    return $"{viewCountString[0]}B views";

                case 9:
                    return $"{viewCountString[0]}{viewCountString[1]}{viewCountString[2]}M views";

                case 8:
                    return $"{viewCountString[0]}{viewCountString[1]}M views";

                case 7:
                    return $"{viewCountString[0]}M views";

                case 6:
                    return $"{viewCountString[0]}{viewCountString[1]}{viewCountString[2]}K views";

                case 5:
                    return $"{viewCountString[0]}{viewCountString[1]}K views";

                case 4:
                    return $"{viewCountString[0]}K views";

                case 3:
                case 2:
                case 1:
                default:
                    return $"{viewCountString} views";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
