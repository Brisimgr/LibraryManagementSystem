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
    public async Task<List<BorrowedDetail>> GetBorrowedDetailsAsync()
    {
        return await _context.BorrowedDetails.ToListAsync();
    }
}
