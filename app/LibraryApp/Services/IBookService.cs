using LibraryApp.Models;

namespace LibraryApp.Services;

public interface IBookService
{
    public Task<List<BookDetail>> GetBookDetailsAsync();
    public Task<BookDetail> GetBookDetailAsync(int bookId);
    public Task<Book> GetBookByIdAsync(int bookId);
    public Task<List<BookDetail>> SearchBooksAsync(string searchOption, string searchCrit);
    public Task UpdateBookAsync(Book book);
    public Task<Book> AddBookAsync(Book newBook);
}
