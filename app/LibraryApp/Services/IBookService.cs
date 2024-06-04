using LibraryApp.Models;

namespace LibraryApp;

public interface IBookService
{
    public Task<List<BookDetail>> GetBookDetailsAsync();
}
