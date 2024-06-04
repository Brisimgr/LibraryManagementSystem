using LibraryApp.Data;
using LibraryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Services;

public class BookService : IBookService
{
    private readonly LibraryDbContext _context;

    public BookService(LibraryDbContext context) 
    {
        _context = context;
    }

    public async Task<List<BookDetail>> GetBookDetailsAsync()
    {
        return await _context.BookDetails.ToListAsync();
    }

     
}
