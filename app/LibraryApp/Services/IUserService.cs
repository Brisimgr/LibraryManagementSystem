﻿using LibraryApp.Models;

namespace LibraryApp.Services;

public interface IUserService
{
    public Task<User> GetUserByNameAsync(string userName);
    public Task<User> GetUserByIdAsync(int userId);
    public Task<List<User>> GetAllUsersAsync();
}
