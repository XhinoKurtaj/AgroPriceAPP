using AgroPrice.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;
using AgroPrice.Domain.Domain.WholeSaleMarket;
using AgroPrice.Services.WholeSaleMarket.Models;
using System.Threading.Tasks;
using System.Linq;
using AgroPrice.Domain.Domain.User;
using AgroPrice.Functional;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AgroPrice.Services.WholeSaleMarket
{
    public class WholeSaleMarketService : IWholeSaleMarketService
    {
        private readonly IRepository<Domain.Domain.WholeSaleMarket.WholeSaleMarket> _wholeSaleMarket;
        private readonly IRepository<Domain.Domain.WholeSaleMarket.PointOfSale> _pointOfSale;
        private readonly IDbContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public WholeSaleMarketService(IRepository<Domain.Domain.WholeSaleMarket.WholeSaleMarket> wholeSaleMarket, IRepository<Domain.Domain.WholeSaleMarket.PointOfSale> pointOfSale, IDbContext context, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _wholeSaleMarket = wholeSaleMarket;
            _pointOfSale = pointOfSale;
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<List<WholeSaleMarketModel>> GetAllWholeSaleMarkets()
        {
            return await _wholeSaleMarket.TableNoTracking.ProjectTo<WholeSaleMarketModel>().ToListAsync();
        }

        /// <summary>
        /// delete whole sale market
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result> DeleteWholeSaleMarket(Guid id)
        {
            try
            {
                var entity = await _wholeSaleMarket.GetByIdAsync(id);
                var pointOfSales = entity.PointOfSales;
                _pointOfSale.AutoSaveChanges = false;
                _wholeSaleMarket.AutoSaveChanges = false;
                if(pointOfSales.Any())
                    await _pointOfSale.DeleteAsync(pointOfSales);
                await _wholeSaleMarket.DeleteAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Result.Fail(null,"Error while deleting wholeSaleMarket");
            }
            return Result.Ok();
        }

        /// <summary>
        /// create new whole sale market 
        /// </summary>
        /// <returns></returns>
        public async Task<Result> CreateWholeSaleMarket(CreateWholeSaleMarketModel model)
        {
            try
            {
                var entity = new Domain.Domain.WholeSaleMarket.WholeSaleMarket
                {
                    Name = model.Name,
                    Address = model.Address,
                    ImageUrl = model.ImageUrl
                };
                await _wholeSaleMarket.InsertAsync(entity);
                var user = new User()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    WholeSaleMarketId = entity.Id
                };
                var createUserResult = await _userManager.CreateAsync(user, model.Password);
                await _userManager.AddToRoleAsync(user, "PriceOperator"); 
                if(createUserResult.Succeeded)
                   return Result.Ok();
                else
                {
                    return Result.Fail(null, "Error while creating new whole sale market!");
                }
            }
            catch (Exception ex)
            {
                return Result.Fail(null, "Error while creating new whole sale market!");
            }
        }

        /// <summary>
        /// update whole sale market with specific id 
        /// </summary>
        /// <returns></returns>
        public async Task<Result> UpdateWholeSaleMarket(UpdateWholeSaleMarketModel model)
        {
            try
            {
                var entity = await _wholeSaleMarket.GetByIdAsync(model.Id);
                entity.Name = model.Name;
                entity.Address = model.Address;
                entity.ImageUrl = model.ImageUrl;
                var user = (User)await _userManager.FindByIdAsync(entity.User.Id);
                if (user == null)
                    return Result.Fail(null, "PriceOperator(user) with given id seem to not exist in database!");
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.WholeSaleMarketId = entity.Id;
                var updatedUserResult = await _userManager.UpdateAsync(user);
                if (updatedUserResult.Succeeded)
                    return Result.Ok();
                else return Result.Fail(null,"Error while updating whole sale market");
            }
            catch (Exception ex)
            {
                return Result.Fail(null, "Error while updating whole sale market!");
            }
        }
    }
}
