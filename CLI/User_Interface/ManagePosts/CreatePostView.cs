using Entities;
using RepositoryContracts;

namespace CLI.User_Interface.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository postRepository;
    private Post post=new Post("Some new posts coming","Here are more to watch",1);
    private Post post2=new Post("I love cats","They are so fluffy",2);
    private Post post3=new Post("My friends are the best","We know eachother from such a long time",2);
    
    public CreatePostView(IPostRepository postRepo)
    {
        postRepository = postRepo;
        postRepository.AddAsync(post);
        postRepository.AddAsync(post2);
        postRepository.AddAsync(post3);
        
    }

    public async void CreatePostAsync()
    {//I also could have done this better by checking the user ID
        Console.WriteLine("Enter Post Title: ");
        string Title=Console.ReadLine();
        Console.WriteLine("Enter Post Description: ");
        string Body = Console.ReadLine();
        Console.WriteLine("Enter User ID: ");
        int UserID=int.Parse(Console.ReadLine());
        Post post=new Post(Title,Body,UserID);
      await postRepository.AddAsync(post);
    }
}