using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Services;

public interface IPostService
{
    public Task<CreatePostDto> CreatePostAsync(CreatePostDto request);
    public Task UpdatePostAsync(int id, PostDto request);
    public Task DeletePostAsync(int id);
    public Task<CreatePostDto> GetSinglePostAsync(int id);
    public Task<IEnumerable<CreatePostDto>> GetAllPostsAsync(string title, int userID);

}