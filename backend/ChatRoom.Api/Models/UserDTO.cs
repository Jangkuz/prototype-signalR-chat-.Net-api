using System;

namespace ChatRoom.Api.Models;

public class UserDTO
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
}
