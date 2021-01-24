using System;
using System.Globalization;
using System.Windows.Data;

namespace YouTubeDownloader
{
    /// <summary>
    /// Inverses a boolean value.
    /// <para>Example: Converts true to false.</para>
    /// </summary>
    [ValueConversion(typeof(bool), typeof(bool))]
    public class InverseBooleanConverter : IValueConverter
    {
        public static InverseBooleanConverter Instance = new InverseBooleanConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
