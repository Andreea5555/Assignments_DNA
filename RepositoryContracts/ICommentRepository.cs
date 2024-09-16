using Entities;

namespace RepositoryContracts;

public interface ICommentRepository
{
    Task<Comment> CreateAsync(Comment comment);
    Task UpdateAsync(Comment comment);
    Task DeleteAsync(int commentId);
    Task<Comment> GetSingleAsync(int commentId);
    IQueryable<Comment> GetMany();
}