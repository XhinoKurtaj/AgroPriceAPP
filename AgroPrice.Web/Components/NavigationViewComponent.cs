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

namespace AgroPrice.Web.Components
{
    public class NavigationViewComponent : ViewComponent
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private readonly IRepository<PointOfSale> _pointOfSale;

        public NavigationViewComponent(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IRepository<PointOfSale> pointOfSale)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            this._roleManager = roleManager;
            _pointOfSale = pointOfSale;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new NavigationViewModel();
            if (User.Identity.Name != null)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var roles = await _userManager.GetRolesAsync(user);
                if (roles[0] == "Seller" || roles[0] == "PriceOperator")
                {
                    model.User = (User) user;
                }
                else
                {
                    model.IdentityUser = user;
                }
            }

            return View(model);

        }
    }
}
