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
    public async Task<ActionResult<CreateCommentDto>> CreateCommentAsync(CreateCommentDto request)
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

    public async Task<IResult> DeleteCommentAsync(int id)
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
        return Results.NoContent();
    }

    public async Task<IResult> UpdateCommentAsync(CreateCommentDto request)
    {
        HttpResponseMessage httpResponse =
            await _httpClient.PostAsJsonAsync("comments",request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }

        JsonSerializer.Deserialize<CreateCommentDto>(response,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return Results.NoContent();
    }

    public async Task<ActionResult<CreateCommentDto>> GetSingleCommentAsync(int id)
    {
        HttpResponseMessage httpResponse =
            await _httpClient.PostAsJsonAsync("comments", id);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }

        return JsonSerializer.Deserialize<CreateCommentDto>(response,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }

    public async Task<IResult> GetAllCommentsAsync(int userID, int postID)
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

        JsonSerializer.Deserialize<CreateCommentDto>(response,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return Results.NoContent();
    }
}