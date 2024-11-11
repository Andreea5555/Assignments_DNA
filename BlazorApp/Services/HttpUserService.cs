using System.Text.Json;
using DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Services;

public class HttpUserService : IUserService
{
    private readonly HttpClient _httpClient;

    public HttpUserService(HttpClient client)
    {
        _httpClient = client;
    }

    public async Task<UserDto> AddUserAsync(CreateUserDto request)
    {
        HttpResponseMessage httpResponse =
            await _httpClient.PostAsJsonAsync("users", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }

        return JsonSerializer.Deserialize<UserDto>(response,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }

    public async Task<IResult> UpdateUserAsync(int id, CreateUserDto request)
    {
        HttpResponseMessage httpResponse =
            await _httpClient.PostAsJsonAsync("users", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }

        JsonSerializer.Deserialize<UserDto>(response,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return Results.NoContent();
    }

    public async Task<IResult> DeleteUserAsync(int id)
    {
        HttpResponseMessage httpResponse =
            await _httpClient.PostAsJsonAsync("users", id);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }

        JsonSerializer.Deserialize<UserDto>(response,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return Results.NoContent();
    }

    public async Task<ActionResult<UserDto>> GetSingleUserAsync(int id)
    {
        HttpResponseMessage httpResponse =
            await _httpClient.PostAsJsonAsync("users", id);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }

        return JsonSerializer.Deserialize<UserDto>(response,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }

    public async Task<IResult> GetAllUsersAsync(string usernameContains)
    {
        HttpResponseMessage httpResponse =
            await _httpClient.PostAsJsonAsync("users", usernameContains);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }

        JsonSerializer.Deserialize<UserDto>(response,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return Results.NoContent();
    }
}