using System.Data;
using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PokemonAPI.Data;
using PokemonAPI.Data.Dto.Pokemon;
using PokemonAPI.Interfaces;
using PokemonAPI.Migrations;
using PokemonAPI.Models;

namespace PokemonAPI.Services;

public class PokemonService : IPokemonService
{
    private readonly HttpClient _client;
    private readonly AppDbDataContext _context;
    private readonly IMapper _mapper;
    private readonly string baseUrl = "https://pokeapi.co/api/v2";

    public PokemonService(HttpClient client, AppDbDataContext context, IMapper mapper)
    {
        _mapper = mapper;
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

    public async Task<Pokemon?> GetPokemonByName(string name)
    {
        try
        {
            var result = await _client.GetFromJsonAsync<Pokemon>($"{baseUrl}/pokemon/{name}");
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception("Pokemon não encontrado", ex);
        }
    }

    public async Task<string> CapturePokemon(int userId, CapturarPokemon nomePokemon)
    {
        bool isCaptured = TryCap();
        var pokemon = GetPokemonByName(nomePokemon.name);
        var result = _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        var pokemonMap = _mapper.Map<Pokemon>(nomePokemon);
        if (pokemon.Result.name != null)
        {
            PokemonsCapturados pokemonsCapturados = new PokemonsCapturados
            {
                PokemonName = pokemonMap.name,
                UserId = userId
            };

            if (isCaptured)
            {
                if (result.Result != null)
                {
                    result.Result.NumeroDePokemon++;
                    _context.PokemonsCapturados.Add(pokemonsCapturados);
                    await _context.SaveChangesAsync();
                }

                return $"Parabéns! {nomePokemon.name} foi capturado com sucesso!";
            }
            else
            {
                return "Mais sorte na próxima!";
            }
        }
        else
        {
            return "Pokemon não encontrado!";
        }
    }

    public async Task<List<string>> GetListPokemon(int userId)
    {
        List<string> listaPokemons = new List<string>();
        using (SqliteConnection connect = new SqliteConnection(@"Data Source= F:\PokeAPI\PokemonAPI\PokemonAPI\app.db"))
        {
            connect.Open();
            string query = $"select PokemonName from PokemonsCapturados WHERE UserId = {userId}";
            using (SqliteCommand command = new SqliteCommand(query, connect))
            {
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string name = reader["PokemonName"].ToString() ?? throw new InvalidOperationException();
                        listaPokemons.Add(name);
                    }
                }
            }
            return listaPokemons;
        }
    }

    private bool TryCap()
    {
        Random number = new Random();
        return number.Next(2) == 0;
    }
}