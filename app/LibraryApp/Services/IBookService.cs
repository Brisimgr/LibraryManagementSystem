using LibraryApp.Models;

namespace LibraryApp.Services;

public interface IBookService
{
    public Task<List<BookDetail>> GetBookDetailsAsync();
}
