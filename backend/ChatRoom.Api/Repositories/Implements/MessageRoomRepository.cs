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


}
