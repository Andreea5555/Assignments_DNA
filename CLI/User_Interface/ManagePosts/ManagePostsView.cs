using Entities;
using RepositoryContracts;

namespace CLI.User_Interface.ManagePosts;

public class ManagePostsView
{
   IPostRepository postRepo;
    public ManagePostsView(IPostRepository postRepository)
    {
        postRepo = postRepository;
    }

    public void DeletePostAsync()
    {
        Console.WriteLine("Please give the id of the post you want to delete");
        int id = int.Parse(Console.ReadLine());
        postRepo.DeleteAsync(id);
        Console.WriteLine("The post was successfully deleted");
    }

    public async Task<Post> UpdatePostAsync()
    {
        Console.WriteLine("Please give the ID of the post you want to update");
        int postId = int.Parse(Console.ReadLine());
        Post post= await postRepo.GetSingleAsync(postId);
        Console.WriteLine("What would you like to update?\n"+"1.Title\n"+"2.Content\n");
         int opt=int.Parse(Console.ReadLine());
         if (opt == 1)
         {
             Console.WriteLine("What is the new title?:");
             post.Title = Console.ReadLine();
         }
         else if (opt == 2)
         {
             Console.WriteLine("What is the new content?:");
             post.Body= Console.ReadLine();
         }
        postRepo.UpdateAsync(post);
        return post;
    }
}