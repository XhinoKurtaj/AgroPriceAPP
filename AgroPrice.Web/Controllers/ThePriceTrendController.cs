using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroPrice.Core.Data;
using AgroPrice.Domain.Domain.WholeSaleMarket;
using AgroPrice.Services.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgroPrice.Web.Controllers
{
    public class ThePriceTrendController : Controller
    {
        private readonly IProductService _productService;
        private readonly IRepository<WholeSaleMarket> _wholeSaleMarket;

        public ThePriceTrendController(IProductService productService, IRepository<WholeSaleMarket> wholeSaleMarket)
        {
            _productService = productService;
            _wholeSaleMarket = wholeSaleMarket;
        }

        public IActionResult Index()
        {
            var result = _wholeSaleMarket.TableNoTracking.Select(w => new SelectListItem
            {
                Value = w.Id.ToString(),
                Text = w.Name
            });
            ViewBag.WholeSaleMarkets = result;
            return View();
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<IActionResult> GetProductListByDateAndWholeSaleMarket(string wholeSaleMarketsID, DateTime startDate, DateTime endDate)
        {
            var result = await _productService.GetProductListByDateAndWholeSaleMarket(wholeSaleMarketsID, startDate, endDate);
            return Json(new { result = result });
        }
    }
}