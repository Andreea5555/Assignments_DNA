using Entities;

namespace RepositoryContracts;

public interface IUserRepository
{
    Task<User> AddUser(User user);
    Task UpdateUser(int idUser);
    Task DeleteUser(int idUser);
    Task<User> GetSingleUser(int idUser);
    IQueryable<User> GetManyUsers();

}