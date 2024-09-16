using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class PostInMemoryRepository : IPostRepository
{
    List<Post> posts = new List<Post>();

    public async Task<Post> AddAsync(Post post)
    {
        post.ID = posts.Any()
            ? posts.Max(p => p.ID) + 1
            : 1;
        posts.Add(post);
        Post created = await Task.FromResult(post);
        return created;
    }

    public Task UpdateAsync(Post post)
    {
        Post? existingPost = posts.SingleOrDefault(p => p.ID == post.ID);
        if (existingPost == null)
        {
            throw new InvalidOperationException(
                "Post with ID '{post.ID}'not found");
        }

        posts.Remove(existingPost);
        posts.Add(post);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Post? postToRemove = posts.SingleOrDefault(p => p.ID == id);
        if (postToRemove == null)
        {
            throw new InvalidOperationException("Post with ID '{id}'not found");
        }

        posts.Remove(postToRemove);
        return Task.CompletedTask;
    }

    public async Task<Post> GetSingleAsync(int id)
    {
        Post? postToReturn = posts.SingleOrDefault(p => p.ID == id);
        if (postToReturn == null)
        {
            throw new InvalidOperationException("Post with ID '{id}'not found");
        }

       Post created= await Task.FromResult(postToReturn);
       return created;
    }

    public IQueryable<Post> GetMany()
    {
        return posts.AsQueryable();
    }
}