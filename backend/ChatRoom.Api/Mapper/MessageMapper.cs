using System;
using ChatRoom.Api.Models;

namespace ChatRoom.Api.Mapper;

public static class MessageMapper
{
    public static MessageDTO ToDTO(this Message message)
    {
        return new MessageDTO
        {
            RoomId = message.ChatRoomId,
            Content = message.Content,
            SenderId = message.UserId,
        };
    }

    public static Message ToEntity(this MessageDTO messageDTO)
    {
        return new Message
        {
            ChatRoomId = messageDTO.RoomId,
            Content = messageDTO.Content,
            UserId = messageDTO.SenderId,
        };
    }
}
