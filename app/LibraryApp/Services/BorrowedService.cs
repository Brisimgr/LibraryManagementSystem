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

    public Task<BorrowedDetail> GetBorrowedDetailByIdAsync(int id)
    {
        return _context.BorrowedDetails.FirstOrDefaultAsync(b => b.BorrowedId == id);
    }

    public Task<Borrowed> GetBorrowedByIdAsync(int id)
    {
       return _context.Borroweds
                      .Include(b => b.User)
                      .Include(b => b.Book)
                      .FirstOrDefaultAsync(b => b.BorrowedId == id);
    }

    public async Task UpdateBorrowedAsync(Borrowed borrowedEntry)
    {
        _context.Borroweds.Update(borrowedEntry);
        await _context.SaveChangesAsync();
    }
}
