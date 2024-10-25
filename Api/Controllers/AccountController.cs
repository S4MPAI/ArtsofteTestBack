using Api.RequestModels;
using Logic.Dto;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
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
        public IActionResult Register(RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var userCreateDto = new UserDto() 
            { 
                FIO = registerRequest.FIO, 
                Email = registerRequest.Email, 
                Password = registerRequest.Password, 
                Phone = registerRequest.Phone 
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

            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = userManager.GetByPhone(loginRequest.Phone);

            if (IsNotCorrectLoginRequest(loginRequest, user))
            {
                ModelState.AddModelError(nameof(loginRequest.Phone), "Ошибка авторизации");
                return View();
            }

            var claims = new List<Claim>() 
            { 
                new Claim(ClaimTypes.Email, user.Email), 
                new Claim(ClaimTypes.Name, user.FIO)
            };
            var claimsIdentity = new ClaimsIdentity(claims);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Cabinet");
        }

        private bool IsNotCorrectLoginRequest(LoginRequest loginRequest, UserDto? user)
        {
            return user == null || !userManager.CheckPassword(user, loginRequest.Password);
        }
    }
}
