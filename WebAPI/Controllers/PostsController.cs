using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class PostsController: ControllerBase
{
    private readonly IPostRepository postRepo;

    public PostsController(IPostRepository postRepo)
    {
        this.postRepo = postRepo;
    }
    [HttpPost]
    public async Task<ActionResult<CreatePostDto>> AddPost([FromBody] CreatePostDto request)
    {
        try
        {
            Post post = new(request.Title, request.Body, request.UserId);
            Post created = await postRepo.AddAsync(post);
            return Created($"/Posts/{created.ID}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    [HttpPut("{id}")]
    public async Task<IResult> UpdatePost([FromRoute] int id,[FromBody] CreatePostDto request)
    {
        Post post=new(request.Title, request.Body, request.UserId);
        await postRepo.UpdateAsync(post);
        return Results.NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IResult> DeletePost([FromBody]int postID)
    {
        await postRepo.DeleteAsync(postID);
        return Results.NoContent();

    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<CreatePostDto>> GetSinglePost([FromRoute]int id)
    {
        try
        {
            Post result = await postRepo.GetSingleAsync(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<IResult> GetAllPosts([FromQuery] string? title, [FromQuery] int? UserId )
    {
        List<Post> posts=postRepo.GetAll();
        if (!string.IsNullOrEmpty(title))
        {
            posts = posts.Where(p => p.Title.Equals(title)).ToList();
        }

       else if (UserId.HasValue)
        {
            posts=posts.Where(p=>p.UserID.Equals(UserId.Value)).ToList();
        }
        return Results.Ok(posts);
    }

}