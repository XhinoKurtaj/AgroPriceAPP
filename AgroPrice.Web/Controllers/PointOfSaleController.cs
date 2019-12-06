using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroPrice.Core.Data;
using AgroPrice.Core.Extensions;
using AgroPrice.Domain.Domain.User;
using AgroPrice.Domain.Domain.WholeSaleMarket;
using AgroPrice.Services.PointOfSale;
using AgroPrice.Services.PointOfSale.Models;
using AgroPrice.Services.WholeSaleMarket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgroPrice.Web.Controllers
{
    
    public class PointOfSaleController : Controller
    {
        private readonly IPointOfSaleService _pointOfSaleService;
        private readonly IWholeSaleMarketService _wholeSaleMarketService;
        private readonly IRepository<WholeSaleMarket> _wholeSaleMarket;
        private readonly IRepository<PointOfSale> _pointOfSale;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IDbContext _context;

        public PointOfSaleController(IPointOfSaleService pointOfSaleService, IWholeSaleMarketService wholeSaleMarketService, IRepository<WholeSaleMarket> wholeSaleMarket, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IDbContext context, IRepository<PointOfSale> pointOfSale)
        {
            _pointOfSaleService = pointOfSaleService;
            _wholeSaleMarketService = wholeSaleMarketService;
            _wholeSaleMarket = wholeSaleMarket;
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
            _pointOfSale = pointOfSale;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _pointOfSaleService.GetAllPointOfSale();
            return View(result);
        }

        [Authorize(Roles = "Seller,Admin")]
        [HttpGet]
        public async Task<IActionResult> PointOfSaleDetailsWithoutID()
        {
            User user =(User) await _userManager.FindByNameAsync(User.Identity.Name);
            return RedirectToAction("PointOfSaleDetails", "PointOfSale", new {id = user.PointOfSaleId});
        }

        [Authorize(Roles = "Seller,Admin")]
        [HttpGet]
        public async Task<IActionResult> PointOfSaleDetails(Guid id)
        {
            var model = await _pointOfSaleService.PointOfSaleDetails(id);
            return View(model);
        }

    }
}