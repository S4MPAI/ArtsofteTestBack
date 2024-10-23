﻿using Api.RequestModels;
using Logic.Dto;
using Logic.Interfaces;
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

            var userCreateDto = new UserCreateDto() 
            { 
                FIO = registerRequest.FIO, 
                Email = registerRequest.Email, 
                Password = registerRequest.Password, 
                Phone = registerRequest.Phone 
            };
            userManager.Create(userCreateDto);

            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}