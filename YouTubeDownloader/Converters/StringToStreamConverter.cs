using System;
using System.Globalization;
using System.Windows.Data;
using System.IO;
using System.Windows.Media.Imaging;

namespace YouTubeDownloader
{
    /// <summary>
    /// Converts the contents of a specified file path to a <see cref="Stream"/> that is stored in memory.
    /// </summary>
    [ValueConversion(typeof(string), typeof(Stream))]
    class StringToStreamConverter : IValueConverter
    {
        public static StringToStreamConverter Instance = new StringToStreamConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var videoId = (string)value;
            var path = $"{Globals.ThumbnailFolderPath}\\{videoId}.jpg";
            if (string.IsNullOrEmpty(path) || !File.Exists(path)) { return null; }
            using (var stream = File.OpenRead(path))
            {
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
