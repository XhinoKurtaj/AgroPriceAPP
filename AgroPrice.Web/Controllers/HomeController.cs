using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AgroPrice.Core.Data;
using AgroPrice.Domain.Domain.Product;
using AgroPrice.Domain.Domain.WholeSaleMarket;
using AgroPrice.Services.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AgroPrice.Web.Models;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using SelectListItem = Microsoft.AspNetCore.Mvc.Rendering.SelectListItem;

namespace AgroPrice.Web.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<WholeSaleMarket> _wholeSaleMarket;
        private readonly IProductService _productService;
        private readonly IRepository<Product> _product;

        public HomeController(ILogger<HomeController> logger, IRepository<WholeSaleMarket> wholeSaleMarket, IProductService productService, IRepository<Product> product)
        {
            _logger = logger;
            _wholeSaleMarket = wholeSaleMarket;
            _productService = productService;
            _product = product;
        }

        public async Task<IActionResult> Index()
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
        public async Task<IActionResult> FindProductByWholeSaleMarket(string WholeSaleMarketsID)
        {

            var result = await _productService.FindProductByWholeSaleMarket(WholeSaleMarketsID);
            return PartialView("Partials/_TodayProducts",result.Products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
