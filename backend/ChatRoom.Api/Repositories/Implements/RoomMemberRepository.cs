using System;
using ChatRoom.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.Api.Repositories;

public class RoomMemberRepository : IRoomMemberRepository
{
    private readonly ChatRoomDbContext _context;

    public RoomMemberRepository(ChatRoomDbContext context)
    {
        _context = context;
    }

    public async Task<MessageRoom?> GetRoomByUserId(int userId)
    {
        var room = await _context.MessageRooms.FirstOrDefaultAsync(r => r.Members.Any(m => m.UserId == userId));
        return room;
    }

    public async Task<RoomMember?> InsertRoomMember(RoomMember roomMember)
    {
        await _context.RoomMembers.AddAsync(roomMember);
        await _context.SaveChangesAsync();
        return roomMember;
    }
}
