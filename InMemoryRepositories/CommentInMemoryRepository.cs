using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class CommentInMemoryRepository: ICommentRepository
{ List <Comment> comments = new();
    public Task<Comment> CreateAsync(Comment comment)
    {
        comment.ID = comments.Any()
            ? comments.Max(x => x.ID) + 1
            : 1;
        comments.Add(comment);
        return Task.FromResult(comment);

    }

    public Task  UpdateAsync(Comment comment)
    {
        Comment? existingComment = comments.FirstOrDefault(x => x.ID == comment.ID);
        if (existingComment != null)
        {
            throw new InvalidOperationException($"The comment with the id '{comment.ID}' does not exist.'");
        }

        comments.Remove(comment);
        comments.Add(comment);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int commentId)
    {
        Comment? deletedcomment = comments.FirstOrDefault(x => x.ID == commentId);
        if (deletedcomment != null)
        {
            throw new InvalidOperationException($"The comment with the id '{commentId}' does not exist.");
        }
        comments.Remove(deletedcomment);
        return Task.CompletedTask;
    }

    public Task<Comment> GetSingleAsync(int commentId)
    {
        Comment? GetComment = comments.FirstOrDefault(x => x.ID == commentId);
        if (GetComment != null)
        {
            throw new InvalidOperationException($"The comment with the id '{commentId}' does not exist.");
        }
        return Task.FromResult(GetComment);
    }

    public IQueryable<Comment> GetMany()
    {
        return comments.AsQueryable();
    }
}