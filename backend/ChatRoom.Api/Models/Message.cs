using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.Api.Models;

[PrimaryKey(nameof(Id))]
public class Message
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public DateTime SentAt { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int ChatRoomId { get; set; }
    public MessageRoom ChatRoom { get; set; } = null!;
}
