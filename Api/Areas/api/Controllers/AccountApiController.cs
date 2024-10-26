using Api.Authentication;
using Api.RequestModels;
using Api.ResponseModels;
using Logic.Dto;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Api.Areas.api.Controllers;

[ApiController]
[Area("api")]
[Route("[area]/account")]
public class AccountApiController : ControllerBase
{
    private readonly IUserManager userManager;
    private readonly AuthOptions authOptions;
    public AccountApiController(IUserManager userManager, AuthOptions authOptions) 
    { 
        this.userManager = userManager;
        this.authOptions = authOptions;
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

        var claimsIdentity = new ClaimsIdentity(result.Value, JwtBearerDefaults.AuthenticationScheme);
        var jwt = new JwtSecurityToken(
            issuer: authOptions.ISSUER, 
            audience: authOptions.AUDIENCE, 
            claims: result.Value, 
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(5)),
            signingCredentials: new SigningCredentials(authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);
        HttpContext.Session.SetString("JWToken", token);

        return Ok();
    }

    [Route("get-my-info")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public IActionResult GetMyInfo()
    {
        var userEmail = User.FindFirst(ClaimTypes.Email);
        var userDto = userManager.GetByEmail(userEmail.Value);

        var userInfoResponse = new UserInfoResponse()
        {
            FIO = userDto.FIO,
            Email = userDto.Email,
            Phone = userDto.Phone,
            LastLogin = userDto.LastLogin.Value
        };

        return Ok(userInfoResponse);
    }

    [Route("[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();

        return Ok();
    }
}
