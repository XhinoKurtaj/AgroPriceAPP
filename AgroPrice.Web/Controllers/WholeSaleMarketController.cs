using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroPrice.Core.Data;
using AgroPrice.Domain.Domain.User;
using AgroPrice.Domain.Domain.WholeSaleMarket;
using AgroPrice.Services.PointOfSale.Models;
using AgroPrice.Services.Product.Infrastructure;
using AgroPrice.Services.Product.Models;
using AgroPrice.Services.WholeSaleMarket;
using AgroPrice.Services.WholeSaleMarket.Models;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Product = AgroPrice.Domain.Domain.Product.Product;

namespace AgroPrice.Web.Controllers
{
    //[Authorize(Roles = "Buyer,Admin,PriceOperator")]
    public class WholeSaleMarketController : Controller
    {
        private readonly IWholeSaleMarketService _wholeSaleMarketService;
        private readonly IRepository<WholeSaleMarket> _wholeSaleMarket;
        private readonly IRepository<Product> _products;
        private readonly IRepository<PointOfSale> _pointOfSale;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public WholeSaleMarketController(IWholeSaleMarketService wholeSaleMarketService, IRepository<WholeSaleMarket> wholeSaleMarket, IRepository<Product> products, IRepository<PointOfSale> pointOfSale, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _wholeSaleMarketService = wholeSaleMarketService;
            _wholeSaleMarket = wholeSaleMarket;
            _products = products;
            _pointOfSale = pointOfSale;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [Authorize(Roles = "Buyer,Admin,PriceOperator,Seller")]
        public async Task<IActionResult> SpecificProductOnWholeSaleMarket(Guid wholeSaleMarketId, string productName)
        {
            var todayProducts = _products.TableNoTracking.Where(x => x.RegisterDate.Date == DateTime.Now.Date);
            var productOfThisWholeSaleMarket = todayProducts
                .Where(x => x.PointOfSale.WholeSaleMarketId == wholeSaleMarketId);
            var prodWithThisName =await productOfThisWholeSaleMarket.Where(x => x.Name == productName).ProjectTo<ProductInCartModel>().ToListAsync();
            return View(prodWithThisName);

        }

        [HttpGet]
        public async Task<IActionResult> PointOfSaleList(Guid wholeSaleMarketId)
        {
            var listOfPointOfSales = await _pointOfSale.TableNoTracking
                .Where(x => x.WholeSaleMarketId == wholeSaleMarketId).ProjectTo<PointOfSaleModel>().ToListAsync();

            return View(listOfPointOfSales);
        }

        //[HttpGet]
        //[Authorize(Roles = "PriceOperator")]
        //public async Task<IActionResult> PointOfSaleListWithoufId()
        //{
        //    var listOfPointOfSales = await _pointOfSale.TableNoTracking
        //        .Where(x => x.WholeSaleMarketId == user.WholeSaleMarketId).ProjectTo<PointOfSaleModel>().ToListAsync();

        //    return View(listOfPointOfSales);
        //}

    }
}