using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ChatRoom.Api.Models;
using ChatRoom.Api.Repositories;
using ChatRoom.Api.Mapper;

namespace ChatRoom.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers(){
        var users = await _userRepository.GetAllAsync();
        return Ok(users.Select(u => u.ToDTO()));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> GetUser(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user == null ? NotFound() : Ok(user.ToDTO());
    }

    [HttpPost]
    public async Task<ActionResult<UserDTO>> CreateUser(UserDTO userDTO)
    {
        var created = await _userRepository.CreateAsync(userDTO.ToEntity());
        return CreatedAtAction(nameof(GetUser), new { id = created.Id }, created.ToDTO());
    }
}
