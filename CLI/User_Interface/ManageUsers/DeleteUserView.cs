using Entities;
using RepositoryContracts;

namespace CLI.User_Interface.ManageUsers;

public class DeleteUserView
{
    private readonly IUserRepository userRepo;

    public DeleteUserView(IUserRepository userRepository)
    {
        userRepo = userRepository;
    }

    public void DeleteUser(int userId)
    {
        userRepo.DeleteAsync(userId);
    }
    
}