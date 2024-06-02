using CreditcardSystem.Domain.Models;

namespace CreditcardSystem.Application.Dtos.Response;

public class CreditcardResponse
{
    public Guid Id { get; set; } = new Guid();
    public string CardName { get; set; }
    public string CardType { get; set; }
    public decimal CardBill { get; set; }

    public Guid OwnerId { get; set; }

    public static explicit operator CreditcardResponse(Creditcard creditcard)
    {
        return new CreditcardResponse()
        {
            Id = creditcard.Id,
            CardName = creditcard.CardName,
            CardType = creditcard.CardType,
            CardBill = creditcard.CardBill,
            OwnerId = creditcard.OwnerId
        };
    }
}
