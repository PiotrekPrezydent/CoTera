using CommunityToolkit.Maui;
using CoTera.Systems;
using Syncfusion.Maui.Core.Hosting;

namespace CoTera
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            DataLoaderSystem.Initialize();
            builder.ConfigureSyncfusionCore();
            return builder.Build();
        }
    }
}
