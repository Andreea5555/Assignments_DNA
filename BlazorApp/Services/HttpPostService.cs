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
    public async Task<ActionResult<CreatePostDto>> CreatePostAsync(CreatePostDto request)
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

    public async Task<IResult> UpdatePostAsync(int id, CreatePostDto request)
    {
        HttpResponseMessage httpResponse =
            await _httpClient.PostAsJsonAsync("posts", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }

        JsonSerializer.Deserialize<CreatePostDto>(response,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return Results.NoContent();
    }

    public async Task<IResult> DeletePostAsync(int id)
    {
        HttpResponseMessage httpResponse =
            await _httpClient.PostAsJsonAsync("posts", id);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }

        JsonSerializer.Deserialize<CreatePostDto>(response,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return Results.NoContent();
    }

    public async Task<ActionResult<CreatePostDto>> GetSinglePostAsync(int id)
    {
        HttpResponseMessage httpResponse =
            await _httpClient.PostAsJsonAsync("posts", id);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }

        return JsonSerializer.Deserialize<CreatePostDto>(response,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }

    public async Task<IResult> GetAllPostsAsync(string title, int userID)
    {
        HttpResponseMessage httpResponse = null;
        if (!string.IsNullOrEmpty(title))
        { 
            httpResponse= await _httpClient.PostAsJsonAsync("posts",title );
        }
        else if (userID!=null)
        {
            httpResponse= await _httpClient.PostAsJsonAsync("posts",userID );
        }
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }

        JsonSerializer.Deserialize<CreatePostDto>(response,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return Results.NoContent();
    }
}