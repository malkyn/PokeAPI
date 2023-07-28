using Microsoft.AspNetCore.Mvc;
using PokemonAPI.Data.Dto.Pokemon;
using PokemonAPI.Interfaces;

namespace PokemonAPI.Controllers;

[ApiController]
public class PokemonController : ControllerBase
{
    private readonly IPokemonService _pokemonService;

    public PokemonController(IPokemonService pokemonService)
    {
        _pokemonService = pokemonService;
    }

    [HttpGet("pokemon")]
    public async Task<IActionResult> GetPokemon()
    {
        return Ok(await _pokemonService.GetRandomPokemon());
    }

    [HttpGet("pokemon/{name}")]
    public async Task<IActionResult> GetPokemonByName([FromRoute] string name)
    {
        return Ok(await _pokemonService.GetPokemonByName(name));
    }

    [HttpGet("listapokemons/{userId}")]
    public async Task<IActionResult> GetListaPokemons(int userId)
    {
        return Ok(await _pokemonService.GetListPokemon(userId));
    }
    
    [HttpPost("capture/{userId}")]
    public async Task<IActionResult> CapturePokemon([FromRoute] int userId, [FromBody] CapturarPokemon pokemonName)
    {
        return Ok(await _pokemonService.CapturePokemon(userId, pokemonName));
    }
}