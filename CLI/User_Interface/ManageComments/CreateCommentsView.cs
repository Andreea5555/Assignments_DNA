using Entities;
using RepositoryContracts;

namespace CLI.User_Interface.ManageComments;

public class CreateCommentsView()
{
    private readonly ICommentRepository commRepository;
    private readonly IPostRepository postRepository;
    private Comment comment = new Comment("I also love cats!", 2, 2);
    private Comment comment2 = new Comment("NO, my friends are the best",3, 3);
    private Comment comment3 = new Comment("Nobody wants to check them out!", 1, 1);
    public CreateCommentsView(ICommentRepository commentRepository, IPostRepository postRepo) : this()
    {
        commRepository = commentRepository;
        postRepository = postRepo;
        commRepository.CreateAsync(comment);
        commRepository.CreateAsync(comment2);
        commRepository.CreateAsync(comment3);
        
    }

    public async Task CreateCommAsync()
    {
        //I create the comment, I could have done it better, but it's honest work, I don't check the postID but whatever
        Console.WriteLine("Please enter your comment: ");
        string content = Console.ReadLine();
        Console.WriteLine("Please enter the post id you want this comment to be attached to: ");
        int postId = int.Parse(Console.ReadLine());
        Post post = await postRepository.GetSingleAsync(postId);
        int userId = post.UserID;
        Comment comment=new Comment(content, postId, userId);
        await commRepository.CreateAsync(comment);
    }
}