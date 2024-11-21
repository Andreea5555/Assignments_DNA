using System.Text.Json;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Services;

public class HttpPostService:IPostService
{
    private readonly HttpClient _httpClient;

    public HttpPostService(HttpClient client)
    {
        _httpClient = client;
    }
    public async Task<CreatePostDto> CreatePostAsync(CreatePostDto request)
    {
        HttpResponseMessage httpResponse =
            await _httpClient.PostAsJsonAsync("posts", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }

        return JsonSerializer.Deserialize<CreatePostDto>(response,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }

    public async Task UpdatePostAsync(int id, PostDto request)
    {
        HttpResponseMessage httpResponse =
            await _httpClient.PutAsJsonAsync($"posts/{id}", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }

        JsonSerializer.Deserialize<PostDto>(response,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task DeletePostAsync(int id)
    {
        HttpResponseMessage httpResponse =
            await _httpClient.DeleteAsync($"posts/{id}");
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        
    }

    public async Task<CreatePostDto> GetSinglePostAsync(int id)
    {
        HttpResponseMessage httpResponse =
            await _httpClient.GetAsync($"posts/{id}");
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }

        return JsonSerializer.Deserialize<CreatePostDto>(response,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }

    public async Task<IEnumerable<CreatePostDto>> GetAllPostsAsync(string title, int userID)
    {
        HttpResponseMessage httpResponse = null;
        if (!string.IsNullOrEmpty(title))
        { 
            httpResponse= await _httpClient.GetAsync($"posts?title={title}");
        }
        else if (userID!=null)
        {
            httpResponse= await _httpClient.GetAsync($"posts?userid={userID}");
        }
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }

       return JsonSerializer.Deserialize<IEnumerable<CreatePostDto>>(response,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }
}