using LibraryApp.Data;
using LibraryApp.Models;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace LibraryApp.Services;

public class AuthorService : IAuthorService
{
    private readonly LibraryDbContext _context;

    public AuthorService(LibraryDbContext context) 
    {
        _context = context;
    }

    public async Task<Author> AddAuthorAsync(Author newAuthor)
    {
        _context.Authors.Add(newAuthor);
        await _context.SaveChangesAsync();
        return newAuthor;
    }

    public Task<List<Author>> GetAllAuthorsAsync()
    {
        return _context.Authors.ToListAsync();
    }
}
