using System.Data;
using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
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
    private readonly IConfiguration _config;

    public PokemonService(HttpClient client, AppDbDataContext context, IMapper mapper, ISqlLiteDB sqlLite,
        IConfiguration config)
    {
        _mapper = mapper;
        _client = client;
        _context = context;
        _sqlLite = sqlLite;
        _config = config;
    }

    public async Task<List<PokemonResult>> GetRandomPokemon()
    {
        var random = new Random();
        var pokemons = new List<PokemonResult>();
        var apiUrl = _config.GetValue<string>("ApiConfig:ApiUrl");

        for (int i = 0; i < 10; i++)
        {
            int id = random.Next(1, 1010);
            var response = await _client.GetFromJsonAsync<Pokemon>($"{apiUrl}/pokemon/{id}");
            var pokemonResult = CreatePokemonResult(response);
            pokemons.Add(pokemonResult);
        }
        return pokemons;
    }

    public async Task<PokemonResult?> GetPokemonByName(string name)
    {
        try
        {
            var apiUrl = _config.GetValue<string>("ApiConfig:ApiUrl");
            var pokemonApiUrl = $"{apiUrl}/pokemon/{name}";

            var result = await _client.GetFromJsonAsync<Pokemon>(pokemonApiUrl);

            return CreatePokemonResult(result);
        }
        catch (Exception e)
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
    
    /********************************************************************************************************************
        *
        *   Métodos Privados
        *
        */

    private PokemonResult CreatePokemonResult(Pokemon? result)
    {
        if (result?.sprites.front_default == null)
        {
            return new PokemonResult
            {
                Nome = result.name,
                Altura = result.height,
                Peso = result.weight,
                Tipo = result.types[0].type.name,
                Base64 = "Sem sprite."
            };
        }

        var pokemon64 = ConvertSpriteToBase64(result.sprites.front_default);
        var tipos = result.types.Select(tipo => tipo.type.name).ToList();

        return new PokemonResult
        {
            Nome = result.name,
            Altura = result.height,
            Peso = result.weight,
            Tipo = string.Join(", ", tipos),
            Base64 = pokemon64.Result
        };
    }

    private static bool TryCap()
    {
        var number = new Random();
        return number.Next(2) == 0;
    }
    
    private static async Task<string> ConvertSpriteToBase64(string spriteUrl)
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(spriteUrl);
            byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();
            return Convert.ToBase64String(imageBytes);
        }
    }
}