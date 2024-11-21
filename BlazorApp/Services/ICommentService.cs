using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Services;

public interface ICommentService
{
    public Task<CreateCommentDto> CreateCommentAsync(CreateCommentDto request);
    public Task DeleteCommentAsync(int id);
    public Task UpdateCommentAsync( CreateCommentDto request);
    public Task<CreateCommentDto> GetSingleCommentAsync(int id);
    public Task<IEnumerable<CreateCommentDto>> GetAllCommentsAsync(int userID, int postID);
}