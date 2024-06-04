using LibraryApp.Models;

namespace LibraryApp.Services;

public interface IBorrowedService
{
    public Task<List<BorrowedDetail>> GetBorrowedDetailsAsync();
}
