using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace QuizAppAvalonia.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool b)
                return b ? Colors.LightGreen : Colors.IndianRed;

            return Colors.Transparent;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

