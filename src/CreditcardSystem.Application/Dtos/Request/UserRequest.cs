using System.ComponentModel.DataAnnotations;
using CreditcardSystem.Domain.Models;

namespace CreditcardSystem.Application.Dtos.Request;

public record UserRequest
{
    [Required(ErrorMessage = "O campo Email é obrigatório.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo Username é obrigatório.")]
    public string Username { get; set; }

    [Required(ErrorMessage = "O  campo Password é obrigatório.")]
    public string Password { get; set; }

    [Compare("Password", ErrorMessage = "As senhas não coincidem.")]
    public string PasswordConfirmation { get; set; }

    public static explicit operator User(UserRequest userRequest)
    {
        return new User() { Email = userRequest.Email, Username = userRequest.Username };
    }
}
