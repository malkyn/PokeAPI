using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonAPI.Data;
using PokemonAPI.Data.Dto.Pokemon;
using PokemonAPI.Interfaces;

namespace PokemonAPI.Controllers;

[ApiController]
public class PokemonController : ControllerBase
{
    private readonly IPokemonService _pokemonService;
    private readonly AppDbDataContext _context;

    public PokemonController(IPokemonService pokemonService, AppDbDataContext context)
    {
        _pokemonService = pokemonService;
        _context = context;
    }

    [HttpGet("pokemon")]
    public async Task<IActionResult> GetPokemon()
    {
        return Ok(await _pokemonService.GetRandomPokemonsAsync());
    }

    [HttpGet("pokemon/{name}")]
    public async Task<IActionResult> GetPokemonByName([FromRoute] string name)
    {
        return Ok(await _pokemonService.GetPokemonByName(name));
    }

    [HttpGet("listapokemons/{userId}")]
    [Authorize]
    public async Task<IActionResult> GetListaPokemons(int userId)
    {
        var usuario = _context.Users.FirstOrDefault(x => x.Id == userId);
        if(User.Identity.Name == usuario.Usuario)
            return Ok(await _pokemonService.GetListPokemon(userId));
        return BadRequest("Acesso negado!");
    }
    
    [HttpPost("capturar/{userId}")]
    [Authorize]
    public async Task<IActionResult> CapturePokemon([FromRoute] int userId, [FromBody] CapturarPokemon pokemonName)
    {
        var usuario = _context.Users.FirstOrDefault(x => x.Id == userId);
        if(User.Identity.Name == usuario.Usuario)
            return Ok(await _pokemonService.CapturePokemon(userId, pokemonName));
        return BadRequest("Acesso negado!");
    }
}