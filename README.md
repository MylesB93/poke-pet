# Poké Pet App

A mobile app built using .NET MAUI that allows user's to raise and take care of up to 6 baby Pokémon of their choosing. Pokémon data is retrieved using the [PokéAPI](https://pokeapi.co/).

## App Structure

### Search Page

This page consists of a search bar where the user can search for Pokémon to add to their list of Pokémon. 

### Pokémon Listing Page

Here, the user can view all of their Pokémon and also release any Pokémon they no longer want.

### Pokémon Page

Each Pokémon has a `Hunger` value, a `Tiredness` value and a `Boredom` value, all which contribute to their overall `Happiness` value. These values can be viewed on the Pokémon page, and the user can choose to feed their Pokémon to raise it's `Hunger` value, play with their Pokémon to raise it's `Boredom` value and put their Pokémon to sleep to raise it's `Tiredness` value. These actions can only be done once an hour (a countdown is displayed to when the action can next be completed again). User's can also rename their Pokémon and give them a nickname of their choice.

## Integrations

### PokéAPI Integration

As mentioned previously, Pokémon data comes from the [PokéAPI](https://pokeapi.co/). When requesting Pokemon data, a GraphQL request is made to retrieve data about a specific baby Pokémon which is then mapped to the custom `Pokemon` object.

## SQLite-net

In order to persist user data, a sqlite database is used. The sqlite-net ORM is used to store Pokémon objects in this database (as well as to retrieve data and map to a Pokémon object). 