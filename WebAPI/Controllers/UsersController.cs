using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository userRepo;

    public UsersController(IUserRepository userRepo)
    {
        this.userRepo = userRepo;
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> AddUser(
        [FromBody] CreateUserDto request)
    {
        try
        {
            await VerifyUserNameIsAvailableAsync(request.UserName);
            User user = new(request.UserName, request.Password);
            User created = await userRepo.AddAsync(user);
            UserDto dto = new()
            {
                Id = created.ID,
                UserName = created.UserName,
            };
            return Created($"/Users/{dto.Id}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IResult> UpdateUser([FromRoute] int id,[FromBody]
        CreateUserDto request)
    {
        User user = new User(request.UserName, request.Password);
        await userRepo.UpdateAsync(id, user);
        return Results.NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IResult> DeleteUser([FromBody] int id)
    {
        await userRepo.DeleteAsync(id);
        return Results.NoContent();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetSingleUser([FromRoute] int id)
    {
        try
        {
            User result = await userRepo.GetSingleAsync(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<IResult> GetAllUsers([FromQuery] string? userNameContains)
    {
        List<User> users = userRepo.GetAll();
        if (!string.IsNullOrEmpty(userNameContains))
        {
            users = users.Where(p =>
                    p.UserName.ToLower().Contains(userNameContains.ToLower()))
                .ToList();
        }

        return Results.Ok(users);
    }

    private async Task VerifyUserNameIsAvailableAsync(string requestUserName)
    {
        foreach (User user in userRepo.GetMany())
        {
            if (user.UserName.Equals(requestUserName))
            {
                throw new Exception($"User {requestUserName} already exists");
            }
        }
    }
}