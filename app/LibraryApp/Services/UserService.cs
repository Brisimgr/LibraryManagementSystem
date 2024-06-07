using LibraryApp.Data;
using LibraryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Services;

public class UserService : IUserService
{
    private readonly LibraryDbContext _context;
    public UserService(LibraryDbContext context) 
    {
        _context = context;
    }

    public async Task<User> GetUserByNameAsync(string userName)
    {
        return await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Login == userName);
    }

    public async Task<User> GetUserByIdAsync(int userId)
    {
        return await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.UserId == userId);
    }
}
