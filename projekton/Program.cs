using Avalonia;

namespace QuizAppAvalonia
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>().UsePlatformDetect().LogToTrace();
    }
}