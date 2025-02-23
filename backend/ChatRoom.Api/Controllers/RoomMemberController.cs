using System;
using ChatRoom.Api.Models;
using ChatRoom.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoom.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomMemberController : ControllerBase
{
    private readonly IRoomMemberRepository _roomMemberRepository;
    private readonly IMessageRoomRepository _messageRoomRepository;

    public RoomMemberController(IRoomMemberRepository roomMemberRepository, IMessageRoomRepository messageRoomRepository)
    {
        _roomMemberRepository = roomMemberRepository;
        _messageRoomRepository = messageRoomRepository;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<MessageRoom>> GetRoomByUserId(int userId)
    {
        var room = await _roomMemberRepository.GetRoomByUserId(userId);
        return room == null ? NotFound() : Ok(room);
    }

    [HttpPost]
    public async Task<ActionResult<RoomMember>> InsertRoomMember(int RoomId, int UserId)
    {
        // var room = await _roomMemberRepository.GetRoomByUserId(UserId);
        // if (room == null)
        // {
        //     var newRoom = await _messageRoomRepository.CreateAsync(new MessageRoom { Name = $"DM:{UserId}_{RoomId}" });
        // }

        var roomMember = new RoomMember { RoomId = RoomId, UserId = UserId };
        var member = await _roomMemberRepository.InsertRoomMember(roomMember);
        return member == null ? NotFound() : Ok(member);
    }
}
