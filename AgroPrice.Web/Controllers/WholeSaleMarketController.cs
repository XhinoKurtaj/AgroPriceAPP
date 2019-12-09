using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroPrice.Core.Data;
using AgroPrice.Domain.Domain.Product;
using AgroPrice.Domain.Domain.WholeSaleMarket;
using AgroPrice.Services.Product.Infrastructure;
using AgroPrice.Services.Product.Models;
using AgroPrice.Services.WholeSaleMarket;
using AgroPrice.Services.WholeSaleMarket.Models;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AgroPrice.Web.Controllers
{
    [Authorize(Roles ="Buyer,Admin")]
    public class WholeSaleMarketController : Controller
    {
        private readonly IWholeSaleMarketService _wholeSaleMarketService;
        private readonly IRepository<WholeSaleMarket> _wholeSaleMarket;
        private readonly IRepository<Product> _products;
        private readonly IRepository<PointOfSale> _pointOfSale;

        public WholeSaleMarketController(IWholeSaleMarketService wholeSaleMarketService, IRepository<WholeSaleMarket> wholeSaleMarket, IRepository<Product> products, IRepository<PointOfSale> pointOfSale)
        {
            _wholeSaleMarketService = wholeSaleMarketService;
            _wholeSaleMarket = wholeSaleMarket;
            _products = products;
            _pointOfSale = pointOfSale;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _wholeSaleMarketService.GetAllWholeSaleMarkets();
            return View(result);
        }

        public async Task<IActionResult> SpecificProductOnWholeSaleMarket(Guid wholeSaleMarketId, string productName)
        {
            var todayProducts = _products.TableNoTracking.Where(x => x.RegisterDate.Date == DateTime.Now.Date);
            var productOfThisWholeSaleMarket = todayProducts
                .Where(x => x.PointOfSale.WholeSaleMarketId == wholeSaleMarketId);
            var prodWithThisName =await productOfThisWholeSaleMarket.Where(x => x.Name == productName).ProjectTo<ProductInCartModel>().ToListAsync();
            return View(prodWithThisName);

        }
        
    }
}