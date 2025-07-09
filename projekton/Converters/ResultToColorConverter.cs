using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace QuizAppAvalonia.Converters
{
    public class ResultToColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string result)
            {
                if (result.StartsWith("Zła odpowiedź"))
                    return Brushes.Red;
                if (result.StartsWith("Dobra odpowiedź"))
                    return Brushes.LightGreen;
            }
            return Brushes.Transparent;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
