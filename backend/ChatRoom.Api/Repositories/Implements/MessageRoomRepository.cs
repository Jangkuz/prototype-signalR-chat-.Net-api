using System;
using ChatRoom.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.Api.Repositories;

public class MessageRoomRepository : IMessageRoomRepository
{
    private readonly ChatRoomDbContext _context;

    public MessageRoomRepository(ChatRoomDbContext context)
    {
        _context = context;
    }


    public async Task<MessageRoom> CreateAsync(MessageRoom chatRoom)
    {
        _context.MessageRooms.Add(chatRoom);
        await _context.SaveChangesAsync();
        return chatRoom;
    }

    public async Task<IEnumerable<MessageRoom>> GetAllAsync()
    {
        return await _context.MessageRooms.ToListAsync();
    }

    public async Task<MessageRoom?> GetByIdAsync(int id)
    {
        return await _context.MessageRooms.FindAsync(id);
    }

    public async Task<MessageRoom?> GetByAccountIds(int acc1Id, int acc2Id)
    {
        var roomDetails = await _context.RoomMembers
            .Where(rm => rm.UserId.Equals(acc1Id) || rm.UserId.Equals(acc2Id)).ToListAsync();

        var roomGrouping = roomDetails.GroupBy(rm => rm.RoomId)
            .FirstOrDefault(g => g.Select(rm => rm.UserId).Distinct().Count() == 2);

        if (roomGrouping == null) {
            return null;
        }

        var room = await _context.MessageRooms.FirstOrDefaultAsync(mr => mr.Id.Equals(roomGrouping.Key));
        return room;
    }

}
