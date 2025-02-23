using System;
using ChatRoom.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.Api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ChatRoomDbContext _context;

    public UserRepository(ChatRoomDbContext context)
    {
        _context = context;
    }

    public async Task<User> CreateAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}
