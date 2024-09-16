using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class UserInMemoryRepository: IUserRepository
{
    List<User> users = new List<User>();
    public Task<User> AddAsync(User user)
    {
        user.ID=users.Any()
            ?users.Max(x => x.ID) + 1
            :1;
        users.Add(user);
        return Task.FromResult(user);

    }
    

    public Task UpdateAsync(int idUser)
    {
        User? existingUser = users.FirstOrDefault(x => x.ID == idUser);
        if (existingUser != null)
        {
            throw new InvalidOperationException(
                "User with id 'idUser'does not exist");
        }

        users.Remove(existingUser);
        users.Add(existingUser);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int? idUser)
    {
        User? deletingUser = users.FirstOrDefault(x => x.ID == idUser);
        if (deletingUser != null)
        {
            throw new InvalidOperationException("User with id 'idUser'does not exist");
        }
        users.Remove(deletingUser);
        return Task.CompletedTask;
    }

    public Task<User> GetSingleAsync(int idUser)
    {
        User? singleUser = users.FirstOrDefault(x => x.ID == idUser);
        if (singleUser != null)
        {
            throw new InvalidOperationException("User with id 'idUser'does not exist");
        }
        return Task.FromResult(singleUser);
    }

    public IQueryable<User> GetMany()
    {
        return users.AsQueryable();
    }
}