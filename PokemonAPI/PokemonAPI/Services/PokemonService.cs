
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonAPI.Data;
using PokemonAPI.Interfaces;
using PokemonAPI.Models;

namespace PokemonAPI.Services;

public class PokemonService : IPokemonService
{
    private readonly HttpClient _client;
    private readonly AppDbDataContext _context;
    private readonly string baseUrl = "https://pokeapi.co/api/v2";

    public PokemonService(HttpClient client, AppDbDataContext context)
    {

        _client = client;
        _context = context;
    }

    public async Task<List<Pokemon>> GetRandomPokemon()
    {
        Random number = new Random();
        List<Pokemon> pokemons = new List<Pokemon>();
        for (int i = 0; i < 10; i++)
        {
            int id = number.Next(1, 1010);
            var response = await _client.GetFromJsonAsync<Pokemon>($"{baseUrl}/pokemon/{id}");
            if (response != null) pokemons.Add(response);
        }
        return pokemons;
    }

    public async Task<Pokemon?> GetPokemonById(int id)
    {
        return await _client.GetFromJsonAsync<Pokemon>($"{baseUrl}/pokemon/{id}");
    }

    public async Task<string> CapturePokemon()
    {
        bool isCaptured = TryCap();
        var pokemon = GetPokemon();
        
        if (isCaptured)
        {
            var result = _context.Users.FirstOrDefaultAsync().Result;
            if (result != null)
            {
                result.NumeroDePokemon++;
                result.Pokemons.Add(pokemon.Result);
                await _context.SaveChangesAsync();
            }
            return pokemon.Result.ToString();
        }
        else
        {
            return "Mais sorte na próxima!";
        }
    }

    private async Task<string> GetPokemon()
    {
        Random number = new Random();
        int id = number.Next(1, 1010);
        var response =  await _client.GetFromJsonAsync<Pokemon>($"{baseUrl}/pokemon/{id}");
        return response.name;
    }
    
    private bool TryCap()
    {
        Random number = new Random();
        return number.Next(2) == 0;
    }
}