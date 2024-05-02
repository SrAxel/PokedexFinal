using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Firebase.Auth.Providers;
using Firebase.Auth;

namespace AppPeliculas;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkitMediaElement()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        builder.Services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig()
        {
            ApiKey = "AIzaSyAI3gWfb15qePFTO5sYXFFC44LuhFp-2eg",
            AuthDomain = "pokedexpractica2023.firebaseapp.com",
            Providers = new FirebaseAuthProvider[]
            {
                new EmailProvider()
            }
        }));

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
