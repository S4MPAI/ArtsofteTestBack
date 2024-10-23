using Api.RequestModels;
using Api.ResponseModels;
using Logic.Dto;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Areas.api.Controllers;

[ApiController]
[Area("api")]
[Route("[area]/account/[action]")]
public class AccountApiController : ControllerBase
{
    private readonly IUserManager userManager;
    public AccountApiController(IUserManager userManager) 
    { 
        this.userManager = userManager;
    }

    [HttpPost]
    public IActionResult Register([FromBody]RegisterRequest request)
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
}
