using Entities;

namespace RepositoryContracts;

public interface IUserRepository
{
    Task<User> AddAsync(User user);
    Task UpdateAsync(int idUser);
    Task DeleteAsync(int? idUser);
    Task<User> GetSingleAsync(int idUser);
    IQueryable<User> GetMany();

}