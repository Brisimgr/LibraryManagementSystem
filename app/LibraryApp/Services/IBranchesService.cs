using LibraryApp.Models;

namespace LibraryApp.Services;

public interface IBranchesService
{
    public Task<List<Branch>> GetAllBranches();
}
