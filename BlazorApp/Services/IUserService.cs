using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Services;

public interface IUserService
{
    public Task<UserDto> AddUserAsync(CreateUserDto request);
    public Task UpdateUserAsync(int id,CreateUserDto request);
    public Task DeleteUserAsync(int id);
    public Task<UserDto> GetSingleUserAsync(int id);
    public Task<IEnumerable<UserDto>> GetAllUsersAsync(string usernameContains);
}