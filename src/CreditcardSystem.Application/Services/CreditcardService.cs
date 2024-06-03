using CreditcardSystem.Application.Dtos.Request;
using CreditcardSystem.Application.Dtos.Response;
using CreditcardSystem.Application.Dtos.Services;
using CreditcardSystem.Application.Exceptions;
using CreditcardSystem.Application.Repositories;
using CreditcardSystem.Domain.Models;

namespace CreditcardSystem.Application.Services;

public class CreditcardService
{
    private readonly ICreditcardRepository _creditcardRepository;
    private readonly UserService _userService;

    public CreditcardService(ICreditcardRepository creditcardRepository, UserService userService)
    {
        _creditcardRepository = creditcardRepository;
        _userService = userService;
    }

    public async Task<List<CreditcardResponse>> GetAllCreditcards(Guid userId)
    {
        List<CreditcardResponse> creditcardResponses = new List<CreditcardResponse>();
        List<Creditcard> creditcards = await _creditcardRepository.GetAllCreditcards(userId);
        creditcards.ForEach(creditcard =>
        {
            var creditcardRespose = (CreditcardResponse)creditcard;
            creditcardResponses.Add(creditcardRespose);
        });

        return creditcardResponses;
    }

    public async Task<CreditcardResponse> GetCreditcardById(Guid creditcardId)
    {
        var creditcard = await _creditcardRepository.GetCreditcardById(creditcardId);
        if (creditcard == null)
        {
            throw new CreditcardNotFoundException(
                "This creditcard don't exists.",
                ExceptionType.NotFoundException
            );
        }
        return (CreditcardResponse)creditcard;
    }

    public async Task<CreditcardResponse> SaveCreditcard(
        CreditcardRequest creditcardRequest,
        Guid userId
    )
    {
        var userFromDb = await _userService.GetUserById(userId);
        var creditcard = new Creditcard(
            creditcardRequest.CardName,
            creditcardRequest.CardType,
            creditcardRequest.CardBill,
            userFromDb.Id
        );

        return (CreditcardResponse)(await _creditcardRepository.SaveCreditcard(creditcard));
    }

    public async Task<CreditcardResponse> UpdateCreditcard(
        CreditcardRequest creditcardRequest,
        Guid creditcardId
    )
    {
        var creditcard = await _creditcardRepository.GetCreditcardById(creditcardId);
        if (creditcard == null)
        {
            throw new CreditcardNotFoundException(
                "This creditcard don't exists.",
                ExceptionType.NotFoundException
            );
        }
        creditcard.CardName = creditcardRequest.CardName;
        creditcard.CardType = creditcardRequest.CardType;
        creditcard.CardBill = creditcardRequest.CardBill;

        return (CreditcardResponse)(await _creditcardRepository.UpdateCreditcard(creditcard));
    }

    public async Task DeleteCreditcard(Guid creditcardId)
    {
        await _creditcardRepository.DeleteCreditcard(creditcardId);
    }
}
