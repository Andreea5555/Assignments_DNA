using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories;

public class EfcUserRepository : IUserRepository
{
    private readonly AppContext ctx;

    public EfcUserRepository(AppContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<User> AddAsync(User user)
    {
        EntityEntry<User> entityEntry = await ctx.Users.AddAsync(user);
        await ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task UpdateAsync(int idUser, User user)
    {
        //idk if its good
        if (!await ctx.Users.AnyAsync(u => u.ID == idUser))
        {
            throw new KeyNotFoundException($"User with id {user.ID} not found");
        }

        ctx.Users.Update(user);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int? idUser)
    {
        User? existing =
            await ctx.Users.SingleOrDefaultAsync(u => u.ID == idUser);
        if (existing == null)
        {
            throw new KeyNotFoundException($"User with id {idUser} not found");
        }

        ctx.Users.Remove(existing);
        await ctx.SaveChangesAsync();
    }

    public Task<User?> GetUserByUsernameAsync(string username)
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetSingleAsync(int id)
    {
        User? entityEntry =
            await ctx.Users.SingleOrDefaultAsync(u => u.ID == id);
        if (entityEntry == null)
        {
            throw new KeyNotFoundException($"User with id {id} not found");
        }

        await ctx.SaveChangesAsync();
        return entityEntry;
    }

    public IQueryable<User> GetMany()
    {
        return ctx.Users.AsQueryable();
    }

    public List<User> GetAll()
    {
        return ctx.Users.ToList();
    }
}