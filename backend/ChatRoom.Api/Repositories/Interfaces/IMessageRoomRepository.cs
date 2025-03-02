using System;
using ChatRoom.Api.Models;
namespace ChatRoom.Api.Repositories;

public interface IMessageRoomRepository
{
    Task<MessageRoom?> GetByIdAsync(int id);
    Task<IEnumerable<MessageRoom>> GetAllAsync();
    Task<MessageRoom> CreateAsync(MessageRoom chatRoom);
    // Task AddUserToRoomAsync(int roomId, int userId);

    Task<MessageRoom?> GetByAccountIds(int acc1Id, int acc2Id);
}
