using LibraryApp.Models;

namespace LibraryApp.Services;

public interface IBorrowedService
{
    public Task<List<BorrowedDetail>> GetBorrowedDetailsAsync();
    public Task<BorrowedDetail> GetBorrowedDetailByIdAsync(int id);
    public Task<List<BorrowedDetail>> GetBorrowedDetailsByUserAsync(string userName);
    public Task<Borrowed> GetBorrowedByIdAsync(int id);
    public Task<Borrowed> AddBorrowedAsync(Borrowed newBorrow);
    public Task UpdateBorrowedAsync(Borrowed borrowedEntry);
}
