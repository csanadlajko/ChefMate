using Microsoft.Extensions.Logging;

namespace ChefMate_YR6LYT
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<AddRecipePageViewModel>();
            builder.Services.AddTransient<AddRecipePage>();
            builder.Services.AddTransient<EditRecipePageViewModel>();
            builder.Services.AddTransient<EditRecipePage>();
            builder.Services.AddSingleton<IChefMateDatabase, SQLiteChefMateDatabase>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
