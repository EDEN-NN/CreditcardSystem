using CreditcardSystem.Application.Dtos.Request;
using CreditcardSystem.Application.Dtos.Response;
using CreditcardSystem.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CreditcardSystem.API.Controllers;

[ApiController]
[Route("creditcards")]
public class CreditcardController : Controller
{
    private readonly CreditcardService _creditCardService;

    public CreditcardController(CreditcardService creditcardService)
    {
        _creditCardService = creditcardService;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllCreditcards()
    {
        return Ok(await _creditCardService.GetAllCreditcards());
    }

    [Authorize]
    [HttpGet("creditcard/{creditcardId}")]
    public async Task<IActionResult> GetCreditcardById(Guid creditcardId)
    {
        return Ok(await _creditCardService.GetCreditcardById(creditcardId));
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> SaveCreditcard(CreditcardRequest creditcardRequest)
    {
        return Accepted(await _creditCardService.SaveCreditcard(creditcardRequest));
    }

    [Authorize]
    [HttpPut("creditcard/{creditcardId}")]
    public async Task<IActionResult> UpdateCreditcard(
        CreditcardRequest creditcardRequest,
        Guid creditcardId
    )
    {
        return Accepted(await _creditCardService.UpdateCreditcard(creditcardRequest, creditcardId));
    }

    [Authorize]
    [HttpDelete("creditcard/{creditcardId}")]
    public async Task<IActionResult> DeleteCreditcard(Guid creditcardId)
    {
        await _creditCardService.DeleteCreditcard(creditcardId);
        return NoContent();
    }
}
