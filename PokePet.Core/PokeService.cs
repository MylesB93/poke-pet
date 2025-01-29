using GraphQL.Client.Http;
using PokePet.Core.Models;
using GraphQL.Client.Serializer.SystemTextJson;
using PokePet.Core.Interfaces;

namespace PokePet.Core
{
	public class PokeService : IPokemonService
	{
		private readonly GraphQLHttpClient _graphQLClient;

		public PokeService(GraphQLHttpClient graphQLClient)
		{
			_graphQLClient = graphQLClient;
		}

		public Pokemon GetPokemon(string name)
		{
			return new Pokemon();
		}
	}
}
