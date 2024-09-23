using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class CommentFileRepository:ICommentRepository
{
    private readonly string filePath = "comments.json";

    public CommentFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }
    
    public async Task<Comment> CreateAsync(Comment comment)
    {
        string commentsAsJson=await File.ReadAllTextAsync(filePath);
        List<Comment> comments=JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        int maxId = comments.Count > 0 ? comments.Max(c => c.ID) : 1;
        comment.ID = maxId + 1;
        comments.Add(comment);
        commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
        return comment;
    }

    public async Task UpdateAsync(Comment comment)
    {
        string commentsAsJson=await File.ReadAllTextAsync(filePath);
        List<Comment> comments=JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        Comment? existingComment = comments.FirstOrDefault(x => x.ID == comment.ID);
        if (existingComment != null)
        {
            throw new InvalidOperationException($"The comment with the id '{comment.ID}' does not exist.'");
        }

        comments.Remove(comment);
        comments.Add(comment);
        commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, JsonSerializer.Serialize(comments));
    }

    public async Task DeleteAsync(int commentId)
    {
        string commentsAsJson=await File.ReadAllTextAsync(filePath);
        List<Comment> comments=JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        Comment? deletedcomment = comments.FirstOrDefault(x => x.ID == commentId);
        if (deletedcomment != null)
        {
            throw new InvalidOperationException($"The comment with the id '{commentId}' does not exist.");
        }
        comments.Remove(deletedcomment);
        commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, JsonSerializer.Serialize(comments));
    }

    public async Task<Comment> GetSingleAsync(int commentId)
    {
        string commentsAsJson=await File.ReadAllTextAsync(filePath);
        List<Comment> comments=JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        Comment? getComment = comments.FirstOrDefault(x => x.ID == commentId);
        if (getComment != null)
        {
            throw new InvalidOperationException($"The comment with the id '{commentId}' does not exist.");
        }
        commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
        return getComment;
    }

    public IQueryable<Comment> GetMany()
    {
        string commentsAsJson = File.ReadAllTextAsync(filePath).Result;
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        return comments.AsQueryable();
    }
}