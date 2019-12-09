using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgroPrice.Core.Data;
using AgroPrice.Domain.Domain.User;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AgroPrice.Services.Account
{

    public class UserService : IUserService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private IDbContext _dbContext;

        public UserService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IDbContext dbContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            this._roleManager = roleManager;
            _dbContext = dbContext;
        }

        /// <summary>
        /// return all sellers
        /// </summary>
        /// <returns></returns>
        public async Task<IList<IdentityUser>> GetAllSellers()
        {
            var users =await _userManager.GetUsersInRoleAsync("Seller");
            return users;
        }

        /// <summary>
        /// return all buyers
        /// </summary>
        /// <returns></returns>
        public async Task<IList<IdentityUser>> GetAllBuyers()
        {
            var users = await _userManager.GetUsersInRoleAsync("Buyer");
            return users;
        }

        /// <summary>
        /// return all priceOperators
        /// </summary>
        /// <returns></returns>
        public async Task<IList<IdentityUser>> GetAllPriceOperators()
        {
            var users = await _userManager.GetUsersInRoleAsync("PriceOperator");
            return users;
        }

    }
}
