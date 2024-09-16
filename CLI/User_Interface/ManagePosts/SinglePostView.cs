using Entities;
using RepositoryContracts;

namespace CLI.User_Interface.ManagePosts;

public class SinglePostView
{
    private readonly IPostRepository postRepository;

    public SinglePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task getSinglePost()
    {
        Console.WriteLine("Please enter the id of the post you want to view");
        int id=int.Parse(Console.ReadLine()); 
       Post post= await postRepository.GetSingleAsync(id);
        Console.WriteLine("the post you are looking for has the title " +
                          post.Title + " and the content " + post.Body);
    }
}