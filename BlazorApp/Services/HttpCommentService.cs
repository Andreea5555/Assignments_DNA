using System.Text.Json;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Services;

public class HttpCommentService: ICommentService
{
    private readonly HttpClient _httpClient;

    public HttpCommentService(HttpClient client)
    {
     _httpClient = client;   
    }
    public async Task<CreateCommentDto> CreateCommentAsync(CreateCommentDto request)
    {
        HttpResponseMessage httpResponse =
            await _httpClient.PostAsJsonAsync("comments", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }

        return JsonSerializer.Deserialize<CreateCommentDto>(response,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }

    public async Task DeleteCommentAsync(int id)
    {
        HttpResponseMessage httpResponse =
            await _httpClient.PostAsJsonAsync("comments", id);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }

        JsonSerializer.Deserialize<CreateCommentDto>(response,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task UpdateCommentAsync(CreateCommentDto request)
    {
        HttpResponseMessage httpResponse =
            await _httpClient.PutAsJsonAsync("comments", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }

        JsonSerializer.Deserialize<CreateCommentDto>(response,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task<CreateCommentDto> GetSingleCommentAsync(int id)
    {
        HttpResponseMessage httpResponse =
            await _httpClient.GetAsync($"comments/{id}");
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }

        return JsonSerializer.Deserialize<CreateCommentDto>(response,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }

    public async Task<IEnumerable<CreateCommentDto>> GetAllCommentsAsync(int userID, int postID)
    {
        HttpResponseMessage httpResponse = null;
        if (userID != null)
        {
           httpResponse= await _httpClient.PostAsJsonAsync("comments", userID);   
        }
        else if (postID != null)
        {
            httpResponse= await _httpClient.PostAsJsonAsync("comments", postID);   
        }
            
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }

        return JsonSerializer.Deserialize<IEnumerable<CreateCommentDto>>(response,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        
    }
}