using Entities;
using RepositoryContracts;

namespace CLI.User_Interface.ManageUsers;

public class CreateUserView
{
    private readonly IUserRepository userRepo;
    private User user = new User("admin", "superSecret");
    private User user2 = new User("andreea", "superSecret2");
    private User user3 = new User("marius", "zarzavat");

    public CreateUserView(IUserRepository userRepository)
    {
        this.userRepo = userRepository;
        userRepo.AddAsync(user);
        userRepo.AddAsync(user2);
        userRepo.AddAsync(user3);
    }
   
    public async Task<User> CreateUser()
    {
        Console.WriteLine("Enter User Name:");
        string userName = Console.ReadLine();
        Console.WriteLine("Enter Password:");
        string password = Console.ReadLine();
        User user = new User(userName, password);
       return await userRepo.AddAsync(user);
    }
}