using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroPrice.Core.Data;
using AgroPrice.Domain.Domain.User;
using AgroPrice.Domain.Domain.WholeSaleMarket;
using AgroPrice.Services.PointOfSale;
using AgroPrice.Services.PointOfSale.Models;
using AgroPrice.Services.WholeSaleMarket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgroPrice.Web.Areas.Admin.Controllers
{
    public class PointOfSaleController : AdminController
    {
        private readonly IPointOfSaleService _pointOfSaleService;
        private readonly IWholeSaleMarketService _wholeSaleMarketService;
        private readonly IRepository<WholeSaleMarket> _wholeSaleMarket;
        private readonly IRepository<PointOfSale> _pointOfSale;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IDbContext _context;
        public PointOfSaleController(IPointOfSaleService pointOfSaleService, IWholeSaleMarketService wholeSaleMarketService, IRepository<WholeSaleMarket> wholeSaleMarket, IRepository<PointOfSale> pointOfSale, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IDbContext context)
        {
            _pointOfSaleService = pointOfSaleService;
            _wholeSaleMarketService = wholeSaleMarketService;
            _wholeSaleMarket = wholeSaleMarket;
            _pointOfSale = pointOfSale;
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _pointOfSaleService.GetAllPointOfSale();
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> PointOfSaleDetailsWithoutID()
        {
            User user = (User)await _userManager.FindByNameAsync(User.Identity.Name);
            return RedirectToAction("PointOfSaleDetails", "PointOfSale", new { id = user.PointOfSaleId });
        }

        
        [HttpGet]
        public async Task<IActionResult> PointOfSaleDetails(Guid id)
        {
            var model = await _pointOfSaleService.PointOfSaleDetails(id);
            return View(model);
        }


        [HttpGet]
        public IActionResult CreateSellerWithPointOfSale()
        {
            var result = _wholeSaleMarket.TableNoTracking.Select(w => new SelectListItem
            {
                Value = w.Id.ToString(),
                Text = w.Name
            });
            ViewBag.WholeSaleMarketDropdown = result;
            return View(new CreateSellerWithPointOfSaleModel());
        }


        [HttpPost]
        public async Task<IActionResult> CreateSellerWithPointOfSale(CreateSellerWithPointOfSaleModel model)
        {
            if (!ModelState.IsValid)
            {
                var result = _wholeSaleMarket.TableNoTracking.Select(w => new SelectListItem
                {
                    Value = w.Id.ToString(),
                    Text = w.Name
                });
                ViewBag.WholeSaleMarketDropdown = result;
                return View(model);
            }

            //create Pointofsale
            var pointOfSale = new PointOfSale
            {
                Description = model.PointOfSaleDescription,
                WholeSaleMarketId = new Guid(model.WholeSaleMarketId)
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
            if (createUserResult.Succeeded)
            {
                if (createUserResult.Succeeded)
                {
                    return RedirectToAction("Index", "PointOfSale");
                }
            }
            return View();
        }
    }
}