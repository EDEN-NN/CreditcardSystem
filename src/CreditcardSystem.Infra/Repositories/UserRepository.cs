namespace CreditcardSystem.Infra.Repositories;

using CreditcardSystem.Application.Repositories;
using CreditcardSystem.Domain.Models;
using CreditcardSystem.Infra.Data;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly CredicardDataContext _context;

    public UserRepository(CredicardDataContext context)
    {
        _context = context;
    }

    public async Task<User> GetByEmail(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        return user;
    }

    public async Task<User> FindByUsername(string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(userDb => userDb.Username == username);
        return user;
    }

    public async Task DeleteUser(Guid userId)
    {
        User? User = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

        if (User != null)
        {
            _context.Users.Remove(User);
            _context.SaveChanges();
        }
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetUserById(Guid userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task<User> SaveUser(User user)
    {
        var response = await _context.Users.AddAsync(user);
        _context.SaveChanges();
        return response.Entity;
    }

    public async Task UpdateUser(User user, Guid userId)
    {
        var userDb = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId);
        user.Id = userId;
        userDb = user;
        _context.Users.Update(userDb);
        _context.SaveChanges();
    }
}
