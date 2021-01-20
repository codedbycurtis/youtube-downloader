using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace YouTubeDownloader
{
    [ValueConversion(typeof(string), typeof(object))]
    public class StringToImageSourceConverter : IValueConverter
    {
        public static StringToImageSourceConverter Instance = new StringToImageSourceConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new ImageSourceConverter().ConvertFromString((string)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
