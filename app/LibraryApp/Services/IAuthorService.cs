using LibraryApp.Models;

namespace LibraryApp.Services;

public interface IAuthorService
{
    public Task<List<Author>> GetAllAuthorsAsync();
    public Task<Author> AddAuthorAsync(Author newAuthor);
}
