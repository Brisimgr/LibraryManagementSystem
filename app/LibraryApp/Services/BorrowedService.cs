using LibraryApp.Data;
using LibraryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Services;

public class BorrowedService : IBorrowedService
{
    private readonly LibraryDbContext _context;
    public BorrowedService(LibraryDbContext context) 
    {
        _context = context;
    }

    public async Task<Borrowed> AddBorrowedAsync(Borrowed newBorrow)
    {
        _context.Borroweds.Add(newBorrow);
        await _context.SaveChangesAsync();

        return newBorrow;
    }

    public async Task<List<BorrowedDetail>> GetBorrowedDetailsByUserAsync(string userName)
    {
        return await _context.BorrowedDetails.Where(b => b.User == userName).ToListAsync();
    }

    public async Task<List<BorrowedDetail>> GetBorrowedDetailsAsync()
    {
        return await _context.BorrowedDetails.ToListAsync();
    }
}
