using Entities;
using RepositoryContracts;

namespace CLI.User_Interface.ManageUsers;

public class ListUsersView
{
    private readonly IUserRepository userRepo;

    public ListUsersView(IUserRepository repository)
    {
        userRepo = repository;
    }

    public void ListUsers()
    {
        foreach (User user in userRepo.GetMany() )
        {
            if (user != null)
            {
                Console.WriteLine("Username "+user.UserName);
                Console.WriteLine("ID"+user.ID);

            }
        }
    }
}