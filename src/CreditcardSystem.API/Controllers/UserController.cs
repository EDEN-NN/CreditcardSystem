using CreditcardSystem.Application.Dtos.Request;
using CreditcardSystem.Application.Dtos.Services;
using CreditcardSystem.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CreditcardSystem.API.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    private readonly CreditcardService _creditCardService;

    public UserController(UserService userService, CreditcardService creditcardService)
    {
        _userService = userService;
        _creditCardService = creditcardService;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        return Ok(await _userService.GetAllUsers());
    }

    [Authorize]
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserById(Guid userId)
    {
        return Ok(await _userService.GetUserById(userId));
    }

    [Authorize]
    [HttpPut("user/{userId}")]
    public async Task<IActionResult> UpdateUser(UserRequest userRequest, Guid userId)
    {
        await _userService.UpdateUser(userRequest, userId);
        return Accepted();
    }

    [Authorize]
    [HttpDelete("user/{userId}")]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        await _userService.DeleteUser(userId);
        return NoContent();
    }
}
