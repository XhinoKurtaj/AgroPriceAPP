using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroPrice.Core.Data;
using AgroPrice.Domain.Domain.User;
using AgroPrice.Domain.Domain.WholeSaleMarket;
using AgroPrice.Services.Account.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgroPrice.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> roleManager;
        private readonly IRepository<PointOfSale> _pointOfSale;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IRepository<PointOfSale> pointOfSale)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            this.roleManager = roleManager;
            _pointOfSale = pointOfSale;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user =await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles[0]=="Seller")
                    {
                        var thisUser = (Domain.Domain.User.User) user;
                        return RedirectToAction("PointOfSaleDetails", "PointOfSale", new {id=thisUser.PointOfSaleId});
                    }
                    if (roles[0] == "PriceOperator")
                    {
                        var thisUser = (Domain.Domain.User.User)user;
                        return RedirectToAction("PointOfSaleList", "WholeSaleMarket", new { wholeSaleMarketId = thisUser.WholeSaleMarketId});
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Email/Fjalekalim gabim!");
            return View("Login", model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", model);
            }
            var user = new IdentityUser()
            { UserName = model.Name,
              Email = model.Email,
              PhoneNumber = model.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, "Buyer");
            if (result.Succeeded)
            {
                var loginResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (loginResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);

        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("Cart");
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}