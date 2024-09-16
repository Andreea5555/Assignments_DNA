// See https://aka.ms/new-console-template for more information

using CLI.User_Interface;
using InMemoryRepositories;
using RepositoryContracts;

Console.WriteLine("Starting CLI Application ");
IUserRepository userRepo=new UserInMemoryRepository();
ICommentRepository commentRepo = new CommentInMemoryRepository();
IPostRepository postRepo = new PostInMemoryRepository();
CLI_App app = new CLI_App(userRepo, commentRepo, postRepo);
await app.StartAsync();