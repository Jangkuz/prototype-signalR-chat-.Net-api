using System;
using ChatRoom.Api.Models;

namespace ChatRoom.Api.Mapper;

public static class UserMapper
{
    public static UserDTO ToDTO(this User user)
    {
        return new UserDTO
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
        };
    }

    public static User ToEntity(this UserDTO userDTO)
    {
        return new User
        {
            Id = userDTO.Id,
            Username = userDTO.Username,
            Email = userDTO.Email,
        };
    }
}
