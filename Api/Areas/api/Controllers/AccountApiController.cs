using Api.RequestModels;
using Api.ResponseModels;
using Logic.Dto;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Areas.api.Controllers;

[ApiController]
[Area("api")]
[Route("[area]/account")]
public class AccountApiController : ControllerBase
{
    private readonly IUserManager userManager;
    public AccountApiController(IUserManager userManager) 
    { 
        this.userManager = userManager;
    }

    [Route("[action]")]
    [HttpPost]
    public IActionResult Register(RegisterRequest request)
    {
        var userDto = new UserDto()
        {
            FIO = request.FIO,
            Email = request.Email,
            Password = request.Password,
            Phone = request.Phone
        };

        var result = userManager.Create(userDto);
        
        if (!result.IsSuccess)
        {
            var error = result.Errors.FirstOrDefault();
            var errorResponse = new ErrorResponse(error?.Code, error?.Message);
            return BadRequest(errorResponse);
        }
        return Ok();
    }

    [Route("[action]")]
    [HttpPost]
    public IActionResult Login(LoginRequest request)
    {
        var userDto = new UserDto() { Phone = request.Phone, Password = request.Password };

        var result = userManager.ApplySignInClaims(userDto);
        if (!result.IsSuccess)
        {
            var error = result.Errors.FirstOrDefault();
            var errorResponse = new ErrorResponse(error.Code, error.Message);
            return BadRequest(errorResponse);
        }

        var claimsIdentity = new ClaimsIdentity(result.Value);

        return Ok();
    }

    [Route("get-my-info")]
    [Authorize]
    [HttpGet]
    public IActionResult GetMyInfo()
    {
        var userEmail = User.FindFirst(ClaimTypes.MobilePhone);
        var userInfo = userManager.GetByEmail(userEmail.Value);

        return Ok();
    }

    [Route("get-my-info")]
    [Authorize]
    [HttpGet]
    public IActionResult Logout()
    {
        var userEmail = User.FindFirst(ClaimTypes.MobilePhone);
        HttpContext.Response.Cookies.Delete("token");

        return Ok();
    }
}
