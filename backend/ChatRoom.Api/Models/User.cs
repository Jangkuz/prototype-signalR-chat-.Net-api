using System;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.Api.Models;

[PrimaryKey(nameof(Id))]
public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public ICollection<RoomMember> Rooms { get; set; } = new List<RoomMember>();
    public ICollection<Message> Messages { get; set; } = new List<Message>();
}
