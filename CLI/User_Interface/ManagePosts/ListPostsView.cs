using Entities;
using RepositoryContracts;

namespace CLI.User_Interface.ManagePosts;

public class ListPostsView
{
    private readonly IPostRepository postRepository;

    public ListPostsView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public void GetAllPosts()
    {
        foreach (var post in postRepository.GetMany())
        {
            if (post != null)
            {
                Console.Write("ID: " + post.ID + " ");
                Console.WriteLine("Title: " + post.Title);
            }
        }
    }
}