using LibraryApp.Data;
using LibraryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Services;

public class GenresService : IGenresService
{
    private readonly LibraryDbContext _context;
    public GenresService(LibraryDbContext context) 
    {
        _context = context;
    }
    public async Task<List<Genre>> GetGenresAsync()
    {
        return await _context.Genres.ToListAsync();
    }
}
