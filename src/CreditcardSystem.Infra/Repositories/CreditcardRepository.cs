namespace CreditcardSystem.Infra.Repositories;

using CreditcardSystem.Application.Repositories;
using CreditcardSystem.Domain.Models;
using CreditcardSystem.Infra.Data;
using Microsoft.EntityFrameworkCore;

public class CreditcardRepository : ICreditcardRepository
{
    private CredicardDataContext _context;

    public CreditcardRepository(CredicardDataContext context)
    {
        _context = context;
    }

    public Task DeleteCreditcard(Guid creditcardId)
    {
        _context.Remove(creditcardId);
        _context.SaveChangesAsync();

        return Task.CompletedTask;
    }

    public Task<List<Creditcard>> GetAllCreditcards(Guid userId)
    {
        return _context
            .Creditcards.AsNoTracking()
            .Where(creditcard => creditcard.OwnerId == userId)
            .ToListAsync();
    }

    public async Task<Creditcard?> GetCreditcardById(Guid creditcardId)
    {
        return await _context
            .Creditcards.AsNoTracking()
            .FirstOrDefaultAsync(creditcard => creditcard.Id == creditcardId);
    }

    public async Task<Creditcard> SaveCreditcard(Creditcard creditcard)
    {
        await _context.Creditcards.AddAsync(creditcard);
        await _context.SaveChangesAsync();
        return creditcard;
    }

    public async Task<Creditcard> UpdateCreditcard(Creditcard creditcard)
    {
        _context.Creditcards.Entry(creditcard).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return creditcard;
        // var creditcardFromDb = await _context
        //     .Creditcards.AsNoTracking()
        //     .FirstOrDefaultAsync(card => card.Id == creditcardId);
        // creditcardFromDb.CardName = creditcard.CardName;
        // creditcardFromDb.CardBill = creditcard.CardBill;
        // creditcardFromDb.CardType = creditcard.CardType;

        // await _context.SaveChangesAsync();
        // return creditcardFromDb;
    }
}
