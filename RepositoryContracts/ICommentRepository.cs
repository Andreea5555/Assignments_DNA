using Entities;

namespace RepositoryContracts;

public interface ICommentRepository
{
    Task<Comment> CreateComment(Comment comment);
    Task UpdateComment(Comment comment);
    Task DeleteComment(int commentId);
    Task<Comment> GetComment(int commentId);
    IQueryable<Comment> GetManyComments();
}