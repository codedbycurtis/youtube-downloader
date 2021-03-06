﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace YouTubeDownloader.Converters
{
    /// <summary>
    /// Inverses a boolean value.
    /// <para>E.g. converts true to false and vice-versa.</para>
    /// </summary>
    [ValueConversion(typeof(bool), typeof(bool))]
    public sealed class InverseBooleanConverter : IValueConverter
    {
        public static InverseBooleanConverter Instance => new InverseBooleanConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => !(bool)value;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => !(bool)value;
    }
}
