namespace CreditcardSystem.Application.Repositories;

using CreditcardSystem.Domain.Models;

public interface IUserRepository
{
    Task<List<User>> GetAllUsers();
    Task<User?> GetUserById(Guid userId);
    Task UpdateUser(User user, Guid userId);
    Task DeleteUser(Guid userId);

    Task<User> FindByUsername(string username);

    Task<User> GetByEmail(string email);
    Task<User> SaveUser(User user);
}
