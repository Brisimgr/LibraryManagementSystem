using LibraryApp.Models;

namespace LibraryApp.Services;

public interface IBorrowedService
{
    public Task<List<BorrowedDetail>> GetBorrowedDetailsAsync();
    public Task<List<BorrowedDetail>> GetBorrowedDetailsByUserAsync(string userName);
    public Task<Borrowed> AddBorrowedAsync(Borrowed newBorrow);
}
