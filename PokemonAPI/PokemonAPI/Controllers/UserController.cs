using AutoMapper;
using Microsoft.AspNetCore.DataProtection.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonAPI.Data;
using PokemonAPI.Data.Dto.Users;
using PokemonAPI.Models;
using PokemonAPI.ViewModels;

namespace PokemonAPI.Controllers;
[ApiController]
public class UserController : ControllerBase
{
    private readonly AppDbDataContext _context;
    private readonly IMapper _mapper;
    public UserController(AppDbDataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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
    public async Task<IActionResult> PostAsync([FromBody] CreateUserDto userDto)
    {
        User user = _mapper.Map<User>(userDto);
        if (!ModelState.IsValid)
            return BadRequest();
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