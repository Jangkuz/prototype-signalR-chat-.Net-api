using System;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.Api.Models;

[PrimaryKey(nameof(UserId), nameof(RoomId))]
public class RoomMember
{
    public int UserId { get; set; }
    public int RoomId { get; set; }
    public User? User { get; set; }
    public MessageRoom? Room { get; set; }
}
