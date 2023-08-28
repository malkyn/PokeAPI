using System.Data;
using PokemonAPI.Data.Dto.Pokemon;
using PokemonAPI.Models;

namespace PokemonAPI.Interfaces;

public interface IPokemonService
{
    public Task<PokemonResult?> GetPokemonByName(string name);
    public Task<string> CapturePokemon(int userId, CapturarPokemon pokemonName);
    public Task<DataTable> GetListPokemon(int userId);
    public Task<List<Pokemon>> GetRandomPokemonsAsync();
}