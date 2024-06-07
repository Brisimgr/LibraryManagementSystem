using LibraryApp.Data;
using LibraryApp.Models;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace LibraryApp.Services;

public class BookService : IBookService
{
    private readonly LibraryDbContext _context;

    public BookService(LibraryDbContext context) 
    {
        _context = context;
    }

    public async Task<BookDetail> GetBookDetailAsync(int bookId)
    {
        return await _context.BookDetails.SingleOrDefaultAsync(b => b.BookId == bookId);
    }

    public async Task<List<BookDetail>> GetBookDetailsAsync()
    {
        return await _context.BookDetails.ToListAsync();
    }

    public async Task<List<BookDetail>> SearchBooksAsync(string searchOption, string searchCrit)
    {
        var searchOptionParam = new MySqlParameter("@search_option", searchOption);
        var searchCritParam = new MySqlParameter("@search_crit", searchCrit);

        return await _context.BookDetails
                             .FromSqlRaw("CALL find_books(@search_option, @search_crit)", searchOptionParam, searchCritParam)
                             .ToListAsync();
    }
}
