// See https://aka.ms/new-console-template for more information

using CLI.User_Interface;
using FileRepositories;
using RepositoryContracts;

Console.WriteLine("Starting CLI Application ");
IUserRepository userRepo=new UserFileRepository();
ICommentRepository commentRepo = new CommentFileRepository();
IPostRepository postRepo = new PostFileRepository();
CLI_App app = new CLI_App(userRepo, commentRepo, postRepo);
await app.StartAsync();