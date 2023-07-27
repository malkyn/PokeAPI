using Microsoft.AspNetCore.DataProtection.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonAPI.Data;
using PokemonAPI.Models;
using PokemonAPI.ViewModels;

namespace PokemonAPI.Controllers;
[ApiController]
public class UserController : ControllerBase
{
    private readonly AppDbDataContext _context;
    public UserController(AppDbDataContext context)
    {
        _context = context;
    }
    [HttpGet]
    [Route("users/{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var users = await _context
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
        
        return users == null ? NotFound() : Ok(users);
    }

    [HttpPost("cadastro")]
    public async Task<IActionResult> PostAsync([FromBody] CreateUserViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var user = new User()
        {
            Name = model.Name,
            Idade = model.Idade,
            CPF = model.CPF
        };

        try
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return Created($"user/cadastro/{user.Id}", user);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
}