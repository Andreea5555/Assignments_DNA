using Entities;

namespace RepositoryContracts;

public interface IPostRepository
{
    Task<Post> AddAsync(Post post);
    Task UpdateAsync(Post post);
    Task DeleteAsync(int id);
    //GetSingleAsync - Get a specific post.
    Task<Post> GetSingleAsync(int id);
    //GetMany – Get multiple posts.
    IQueryable<Post> GetMany();
}