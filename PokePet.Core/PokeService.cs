﻿using GraphQL.Client.Http;
using PokePet.Core.Models;
using GraphQL.Client.Serializer.SystemTextJson;
using PokePet.Core.Interfaces;
using GraphQL;

namespace PokePet.Core
{
	public class PokeService : IPokemonService
	{
		private readonly GraphQLHttpClient _graphQLClient;

		public PokeService(GraphQLHttpClient graphQLClient)
		{
			_graphQLClient = graphQLClient;
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
	}
}
