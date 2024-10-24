using Api.RequestModels;
using Logic.Dto;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            
            return RedirectToAction(nameof(Cabinet));
        }

        [Authorize]
        [HttpGet]
        public IActionResult Cabinet()
        {
            return View();
        }

        private bool IsNotCorrectLoginRequest(LoginRequest loginRequest, UserDto? user)
        {
            return user == null || !userManager.CheckPassword(user, loginRequest.Password);
        }
    }
}
