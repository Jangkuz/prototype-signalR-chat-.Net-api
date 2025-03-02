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
        return Ok(room);
    }

    [HttpGet("{acc1Id}/{acc2Id}")]
    public async Task<ActionResult<MessageRoom>> GetRoomByAccountIds(int acc1Id, int acc2Id)
    {
        var room = await _roomRepository.GetByAccountIds(acc1Id, acc2Id);
        return Ok(room);
    }
    [HttpPost]
    public async Task<ActionResult<MessageRoom>> CreateRoom(CreateMessageRoomDTO roomDTO)
    {
        //set room name for easy data checking
        var created = await _roomRepository.CreateAsync(new MessageRoom { Name = $"DM:{roomDTO.Acc1Id}_{roomDTO.Acc2Id}" });
        return CreatedAtAction(nameof(GetRoom), new { id = created.Id }, created);
    }


}
