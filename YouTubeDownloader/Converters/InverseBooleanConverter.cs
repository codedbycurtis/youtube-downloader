using System;
using System.Globalization;
using System.Windows.Data;

namespace YouTubeDownloader
{
    /// <summary>
    /// Inverses a boolean value.
    /// <para>E.g. converts true to false and vice-versa.</para>
    /// </summary>
    [ValueConversion(typeof(bool), typeof(bool))]
    public sealed class InverseBooleanConverter : IValueConverter
    {
        public static InverseBooleanConverter Instance { get; } = new InverseBooleanConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) { return !(bool)value; }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
