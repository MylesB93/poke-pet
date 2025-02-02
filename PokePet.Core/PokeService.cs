using GraphQL.Client.Http;
using PokePet.Core.Models;
using GraphQL.Client.Serializer.SystemTextJson;
using PokePet.Core.Interfaces;
using GraphQL;

namespace PokePet.Core
{
	public class PokeService : IPokemonService
	{
		private readonly GraphQLHttpClient _graphQLClient;
		private readonly IPokemonRepository _pokemonRepository;

		public PokeService(GraphQLHttpClient graphQLClient, IPokemonRepository pokemonRepository)
		{
			_graphQLClient = graphQLClient;
			_pokemonRepository = pokemonRepository;
		}

		public async Task<Pokemon> GetPokemonAsync(string name)
		{
			var query = new GraphQLRequest
			{
				Query = @"
					query babyPokemon($name: String!) {
					  babies: pokemon_v2_pokemonspecies(
						order_by: {id: asc, is_baby: asc, name: asc}
						where: {is_baby: {_eq: true}, name: { _eq: $name} }
					  ) {
						name
						id
						is_baby    
					  }
					}
				",
				Variables = new { name }
			};

			var response = await _graphQLClient.SendQueryAsync<PokemonResponse>(query);

			return response.Data.Babies.FirstOrDefault(); //TODO: Address null reference exception
		}

		public async Task SetPokemonAsync(Pokemon pokemon)
		{
			await _pokemonRepository.SetPokemonAsync(pokemon);
		}
	}
}
