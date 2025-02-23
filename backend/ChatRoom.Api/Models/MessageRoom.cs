using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.Api.Models;

[PrimaryKey(nameof(Id))]
public class MessageRoom
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<RoomMember> Members { get; set; } = new List<RoomMember>();
    public ICollection<Message> Messages { get; set; } = new List<Message>();
}
