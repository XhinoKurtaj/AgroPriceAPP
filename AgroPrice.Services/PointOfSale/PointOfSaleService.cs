﻿using AgroPrice.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgroPrice.Core.Extensions;
using AgroPrice.Domain.Domain.Product;
using AgroPrice.Services.PointOfSale.Models;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using AgroPrice.Functional;
using AgroPrice.Services.Product.Infrastructure;
using AgroPrice.Domain.Domain.User;
using Microsoft.AspNetCore.Identity;

namespace AgroPrice.Services.PointOfSale
{
    public class PointOfSaleService : IPointOfSaleService
    {
        private readonly IRepository<Domain.Domain.WholeSaleMarket.PointOfSale> _pointOfSale;
        private readonly IRepository<ProductDetails> _productDetails;
        private readonly IRepository<Domain.Domain.Product.Product> _products;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IDbContext _context;

        public PointOfSaleService(IRepository<Domain.Domain.WholeSaleMarket.PointOfSale> pointOfSale, IRepository<Domain.Domain.Product.Product> products, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IDbContext context, IRepository<ProductDetails> productDetails)
        {
            _pointOfSale = pointOfSale;
            _products = products;
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
            _productDetails = productDetails;
        }

        public async Task<List<PointOfSaleModel>> GetAllPointOfSale()
        {
            return await _pointOfSale.TableNoTracking.ProjectTo<PointOfSaleModel>().ToListAsync();
        }

        public async Task<Result> CreateSellerWithPointOfSale(CreateSellerWithPointOfSaleModel model)
        {
            return Result.Ok();
        }

        public async Task<PointOfSaleDetailsModel> PointOfSaleDetails(Guid id)
        {
            var model = new PointOfSaleDetailsModel();

            //get point of sale with this id
            var pointOfSaleDetails = await _pointOfSale.GetByIdAsync(id);
            //convert to model
            var pointOfSaleModel = pointOfSaleDetails.ToModel<PointOfSaleModel>();
            model.PointOfSaleModel = pointOfSaleModel;

            //get all products for this point of sale
            var thisPointOfSaleProducts = _products.TableNoTracking.Where(x => x.PointOfSaleId == id);
            //var todayPointOfSaleProducts =await thisPointOfSaleProducts.Where(x => x.RegisterDate.Date == today).ToListAsync();
            List<Models.Product> products = new List<Models.Product>();
            foreach (var productName in Products.ProductNames)
            {
                Domain.Domain.Product.Product thisProduct = null;
                var newProduct = new Models.Product
                {
                    Name = productName
                };
                // this product with specific name
                try
                {
                    var maxDate = await thisPointOfSaleProducts.Where(x => x.Name == productName)
                        .MaxAsync(x => x.RegisterDate);
                    thisProduct = await thisPointOfSaleProducts.Where(x => x.Name == productName)
                        .FirstOrDefaultAsync(x => x.RegisterDate == maxDate);
                }
                catch
                {
                    //ignore
                }

                if (thisProduct==null)
                {
                    products.Add(newProduct);
                }
                else
                {

                    var x = _productDetails.TableNoTracking.Where(x => x.ProductId == thisProduct.Id);
                    var maxProdDetDate =await x.MaxAsync(x => x.ModificationDate);
                    var productDetails = await x.FirstOrDefaultAsync(x => x.ModificationDate == maxProdDetDate);
                    if (productDetails != null)
                    {
                        if (productDetails.Quantity > 0)
                        {
                            newProduct.Id = thisProduct.Id;
                            newProduct.Origin = thisProduct.Origin;
                            newProduct.Price = productDetails.Price;
                            newProduct.Quantity = productDetails.Quantity;
                            newProduct.RegisterDate = thisProduct.RegisterDate;
                            products.Add(newProduct);
                        }
                    }
                   
                }
            }

            model.Products = products;
            return model;

        }


        /// <summary>
        /// Create a pointOfSale and a seller
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> CreatePointOfSaleAndSeller(CreateSellerWithPointOfSaleModel model)
        {
            try
            {
                var pointOfSale = new Domain.Domain.WholeSaleMarket.PointOfSale
                {
                    Description = model.PointOfSaleDescription,
                    WholeSaleMarketId = model.WholeSaleMarketId
                };
                await _pointOfSale.InsertAsync(pointOfSale);
                var user = new User()
                {
                    UserName = model.Name,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    PointOfSaleId = pointOfSale.Id
                };
                var createUserResult = await _userManager.CreateAsync(user, model.Password);
                await _userManager.AddToRoleAsync(user, "Seller");
            }
            catch
            { 
                return Result.Fail(null,"Error while creating a point of sale and a seller");
            }
            return Result.Ok();
        }

        /// <summary>
        /// Update a specific point of sale and seller 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> UpdatePointOfSaleAndSeller(UpdateSellerWithPointOfSaleModel model)
        {
            try
            {
                var entity = await _pointOfSale.GetByIdAsync(model.Id);
                entity.Description = model.PointOfSaleDescription;
                entity.WholeSaleMarketId = new Guid(model.WholeSaleMarketId);
                await _pointOfSale.UpdateAsync(entity);
                var user =(User)await _userManager.FindByIdAsync(entity.User.Id);
                if(user==null)
                    return Result.Fail(null, "Seller(user) with given id seem to not exist in database!");
                user.UserName = model.Name;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.PointOfSaleId = entity.Id;
                var updatedUserResult = await _userManager.UpdateAsync(user);
                if (updatedUserResult.Succeeded)
                    return Result.Ok();
                else return Result.Fail(null, "Error while updating whole sale market");
            }
            catch
            {
                return Result.Fail(null, "Error while updating a point of sale and a seller");
            }
        }

        /// <summary>
        /// return PointOfSale entity to model
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<UpdateSellerWithPointOfSaleModel> ReturnEntityToModel(Domain.Domain.WholeSaleMarket.PointOfSale entity)
        {
            var model = new UpdateSellerWithPointOfSaleModel
            {
                Id = entity.Id,
                Name = entity.User.UserName,
                Email = entity.User.Email,
                PhoneNumber = entity.User.PhoneNumber,
                WholeSaleMarketId = entity.WholeSaleMarketId.ToString(),
                PointOfSaleDescription = entity.Description,
                UserId = new Guid(entity.User.Id)
            };
            return model;
        }

        /// <summary>
        /// delete point of sale
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result> DeletePointOfSale(Guid id)
        {
            try
            {
                var entity = await _pointOfSale.GetByIdAsync(id);
                var products = entity.Products;
                var user = entity.User;
                _products.AutoSaveChanges = false;
                _pointOfSale.AutoSaveChanges = false;
                
                if (products.Any()) 
                    await _products.DeleteAsync(products);
                await _pointOfSale.DeleteAsync(entity);
                await _context.SaveChangesAsync();
                await _userManager.DeleteAsync(user);
                
            }
            catch (Exception ex)
            {
                return Result.Fail(null, "Error while deleting wholeSaleMarket");
            }
            return Result.Ok();
        }

    }
}
