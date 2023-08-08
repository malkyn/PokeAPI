using System.Data;
using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PokemonAPI.Data;
using PokemonAPI.Data.Dto.Pokemon;
using PokemonAPI.Exceptions;
using PokemonAPI.Interfaces;
using PokemonAPI.Migrations;
using PokemonAPI.Models;

namespace PokemonAPI.Services;

public class PokemonService : IPokemonService
{
    private readonly HttpClient _client;
    private readonly AppDbDataContext _context;
    private readonly IMapper _mapper;
    private readonly ISqlLiteDB _sqlLite;
    private readonly string baseUrl = "https://pokeapi.co/api/v2";

    public PokemonService(HttpClient client, AppDbDataContext context, IMapper mapper, ISqlLiteDB sqlLite)
    {
        _mapper = mapper;
        _client = client;
        _context = context;
        _sqlLite = sqlLite;
    }

    public PokemonService()
    {
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
            throw new Exception(ExceptionConsts.Pokemon.PokemonNaoEncontrado);
        }
    }

    public async Task<string> CapturePokemon(int userId, CapturarPokemon nomePokemon)
    {
        var isCaptured = TryCap();
        var pokemon = GetPokemonByName(nomePokemon.name);
        var result = _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        var pokemonMap = _mapper.Map<Pokemon>(nomePokemon);
        var pokemonsCapturados = new PokemonsCapturados
        {
            PokemonName = pokemonMap.name,
            UserId = userId
        };

        if (result.Result == null)
            throw new Exception(ExceptionConsts.Users.UsuarioNaoEncontrado);
        if (pokemon.Result == null)
            throw new Exception(ExceptionConsts.Pokemon.PokemonNaoEncontrado);
        if (!isCaptured)
            throw new Exception(ExceptionConsts.Pokemon.FalhaCaptura);
        if (result.Result != null && pokemon.Result != null && isCaptured)
        {
            _context.PokemonsCapturados.Add(pokemonsCapturados);
            await _context.SaveChangesAsync();
        }
        
        return $"Parabéns! {nomePokemon.name} foi capturado com sucesso!";
    }

    public async Task<DataTable> GetListPokemon(int userId)
    {
        DataTable data = await _sqlLite.ReturnData(userId);
        return data;
    }

    private bool TryCap()
    {
        Random number = new Random();
        return number.Next(2) == 0;
    }
}