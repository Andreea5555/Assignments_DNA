using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Services;

public interface ICommentService
{
    public Task<ActionResult<CreateCommentDto>> CreateCommentAsync(CreateCommentDto request);
    public Task<IResult> DeleteCommentAsync(int id);
    public Task<IResult> UpdateCommentAsync( CreateCommentDto request);
    public Task<ActionResult<CreateCommentDto>> GetSingleCommentAsync(int id);
    public Task<IResult> GetAllCommentsAsync(int userID, int postID);
}