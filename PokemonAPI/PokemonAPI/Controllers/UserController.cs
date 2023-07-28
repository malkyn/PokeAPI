using AutoMapper;
using Microsoft.AspNetCore.DataProtection.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonAPI.Data;
using PokemonAPI.Data.Dto.Users;
using PokemonAPI.Interfaces;
using PokemonAPI.Models;

namespace PokemonAPI.Controllers;
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserServices _userServices;
    public UserController(IUserServices userServices)
    {
        _userServices = userServices;
    }
    [HttpGet]
    [Route("users/{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        return Ok(_userServices.GetUser(id));
    }

    [HttpPost("cadastro")]
    public async Task<IActionResult> PostAsync([FromBody] CreateUserDto userDto)
    {
        return Ok(_userServices.RegisterUser(userDto));
    }
    
}