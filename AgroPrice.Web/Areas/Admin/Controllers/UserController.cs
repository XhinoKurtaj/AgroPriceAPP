using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroPrice.Services.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Manage.Internal;
using Microsoft.AspNetCore.Mvc;

namespace AgroPrice.Web.Areas.Admin.Controllers
{
    public class UserController : AdminController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> roleManager;
        private readonly IUserService _userService;

        public UserController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IUserService userService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            this.roleManager = roleManager;
            _userService = userService;
        }

        public async Task<IActionResult> SellersList()
        {
            var users = await _userService.GetAllSellers();
            return View(users);
        }

        public async Task<IActionResult> BuyersList()
        {
            var users = await _userService.GetAllBuyers();
            return View(users);
        }

        public async Task<IActionResult> PriceOperatorsList()
        {
            var users = await _userService.GetAllPriceOperators();
            return View(users);
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user==null)
                return Json(new { success = false });
            var result = await _userManager.DeleteAsync(user);
            if(!result.Succeeded)
                return Json(new { success = false });
            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword(string id)
        {
            var model = new Services.Account.Models.ChangePasswordModel
            {
                Id = new Guid(id)
            };
            return PartialView("Partials/_ChangePassword",model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(Services.Account.Models.ChangePasswordModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id.ToString());
            var token = await _userManager.RemovePasswordAsync(user);
            var result = await _userManager.AddPasswordAsync(user, model.Password);
            if (!result.Succeeded)
                return Json(new { success = false });
            return Json(new { success = true });
        }

    }

}