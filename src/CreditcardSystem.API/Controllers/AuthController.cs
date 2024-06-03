using CreditcardSystem.Application.Dtos.Request;
using CreditcardSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CreditcardSystem.API.Controllers;

[ApiController]
[Route("")]
public class AuthController : ApiController
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> Register(UserRequest userRequest)
    {
        return Ok(await _authService.RegisterUser(userRequest));
    }

    [HttpPost("signin")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        return Ok(await _authService.Login(loginRequest));
    }
}
