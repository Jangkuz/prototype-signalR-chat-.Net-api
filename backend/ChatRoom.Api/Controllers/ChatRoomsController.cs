using System;
using Microsoft.AspNetCore.Mvc;
using ChatRoom.Api.Models;
using ChatRoom.Api.Repositories;

namespace ChatRoom.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatRoomsController : ControllerBase
{
    private readonly IMessageRoomRepository _roomRepository;

    public ChatRoomsController(IMessageRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MessageRoom>>> GetRooms()
        => Ok(await _roomRepository.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<MessageRoom>> GetRoom(int id)
    {
        var room = await _roomRepository.GetByIdAsync(id);
        return room == null ? NotFound() : Ok(room);
    }

    [HttpPost]
    public async Task<ActionResult<MessageRoom>> CreateRoom(int RoomId, int UserId)
    {
        var created = await _roomRepository.CreateAsync(new MessageRoom { Name = $"DM:{UserId}_{RoomId}" });
        return CreatedAtAction(nameof(GetRoom), new { id = created.Id }, created);
    }


}
