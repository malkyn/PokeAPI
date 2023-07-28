using PokemonAPI.Models;

namespace PokemonAPI.Interfaces;

public interface PokemonInterface
{
    public Task<Pokemon> GetPokemon();
}