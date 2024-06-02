namespace CreditcardSystem.Application.Dtos.Response;

using CreditcardSystem.Domain.Models;

public record UserResponse
{
    public UserResponse() { }

    public UserResponse(Guid id, string email, string username, string password)
    {
        Id = id;
        Email = email;
        Username = username;
        Password = password;
    }

    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public static explicit operator UserResponse(User user)
    {
        return new UserResponse()
        {
            Id = user.Id,
            Email = user.Email,
            Username = user.Username,
            Password = user.Password
        };
    }
}
