namespace CreditcardSystem.Application.Repositories;

using CreditcardSystem.Domain.Models;

public interface ICreditcardRepository
{
    Task<List<Creditcard>> GetAllCreditcards(Guid userId);
    Task<Creditcard?> GetCreditcardById(Guid creditcardId);

    Task<Creditcard> UpdateCreditcard(Creditcard creditcard);

    Task<Creditcard> SaveCreditcard(Creditcard creditcard);

    Task DeleteCreditcard(Guid creditcardId);
}
