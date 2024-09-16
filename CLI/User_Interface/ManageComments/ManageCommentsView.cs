using Entities;
using RepositoryContracts;

namespace CLI.User_Interface.ManageComments;

public class ManageCommentsView
{
    private readonly ICommentRepository commRepo;
    private readonly IPostRepository postRepo;
    public ManageCommentsView(ICommentRepository commRepository, IPostRepository postRepository)
    {
        this.commRepo = commRepository;
        postRepo = postRepository;
    }

    public void DeleteComments()
    {
        Console.WriteLine("Please enter the comment ID you want to delete");
        int commentID = int.Parse(Console.ReadLine());
        commRepo.DeleteAsync(commentID);
    }

    public async void ListComments()
    {
        foreach (var comment in commRepo.GetMany())
        {
            if (comment != null)
            {
                Console.WriteLine("ID of the comment: " + comment.ID+ " \n");
                Post post=await postRepo.GetSingleAsync(comment.PostID);
                Console.WriteLine("The title of the post that the comment is attached to:  " + post.Title+"\n");
                Console.WriteLine("Content: " + comment.Body+"\n");
            }
        }
    }
}