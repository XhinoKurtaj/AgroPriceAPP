﻿using System;
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

            var serviceResult = await _pointOfSaleService.CreatePointOfSaleAndSeller(model); 
            if (serviceResult.Success)
                return RedirectToAction("Index", "PointOfSale");
           
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> UpdateSellerWithPointOfSale(Guid id)
        {
            var entity = await _pointOfSale.GetByIdAsync(id);
            var model = await _pointOfSaleService.ReturnEntityToModel(entity);
            var result = _wholeSaleMarket.TableNoTracking.Select(w => new SelectListItem
            {
                Value = w.Id.ToString(),
                Text = w.Name
            });
            ViewBag.WholeSaleMarketDropdown = result;
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateSellerWithPointOfSale(UpdateSellerWithPointOfSaleModel model)
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

            var serviceResult = await _pointOfSaleService.UpdatePointOfSaleAndSeller(model);
            if (serviceResult.Success)
                return RedirectToAction("Index", "PointOfSale");

            ViewBag.Error = serviceResult.Errors.Values.ToString();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var serviceResult = await _pointOfSaleService.DeletePointOfSale(id);
            return RedirectToAction("Index", "WholeSaleMarket");
        }
    }
}