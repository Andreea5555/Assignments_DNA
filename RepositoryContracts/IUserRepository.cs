using Entities;

namespace RepositoryContracts;

public interface IUserRepository
{
    Task<User> AddAsync(User user);
    Task UpdateAsync(int idUser, User user);
    Task DeleteAsync(int? idUser);
    Task<User?> GetUserByUsernameAsync(string username); 
    Task<User> GetSingleAsync(int idUser);
    IQueryable<User> GetMany();
    List<User> GetAll();

}