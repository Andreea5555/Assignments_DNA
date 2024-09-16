using CLI.User_Interface.ManageComments;
using CLI.User_Interface.ManagePosts;
using CLI.User_Interface.ManageUsers;
using Entities;
using RepositoryContracts;

namespace CLI.User_Interface;

public class CLI_App
{
    //hello there :)
    IUserRepository userRepository;
    IPostRepository postRepository;
    ICommentRepository commentRepository;
    private readonly CreatePostView postView;
    private readonly ManagePostsView mPView;
    private readonly ListPostsView lPView;
    private readonly SinglePostView sPView;
    private readonly CreateUserView cUView;
    private readonly ListUsersView lUView;
    private readonly DeleteUserView delUView;
    private readonly CreateCommentsView clsView;
    private readonly ManageCommentsView mCView;

    public CLI_App(IUserRepository userRepo, ICommentRepository commentRepo,
        IPostRepository postRepo)
    {//this is where I initialize everything
        userRepository = userRepo;
        postRepository = postRepo;
        commentRepository = commentRepo;
        postView = new CreatePostView(postRepository);
        mPView = new ManagePostsView(postRepository);
        lPView = new ListPostsView(postRepository);
        sPView = new SinglePostView(postRepository);
        cUView = new CreateUserView(userRepository);
        lUView = new ListUsersView(userRepository);
        delUView = new DeleteUserView(userRepository);
        clsView = new CreateCommentsView(commentRepository, postRepository);
        mCView = new ManageCommentsView(commentRepository,postRepository);
    }
  //most of these functions are self explanatory
    public async Task StartAsync()
    { //first i get my userId so i don't have to always look for it
        int userId = 0;
        Console.WriteLine("Hello! Welcome to MyApp Forum :D");

        Console.WriteLine("Please choose one of the following options: \n" +
                          "1.)Users \n" +
                          "2.)Posts \n" +
                          "3.)Comments \n" +
                          "-1.)Quit");
        int opt = int.Parse(Console.ReadLine());
        if (opt == 1)
        {//first i can do some fun stuff with users
            Console.WriteLine("What would you like to do?\n" +
                              "a.Create User\n" +
                              "b.List Users \n" + "c.Delete User");
            string opt1 = Console.ReadLine();
            if (opt1.Equals("a"))
            {
                User user = await cUView.CreateUser();
                userId = user.ID;
            }
            else if (opt1.Equals("b"))
            {
                lUView.ListUsers();
            }
            else if (opt1.Equals("c"))
            {
                delUView.DeleteUser(userId);
            }

            await StartAsync();
        }
        else if (opt == 2)
        {//then with posts
            Console.WriteLine("What would you like to do?\n" + "c.New Post\n" +
                              "d.Manage Posts\n" + "e.List Posts\n" +
                              "f.Show Single Post\n");
            string opt2 = Console.ReadLine();
            if (opt2.Equals("c"))
            {
                postView.CreatePostAsync();
                Console.WriteLine("Post created!");
            }
            else if (opt2.Equals("d"))
            {
                Console.WriteLine("Manage posts:\n" + "dp- delete post\n" +
                                  "up- update post\n");
                string opt21 = Console.ReadLine();
                if (opt21.Equals("dp"))
                {
                    mPView.DeletePostAsync();
                }
                else if (opt21.Equals("up"))
                {
                    await mPView.UpdatePostAsync();
                }
            }
            else if (opt2.Equals("e"))
            {
                lPView.GetAllPosts();
            }
            else if (opt2.Equals("f"))
            {
                await sPView.getSinglePost();
            }

            await StartAsync();
        }

        else if (opt == 3)
        {//then with comments
            Console.WriteLine("What would you like to do?\n" +
                              "g.New Comment\n" + "h.Delete Comments\n" +
                              "i.List Comments\n");
            string opt3 = Console.ReadLine();
            if (opt3.Equals("g"))
            {
                await clsView.CreateCommAsync();
            }

            else if (opt3.Equals("h"))
            {
                mCView.DeleteComments();
            }
            else if (opt3.Equals("i"))
            {
                mCView.ListComments();
            }

          await  StartAsync();
        }
        else if (opt == -1)
            //this is only for quitting
        {
            return;
        }
    }
}