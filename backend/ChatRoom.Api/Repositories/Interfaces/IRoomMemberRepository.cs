using System;
using ChatRoom.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoom.Api.Repositories;

public interface IRoomMemberRepository
{
    Task<MessageRoom?> GetRoomByUserId(int userId);
    Task<RoomMember?> InsertRoomMember(RoomMember roomMember);
}
