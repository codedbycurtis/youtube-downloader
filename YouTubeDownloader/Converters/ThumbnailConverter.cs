using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using YoutubeExplode.Common;

namespace YouTubeDownloader.Converters
{
    /// <summary>
    /// Gets the URL of the highest-quality thumbnail image.
    /// </summary>
    [ValueConversion(typeof(IReadOnlyList<Thumbnail>), typeof(string))]
    public sealed class ThumbnailConverter : IValueConverter
    {
        public static ThumbnailConverter Instance => new ThumbnailConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var thumbnails = (IReadOnlyList<Thumbnail>)value;
            return thumbnails.GetWithHighestResolution().Url;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
