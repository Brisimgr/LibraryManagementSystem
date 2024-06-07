using LibraryApp.Data;
using LibraryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Services;

public class BranchesService : IBranchesService
{
    private readonly LibraryDbContext _context;
    public BranchesService(LibraryDbContext context) 
    {
        _context = context;
    }

    public async Task<List<Branch>> GetAllBranches()
    {
        return await _context.Branches.ToListAsync();
    }
}
