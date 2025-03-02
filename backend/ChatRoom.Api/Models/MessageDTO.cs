using System;

namespace ChatRoom.Api.Models;

public class MessageDTO
{
    public int RoomId { get; set; }
    public string Content { get; set; } = string.Empty;
    public int SenderId { get; set; }
}
