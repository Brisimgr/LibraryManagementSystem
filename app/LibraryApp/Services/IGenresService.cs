using LibraryApp.Models;
namespace LibraryApp.Services;

public interface IGenresService
{
    public Task<List<Genre>> GetGenresAsync();
}
