using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using AgroPrice.Core.Data;
using AgroPrice.Core.Extensions;
using AgroPrice.Domain.Domain.WholeSaleMarket;
using AgroPrice.Services.PointOfSale;
using AgroPrice.Services.PointOfSale.Models;
using AgroPrice.Services.WholeSaleMarket;
using AgroPrice.Services.WholeSaleMarket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AgroPrice.Web.Areas.Admin.Controllers
{
    public class WholeSaleMarketController : AdminController
    {
        private readonly IWholeSaleMarketService _wholeSaleMarketService;
        private readonly IRepository<WholeSaleMarket> _wholeSaleMarket;
        private readonly IRepository<PointOfSale> _pointOfSale;
        private readonly IPointOfSaleService _pointOfSaleService;

        public WholeSaleMarketController(IWholeSaleMarketService wholeSaleMarketService, IRepository<WholeSaleMarket> wholeSaleMarket, IRepository<PointOfSale> pointOfSale, IPointOfSaleService pointOfSaleService)
        {
            _wholeSaleMarketService = wholeSaleMarketService;
            _wholeSaleMarket = wholeSaleMarket;
            _pointOfSale = pointOfSale;
            _pointOfSaleService = pointOfSaleService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _wholeSaleMarketService.GetAllWholeSaleMarkets();
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var entity = await _wholeSaleMarket.GetByIdAsync(id);
            var model = entity.ToModel<WholeSaleMarketModel>();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateWholeSaleMarket()
        {
            return View(new CreateWholeSaleMarketModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateWholeSaleMarket(CreateWholeSaleMarketModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var serviceResult = await _wholeSaleMarketService.CreateWholeSaleMarket(model);
            if (serviceResult.Success)
                return RedirectToAction("Index", "WholeSaleMarket");

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateWholeSaleMarket(Guid id)
        {
            var entity = await _wholeSaleMarket.GetByIdAsync(id);
            var priceOperator = entity.User;
            var model = new UpdateWholeSaleMarketModel
            {
                Name = entity.Name,
                Address = entity.Address,
                ImageUrl = entity.ImageUrl,
                UserName = priceOperator.UserName,
                Email = priceOperator.Email,
                PhoneNumber = priceOperator.PhoneNumber
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateWholeSaleMarket(UpdateWholeSaleMarketModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var serviceResult = await _wholeSaleMarketService.UpdateWholeSaleMarket(model);
            if (serviceResult.Success)
                return RedirectToAction("Index", "WholeSaleMarket");

            return View(model);
        }

        [HttpGet]
        public IActionResult CreateSellerWithPointOfSale(Guid id)
        {
            var model = new CreateSellerWithPointOfSaleModel
            {
                WholeSaleMarketId = id
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSellerWithPointOfSale(CreateSellerWithPointOfSaleModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var serviceResult = await _pointOfSaleService.CreatePointOfSaleAndSeller(model);
            if (serviceResult.Success)
                return RedirectToAction("Details", "WholeSaleMarket", new {id= model.WholeSaleMarketId });

            return View(model);
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
                return RedirectToAction("Details", "WholeSaleMarket", new{id=model.WholeSaleMarketId});

            ViewBag.Error = serviceResult.Errors.Values.ToString();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeletePointOfSale(Guid id , Guid wholeSaleMarketId)
        {
            var serviceResult = await _pointOfSaleService.DeletePointOfSale(id);
            return RedirectToAction("Details", "WholeSaleMarket", new { id = wholeSaleMarketId });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteWholeSaleMarket(Guid wholeSaleMarketId)
        {
            var serviceResult = await _wholeSaleMarketService.DeleteWholeSaleMarket(wholeSaleMarketId);
            return RedirectToAction("Index", "WholeSaleMarket");
        }
    }
}