using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ChatRoom.Api.Repositories;
using ChatRoom.Api.Models;
using ChatRoom.Api.Mapper;
namespace ChatRoom.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepository _messageRepository;

        public MessageController(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessageById(int id)
        {
            var message = await _messageRepository.GetMessageById(id);
            return message == null ? NotFound() : Ok(message);
        }

        [HttpGet("{roomId}/messages")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessagesByRoomId(int roomId)
        {
            var messages = await _messageRepository.GetMessagesByRoomId(roomId);
            return messages == null ? NotFound() : Ok(messages);
        }
        
        [HttpPost]
        public async Task<ActionResult<Message>> InsertMessage(MessageDTO messageDTO)
        {
            var message = messageDTO.ToEntity();
            var newMessage = await _messageRepository.InsertMessage(message);
            return newMessage == null ? NotFound() : Ok(newMessage);
        }
    }


}
