using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.Api.Models;

[PrimaryKey(nameof(Id))]
public class MessageRoom
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    [JsonIgnore]
    public ICollection<RoomMember> Members { get; set; } = new List<RoomMember>();
    [JsonIgnore]
    public ICollection<Message> Messages { get; set; } = new List<Message>();
}
