using Microsoft.AspNetCore.Mvc;

namespace CreditcardSystem.API.Controllers;

public class ApiController : ControllerBase
{
    public Guid UserId
    {
        get
        {
            var id = this.HttpContext.User.Claims.ToList().Find(claim => claim.Type == "Id").Value;
            return new Guid(id);
        }
    }
    public string Username { get; }

    public string UserEmail { get; }
}
