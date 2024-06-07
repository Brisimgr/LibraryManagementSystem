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

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _context.Users
                       .Where(u => u.UserRole != "admin")
                       .ToListAsync();
    }

    public async Task<int> GetCurrentlyBorrowedAsync(int userId)
    {
        return await _context.Borroweds
                             .Where(b => b.UserId == userId && b.ReturnDate == null)
                             .CountAsync();
    }

    public async Task<int> GetReturnedAsync(int userId)
    {
        return await _context.Borroweds
                             .Where(b => b.UserId == userId && b.ReturnDate != null)
                             .CountAsync();
    }
}
