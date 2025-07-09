using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Converters;
using Avalonia.Media;
using QuizAppAvalonia.ViewModels;
using System;
using System.Globalization;
using Avalonia;

namespace QuizAppAvalonia.Converters
{
    public class BoolToBrushConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var selected = value as string;
            var current = parameter as string;
            var vm = Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop ?
                     (desktop.MainWindow.DataContext as MainWindowViewModel) : null;

            if (vm == null || string.IsNullOrEmpty(selected))
                return Brushes.SteelBlue; // domyślny (brak wyboru)

            if (current == selected)
            {
                if (selected == vm.CorrectAnswer)
                    return Brushes.Green;
                else
                    return Brushes.Red;
            }

            return Brushes.SteelBlue; // inne przyciski
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
