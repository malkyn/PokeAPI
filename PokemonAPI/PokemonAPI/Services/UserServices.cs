using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonAPI.Data;
using PokemonAPI.Data.Dto.Users;
using PokemonAPI.Exceptions;
using PokemonAPI.Interfaces;
using PokemonAPI.Models;

namespace PokemonAPI.Services;

public class UserServices : IUserServices
{
    private readonly AppDbDataContext _context;
    private readonly IMapper _mapper;

    public UserServices(AppDbDataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ReadUserDto> GetUser(int id)
    {
        var users = await _context
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
        if (users != null)
        {
            ReadUserDto userDto = _mapper.Map<ReadUserDto>(users);
            return userDto;
        }
        throw new Exception(ExceptionConsts.Users.UsuarioNaoEncontrado);
    }

    public async Task<ReadUserDto> RegisterUser(CreateUserDto userDto)
    {
        User user = _mapper.Map<User>(userDto);
        var token = TokenService.GenerateToken(user);
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return _mapper.Map<ReadUserDto>(user);
    }
}