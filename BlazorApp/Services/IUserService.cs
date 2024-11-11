using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Services;

public interface IUserService
{
    public Task<UserDto> AddUserAsync(CreateUserDto request);
    public Task<IResult> UpdateUserAsync(int id,CreateUserDto request);
    public Task<IResult> DeleteUserAsync(int id);
    public Task<ActionResult<UserDto>> GetSingleUserAsync(int id);
    public Task<IResult> GetAllUsersAsync(string usernameContains);
}