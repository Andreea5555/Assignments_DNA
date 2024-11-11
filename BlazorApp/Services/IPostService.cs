using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Services;

public interface IPostService
{
    public Task<ActionResult<CreatePostDto>> CreatePostAsync(CreatePostDto request);
    public Task<IResult> UpdatePostAsync(int id, CreatePostDto request);
    public Task<IResult> DeletePostAsync(int id);
    public Task<ActionResult<CreatePostDto>> GetSinglePostAsync(int id);
    public Task<IResult> GetAllPostsAsync(string title, int userID);

}