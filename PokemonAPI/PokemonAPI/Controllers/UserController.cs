using AutoMapper;
using Microsoft.AspNetCore.DataProtection.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonAPI.Data;
using PokemonAPI.Data.Dto.Users;
using PokemonAPI.Interfaces;
using PokemonAPI.Models;
using PokemonAPI.Services;

namespace PokemonAPI.Controllers;
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserServices _userServices;
    private readonly IMapper _mapper;
    private readonly AppDbDataContext _context;
    public UserController(IUserServices userServices, IMapper mapper, AppDbDataContext context)
    {
        _userServices = userServices;
        _mapper = mapper;
        _context = context;
    }
    [HttpGet]
    [Route("users/{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        return Ok(await _userServices.GetUser(id));
    }

    [HttpPost("cadastro")]
    public async Task<IActionResult> PostAsync([FromBody] CreateUserDto userDto)
    {
        return Ok(await _userServices.RegisterUser(userDto));
    }

    [HttpPost("login")]
    public async Task<ActionResult<dynamic>> Authenticate([FromBody] LoginUserDto userDto)
    {
        User user = _mapper.Map<User>(userDto);
        user = _context.Get(user.Usuario, user.Senha);

        if (user == null)
            return NotFound(new { message = "Usuário ou senha inválidos." });

        var token = await TokenService.GenerateToken(user);
        user.Senha = "";

        return Ok(new
        {
            usuario = user,
            tokenDeAcesso = token
        });
    }
}