using Api.ResponseModels;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers;

[Authorize]
public class CabinetController : Controller
{
    private readonly IUserManager userManager;

    public CabinetController(IUserManager userManager)
    {
        this.userManager = userManager;
    }

    [HttpGet]
    [Route("cabinet")]
    public IActionResult Index()
    {
        var emailClaim = User.FindFirst(ClaimTypes.Email);

        var user = userManager.GetByEmail(emailClaim.Value);

        if (user == null)
            return NotFound();

        var userInfoResponse = new UserInfoResponse()
        {
            FIO = user.FIO,
            Email = user.Email,
            Phone = user.Phone,
            LastLogin = user.LastLogin.Value
        };

        return View(userInfoResponse);
    }
}
