using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace QuizAppAvalonia.Converters
{
    public class EndQuizVisibilityConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string text && text.Contains("Twój wynik:"))
                return true; // IsVisible = true

            return false; // IsVisible = false
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
            throw new NotSupportedException();
    }
}
