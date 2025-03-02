using System;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.Api.Models;

public class RoomMemberDTO
{
    public int AccountId { get; set; }
    public int RoomId { get; set; }
}
