using Microsoft.AspNetCore.Mvc;
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

    [HttpGet("pokemon/{id}")]
    public async Task<IActionResult> GetPokemonById([FromRoute] int id)
    {
        return Ok(await _pokemonService.GetPokemonById(id));
    }

    [HttpGet("capture")]
    public async Task<IActionResult> CapturePokemon()
    {
        return Ok(await _pokemonService.CapturePokemon());
    }
}