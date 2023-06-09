﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TestShopv1.Models;
using TestShopv1.Models.ViewModel;
using TestShopv1.Services;

namespace TestShopv1.Controllers
{
    public class AccountController : Controller
    {
 
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(CustomerVM customerVM)
        {
            UserService.Register(UserService.Mapping(customerVM));
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Customer user)
        {
            if (UserService.IsUserValid(user))
            {
                await SignInUser(user);

                return RedirectToAction(nameof(HomeController.Index), HomeController.Name);
            }
            return View("Error");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
             return RedirectToAction(nameof(HomeController.Privacy), "Home");
        }

        private async Task SignInUser(Customer user)
        {
            Customer customerDB = UserService.Get(user);

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, customerDB.EmailAddress));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, customerDB.Id.ToString()));
            //claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            //claims.Add(new Claim(ClaimTypes.Role, "Manager"));
            //claims.Add(new Claim(ClaimTypes.Role, "Customer"));

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));
        }
    }
}
