using PokemonAPI.Models;

namespace PokemonAPI.Interfaces;

public interface IPokemonService
{
    public Task<List<Pokemon>> GetRandomPokemon();
    public Task<Pokemon?> GetPokemonById(int id);
    public Task<string> CapturePokemon();
}