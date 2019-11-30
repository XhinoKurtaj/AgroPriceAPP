using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroPrice.Services.WholeSaleMarket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgroPrice.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class WholeSaleMarketController : Controller
    {
        private readonly IWholeSaleMarketService _wholeSaleMarketService;

        public WholeSaleMarketController(IWholeSaleMarketService wholeSaleMarketService)
        {
            _wholeSaleMarketService = wholeSaleMarketService;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var result = await _wholeSaleMarketService.GetAllWholeSaleMarkets();
            return View(result);
        }
    }
}