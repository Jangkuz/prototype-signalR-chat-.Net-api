using System;
using ChatRoom.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.Api.Repositories;

public class ChatRoomDbContext : DbContext
{
    public ChatRoomDbContext(DbContextOptions<ChatRoomDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<MessageRoom> MessageRooms { get; set; } = null!;
    public DbSet<Message> Messages { get; set; } = null!;
    public DbSet<RoomMember> RoomMembers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MessageRoom>() 
            .HasMany(r => r.Members)
            .WithOne(m => m.Room)
            .HasForeignKey(m => m.RoomId);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.User)
            .WithMany(u => u.Messages)
            .HasForeignKey(m => m.UserId);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.ChatRoom)
            .WithMany(r => r.Messages)
            .HasForeignKey(m => m.ChatRoomId);
    }
}
