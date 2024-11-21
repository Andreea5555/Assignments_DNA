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

    public async Task UpdateUserAsync(int id, CreateUserDto request)
    {
        HttpResponseMessage httpResponse =
            await _httpClient.PutAsJsonAsync($"users/{id}", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }

        JsonSerializer.Deserialize<CreateUserDto>(response,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task DeleteUserAsync(int id)
    {
        HttpResponseMessage httpResponse =
            await _httpClient.DeleteAsync($"users/{id}");
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
    }

    public async Task<UserDto> GetSingleUserAsync(int id)
    {
        HttpResponseMessage httpResponse =
            await _httpClient.GetAsync($"users/{id}");
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }

        return JsonSerializer.Deserialize<UserDto>(response,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync(string usernameContains)
    {
        string response = null;
        HttpResponseMessage httpResponse = null;
        if (usernameContains != null)
        {  
            httpResponse =
                await _httpClient.GetAsync($"users/{ usernameContains}");
            response = await httpResponse.Content.ReadAsStringAsync();
        } 
        else if (usernameContains == null)
        {
            httpResponse = await _httpClient.GetAsync("users"); 
            response = await httpResponse.Content.ReadAsStringAsync();
        }
        
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }

        return JsonSerializer.Deserialize<IEnumerable<UserDto>>(response,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        
    }
}