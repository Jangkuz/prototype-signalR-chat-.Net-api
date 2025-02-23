using System;
using ChatRoom.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.Api.Repositories;

public class MessageRepository: IMessageRepository
{
    private readonly ChatRoomDbContext _context;

    public MessageRepository(ChatRoomDbContext context)
    {
        _context = context;
        }

    public async Task<Message?> GetMessageById(int id)
    {
        return await _context.Messages.FindAsync(id);
    }

    public async Task<IEnumerable<Message>> GetMessagesByRoomId(int roomId)
    {
        return await _context.Messages.Where(m => m.ChatRoomId == roomId).ToListAsync();
    }

    public async Task<Message?> InsertMessage(Message message)
    {
        await _context.Messages.AddAsync(message);
        await _context.SaveChangesAsync();
        return message;
    }
}
