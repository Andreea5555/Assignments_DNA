using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class CommentsController: ControllerBase
{
    private readonly ICommentRepository commentRepo;

    public CommentsController(ICommentRepository commentRepo)
    {
        this.commentRepo = commentRepo;
    }
    [HttpPost]
    public async Task<ActionResult<CreateCommentDto>> AddComment([FromBody]CreateCommentDto request)
    {
        try
        {
            Comment comment = new(request.Body, request.UserID, request.PostID);
            Comment created = await commentRepo.CreateAsync(comment);
            return Created($"/Users/{created.ID}", created);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
        
    }
    
    [HttpPut("{id}")]
    public async Task<IResult> UpdateComment([FromRoute] int id,[FromBody] CreateCommentDto request)
    {
        Comment comment=new Comment(request.Body,request.PostID,request.UserID);
        comment.ID = id;
        await commentRepo.UpdateAsync(comment);
        return Results.Ok(comment);
    }
    
    [HttpDelete("{id}")]
    public async Task<IResult> DeleteComment( [FromRoute] int id)
    {
        await commentRepo.DeleteAsync(id);
        return Results.Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CreateCommentDto>> GetSingleAsync(
        [FromRoute] int id)
    {
        try
        {
            Comment result = await commentRepo.GetSingleAsync(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("")]
    public async Task<IResult> GetAllComments([FromQuery] int? userId,
       [FromQuery] int? postId)
    {
        List<Comment> comments = commentRepo.GetAll();
        if (userId != null)
        {
            comments = comments.Where(c => c.UserID == userId).ToList();
        }

       else if (postId != null)
        {
            comments = comments.Where(c => c.PostID == postId).ToList();
        }
        return Results.Ok(comments);
    }
}