using System;
using ChatRoom.Api.Models;
namespace ChatRoom.Api.Repositories;

public interface IMessageRepository
{
    Task<IEnumerable<Message>> GetMessagesByRoomId(int roomId);
    Task<Message?> GetMessageById(int id);
    Task<Message?> InsertMessage(Message message);
}
