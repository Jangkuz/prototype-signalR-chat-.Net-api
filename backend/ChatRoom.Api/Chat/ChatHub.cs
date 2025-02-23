using System;
using ChatRoom.Api.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatRoom.Api.Chat;

public class ChatHub : Hub
{
    public async Task JoinRoom(int roomId)
        => await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());

    public async Task LeaveRoom(int roomId)
        => await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId.ToString());

    public async Task SendMessage(int roomId, string message, int userId)
        {
            Console.WriteLine($"=======Sending message to room: {roomId}, message: {message}, userId: {userId}============");
            MessageDTO messageDTO = new MessageDTO{UserId = userId, Content = message, RoomId = roomId};
            await Clients.Group(roomId.ToString()).SendAsync("ReceiveMessage", messageDTO);
            Console.WriteLine($"MessageDTO: {messageDTO.UserId}, {messageDTO.Content}, {messageDTO.RoomId}");
        }
    public async Task SendMessage2(MessageDTO messageDTO)
        {
            Console.WriteLine($"=======Sending message to room: {messageDTO.RoomId}, message: {messageDTO.Content}, userId: {messageDTO.UserId}============");
            await Clients.Group(messageDTO.RoomId.ToString()).SendAsync("ReceiveMessage", messageDTO);
            Console.WriteLine($"MessageDTO: {messageDTO.UserId}, {messageDTO.Content}, {messageDTO.RoomId}");
        }
}