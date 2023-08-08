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

    public PokemonService()
    {
    }

    public async Task<List<PokemonResult>> GetRandomPokemon()
    {
        Random number = new Random();
        List<PokemonResult> pokemons = new List<PokemonResult>();
        for (int i = 0; i < 10; i++)
        {
            int id = number.Next(1, 1010);
            var response =
                await _client.GetFromJsonAsync<Pokemon>($"{_config.GetValue<string>("ApiConfig:ApiUrl")}/pokemon/{id}");
            var pokemon64 = ConvertSpriteToBase64(response.sprites.front_default);
            if (response?.sprites.front_default == null)
            {
                pokemons.Add(new PokemonResult
                {
                    Nome = response.name,
                    Altura = response.height,
                    Peso = response.weight,
                    Tipo = response.types[0].type.name,
                    Base64 = "Sem sprite."
                });
            }
            else
            {
                pokemons.Add(new PokemonResult
                {
                    Nome = response.name,
                    Altura = response.height,
                    Peso = response.weight,
                    Tipo = response.types[0].type.name,
                    Base64 = pokemon64.Result
                });
            }
        }

        return pokemons;
    }

    public async Task<PokemonResult?> GetPokemonByName(string name)
    {
        try
        {
            var result =
                await _client.GetFromJsonAsync<Pokemon>(
                    $"{_config.GetValue<string>("ApiConfig:ApiUrl")}/pokemon/{name}");
            if (result != null)
            {
                var pokemon64 = ConvertSpriteToBase64(result.sprites.front_default);
                return new PokemonResult
                {
                    Nome = result.name,
                    Altura = result.height,
                    Peso = result.weight,
                    Tipo = result.types[0].type.name,
                    Base64 = pokemon64.Result
                };
            }

            return new PokemonResult
            {
                Nome = result.name,
                Altura = result.height,
                Peso = result.weight,
                Tipo = result.types[0].type.name,
                Base64 = "Sem sprite."
            };
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