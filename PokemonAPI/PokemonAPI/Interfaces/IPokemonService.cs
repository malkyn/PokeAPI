using PokemonAPI.Data.Dto.Pokemon;
using PokemonAPI.Models;

namespace PokemonAPI.Interfaces;

public interface IPokemonService
{
    public Task<List<Pokemon>> GetRandomPokemon();
    public Task<Pokemon?> GetPokemonByName(string name);
    public Task<string> CapturePokemon(int userId, CapturarPokemon pokemonName);
    public Task<List<string>> GetListPokemon(int userId);
}