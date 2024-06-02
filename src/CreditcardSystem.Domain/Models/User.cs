namespace CreditcardSystem.Domain.Models;

public class User
{
    private List<Creditcard> _creditcards;

    public User()
    {
        _creditcards = new List<Creditcard>();
    }

    public Guid Id { get; set; } = new Guid();
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PasswordSalt { get; set; }
    public IReadOnlyCollection<Creditcard> Creditcards
    {
        get { return _creditcards.ToArray(); }
    }

    public void AddCreditcard(Creditcard creditcard)
    {
        _creditcards.Add(creditcard);
    }
}
