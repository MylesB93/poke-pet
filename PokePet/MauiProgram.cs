﻿using GraphQL.Client.Http;
using Microsoft.Extensions.Logging;
using GraphQL.Client.Serializer.SystemTextJson;
using PokePet.Core.Interfaces;
using PokePet.Core.Repositories;
using PokePet.Core.Services;

namespace PokePet
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

			builder.Services.AddSingleton(s => new GraphQLHttpClient("https://beta.pokeapi.co/graphql/v1beta", new SystemTextJsonSerializer()));
            builder.Services.AddScoped<IPokemonService, PokeService>();
			builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();

#if DEBUG
			builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
