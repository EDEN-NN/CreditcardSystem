using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CreditcardSystem.Application.Dtos.Request;
using CreditcardSystem.Application.Dtos.Response;
using CreditcardSystem.Application.Exceptions;
using CreditcardSystem.Application.Repositories;
using CreditcardSystem.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CreditcardSystem.Application.Services;

public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _config;

    public AuthService(IUserRepository userRepository, IConfiguration config)
    {
        _userRepository = userRepository;
        _config = config;
    }

    public async Task<string> Login(LoginRequest loginRequest)
    {
        var user = await _userRepository.FindByUsername(loginRequest.Username);
        if (user == null)
        {
            throw new UserNotFoundException(
                "Invalid Credentials.",
                ExceptionType.NotFoundException
            );
        }

        if (!VerifyPassword(loginRequest.Password, user.Password, user.PasswordSalt))
        {
            throw new UserNotFoundException(
                "Invalid Credentials.",
                ExceptionType.NotFoundException
            );
        }

        var token = CreateToken(user);
        return token;
    }

    public async Task<UserResponse> RegisterUser(UserRequest userRequest)
    {
        if (!await CanRegister(userRequest.Email))
        {
            throw new EmailAlreadyInUseException(
                "This email is already in use.",
                ExceptionType.EmailAlreadyInUseException
            );
        }

        var user = (User)userRequest;
        this.HashPassword(userRequest.Password, out byte[] passwordHash, out byte[] passwordSalt);
        user.Password = Encoding.UTF8.GetString(passwordHash);
        user.PasswordSalt = Encoding.UTF8.GetString(passwordSalt);

        var response = await _userRepository.SaveUser(user);

        return (UserResponse)user;
    }

    private async Task<Boolean> CanRegister(string email)
    {
        var user = await _userRepository.GetByEmail(email);
        return user == null ? true : false;
    }

    private void HashPassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    private bool VerifyPassword(string password, string passwordHash, string passwordSalt)
    {
        using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(passwordSalt)))
        {
            var passwordHashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(passwordHash))
                .ToString();
            var passwordRequest = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)).ToString();

            return passwordHashBytes.SequenceEqual(passwordRequest);
        }
    }

    private string CreateToken(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim("Username", user.Username),
            new Claim("Email", user.Email),
            new Claim("Id", user.Id.ToString())
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value)
        );

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }

    public async Task<string> GetAuthToken(string token)
    {
        var formatedToken = token.Split(' ').GetValue(1).ToString().Replace('"', ' ');
        var tokenReceived = new JwtSecurityTokenHandler().ReadToken(formatedToken);
        var tokenData = (JwtSecurityToken)tokenReceived;
        var userId = tokenData.Claims.ToList().Find(claim => claim.Type == "Id");

        return userId.ToString();
    }
}
