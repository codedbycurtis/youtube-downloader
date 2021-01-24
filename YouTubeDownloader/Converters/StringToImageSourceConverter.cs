using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.IO;

namespace YouTubeDownloader
{
    [ValueConversion(typeof(string), typeof(object))]
    public class StringToImageSourceConverter : IValueConverter
    {
        public static StringToImageSourceConverter Instance = new StringToImageSourceConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string videoId = (string)value;
            string path = $"{Globals.ThumbnailFolderPath}\\{videoId}.jpg";
            if (!File.Exists(path)) { return null; }
            else { return new ImageSourceConverter().ConvertFromString(path); }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
