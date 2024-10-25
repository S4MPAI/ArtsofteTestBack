using Api.RequestModels;
using Logic.Dto;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers;

public class AccountController : Controller
{
    private IUserManager userManager;

    public AccountController(IUserManager userManager)
    {
        this.userManager = userManager;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        var userCreateDto = new UserDto() 
        { 
            FIO = request.FIO, 
            Email = request.Email, 
            Password = request.Password, 
            Phone = request.Phone 
        };

        var result = userManager.Create(userCreateDto);

        if (!result.IsSuccess)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Message);
            }

            return View();
        }

        return RedirectToAction(nameof(Welcome), new { userCreateDto.FIO });
    }

    [HttpGet]
    public IActionResult Welcome([FromQuery]string FIO)
    {
        return View(model: FIO);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        var user = new UserDto() { Phone = request.Phone, Password = request.Password };

        var result = userManager.ApplySignInClaims(user);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError(nameof(request.Phone), "Ошибка авторизации");
            return View();
        }

        var claimsIdentity = new ClaimsIdentity(result.Value, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));

        return RedirectToAction("Index", "Cabinet");
    }

    [Authorize]
    [HttpPost]
    public IActionResult Logout()
    {
        HttpContext.SignOutAsync();

        return RedirectToAction(nameof(Login));
    }
}
