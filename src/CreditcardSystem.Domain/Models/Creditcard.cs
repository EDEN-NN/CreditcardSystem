namespace CreditcardSystem.Domain.Models;

public class Creditcard
{
    public Creditcard() { }

    public Creditcard(string cardName, string cardType, decimal cardBill, Guid ownerId)
    {
        CardName = cardName;
        CardType = cardType;
        CardBill = cardBill;
        OwnerId = ownerId;
    }

    public Guid Id { get; set; } = new Guid();
    public string CardName { get; set; }
    public string CardType { get; set; }
    public decimal CardBill { get; set; }
    public Guid OwnerId { get; set; }
    public User Owner { get; set; }
}
